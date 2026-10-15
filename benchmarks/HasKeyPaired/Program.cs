using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Nethermind.RocksDbBindings;
using Nethermind.RocksDbBindings.Native;

PairedPresence.Run(args[0]);

static unsafe class PairedPresence
{
    private const int RecordCount = 1024;
    private const int QueryCount = 4096;
    private const int Batches = 16;
    private const int Rounds = 12;

    public static void Run(string output)
    {
        List<CaseResult> results = [];
        foreach (int valueSize in new[] { 0, 128, 4096 })
            foreach (int hitPercent in new[] { 0, 50, 100 })
                foreach (bool flushed in new[] { false, true })
                    results.Add(MeasureCase(valueSize, hitPercent, flushed));
        File.WriteAllText(output, JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true }));
    }

    private static CaseResult MeasureCase(int valueSize, int hitPercent, bool flushed)
    {
        string path = Path.Combine(Environment.GetEnvironmentVariable("BENCH_DATA")!, Guid.NewGuid().ToString("N"));
        try
        {
            using DbOptions options = new DbOptions().SetCreateIfMissing().SetDisableAutoCompactions(1);
            using RocksDb db = RocksDb.Open(options, path);
            using ReadOptions reads = new();
            byte[] value = new byte[valueSize];
            Random random = new(1701);
            random.NextBytes(value);
            using (WriteBatch batch = new())
            {
                for (int i = 0; i < RecordCount; i++) batch.Put(Key(i), value);
                db.Write(batch);
            }
            if (flushed)
            {
                using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
                db.Flush(flush);
            }

            byte[][] keys = new byte[QueryCount][];
            int expected = 0;
            for (int i = 0; i < QueryCount; i++)
            {
                bool present = random.Next(100) < hitPercent;
                expected += present ? 1 : 0;
                keys[i] = Key(random.Next(RecordCount) + (present ? 0 : RecordCount));
                if (OriginalHasKey(db, keys[i], reads) != present || db.HasKey(keys[i], readOptions: reads) != present)
                    throw new InvalidOperationException($"Incorrect presence for query {i}");
            }

            for (int i = 0; i < 32; i++)
            {
                _ = PinnedBatch(db, reads, keys);
                _ = StackBatch(db, reads, keys);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            List<Round> control = Compare(db, reads, keys, expected, PinnedBatch, PinnedBatch);
            List<Round> candidate = Compare(db, reads, keys, expected, PinnedBatch, StackBatch);
            CaseResult result = new(valueSize, hitPercent, flushed, expected, control, candidate);
            Console.WriteLine($"ValueSize={valueSize},HitPercent={hitPercent},Flushed={flushed},Control={MedianDelta(control):F2}%,Candidate={MedianDelta(candidate):F2}%");
            return result;
        }
        finally
        {
            if (Directory.Exists(path)) Directory.Delete(path, recursive: true);
        }
    }

    private static List<Round> Compare(RocksDb db, ReadOptions reads, byte[][] keys, int expected,
        Func<RocksDb, ReadOptions, byte[][], int> first, Func<RocksDb, ReadOptions, byte[][], int> second)
    {
        List<Round> result = new(Rounds);
        for (int i = 0; i < Rounds; i++)
        {
            bool reversed = (i & 1) != 0;
            Window outer1 = Measure(db, reads, keys, expected, reversed ? second : first);
            Window inner1 = Measure(db, reads, keys, expected, reversed ? first : second);
            Window inner2 = Measure(db, reads, keys, expected, reversed ? first : second);
            Window outer2 = Measure(db, reads, keys, expected, reversed ? second : first);
            result.Add(reversed ? new(inner1, inner2, outer1, outer2) : new(outer1, outer2, inner1, inner2));
        }
        return result;
    }

    private static Window Measure(RocksDb db, ReadOptions reads, byte[][] keys, int expected,
        Func<RocksDb, ReadOptions, byte[][], int> probe)
    {
        int gc = GC.CollectionCount(0);
        long allocated = GC.GetAllocatedBytesForCurrentThread();
        TimeSpan pause = GC.GetTotalPauseDuration();
        int found = 0;
        long start = Stopwatch.GetTimestamp();
        for (int i = 0; i < Batches; i++) found += probe(db, reads, keys);
        long elapsed = Stopwatch.GetTimestamp() - start;
        if (found != expected * Batches) throw new InvalidOperationException("Incorrect measured presence count");
        return new(elapsed * (1e9 / Stopwatch.Frequency) / (QueryCount * Batches),
            GC.GetAllocatedBytesForCurrentThread() - allocated, GC.CollectionCount(0) - gc,
            (GC.GetTotalPauseDuration() - pause).TotalNanoseconds);
    }

    private static int PinnedBatch(RocksDb db, ReadOptions reads, byte[][] keys)
    {
        int found = 0;
        foreach (byte[] key in keys)
            if (OriginalHasKey(db, key, reads)) found++;
        return found;
    }

    private static int StackBatch(RocksDb db, ReadOptions reads, byte[][] keys)
    {
        int found = 0;
        foreach (byte[] key in keys)
            if (db.HasKey(key, readOptions: reads)) found++;
        return found;
    }

    private static bool OriginalHasKey(RocksDb db, ReadOnlySpan<byte> key, ReadOptions reads)
    {
        fixed (byte* data = key)
        {
            rocksdb_pinnableslice_t* pinned = GetPinned(db, data, (nuint)key.Length, null, reads);
            if (pinned is null) return false;
            RocksDbNative.rocksdb_pinnableslice_destroy(pinned);
            return true;
        }
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "GetPinned")]
    private static extern rocksdb_pinnableslice_t* GetPinned(RocksDb db, byte* key, nuint length,
        IColumnFamilyHandle? family, ReadOptions? reads);

    private static byte[] Key(int index)
    {
        byte[] key = new byte[32];
        BinaryPrimitives.WriteInt32BigEndian(key, index);
        return key;
    }

    private static double MedianDelta(List<Round> rounds)
    {
        double[] ratios = rounds.Select(r => (r.B1.Nanoseconds + r.B2.Nanoseconds) / (r.A1.Nanoseconds + r.A2.Nanoseconds)).Order().ToArray();
        return ((ratios[5] + ratios[6]) / 2 - 1) * 100;
    }

    private sealed record Window(double Nanoseconds, long AllocatedBytes, int Gen0Collections, double GcPauseNanoseconds);
    private sealed record Round(Window A1, Window A2, Window B1, Window B2);
    private sealed record CaseResult(int ValueSize, int HitPercent, bool Flushed, int ExpectedHits,
        List<Round> Control, List<Round> Candidate);
}
