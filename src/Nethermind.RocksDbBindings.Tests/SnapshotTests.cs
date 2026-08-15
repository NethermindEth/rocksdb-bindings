// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class SnapshotTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Original = "original"u8.ToArray();
    private static readonly byte[] Replacement = "replacement"u8.ToArray();

    [Test]
    public async Task ASnapshotRead_StillSeesAnOverwrittenValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Original);
        using var snapshot = database.Db.CreateSnapshot();
        var readOptions = new ReadOptions().SetSnapshot(snapshot);

        database.Db.Put(Key, Replacement);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key, readOptions: readOptions)).IsEquivalentTo(Original, CollectionOrdering.Matching);
            await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Replacement, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task ASnapshotRead_StillSeesADeletedValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Original);
        using var snapshot = database.Db.CreateSnapshot();
        var readOptions = new ReadOptions().SetSnapshot(snapshot);

        database.Db.Remove(Key);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key, readOptions: readOptions)).IsEquivalentTo(Original, CollectionOrdering.Matching);
            await Assert.That(database.Db.Get(Key)).IsNull();
        }
    }

    [Test]
    public async Task ASnapshotRead_DoesNotSeeKeysAddedAfterIt()
    {
        using var database = TestDatabase.Create();
        using var snapshot = database.Db.CreateSnapshot();
        var readOptions = new ReadOptions().SetSnapshot(snapshot);

        database.Db.Put(Key, Original);

        await Assert.That(database.Db.Get(Key, readOptions: readOptions)).IsNull();
    }

    [Test]
    public async Task AnIteratorOverASnapshot_SeesTheSnapshottedKeys()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "A");
        using var snapshot = database.Db.CreateSnapshot();
        var readOptions = new ReadOptions().SetSnapshot(snapshot);
        database.Db.Put("b", "B");

        var keys = new List<string>();
        using (var iterator = database.Db.NewIterator(readOptions: readOptions))
        {
            for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
                keys.Add(iterator.StringKey());
        }

        await Assert.That(keys).IsEquivalentTo(new[] { "a" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task SetSnapshot_IsFluent()
    {
        using var database = TestDatabase.Create();
        using var snapshot = database.Db.CreateSnapshot();
        var readOptions = new ReadOptions();

        await Assert.That(readOptions.SetSnapshot(snapshot)).IsSameReferenceAs(readOptions);
    }

    [Test]
    public async Task CreateSnapshot_ReturnsDistinctSnapshots()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Original);

        using var first = database.Db.CreateSnapshot();
        database.Db.Put(Key, Replacement);
        using var second = database.Db.CreateSnapshot();

        using (Assert.Multiple())
        {
            await Assert.That(first.Handle).IsNotEqualTo(second.Handle);
            await Assert.That(database.Db.Get(Key, readOptions: new ReadOptions().SetSnapshot(first))).IsEquivalentTo(Original, CollectionOrdering.Matching);
            await Assert.That(database.Db.Get(Key, readOptions: new ReadOptions().SetSnapshot(second))).IsEquivalentTo(Replacement, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        using var database = TestDatabase.Create();
        var snapshot = database.Db.CreateSnapshot();

        snapshot.Dispose();

        await Assert.That(snapshot.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        using var database = TestDatabase.Create();
        var snapshot = database.Db.CreateSnapshot();
        snapshot.Dispose();

        await Assert.That(snapshot.Dispose).ThrowsNothing();
    }
}
