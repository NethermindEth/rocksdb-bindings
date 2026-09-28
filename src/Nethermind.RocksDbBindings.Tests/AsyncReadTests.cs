// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

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
}
