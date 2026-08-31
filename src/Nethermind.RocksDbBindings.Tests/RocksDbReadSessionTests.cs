// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;

namespace Nethermind.RocksDbBindings.Tests;

public class RocksDbReadSessionTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Value = "value"u8.ToArray();

    private sealed class Int32Deserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => BitConverter.ToInt32(buffer);
    }

    [Test]
    public async Task Reads_ReuseTheSessionAcrossReadShapes()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        database.Db.Put("number"u8, BitConverter.GetBytes(1234));
        database.Db.Put("empty"u8, ReadOnlySpan<byte>.Empty);
        using var session = database.Db.CreateReadSession();
        var destination = new byte[Value.Length + 2];
        var fixedSizeDestination = new byte[Value.Length];

        byte[]? value = session.Get(Key);
        int length = session.Get(Key, destination);
        bool fixedSizeFound = session.GetFixedSizeValue(Key, fixedSizeDestination);
        var pinned = GetPinned(session, Key);
        byte[] spanValue = GetSpanCopy(session, Key);
        int number = session.Get("number"u8, new Int32Deserializer());

        using (Assert.Multiple())
        {
            await Assert.That(value).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(length).IsEqualTo(Value.Length);
            await Assert.That(destination[..length]).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(fixedSizeFound).IsTrue();
            await Assert.That(fixedSizeDestination).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(pinned.Found).IsTrue();
            await Assert.That(pinned.Value).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(spanValue).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(number).IsEqualTo(1234);
            await Assert.That(session.HasKey("empty"u8)).IsTrue();
            await Assert.That(session.HasKey("missing"u8)).IsFalse();
        }
    }

    [Test]
    public async Task CreateReadSession_UsesTheProvidedReadOptions()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, "original"u8);
        using var snapshot = database.Db.CreateSnapshot();
        database.Db.Put(Key, "replacement"u8);
        using var readOptions = new ReadOptions().SetSnapshot(snapshot);
        using var session = database.Db.CreateReadSession(readOptions);

        using (Assert.Multiple())
        {
            await Assert.That(session.Get(Key)).IsEquivalentTo("original"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(database.Db.Get(Key)).IsEquivalentTo("replacement"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Reads_CanTargetAColumnFamily()
    {
        using var options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using var familyOptions = new ColumnFamilyOptions();
        var families = new ColumnFamilies { { "blocks", familyOptions } };
        using var database = TestDatabase.Create(options, families);
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, "default"u8);
        database.Db.Put(Key, "blocks"u8, blocks);
        using var session = database.Db.CreateReadSession();

        using (Assert.Multiple())
        {
            await Assert.That(session.Get(Key)).IsEquivalentTo("default"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(session.Get(Key, blocks)).IsEquivalentTo("blocks"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task DisposeOfReadOptions_IsDeferredUntilSessionDispose()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        var readOptions = new ReadOptions();
        var session = database.Db.CreateReadSession(readOptions);

        readOptions.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(session.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(readOptions.SafeHandle.IsClosed).IsFalse();
        }

        session.Dispose();

        await Assert.That(readOptions.SafeHandle.IsClosed).IsTrue();
    }

    [Test]
    public async Task DisposeOfDatabase_ClosesImmediatelyAndMakesSessionReadsThrow()
    {
        using var directory = new TempDirectory();
        string path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var database = RocksDb.Open(creatingOptions, path);
        database.Put(Key, Value);
        using var session = database.CreateReadSession();

        database.Dispose();

        using var reopenOptions = new DbOptions();
        using (Assert.Multiple())
        {
            await Assert.That(() => session.Get(Key)).Throws<ObjectDisposedException>();
            await Assert.That(() => session.HasKey(Key)).Throws<ObjectDisposedException>();
            await Assert.That(() => session.GetSpan(Key).Length).Throws<ObjectDisposedException>();
            await Assert.That(() => TryGetPinnedAndDispose(session)).Throws<ObjectDisposedException>();
            await Assert.That(() => session.GetFixedSizeValue(Key, new byte[Value.Length])).Throws<ObjectDisposedException>();
            await Assert.That(() => session.Get(Key, new byte[Value.Length])).Throws<ObjectDisposedException>();
            await Assert.That(() => session.Get(Key, new Int32Deserializer())).Throws<ObjectDisposedException>();
            await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).ThrowsNothing();
        }
    }

    [Test]
    public async Task Dispose_IsIdempotentAndMakesLaterReadsThrow()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        var session = database.Db.CreateReadSession();

        session.Dispose();
        session.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(() => session.Get(Key)).Throws<ObjectDisposedException>();
            await Assert.That(() => session.HasKey(Key)).Throws<ObjectDisposedException>();
            await Assert.That(() => session.GetSpan(Key).Length).Throws<ObjectDisposedException>();
            await Assert.That(() => TryGetPinnedAndDispose(session)).Throws<ObjectDisposedException>();
            await Assert.That(() => session.GetFixedSizeValue(Key, new byte[Value.Length])).Throws<ObjectDisposedException>();
            await Assert.That(() => session.Get(Key, new byte[Value.Length])).Throws<ObjectDisposedException>();
            await Assert.That(() => session.Get(Key, new Int32Deserializer())).Throws<ObjectDisposedException>();
            await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task CreateReadSession_RejectsDisposedHandles()
    {
        using var directory = new TempDirectory();
        string path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var database = RocksDb.Open(creatingOptions, path);
        var readOptions = new ReadOptions();
        readOptions.Dispose();

        await Assert.That(() => database.CreateReadSession(readOptions)).Throws<ObjectDisposedException>();

        database.Dispose();

        using var reopenOptions = new DbOptions();
        using (Assert.Multiple())
        {
            await Assert.That(() => database.CreateReadSession()).Throws<ObjectDisposedException>();
            await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).ThrowsNothing();
        }
    }

    [Test]
    public async Task Finalizer_ReleasesAnAbandonedOptionsLease()
    {
        using var directory = new TempDirectory();
        string path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var database = RocksDb.Open(creatingOptions, path);
        var readOptions = new ReadOptions();
        WeakReference session = CreateAbandonedSession(database, readOptions);

        readOptions.Dispose();
        database.Dispose();
        Collect();

        using var reopenOptions = new DbOptions();
        using (Assert.Multiple())
        {
            await Assert.That(session.IsAlive).IsFalse();
            await Assert.That(readOptions.SafeHandle.IsClosed).IsTrue();
            await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).ThrowsNothing();
        }
    }

    [Test]
    public async Task Reads_CanRunConcurrentlyOnOneSession()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        using var session = database.Db.CreateReadSession();

        Task<bool>[] readers = Enumerable.Range(0, 8)
            .Select(_ => Task.Run(() =>
            {
                for (var i = 0; i < 100; i++)
                {
                    if (!session.Get(Key)!.AsSpan().SequenceEqual(Value))
                        return false;
                }

                return true;
            }))
            .ToArray();

        bool[] results = await Task.WhenAll(readers);

        await Assert.That(results).IsEquivalentTo(Enumerable.Repeat(true, readers.Length), CollectionOrdering.Matching);
    }

    [Test]
    public async Task TryGetPinned_ReturnedSliceOutlivesSessionAndDatabase()
    {
        using var directory = new TempDirectory();
        string path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var database = RocksDb.Open(creatingOptions, path);
        byte[] expected = new byte[64 * 1024];
        new Random(42).NextBytes(expected);
        database.Put(Key, expected);
        using (var flushOptions = new FlushOptions().SetWaitForFlush(true))
            database.Flush(flushOptions);

        byte[]? warmedValue = database.Get(Key);
        bool readPinnedUsageBefore = database.TryGetIntProperty("rocksdb.block-cache-pinned-usage", out ulong pinnedUsageBefore);
        var session = database.CreateReadSession();
        bool found = session.TryGetPinned(Key, out var slice);
        bool readPinnedUsageWhileHeld = database.TryGetIntProperty("rocksdb.block-cache-pinned-usage", out ulong pinnedUsageWhileHeld);

        session.Dispose();
        database.Dispose();

        byte[] value;
        try
        {
            value = slice.Value.ToArray();
        }
        finally
        {
            slice.Dispose();
        }

        using var reopenOptions = new DbOptions();
        RocksDb.Open(reopenOptions, path).Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(warmedValue).IsEquivalentTo(expected, CollectionOrdering.Matching);
            await Assert.That(readPinnedUsageBefore).IsTrue();
            await Assert.That(readPinnedUsageWhileHeld).IsTrue();
            await Assert.That(pinnedUsageWhileHeld).IsGreaterThan(pinnedUsageBefore);
            await Assert.That(value).IsEquivalentTo(expected, CollectionOrdering.Matching);
        }
    }

    private static (bool Found, byte[] Value) GetPinned(RocksDbReadSession session, byte[] key)
    {
        bool found = session.TryGetPinned(key, out var slice);
        try
        {
            return (found, slice.Value.ToArray());
        }
        finally
        {
            slice.Dispose();
        }
    }

    private static byte[] GetSpanCopy(RocksDbReadSession session, byte[] key)
    {
        Span<byte> span = session.GetSpan(key);
        try
        {
            return span.ToArray();
        }
        finally
        {
            session.DangerousReleaseMemory(span);
        }
    }

    private static bool TryGetPinnedAndDispose(RocksDbReadSession session)
    {
        bool found = session.TryGetPinned(Key, out var slice);
        slice.Dispose();
        return found;
    }

    private static void Collect()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference CreateAbandonedSession(RocksDb database, ReadOptions readOptions)
    {
        var session = database.CreateReadSession(readOptions);
        return new WeakReference(session);
    }
}
