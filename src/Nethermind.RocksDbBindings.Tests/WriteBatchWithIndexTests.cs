// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class WriteBatchWithIndexTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Stored = "stored"u8.ToArray();
    private static readonly byte[] Pending = "pending"u8.ToArray();

    private static List<string> Overlay(RocksDb db, WriteBatchWithIndex batch)
    {
        // NewIterator detaches the base iterator, so only the returned one may be disposed.
        using var iterator = batch.NewIterator(db.NewIterator());
        var keys = new List<string>();

        for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
            keys.Add(iterator.StringKey());

        return keys;
    }

    [Test]
    public async Task NewBatch_IsEmpty()
    {
        using var batch = new WriteBatchWithIndex();

        await Assert.That(batch.Count()).IsEqualTo(0);
    }

    [Test]
    public async Task Put_IsCounted()
    {
        using var batch = new WriteBatchWithIndex();

        batch.Put(Key, Pending);

        await Assert.That(batch.Count()).IsEqualTo(1);
    }

    [Test]
    public async Task Clear_DropsEverythingRecordedSoFar()
    {
        using var batch = new WriteBatchWithIndex();
        batch.Put(Key, Pending);

        batch.Clear();

        await Assert.That(batch.Count()).IsEqualTo(0);
    }

    [Test]
    public async Task Get_ReadsBackAPendingWrite()
    {
        using var batch = new WriteBatchWithIndex();

        batch.Put(Key, Pending);

        await Assert.That(batch.Get(Key)).IsEquivalentTo(Pending, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Get_ForAKeyTheBatchDoesNotTouch_ReturnsNull()
    {
        using var batch = new WriteBatchWithIndex();

        await Assert.That(batch.Get(Key)).IsNull();
    }

    [Test]
    public async Task Get_OfAString_RoundTripsThroughTheEncoding()
    {
        using var batch = new WriteBatchWithIndex();

        batch.Put("kü", "vé", Encoding.Unicode);

        await Assert.That(batch.Get("kü", encoding: Encoding.Unicode)).IsEqualTo("vé");
    }

    [Test]
    public async Task Get_IntoABuffer_ReturnsTheNumberOfBytesCopied()
    {
        using var batch = new WriteBatchWithIndex();
        batch.Put(Key, Pending);
        var buffer = new byte[16];

        var copied = batch.Get(Key, buffer, offset: 0, length: 3);

        using (Assert.Multiple())
        {
            await Assert.That(copied).IsEqualTo(3ul);
            await Assert.That(buffer.AsSpan(0, 3).ToArray()).IsEquivalentTo("pen"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Get_IntoABuffer_ReturnsZeroForAKeyTheBatchDoesNotTouch()
    {
        using var batch = new WriteBatchWithIndex();

        await Assert.That(batch.Get(Key, new byte[16], 0ul, 16ul)).IsEqualTo(0ul);
    }

    [Test]
    public async Task GetFromBatchAndDb_FallsBackToTheStoredValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Stored);
        using var batch = new WriteBatchWithIndex();

        await Assert.That(batch.Get(database.Db, Key)).IsEquivalentTo(Stored, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetFromBatchAndDb_PrefersThePendingWrite()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Stored);
        using var batch = new WriteBatchWithIndex();

        batch.Put(Key, Pending);

        await Assert.That(batch.Get(database.Db, Key)).IsEquivalentTo(Pending, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetFromBatchAndDb_HonoursAPendingDelete()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Stored);
        using var batch = new WriteBatchWithIndex();

        batch.Delete(Key);

        await Assert.That(batch.Get(database.Db, Key)).IsNull();
    }

    [Test]
    public async Task GetFromBatchAndDb_OfAString_ReadsThroughToTheDatabase()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("key", "stored");
        using var batch = new WriteBatchWithIndex();

        await Assert.That(batch.Get(database.Db, "key")).IsEqualTo("stored");
    }

    [Test]
    public async Task Write_AppliesThePendingOperationsToTheDatabase()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("stale"u8.ToArray(), Stored);
        using var batch = new WriteBatchWithIndex();

        batch.Put(Key, Pending).Delete("stale"u8.ToArray());
        database.Db.Write(batch);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Pending, CollectionOrdering.Matching);
            await Assert.That(database.Db.Get("stale"u8.ToArray())).IsNull();
        }
    }

    [Test]
    public async Task AnOverlayIterator_MergesPendingWritesIntoTheStoredKeys()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "A");
        database.Db.Put("c", "C");
        using var batch = new WriteBatchWithIndex();

        batch.Put("b"u8.ToArray(), "B"u8.ToArray());

        await Assert.That(Overlay(database.Db, batch)).IsEquivalentTo(new[] { "a", "b", "c" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task AnOverlayIterator_HidesKeysThePatchDeletes()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "A");
        database.Db.Put("b", "B");
        using var batch = new WriteBatchWithIndex();

        batch.Delete("a"u8.ToArray());

        await Assert.That(Overlay(database.Db, batch)).IsEquivalentTo(new[] { "b" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task NewIterator_TakesOwnershipOfTheBaseIterator()
    {
        using var database = TestDatabase.Create();
        using var batch = new WriteBatchWithIndex();
        var baseIterator = database.Db.NewIterator();

        using var overlay = batch.NewIterator(baseIterator);

        await Assert.That(baseIterator.Handle).IsEqualTo(nint.Zero);
    }

    /// <remarks>
    /// Unlike <c>NewIterator</c>, this overload leaves the base iterator wrapper holding a handle
    /// rocksdb now owns, so the caller has to detach it to avoid destroying it twice.
    /// </remarks>
    [Test]
    public async Task CreateIteratorWithBase_ProducesAUsableOverlay()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "A");
        using var batch = new WriteBatchWithIndex();
        batch.Put("b"u8.ToArray(), "B"u8.ToArray());

        var baseIterator = database.Db.NewIterator();
        using var overlay = batch.CreateIteratorWithBase(baseIterator);
        baseIterator.Detach();

        await Assert.That(overlay.SeekToFirst().StringKey()).IsEqualTo("a");
    }

    [Test]
    public async Task ToBytes_ProducesARepresentationAPlainWriteBatchAccepts()
    {
        using var batch = new WriteBatchWithIndex();
        batch.Put(Key, Pending);

        using var plain = new WriteBatch(batch.ToBytes());

        await Assert.That(plain.Count()).IsEqualTo(1);
    }

    [Test]
    public async Task RollbackToSavePoint_DiscardsWhatCameAfterIt()
    {
        using var batch = new WriteBatchWithIndex();
        batch.Put(Key, Pending);
        batch.SetSavePoint();
        batch.Put("later"u8.ToArray(), Pending);

        batch.RollbackToSavePoint();

        using (Assert.Multiple())
        {
            await Assert.That(batch.Count()).IsEqualTo(1);
            await Assert.That(batch.Get("later"u8.ToArray())).IsNull();
        }
    }

    [Test]
    public async Task RollbackToSavePoint_WithoutASavePoint_Throws()
    {
        using var batch = new WriteBatchWithIndex();

        await Assert.That(batch.RollbackToSavePoint).Throws<RocksDbException>();
    }

    [Test]
    public async Task Merge_IsResolvedByTheDatabaseMergeOperator()
    {
        using var database = TestDatabase.Create(new DbOptions().SetCreateIfMissing().SetUint64addMergeOperator());
        database.Db.Merge("counter"u8.ToArray(), BitConverter.GetBytes(5ul));
        using var batch = new WriteBatchWithIndex();

        batch.Merge("counter"u8, BitConverter.GetBytes(7ul));
        database.Db.Write(batch);

        await Assert.That(BitConverter.ToUInt64(database.Db.Get("counter"u8.ToArray())!)).IsEqualTo(12ul);
    }

    [Test]
    public async Task OverwriteKeys_CollapsesRepeatedWritesOfTheSameKey()
    {
        using var batch = new WriteBatchWithIndex(overwriteKeys: true);

        batch.Put(Key, Stored);
        batch.Put(Key, Pending);

        await Assert.That(batch.Get(Key)).IsEquivalentTo(Pending, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        var batch = new WriteBatchWithIndex();

        batch.Dispose();

        await Assert.That(batch.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        var batch = new WriteBatchWithIndex();
        batch.Dispose();

        await Assert.That(batch.Dispose).ThrowsNothing();
    }
}
