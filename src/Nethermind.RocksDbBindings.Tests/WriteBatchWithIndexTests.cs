// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;
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
    public async Task Get_OfAString_RoundTripsThroughUtf8()
    {
        using var batch = new WriteBatchWithIndex();

        batch.Put("kü", "vé");

        await Assert.That(batch.Get("kü")).IsEqualTo("vé");
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
    /// The overlay must root the base iterator's read options: the native base iterator keeps
    /// reading the iterate-bound buffers those options own, so if only the discarded base
    /// wrapper held them, finalization would free memory still in use.
    /// </remarks>
    [Test]
    public async Task NewIterator_KeepsTheBaseReadOptionsAliveForTheOverlay()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "1");
        database.Db.Put("b", "2");
        database.Db.Put("d", "4");
        using var batch = new WriteBatchWithIndex();
        batch.Put("c"u8.ToArray(), "3"u8.ToArray());

        var (overlay, weakOptions) = CreateBoundedOverlay(database.Db, batch);
        using var overlayLifetime = overlay;

        // The bounded ReadOptions wrapper is now unreachable except through the overlay.
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // The weak reference is the deterministic proof, and it must come before touching the
        // iterator: on a regression the bounds are already freed, and a collected wrapper cannot
        // resolve, whereas freed native memory could still happen to hold the old bound bytes.
        await Assert.That(weakOptions.TryGetTarget(out _)).IsTrue();

        var keys = new List<string>();
        for (overlay.SeekToFirst(); overlay.Valid(); overlay.Next())
            keys.Add(overlay.StringKey());

        await Assert.That(keys).IsEquivalentTo(new[] { "b", "c" }, CollectionOrdering.Matching);

        // Not inlined so the caller never roots the base wrapper or its read options; the options
        // are deliberately not disposed because their finalization is what is being exercised.
        [MethodImpl(MethodImplOptions.NoInlining)]
        static (Iterator Overlay, WeakReference<ReadOptions> Options) CreateBoundedOverlay(RocksDb db, WriteBatchWithIndex batch)
        {
            var options = new ReadOptions().SetIterateBounds("b"u8, "d"u8);
            return (batch.NewIterator(db.NewIterator(readOptions: options)), new WeakReference<ReadOptions>(options));
        }
    }

    [Test]
    public async Task CreateIteratorWithBase_ProducesAUsableOverlay()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "A");
        using var batch = new WriteBatchWithIndex();
        batch.Put("b"u8.ToArray(), "B"u8.ToArray());

        var baseIterator = database.Db.NewIterator();
        // CreateIteratorWithBase takes ownership of the base iterator.
        using var overlay = batch.CreateIteratorWithBase(baseIterator);

        await Assert.That(overlay.SeekToFirst().StringKey()).IsEqualTo("a");
    }

    [Test]
    public async Task ToBytes_ProducesARepresentationAPlainWriteBatchAccepts()
    {
        using var batch = new WriteBatchWithIndex();
        batch.Put(Key, Pending);

        using var plain = WriteBatch.FromSpan(batch.ToBytes());

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
