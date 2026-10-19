// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers.Binary;
using System.Diagnostics;
using Nethermind.RocksDbBindings;

// Two consumer-shaped cold read benchmarks. Each process reads every key once, so with the page cache dropped
// before it starts, every read goes to the disk.
//   blobs: Nethermind's blob pool (BlobTxStorage.TryGetMany): 40-byte keys, values of 1-6 blobs, 32 MB cache,
//          sync MultiGet(byte[][]) per batch, then the same number of keys read one by one.
//   state: Nethermind's state prefetch (#14886): 64 hashed 32-byte keys, 80-byte values; the MultiGetAsync path it
//          uses today against PrefetchAsync, warm (every block cached) and cold, with allocated bytes per batch.
// Output lines: label,case,arm,batch,microseconds per batch,bytes allocated per batch
// Usage: ColdReads build-blobs <db> | blobs <db> <label> <batch> <batches> <seed>
//        ColdReads build-state <db> | state <db> <label> <batches> <seed>
const int BlobLength = 131_072 + 200;
const int BlobKeyLength = 40;
const int BlobRecords = 8_000;
const int StateKeyLength = 32;
const int StateValueLength = 80;
const int StateRecords = 4 << 20;
const int StateBatch = 64;

switch (args[0])
{
    case "build-blobs": BuildBlobs(args[1]); break;
    case "blobs": RunBlobs(args[1], args[2], int.Parse(args[3]), int.Parse(args[4]), ulong.Parse(args[5])); break;
    case "build-state": BuildState(args[1]); break;
    case "state": RunState(args[1], args[2], int.Parse(args[3]), ulong.Parse(args[4])); break;
    default: throw new ArgumentException($"Unknown mode {args[0]}");
}

static RocksDb Open(string dir, ulong cacheBytes, out Cache cache, out DbOptions options)
{
    cache = Cache.CreateLru(cacheBytes);
    BlockBasedTableOptions table = new BlockBasedTableOptions().SetBlockCache(cache).SetBlockSize(16 * 1024);
    options = new DbOptions().SetCreateIfMissing(true).SetBlockBasedTableFactory(table);
    return RocksDb.Open(options, dir);
}

static int BlobCount(int record) => 1 + (int)(Mix((ulong)record) % 6);

static void BlobKey(int record, Span<byte> key)
{
    BinaryPrimitives.WriteInt64BigEndian(key, 1_700_000_000 + record);
    for (int i = 8; i < BlobKeyLength; i += 8) BinaryPrimitives.WriteUInt64BigEndian(key[i..], Mix((ulong)(record * 4 + i)));
}

static void Fill(Span<byte> value, ulong seed)
{
    for (int i = 0; i + 8 <= value.Length; i += 8) BinaryPrimitives.WriteUInt64LittleEndian(value[i..], seed = Mix(seed));
}

static void BuildBlobs(string dir)
{
    using RocksDb db = Open(dir, 32UL << 20, out Cache cache, out DbOptions options);
    byte[] key = new byte[BlobKeyLength];
    byte[] value = new byte[6 * BlobLength];
    using WriteBatch batch = new();
    for (int record = 0; record < BlobRecords; record++)
    {
        BlobKey(record, key);
        Span<byte> span = value.AsSpan(0, BlobCount(record) * BlobLength);
        Fill(span, (ulong)record);
        batch.Put(key, span);
        if (batch.Count() == 64) { db.Write(batch); batch.Clear(); }
    }
    db.Write(batch);
    Compact(db);
    options.Dispose();
    cache.Dispose();
}

static void RunBlobs(string dir, string label, int batchSize, int batches, ulong seed)
{
    using RocksDb db = Open(dir, 32UL << 20, out Cache cache, out DbOptions options);
    int[] records = Shuffle(BlobRecords, seed);
    if (records.Length < 3 * batches * batchSize) throw new ArgumentException("Not enough records for disjoint sets.");
    byte[][] keys = new byte[batchSize][];
    for (int i = 0; i < batchSize; i++) keys[i] = new byte[BlobKeyLength];
    byte[] fixedKeys = new byte[batchSize * BlobKeyLength];
    int[] lengths = new int[batchSize];
    int next = 0;
    string[] arms = ["multiget", "decoded", "scalar"];
    // Rotated per seed, so no arm always reads first.
    for (int shift = (int)(seed % 3), a = 0; a < arms.Length; a++)
    {
        string arm = arms[(a + shift) % arms.Length];
        for (int b = 0; b < batches; b++)
        {
            int first = next;
            for (int i = 0; i < batchSize; i++) BlobKey(records[next++], keys[i]);
            long t0 = Stopwatch.GetTimestamp();
            if (arm == "multiget")
            {
                KeyValuePair<byte[], byte[]?>[] values = db.MultiGet(keys);
                for (int i = 0; i < batchSize; i++) lengths[i] = values[i].Value?.Length ?? -1;
            }
            else if (arm == "decoded")
            {
                for (int i = 0; i < batchSize; i++) keys[i].CopyTo(fixedKeys, i * BlobKeyLength);
                db.MultiGet<int, LengthDecoder>(fixedKeys, BlobKeyLength, lengths, default);
            }
            else
            {
                for (int i = 0; i < batchSize; i++) lengths[i] = db.Get(keys[i])?.Length ?? -1;
            }
            double us = Stopwatch.GetElapsedTime(t0).TotalMicroseconds;
            for (int i = 0; i < batchSize; i++)
                if (lengths[i] != BlobCount(records[first + i]) * BlobLength) throw new InvalidOperationException("Wrong blob value.");
            Console.WriteLine($"{label},blobs-{batchSize},{arm},{b},{us:F1},0");
        }
    }
    options.Dispose();
    cache.Dispose();
}

static void StateKey(int record, Span<byte> key)
{
    for (int i = 0; i < StateKeyLength; i += 8) BinaryPrimitives.WriteUInt64BigEndian(key[i..], Mix((ulong)(record * 4 + i)));
}

static void BuildState(string dir)
{
    using RocksDb db = Open(dir, 64UL << 20, out Cache cache, out DbOptions options);
    Span<byte> key = stackalloc byte[StateKeyLength];
    Span<byte> value = stackalloc byte[StateValueLength];
    using WriteBatch batch = new();
    for (int record = 0; record < StateRecords; record++)
    {
        StateKey(record, key);
        Fill(value, (ulong)record);
        batch.Put(key, value);
        if (batch.Count() == 65536) { db.Write(batch); batch.Clear(); }
    }
    db.Write(batch);
    Compact(db);
    options.Dispose();
    cache.Dispose();
}

static void RunState(string dir, string label, int batches, ulong seed)
{
    using RocksDb db = Open(dir, 1UL << 30, out Cache cache, out DbOptions options);
    using ReadOptions readOptions = new();
    int[] records = Shuffle(StateRecords, seed);
    byte[] keys = new byte[StateBatch * StateKeyLength];
    // The arrays Nethermind reuses per thread for its MultiGetAsync prefetch.
    byte[][] keyArrays = new byte[StateBatch][];
    for (int i = 0; i < StateBatch; i++) keyArrays[i] = new byte[StateKeyLength];
    int next = 0;

    // Cold: disjoint key sets per arm, each read once. Warm: one more pass over keys the cold arms just cached.
    List<string> arms = ["multigetasync"];
#if HAS_PREFETCH
    arms.Add("prefetch");
#endif
    // Rotated per seed, so no arm always reads first.
    if (seed % 2 == 0) arms.Reverse();
    Dictionary<string, int> starts = [];
    foreach (string arm in arms)
    {
        starts[arm] = next;
        for (int b = 0; b < batches; b++, next += StateBatch) Measure(arm, "cold", b, next);
    }
    foreach (string arm in arms)
        for (int b = 0; b < batches; b++) Measure(arm, "warm", b, starts[arm] + b * StateBatch);

    options.Dispose();
    cache.Dispose();

    void Measure(string arm, string temperature, int b, int start)
    {
        for (int i = 0; i < StateBatch; i++) StateKey(records[start + i], keys.AsSpan(i * StateKeyLength, StateKeyLength));
        long allocated = GC.GetTotalAllocatedBytes(precise: true);
        long t0 = Stopwatch.GetTimestamp();
        if (arm == "multigetasync")
        {
            for (int i = 0; i < StateBatch; i++) keys.AsSpan(i * StateKeyLength, StateKeyLength).CopyTo(keyArrays[i]);
            db.MultiGetAsync(keyArrays, null, readOptions).AsTask().GetAwaiter().GetResult();
        }
#if HAS_PREFETCH
        else
        {
            db.PrefetchAsync(keys, StateKeyLength, null, readOptions).GetAwaiter().GetResult();
        }
#endif
        double us = Stopwatch.GetElapsedTime(t0).TotalMicroseconds;
        long bytes = GC.GetTotalAllocatedBytes(precise: true) - allocated;
        Console.WriteLine($"{label},state-{temperature},{arm},{b},{us:F1},{bytes}");
    }
}

static void Compact(RocksDb db)
{
    using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
    db.Flush(flush);
    db.CompactRange(null, null);
}

static int[] Shuffle(int count, ulong seed)
{
    int[] records = new int[count];
    for (int i = 0; i < count; i++) records[i] = i;
    for (int i = count - 1; i > 0; i--)
    {
        seed = Mix(seed);
        int j = (int)(seed % (ulong)(i + 1));
        (records[i], records[j]) = (records[j], records[i]);
    }
    return records;
}

static ulong Mix(ulong x)
{
    x += 0x9E3779B97F4A7C15UL;
    x = (x ^ (x >> 30)) * 0xBF58476D1CE4E5B9UL;
    x = (x ^ (x >> 27)) * 0x94D049BB133111EBUL;
    return x ^ (x >> 31);
}

// Decodes a value to its length only: the decoded MultiGet path, with no copy of the value.
internal readonly struct LengthDecoder : ISpanDeserializer<int>
{
    public int Deserialize(ReadOnlySpan<byte> buffer) => buffer.Length;
}
