// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

// Steady-state partial-miss read benchmark: does a MultiGet batch overlap its disk reads?
//
// The database is larger than the block cache and is read with direct IO, so the page cache
// cannot serve a miss. A fixed hot set is read before timing; every timed read uses
// fill_cache=false so the cache stays exactly at that hot set and every arm sees the same
// hit/miss mix. Timed batches are 64 keys: a share from the hot set, the rest uniform over
// the whole key space, plus a few absent keys for the filters.
//
// Arms: one-thread scalar Get, N-thread scalar Get, sync MultiGet, MultiGet with
// async_io + optimize_multiget_for_io, and the native callback MultiGetAsync. The perf
// context of the calling thread reports block reads and, for scalar reads, the time spent in
// them (the batched MultiGet read path does not post block_read_time, and the N-thread arm reads
// on other threads, so those cells stay 0). The verdict comes from wall time per batch:
// multiget_async against multiget on the same database is the IO-overlap test.

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

using Nethermind.RocksDbBindings;
using Nethermind.RocksDbBindings.Native;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings.Benchmarks.ColdMultiGet;

internal sealed class Config
{
    public string DbPath { get; set; } = "cold-multiget-db";
    public string Mode { get; set; } = "all"; // build | run | all
    public double SizeGb { get; set; } = 4;
    public int CacheMb { get; set; } = 256;
    public int Batch { get; set; } = 64;
    public int Batches { get; set; } = 150;
    public int Rounds { get; set; } = 6;
    public int Threads { get; set; } = 8;
    public double HotShare { get; set; } = 0.3;
    public double AbsentShare { get; set; } = 0.05;
    public int HotKeys { get; set; } = 200_000;
    public ulong Seed { get; set; } = 20261018;
    public string Out { get; set; } = "cold-multiget-results.json";
    public string Label { get; set; } = "unlabeled";
    public string Arms { get; set; } = "scalar1,scalarN,multiget,multiget_async,multiget_api";
    public bool Sorted { get; set; }

    public static Config Parse(string[] args)
    {
        Config config = new();
        for (int i = 0; i + 1 < args.Length; i += 2)
        {
            string value = args[i + 1];
            switch (args[i])
            {
                case "--db": config.DbPath = value; break;
                case "--mode": config.Mode = value; break;
                case "--size-gb": config.SizeGb = double.Parse(value); break;
                case "--cache-mb": config.CacheMb = int.Parse(value); break;
                case "--batch": config.Batch = int.Parse(value); break;
                case "--batches": config.Batches = int.Parse(value); break;
                case "--rounds": config.Rounds = int.Parse(value); break;
                case "--threads": config.Threads = int.Parse(value); break;
                case "--hot-share": config.HotShare = double.Parse(value); break;
                case "--absent-share": config.AbsentShare = double.Parse(value); break;
                case "--hot-keys": config.HotKeys = int.Parse(value); break;
                case "--seed": config.Seed = ulong.Parse(value); break;
                case "--out": config.Out = value; break;
                case "--label": config.Label = value; break;
                case "--arms": config.Arms = value; break;
                case "--sorted": config.Sorted = bool.Parse(value); break;
                default: throw new ArgumentException($"Unknown argument {args[i]}");
            }
        }
        return config;
    }
}

/// <summary>Storage-slot shape: 52-byte keys (20-byte address prefix + 32-byte slot), 32-byte values.</summary>
internal static class Workload
{
    public const int KeyLength = 52;
    public const int ValueLength = 32;
    public const int SlotsPerAccount = 64;
    public const string Family = "storage";

    public static ulong Mix(ulong x)
    {
        x += 0x9E3779B97F4A7C15UL;
        x = (x ^ (x >> 30)) * 0xBF58476D1CE4E5B9UL;
        x = (x ^ (x >> 27)) * 0x94D049BB133111EBUL;
        return x ^ (x >> 31);
    }

    /// <summary>Record i belongs to account i / SlotsPerAccount, so one account's slots share a 20-byte prefix.</summary>
    public static void WriteKey(ulong record, ulong seed, Span<byte> key)
    {
        ulong account = record / SlotsPerAccount;
        ulong slot = record % SlotsPerAccount;
        ulong a = Mix(seed ^ account);
        ulong b = Mix(a);
        ulong c = Mix(b);
        BitConverter.TryWriteBytes(key[..8], a);
        BitConverter.TryWriteBytes(key.Slice(8, 8), b);
        BitConverter.TryWriteBytes(key.Slice(16, 4), (uint)c);
        ulong s0 = Mix(seed ^ (account << 20) ^ slot ^ 0xABCDUL);
        ulong s1 = Mix(s0);
        ulong s2 = Mix(s1);
        ulong s3 = Mix(s2);
        BitConverter.TryWriteBytes(key.Slice(20, 8), s0);
        BitConverter.TryWriteBytes(key.Slice(28, 8), s1);
        BitConverter.TryWriteBytes(key.Slice(36, 8), s2);
        BitConverter.TryWriteBytes(key.Slice(44, 8), s3);
    }

    /// <summary>An absent key: a valid account prefix with a slot index past the written range.</summary>
    public static void WriteAbsentKey(ulong record, ulong seed, Span<byte> key)
    {
        WriteKey(record, seed, key);
        ulong s0 = Mix(seed ^ 0xFFFF_0000UL ^ record);
        BitConverter.TryWriteBytes(key.Slice(20, 8), s0);
        BitConverter.TryWriteBytes(key.Slice(28, 8), Mix(s0));
    }

    public static void WriteValue(ReadOnlySpan<byte> key, Span<byte> value)
    {
        ulong h = Mix(BitConverter.ToUInt64(key[..8]) ^ BitConverter.ToUInt64(key.Slice(20, 8)) ^ BitConverter.ToUInt64(key.Slice(44, 8)));
        for (int i = 0; i < ValueLength; i += 8)
        {
            BitConverter.TryWriteBytes(value.Slice(i, 8), h);
            h = Mix(h);
        }
    }

    public static ulong ExpectedValuePrefix(ReadOnlySpan<byte> key)
    {
        Span<byte> value = stackalloc byte[ValueLength];
        WriteValue(key, value);
        return BitConverter.ToUInt64(value[..8]);
    }
}

/// <summary>Decodes a value to its first eight bytes so the caller can check it without allocation.</summary>
internal readonly struct PrefixDeserializer : ISpanDeserializer<long>
{
    public long Deserialize(ReadOnlySpan<byte> buffer)
        => buffer.Length == Workload.ValueLength ? (long)BitConverter.ToUInt64(buffer[..8]) : -1;
}

internal sealed class Query
{
    public byte[] Keys = [];
    public bool[] Present = [];
    public int Count;
}

internal sealed class ArmResult
{
    public string Arm { get; set; } = "";
    public int Batches { get; set; }
    public double MeanUs { get; set; }
    public double P50Us { get; set; }
    public double P90Us { get; set; }
    public double P99Us { get; set; }
    public double MeanUsPerKey { get; set; }
    public double BlockReadsPerBatch { get; set; }
    public double BlockReadBytesPerBatch { get; set; }
    public double BlockReadTimeUsPerBatch { get; set; }
    public double IoOverlap { get; set; }
    public double CacheHitsPerBatch { get; set; }
    public long Verified { get; set; }
    public long Mismatches { get; set; }
    public List<double[]> RoundMeansUs { get; set; } = [];
}

internal sealed class Report
{
    public string Label { get; set; } = "";
    public Config Config { get; set; } = new();
    public string Os { get; set; } = "";
    public string Arch { get; set; } = "";
    public int Cores { get; set; }
    public string NativeLibrary { get; set; } = "";
    public string NativeSha256 { get; set; } = "";
    public bool IoUringObserved { get; set; }
    public string IoUringEnvironment { get; set; } = "";
    public long Records { get; set; }
    public string DbFiles { get; set; } = "";
    public List<ArmResult> Arms { get; set; } = [];
}

internal static unsafe class Program
{
    private const int PerfLevelTimeExceptMutex = 4;
    private const int PerfBlockCacheHitCount = 1;
    private const int PerfBlockReadCount = 2;
    private const int PerfBlockReadByte = 3;
    private const int PerfBlockReadTime = 4;

    private static int Main(string[] args)
    {
        Config config = Config.Parse(args);
        long records = (long)(config.SizeGb * 1024 * 1024 * 1024 / 110); // ~110 B per record on disk before compression
        records -= records % Workload.SlotsPerAccount;
        Console.WriteLine($"label={config.Label} mode={config.Mode} records={records:N0} cache={config.CacheMb} MB batch={config.Batch} hot={config.HotShare:P0} absent={config.AbsentShare:P0}");

        if (config.Mode is "build" or "all") Build(config, records);
        if (config.Mode is "run" or "all") return Run(config, records);
        return 0;
    }

    private static BlockBasedTableOptions TableOptions(Cache? cache)
    {
        BlockBasedTableOptions table = new BlockBasedTableOptions()
            .SetBlockSize(8000)
            .SetBlockRestartInterval(4)
            .SetFilterPolicy(BloomFilterPolicy.Create(10, false))
            .SetWholeKeyFiltering(true)
            .SetIndexType(BlockBasedTableIndexType.Binary)
            .SetCacheIndexAndFilterBlocks(true)
            .SetPinL0FilterAndIndexBlocksInCache(true);
        if (cache is not null) table.SetBlockCache(cache);
        return table;
    }

    private static ColumnFamilyOptions FamilyOptions(Cache? cache)
        => new ColumnFamilyOptions()
            .SetCompression(Compression.Lz4)
            .SetWriteBufferSize(64UL * 1024 * 1024)
            .SetTargetFileSizeBase(64UL * 1024 * 1024)
            .SetMaxBytesForLevelBase(256UL * 1024 * 1024)
            .SetLevelCompactionDynamicLevelBytes(false)
            .SetBlockBasedTableFactory(TableOptions(cache));

    private static void Build(Config config, long records)
    {
        if (Directory.Exists(config.DbPath)) Directory.Delete(config.DbPath, recursive: true);
        Stopwatch clock = Stopwatch.StartNew();
        using Cache cache = Cache.CreateLru(64UL * 1024 * 1024);
        DbOptions options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        ColumnFamilies families = new();
        families.Add(Workload.Family, FamilyOptions(cache));
        using (RocksDb db = RocksDb.Open(options, config.DbPath, families))
        {
            IColumnFamilyHandle family = db.GetColumnFamily(Workload.Family);
            using WriteOptions write = new WriteOptions().SetDisableWal();
            Span<byte> key = stackalloc byte[Workload.KeyLength];
            Span<byte> value = stackalloc byte[Workload.ValueLength];
            // Record order; the hashed account prefix already spreads the writes over the key space, and the
            // full compaction below leaves one sorted level for every arm to read.
            for (long i = 0; i < records; i++)
            {
                Workload.WriteKey((ulong)i, config.Seed, key);
                Workload.WriteValue(key, value);
                db.Put(key, value, family, write);
                if (i % 5_000_000 == 0 && i > 0) Console.WriteLine($"  wrote {i:N0} records in {clock.Elapsed.TotalSeconds:F0} s");
            }
            db.Flush(new FlushOptions().SetWaitForFlush(true), family);
            Console.WriteLine($"  flushed {records:N0} records in {clock.Elapsed.TotalSeconds:F0} s, compacting");
            db.CompactRange(null, null, family);
            Console.WriteLine($"  compacted in {clock.Elapsed.TotalSeconds:F0} s; files: {db.GetProperty("rocksdb.num-files-at-level0", family)}/{db.GetProperty("rocksdb.num-files-at-level1", family)}/{db.GetProperty("rocksdb.num-files-at-level2", family)}/{db.GetProperty("rocksdb.num-files-at-level3", family)}");
        }
        Console.WriteLine($"build done in {clock.Elapsed.TotalSeconds:F0} s, {DirectorySize(config.DbPath) / 1024 / 1024:N0} MB on disk");
    }

    private static long DirectorySize(string path)
        => Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories).Sum(f => new FileInfo(f).Length);

    private static int Run(Config config, long records)
    {
        ThreadPool.SetMinThreads(Math.Max(config.Threads * 2, 16), 16);
        using Cache cache = Cache.CreateLru((ulong)config.CacheMb * 1024 * 1024);
        DbOptions options = new DbOptions()
            .SetCreateIfMissing(false)
            .SetUseDirectReads(true)
            .SetMaxOpenFiles(-1);
        ColumnFamilies families = new();
        families.Add(Workload.Family, FamilyOptions(cache));
        using RocksDb db = RocksDb.OpenReadOnly(options, config.DbPath, families, false);
        IColumnFamilyHandle family = db.GetColumnFamily(Workload.Family);

        Report report = new()
        {
            Label = config.Label,
            Config = config,
            Os = RuntimeInformation.OSDescription,
            Arch = RuntimeInformation.ProcessArchitecture.ToString(),
            Cores = Environment.ProcessorCount,
            Records = records,
            IoUringEnvironment = Environment.GetEnvironmentVariable("ROCKSDB_NET_IO_URING") ?? "",
        };
        (report.NativeLibrary, report.NativeSha256) = NativeLibraryIdentity();
        report.DbFiles = $"{db.GetProperty("rocksdb.num-files-at-level0", family)}/{db.GetProperty("rocksdb.num-files-at-level1", family)}/{db.GetProperty("rocksdb.num-files-at-level2", family)}/{db.GetProperty("rocksdb.num-files-at-level3", family)}";
        Console.WriteLine($"native={report.NativeLibrary} sha256={report.NativeSha256} files L0-L3={report.DbFiles}");

        using ReadOptions plain = new ReadOptions().SetFillCache(false).SetVerifyChecksums(true);
        using ReadOptions asyncIo = new ReadOptions().SetFillCache(false).SetVerifyChecksums(true).SetAsyncIO(true).SetOptimizeMultiGetForIO(true);
        using ReadOptions warm = new ReadOptions().SetFillCache(true);

        WarmHotSet(db, family, config, records, warm);

        rocksdb_set_perf_level(PerfLevelTimeExceptMutex);
        string[] arms = config.Arms.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        Dictionary<string, List<Sample>> samples = arms.ToDictionary(a => a, _ => new List<Sample>());
        Dictionary<string, List<double>> roundMeans = arms.ToDictionary(a => a, _ => new List<double>());
        ulong querySeed = config.Seed ^ 0x5EEDUL;

        // One untimed pass per arm so JIT, thread pool and native lazy state are ready.
        foreach (string arm in arms)
        {
            Query query = MakeQuery(config, records, ref querySeed);
            RunBatch(arm, db, family, config, query, plain, asyncIo, timed: false);
        }
        report.IoUringObserved = IoUringInUse();

        for (int round = 0; round < config.Rounds; round++)
        {
            // ABBA: alternate the arm order every round so drift does not favor one arm.
            IEnumerable<string> order = round % 2 == 0 ? arms : arms.Reverse();
            foreach (string arm in order)
            {
                double sum = 0;
                for (int b = 0; b < config.Batches; b++)
                {
                    Query query = MakeQuery(config, records, ref querySeed);
                    Sample sample = RunBatch(arm, db, family, config, query, plain, asyncIo, timed: true);
                    samples[arm].Add(sample);
                    sum += sample.WallUs;
                }
                roundMeans[arm].Add(sum / config.Batches);
            }
            Console.WriteLine($"round {round + 1}/{config.Rounds}: " + string.Join("  ", arms.Select(a => $"{a}={roundMeans[a][^1]:F1}us")));
        }

        foreach (string arm in arms)
        {
            List<Sample> list = samples[arm];
            double[] walls = list.Select(s => s.WallUs).Order().ToArray();
            ArmResult result = new()
            {
                Arm = arm,
                Batches = list.Count,
                MeanUs = walls.Average(),
                P50Us = Percentile(walls, 0.50),
                P90Us = Percentile(walls, 0.90),
                P99Us = Percentile(walls, 0.99),
                MeanUsPerKey = walls.Average() / config.Batch,
                BlockReadsPerBatch = list.Average(s => (double)s.BlockReads),
                BlockReadBytesPerBatch = list.Average(s => (double)s.BlockReadBytes),
                BlockReadTimeUsPerBatch = list.Average(s => s.BlockReadUs),
                CacheHitsPerBatch = list.Average(s => (double)s.CacheHits),
                Verified = list.Sum(s => (long)s.Verified),
                Mismatches = list.Sum(s => (long)s.Mismatches),
                RoundMeansUs = [roundMeans[arm].ToArray()],
            };
            result.IoOverlap = result.MeanUs > 0 && result.BlockReadTimeUsPerBatch > 0 ? result.BlockReadTimeUsPerBatch / result.MeanUs : 0;
            report.Arms.Add(result);
        }

        File.WriteAllText(config.Out, JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true }));
        PrintTable(report);
        long mismatches = report.Arms.Sum(a => a.Mismatches);
        if (mismatches != 0)
        {
            Console.Error.WriteLine($"FAILED: {mismatches} value mismatches");
            return 2;
        }
        return 0;
    }

    private static void WarmHotSet(RocksDb db, IColumnFamilyHandle family, Config config, long records, ReadOptions warm)
    {
        Stopwatch clock = Stopwatch.StartNew();
        Span<byte> key = stackalloc byte[Workload.KeyLength];
        Span<byte> value = stackalloc byte[Workload.ValueLength + 8];
        long hot = Math.Min(config.HotKeys, records);
        // Twice, so the second pass is served by the block cache and proves the set fits.
        for (int pass = 0; pass < 2; pass++)
        {
            for (long i = 0; i < hot; i++)
            {
                Workload.WriteKey((ulong)i, config.Seed, key);
                int length = db.Get(key, value, family, warm);
                if (length != Workload.ValueLength) throw new InvalidOperationException($"Hot key {i} returned {length} bytes");
            }
        }
        Console.WriteLine($"hot set {hot:N0} keys warmed twice in {clock.Elapsed.TotalSeconds:F1} s");
    }

    private struct Sample
    {
        public double WallUs;
        public ulong BlockReads;
        public ulong BlockReadBytes;
        public double BlockReadUs;
        public ulong CacheHits;
        public int Verified;
        public int Mismatches;
    }

    private static Query MakeQuery(Config config, long records, ref ulong seed)
    {
        int count = config.Batch;
        Query query = new() { Keys = new byte[count * Workload.KeyLength], Present = new bool[count], Count = count };
        long hot = Math.Min(config.HotKeys, records);
        int hotCount = (int)Math.Round(count * config.HotShare);
        int absentCount = (int)Math.Round(count * config.AbsentShare);
        for (int i = 0; i < count; i++)
        {
            seed = Workload.Mix(seed);
            Span<byte> key = query.Keys.AsSpan(i * Workload.KeyLength, Workload.KeyLength);
            if (i < hotCount)
            {
                Workload.WriteKey(seed % (ulong)hot, config.Seed, key);
                query.Present[i] = true;
            }
            else if (i < hotCount + absentCount)
            {
                Workload.WriteAbsentKey(seed % (ulong)records, config.Seed, key);
                query.Present[i] = false;
            }
            else
            {
                Workload.WriteKey(seed % (ulong)records, config.Seed, key);
                query.Present[i] = true;
            }
        }
        if (config.Sorted) SortKeys(query);
        return query;
    }

    private static void SortKeys(Query query)
    {
        int n = query.Count;
        int[] order = Enumerable.Range(0, n).ToArray();
        byte[] keys = query.Keys;
        Array.Sort(order, (a, b) => keys.AsSpan(a * Workload.KeyLength, Workload.KeyLength).SequenceCompareTo(keys.AsSpan(b * Workload.KeyLength, Workload.KeyLength)));
        byte[] sorted = new byte[keys.Length];
        bool[] present = new bool[n];
        for (int i = 0; i < n; i++)
        {
            keys.AsSpan(order[i] * Workload.KeyLength, Workload.KeyLength).CopyTo(sorted.AsSpan(i * Workload.KeyLength, Workload.KeyLength));
            present[i] = query.Present[order[i]];
        }
        query.Keys = sorted;
        query.Present = present;
    }

    private static Sample RunBatch(string arm, RocksDb db, IColumnFamilyHandle family, Config config, Query query,
        ReadOptions plain, ReadOptions asyncIo, bool timed)
    {
        rocksdb_perfcontext_t* perf = rocksdb_perfcontext_create();
        rocksdb_perfcontext_reset(perf);
        long[] results = new long[query.Count];
        long start = Stopwatch.GetTimestamp();
        switch (arm)
        {
            case "scalar1": ScalarReads(db, family, query, plain, results, 0, query.Count); break;
            case "scalarN": ScalarReadsParallel(db, family, query, plain, results, config.Threads); break;
            case "multiget": db.MultiGet<long, PrefixDeserializer>(query.Keys, Workload.KeyLength, results, default, family, plain, config.Sorted); break;
            case "multiget_async": db.MultiGet<long, PrefixDeserializer>(query.Keys, Workload.KeyLength, results, default, family, asyncIo, config.Sorted); break;
            case "multiget_api": ApiAsyncReads(db, family, query, asyncIo, results); break;
            default: throw new ArgumentException($"Unknown arm {arm}");
        }
        long end = Stopwatch.GetTimestamp();
        Sample sample = new()
        {
            WallUs = (end - start) * 1_000_000.0 / Stopwatch.Frequency,
            BlockReads = rocksdb_perfcontext_metric(perf, PerfBlockReadCount),
            BlockReadBytes = rocksdb_perfcontext_metric(perf, PerfBlockReadByte),
            BlockReadUs = rocksdb_perfcontext_metric(perf, PerfBlockReadTime) / 1000.0,
            CacheHits = rocksdb_perfcontext_metric(perf, PerfBlockCacheHitCount),
        };
        rocksdb_perfcontext_destroy(perf);
        for (int i = 0; i < query.Count; i++)
        {
            ReadOnlySpan<byte> key = query.Keys.AsSpan(i * Workload.KeyLength, Workload.KeyLength);
            bool ok = query.Present[i] ? results[i] == (long)Workload.ExpectedValuePrefix(key) : results[i] == 0;
            if (ok) sample.Verified++; else sample.Mismatches++;
        }
        if (!timed) sample.Mismatches = 0;
        return sample;
    }

    private static void ScalarReads(RocksDb db, IColumnFamilyHandle family, Query query, ReadOptions options, long[] results, int start, int end)
    {
        Span<byte> value = stackalloc byte[Workload.ValueLength + 8];
        for (int i = start; i < end; i++)
        {
            int length = db.Get(query.Keys.AsSpan(i * Workload.KeyLength, Workload.KeyLength), value, family, options);
            results[i] = length == Workload.ValueLength ? (long)BitConverter.ToUInt64(value[..8]) : length <= 0 ? 0 : -1;
        }
    }

    private static void ScalarReadsParallel(RocksDb db, IColumnFamilyHandle family, Query query, ReadOptions options, long[] results, int threads)
    {
        int per = (query.Count + threads - 1) / threads;
        Parallel.For(0, threads, new ParallelOptions { MaxDegreeOfParallelism = threads }, t =>
        {
            int start = t * per;
            int end = Math.Min(start + per, query.Count);
            if (start < end) ScalarReads(db, family, query, options, results, start, end);
        });
    }

    private static void ApiAsyncReads(RocksDb db, IColumnFamilyHandle family, Query query, ReadOptions options, long[] results)
    {
        byte[][] keys = new byte[query.Count][];
        IColumnFamilyHandle[] families = new IColumnFamilyHandle[query.Count];
        for (int i = 0; i < query.Count; i++)
        {
            keys[i] = query.Keys.AsSpan(i * Workload.KeyLength, Workload.KeyLength).ToArray();
            families[i] = family;
        }
        KeyValuePair<byte[], byte[]?>[] pairs = db.MultiGetAsync(keys, families, options).AsTask().GetAwaiter().GetResult();
        for (int i = 0; i < query.Count; i++)
        {
            byte[]? value = pairs[i].Value;
            results[i] = value is null ? 0 : value.Length == Workload.ValueLength ? (long)BitConverter.ToUInt64(value.AsSpan(0, 8)) : -1;
        }
    }

    private static double Percentile(double[] sorted, double p)
    {
        if (sorted.Length == 0) return 0;
        double rank = p * (sorted.Length - 1);
        int low = (int)Math.Floor(rank);
        int high = Math.Min(low + 1, sorted.Length - 1);
        return sorted[low] + (sorted[high] - sorted[low]) * (rank - low);
    }

    /// <summary>The native library that ran: from the loaded modules where the runtime lists them, else the file beside the app.</summary>
    private static (string Path, string Sha256) NativeLibraryIdentity()
    {
        foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
        {
            if (Path.GetFileName(module.FileName).Contains("rocksdb", StringComparison.OrdinalIgnoreCase))
                return (module.FileName, FileSha256(module.FileName));
        }
        string native = Path.Combine(AppContext.BaseDirectory, "runtimes", RuntimeInformation.RuntimeIdentifier, "native");
        if (Directory.Exists(native))
        {
            foreach (string file in Directory.EnumerateFiles(native))
            {
                if (Path.GetFileName(file).Contains("rocksdb", StringComparison.OrdinalIgnoreCase))
                    return (file, FileSha256(file));
            }
        }
        return ("not-found", "unknown");
    }

    private static string FileSha256(string path)
    {
        using FileStream stream = File.OpenRead(path);
        return Convert.ToHexString(SHA256.HashData(stream)).ToLowerInvariant();
    }

    /// <summary>On Linux an io_uring queue is an anonymous inode held by the process.</summary>
    private static bool IoUringInUse()
    {
        if (!OperatingSystem.IsLinux()) return false;
        try
        {
            foreach (string fd in Directory.EnumerateFiles("/proc/self/fd"))
            {
                FileSystemInfo info = new FileInfo(fd);
                string? target = info.LinkTarget;
                if (target is not null && target.Contains("io_uring", StringComparison.Ordinal)) return true;
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
        return false;
    }

    private static void PrintTable(Report report)
    {
        StringBuilder table = new();
        table.AppendLine($"label={report.Label} os={report.Os} arch={report.Arch} cores={report.Cores} io_uring_observed={report.IoUringObserved} native_sha256={report.NativeSha256}");
        table.AppendLine("| arm | mean us | p50 | p90 | p99 | us/key | block reads | read us | overlap | cache hits | mismatches |");
        table.AppendLine("|---|---:|---:|---:|---:|---:|---:|---:|---:|---:|---:|");
        foreach (ArmResult arm in report.Arms)
        {
            table.AppendLine($"| {arm.Arm} | {arm.MeanUs:F1} | {arm.P50Us:F1} | {arm.P90Us:F1} | {arm.P99Us:F1} | {arm.MeanUsPerKey:F2} | {arm.BlockReadsPerBatch:F1} | {arm.BlockReadTimeUsPerBatch:F1} | {arm.IoOverlap:F2} | {arm.CacheHitsPerBatch:F1} | {arm.Mismatches} |");
        }
        Console.WriteLine(table.ToString());
    }
}
