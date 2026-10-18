// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Nethermind.RocksDbBindings.Native;

namespace Nethermind.RocksDbBindings.Tests;

public class AsyncReadTests
{
    [Test]
    public async Task NativeCallback_CompletesOnTheReadExecutorWhenEnabled()
    {
        using TestDatabase database = TestDatabase.Create();
        using ReadOptions options = new();
        using HandleLease dbLease = database.Db.LeaseHandle(out nint db);
        using HandleLease optionsLease = options.Lease(out nint readOptions);
        TaskCompletionSource<int> completion = new(TaskCreationOptions.RunContinuationsAsynchronously);
        GCHandle context = GCHandle.Alloc(completion);
        nint request = 0;
        int submittingThread = Environment.CurrentManagedThreadId;
        try
        {
            request = SubmitNativeRead(db, readOptions, GCHandle.ToIntPtr(context));
            int completingThread = await completion.Task;
            if (Environment.GetEnvironmentVariable("ROCKSDB_EXPECT_NATIVE_ASYNC") == "1")
                await Assert.That(completingThread).IsNotEqualTo(submittingThread);
        }
        finally
        {
            DestroyNativeRead(request);
            context.Free();
        }
    }

    private static unsafe nint SubmitNativeRead(nint db, nint options, nint context)
    {
        sbyte* key = null;
        nuint length = 0;
        sbyte* error = null;
        rocksdb_net_read_t* request = RocksDbNative.rocksdb_net_read_create(RocksDbInterop.Db(db),
            RocksDbInterop.ReadOptions(options), 1, null, &key, &length, &error);
        RocksDbInterop.ThrowIfError(error);
        RocksDbNative.rocksdb_net_get_async(request, (void*)context, &NativeCompleted);
        return (nint)request;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void NativeCompleted(void* context)
        => ((TaskCompletionSource<int>)GCHandle.FromIntPtr((nint)context).Target!)
            .SetResult(Environment.CurrentManagedThreadId);

    private static unsafe void DestroyNativeRead(nint request)
        => RocksDbNative.rocksdb_net_read_destroy((rocksdb_net_read_t*)request);

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task GetAsync_DistinguishesMissingEmptyAndBinaryValues(bool flush)
    {
        using TestDatabase database = TestDatabase.Create();
        byte[] key = [0, 255, 1];
        byte[] value = [255, 0, 254];
        database.Db.Put(key, value);
        database.Db.Put([], []);
        if (flush)
        {
            using FlushOptions options = new FlushOptions().SetWaitForFlush(true);
            database.Db.Flush(options);
        }

        await Assert.That(await database.Db.GetAsync(key)).IsEquivalentTo(value, CollectionOrdering.Matching);
        await Assert.That(await database.Db.GetAsync([])).IsNotNull().And.IsEmpty();
        await Assert.That(await database.Db.GetAsync("missing"u8)).IsNull();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task GetAsync_DeserializesValuesAndReturnsDefaultForMissingKeys(bool typed)
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put("key"u8, new byte[] { 42, 0, 0, 0 });
        database.Db.Put("empty"u8, []);
        using FlushOptions options = new FlushOptions().SetWaitForFlush(true);
        database.Db.Flush(options);
        for (int i = 0; i < 32; i++)
        {
            int value = typed
                ? await database.Db.GetAsync<int, Int32Deserializer>("key"u8, default)
                : await database.Db.GetAsync("key"u8, new Int32Deserializer());
            int missing = typed
                ? await database.Db.GetAsync<int, Int32Deserializer>("missing"u8, default)
                : await database.Db.GetAsync("missing"u8, new Int32Deserializer());
            await Assert.That(value).IsEqualTo(42);
            await Assert.That(missing).IsEqualTo(0);
        }
        LengthDeserializer deserializer = new();
        await Assert.That(await database.Db.GetAsync("empty"u8, deserializer)).IsEqualTo("0");
        await Assert.That(await database.Db.GetAsync("missing"u8, deserializer)).IsNull();
        await Assert.That(deserializer.Calls).IsEqualTo(1);
        await Assert.That(async () => await database.Db.GetAsync<int>([], null!))
            .ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task GetAsync_KeepsNativeMemoryAliveDuringDeserializationAndReleasesOnCompletion(bool fail)
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put("key"u8, new byte[] { 42, 0, 0, 0 });
        using Snapshot snapshot = database.Db.CreateSnapshot();
        using ReadOptions options = new ReadOptions().SetSnapshot(snapshot);
        ActionDeserializer deserializer = new(() =>
        {
            options.Dispose();
            snapshot.Dispose();
            database.Db.Dispose();
            if (fail)
                throw new FormatException("Invalid value");
        });
        ValueTask<int> read = database.Db.GetAsync("key"u8, deserializer, readOptions: options);
        if (fail)
            await Assert.That(async () => await read).ThrowsExactly<FormatException>();
        else
            await Assert.That(await read).IsEqualTo(42);

        using DbOptions reopenOptions = new();
        using RocksDb reopened = RocksDb.Open(reopenOptions, database.Path);
        await Assert.That(await reopened.GetAsync("key"u8, new ActionDeserializer(() => { }))).IsEqualTo(42);
    }

    [Test]
    public async Task GetAsync_ConcurrentDeserializersKeepTheirOwnState()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put("key"u8, new byte[] { 42, 0, 0, 0 });
        for (int round = 0; round < 4; round++)
        {
            Task<int>[] reads = new Task<int>[128];
            for (int i = 0; i < reads.Length; i++)
                reads[i] = database.Db.GetAsync<int, Int32Deserializer>("key"u8, new(i)).AsTask();
            int[] results = await Task.WhenAll(reads);
            for (int i = 0; i < results.Length; i++)
                await Assert.That(results[i]).IsEqualTo(42 + i);
        }
    }

    [Test]
    public async Task GetAsync_ReleasesNativeResourcesBeforeResultIsConsumed()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put("key"u8, new byte[] { 42, 0, 0, 0 });
        ValueTask<int> read = database.Db.GetAsync<int, Int32Deserializer>("key"u8, default);
        while (!read.IsCompleted)
            await Task.Yield();
        database.Db.Dispose();
        using DbOptions options = new();
        using RocksDb reopened = RocksDb.Open(options, database.Path);
        await Assert.That(await read).IsEqualTo(42);
    }

    private readonly struct Int32Deserializer(int offset) : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => BinaryPrimitives.ReadInt32LittleEndian(buffer) + offset;
    }

    private sealed class LengthDeserializer : ISpanDeserializer<string>
    {
        internal int Calls { get; private set; }

        public string Deserialize(ReadOnlySpan<byte> buffer)
        {
            Calls++;
            return buffer.Length.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }
    }

    private sealed class ActionDeserializer(Action action) : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer)
        {
            action();
            return BinaryPrimitives.ReadInt32LittleEndian(buffer);
        }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGetAsync_PreservesOrderDuplicatesAndColumnFamilies(bool flush)
    {
        using DbOptions options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using ColumnFamilyOptions familyOptions = new();
        using TestDatabase database = TestDatabase.Create(options, new ColumnFamilies { { "blocks", familyOptions } });
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        IColumnFamilyHandle defaultFamily = database.Db.GetDefaultColumnFamily();
        byte[] key = [0, 255];
        database.Db.Put(key, "default"u8);
        database.Db.Put(key, "block"u8, blocks);
        database.Db.Put([], []);
        if (flush)
        {
            using FlushOptions flushOptions = new FlushOptions().SetWaitForFlush(true);
            database.Db.Flush(flushOptions);
            database.Db.Flush(flushOptions, blocks);
        }

        KeyValuePair<byte[], byte[]?>[] values = await database.Db.MultiGetAsync([key, [], key, "missing"u8.ToArray(), key],
            [blocks, defaultFamily, defaultFamily, blocks, blocks]);

        await Assert.That(values.Length).IsEqualTo(5);
        await Assert.That(values[0].Value).IsEquivalentTo("block"u8.ToArray(), CollectionOrdering.Matching);
        await Assert.That(values[1].Value).IsNotNull().And.IsEmpty();
        await Assert.That(values[2].Value).IsEquivalentTo("default"u8.ToArray(), CollectionOrdering.Matching);
        await Assert.That(values[3].Value).IsNull();
        await Assert.That(values[4].Value).IsEquivalentTo("block"u8.ToArray(), CollectionOrdering.Matching);
        await Assert.That(values[0].Key).IsEquivalentTo(key, CollectionOrdering.Matching);
        await Assert.That(await database.Db.GetAsync(key, blocks)).IsEquivalentTo("block"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Reads_SurviveDisposalOfDatabaseOptionsAndSnapshot()
    {
        using TestDatabase database = TestDatabase.Create();
        byte[] key = [1];
        database.Db.Put(key, "old"u8);
        using Snapshot snapshot = database.Db.CreateSnapshot();
        using ReadOptions options = new ReadOptions().SetSnapshot(snapshot);
        database.Db.Put(key, "new"u8);

        Task<byte[]?>[] reads = Enumerable.Range(0, 128)
            .Select(_ => database.Db.GetAsync(key, readOptions: options).AsTask()).ToArray();
        ValueTask<KeyValuePair<byte[], byte[]?>[]> batch = database.Db.MultiGetAsync([key, key], readOptions: options);
        options.Dispose();
        snapshot.Dispose();
        database.Db.Dispose();

        foreach (byte[]? value in await Task.WhenAll(reads))
            await Assert.That(value).IsEquivalentTo("old"u8.ToArray(), CollectionOrdering.Matching);
        foreach (KeyValuePair<byte[], byte[]?> pair in await batch)
            await Assert.That(pair.Value).IsEquivalentTo("old"u8.ToArray(), CollectionOrdering.Matching);

        // All operation leases must have been released before completion.
        using DbOptions reopenOptions = new();
        using RocksDb reopened = RocksDb.Open(reopenOptions, database.Path);
        await Assert.That(reopened.Get(key)).IsEquivalentTo("new"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task MultiGetAsync_CopiesSubmittedKeys()
    {
        using TestDatabase database = TestDatabase.Create();
        byte[] key = [1];
        database.Db.Put(key, [2]);
        byte[][] keys = [key];
        ValueTask<KeyValuePair<byte[], byte[]?>[]> read = database.Db.MultiGetAsync(keys);
        key[0] = 3;
        keys[0] = [4];

        KeyValuePair<byte[], byte[]?>[] result = await read;
        await Assert.That(result[0].Key).IsEquivalentTo(new byte[] { 1 }, CollectionOrdering.Matching);
        await Assert.That(result[0].Value).IsEquivalentTo(new byte[] { 2 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ReadError_FaultsSingleAndBatchReads()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put("key"u8, "value"u8);
        using ReadOptions options = new();
        using PinnedGCHandle<byte[]> timestamp = new(new byte[1]);
        SetTimestamp(options, timestamp);

        await Assert.That(async () => await database.Db.GetAsync("key"u8, readOptions: options))
            .Throws<RocksDbException>();
        await Assert.That(async () => await database.Db.GetAsync<int, Int32Deserializer>("key"u8, default, readOptions: options))
            .Throws<RocksDbException>();
        await Assert.That(async () => await database.Db.MultiGetAsync(["key"u8.ToArray()], readOptions: options))
            .Throws<RocksDbException>();
    }

    private static unsafe void SetTimestamp(ReadOptions options, PinnedGCHandle<byte[]> timestamp)
    {
        using HandleLease lease = options.Lease(out nint handle);
        // The default comparator does not accept a read timestamp.
        RocksDbNative.rocksdb_readoptions_set_timestamp(RocksDbInterop.ReadOptions(handle),
            (sbyte*)timestamp.GetAddressOfArrayData(), 1);
    }

    [Test]
    public async Task MultiGetAsync_ValidatesInputsAndAcceptsEmptyBatches()
    {
        using TestDatabase database = TestDatabase.Create();
        await Assert.That(await database.Db.MultiGetAsync([])).IsEmpty();
        await Assert.That(async () => await database.Db.MultiGetAsync(null!)).ThrowsExactly<ArgumentNullException>();
        await Assert.That(async () => await database.Db.MultiGetAsync([null!])).ThrowsExactly<ArgumentException>();
        await Assert.That(async () => await database.Db.MultiGetAsync([[]], [])).ThrowsExactly<ArgumentException>();
        using ReadOptions options = new();
        options.Dispose();
        await Assert.That(async () => await database.Db.GetAsync([], readOptions: options)).ThrowsExactly<ObjectDisposedException>();
        database.Db.Dispose();
        await Assert.That(async () => await database.Db.GetAsync([])).ThrowsExactly<ObjectDisposedException>();
        await Assert.That(async () => await database.Db.MultiGetAsync([])).ThrowsExactly<ObjectDisposedException>();
    }

    private static byte[] FixedKeys(params int[] ids)
    {
        byte[] keys = new byte[ids.Length * 4];
        for (int i = 0; i < ids.Length; i++) BinaryPrimitives.WriteInt32BigEndian(keys.AsSpan(i * 4, 4), ids[i]);
        return keys;
    }

    private static byte[] Value(int id)
    {
        byte[] value = new byte[4];
        BinaryPrimitives.WriteInt32LittleEndian(value, id * 10);
        return value;
    }

    [Test]
    [Arguments(false, false, 3)]
    [Arguments(false, true, 3)]
    [Arguments(true, false, 3)]
    [Arguments(true, true, 3)]
    [Arguments(true, false, 300)]
    [Arguments(true, true, 300)]
    public async Task MultiGetAsync_Decoded_MatchesTheSyncDecodedRead(bool flush, bool namedFamily, int count)
    {
        using DbOptions options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using ColumnFamilyOptions familyOptions = new();
        using TestDatabase database = TestDatabase.Create(options, new ColumnFamilies { { "blocks", familyOptions } });
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        int[] ids = Enumerable.Range(0, count).Select(i => i % 7 == 3 ? i - 1 : i).ToArray();
        for (int i = 0; i < count; i++)
        {
            if (i % 5 != 4) database.Db.Put(FixedKeys(i), Value(i), family);
        }
        if (flush)
        {
            using FlushOptions flushOptions = new FlushOptions().SetWaitForFlush(true);
            if (family is null) database.Db.Flush(flushOptions);
            else database.Db.Flush(flushOptions, family);
        }

        byte[] keys = FixedKeys(ids);
        int[] expected = new int[count];
        int[] actual = Enumerable.Repeat(-1, count).ToArray();
        database.Db.MultiGet<int, Int32Deserializer>(keys, 4, expected, new Int32Deserializer(1), family);
        await database.Db.MultiGetAsync<int, Int32Deserializer>(keys, 4, actual, new Int32Deserializer(1), family);

        await Assert.That(actual).IsEquivalentTo(expected, CollectionOrdering.Matching);
        for (int i = 0; i < count; i++)
        {
            int id = ids[i];
            await Assert.That(actual[i]).IsEqualTo(id % 5 == 4 ? 0 : id * 10 + 1);
        }
    }

    [Test]
    public async Task MultiGetAsync_Decoded_CopiesKeysAndDistinguishesEmptyValues()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put(FixedKeys(1), Value(1));
        database.Db.Put(FixedKeys(2), []);
        LengthDeserializer deserializer = new();
        byte[] keys = FixedKeys(1, 2, 3);
        string[] values = new string[3];

        ValueTask read = database.Db.MultiGetAsync<string, LengthDeserializer>(keys, 4, values, deserializer);
        keys.AsSpan().Fill(0xFF);
        await read;

        await Assert.That(values[0]).IsEqualTo("4");
        await Assert.That(values[1]).IsEqualTo("0");
        await Assert.That(values[2]).IsNull();
        await Assert.That(deserializer.Calls).IsEqualTo(2);
    }

    [Test]
    public async Task MultiGetAsync_Decoded_ClearsTheTailOnDecoderFailureAndReleasesResources()
    {
        using TestDatabase database = TestDatabase.Create();
        for (int i = 0; i < 4; i++) database.Db.Put(FixedKeys(i), Value(i));
        int calls = 0;
        ActionDeserializer deserializer = new(() =>
        {
            if (++calls == 3) throw new InvalidOperationException("decoder");
        });
        int[] values = [-1, -1, -1, -1];

        await Assert.That(async () => await database.Db.MultiGetAsync<int, ActionDeserializer>(FixedKeys(0, 1, 2, 3), 4, values, deserializer))
            .ThrowsExactly<InvalidOperationException>();
        await Assert.That(values).IsEquivalentTo(new[] { 0, 10, 0, 0 }, CollectionOrdering.Matching);

        database.Db.Dispose();
        using DbOptions reopenOptions = new();
        using RocksDb reopened = RocksDb.Open(reopenOptions, database.Path);
        await Assert.That(reopened.Get(FixedKeys(1))).IsEquivalentTo(Value(1), CollectionOrdering.Matching);
    }

    [Test]
    public async Task MultiGetAsync_Decoded_FaultsOnReadErrorAndValidatesInputs()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put(FixedKeys(1), Value(1));
        using ReadOptions options = new();
        using PinnedGCHandle<byte[]> timestamp = new(new byte[1]);
        SetTimestamp(options, timestamp);
        int[] values = [-1];

        await Assert.That(async () => await database.Db.MultiGetAsync<int, Int32Deserializer>(FixedKeys(1), 4, values, default, readOptions: options))
            .Throws<RocksDbException>();
        await Assert.That(values[0]).IsEqualTo(0);

        await database.Db.MultiGetAsync<int, Int32Deserializer>([], 4, Memory<int>.Empty, default);
        await Assert.That(async () => await database.Db.MultiGetAsync<int, Int32Deserializer>(FixedKeys(1), 0, new int[1], default))
            .ThrowsExactly<ArgumentOutOfRangeException>();
        await Assert.That(async () => await database.Db.MultiGetAsync<int, Int32Deserializer>(FixedKeys(1), 4, new int[2], default))
            .ThrowsExactly<ArgumentException>();
        await Assert.That(async () => await database.Db.MultiGetAsync<string, LengthDeserializer>(FixedKeys(1), 4, new string[1], null!))
            .ThrowsExactly<ArgumentNullException>();
    }
}
