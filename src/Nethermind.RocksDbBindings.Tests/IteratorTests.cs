// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

using Nethermind.RocksDbBindings.Native;

namespace Nethermind.RocksDbBindings.Tests;

public class IteratorTests
{
    private sealed class Utf8Deserializer : ISpanDeserializer<string>
    {
        public string Deserialize(ReadOnlySpan<byte> buffer) => Encoding.UTF8.GetString(buffer);
    }

    /// <summary>A database holding the keys <c>a</c> to <c>e</c>, each mapped to its upper case form.</summary>
    private static TestDatabase Alphabet()
    {
        var database = TestDatabase.Create();

        foreach (var key in new[] { "a", "b", "c", "d", "e" })
            database.Db.Put(key, key.ToUpperInvariant());

        return database;
    }

    private static List<string> KeysFromFirst(RocksDb db, ReadOptions? readOptions = null)
    {
        using var iterator = db.NewIterator(readOptions: readOptions);
        var keys = new List<string>();

        for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
            keys.Add(iterator.StringKey());

        return keys;
    }

    private static List<string> KeysFromLast(RocksDb db)
    {
        using var iterator = db.NewIterator();
        var keys = new List<string>();

        for (iterator.SeekToLast(); iterator.Valid(); iterator.Prev())
            keys.Add(iterator.StringKey());

        return keys;
    }

    private static byte[] KeySpan(Iterator iterator) => iterator.GetKeySpan().ToArray();

    private static byte[] ValueSpan(Iterator iterator) => iterator.GetValueSpan().ToArray();

    private static unsafe void Destroy(nint iteratorHandle)
        => RocksDbNative.rocksdb_iter_destroy((rocksdb_iterator_t*)iteratorHandle);

    [Test]
    public async Task SeekToFirst_ThenNext_WalksTheKeysInOrder()
    {
        using var database = Alphabet();

        await Assert.That(KeysFromFirst(database.Db)).IsEquivalentTo(new[] { "a", "b", "c", "d", "e" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task SeekToLast_ThenPrev_WalksTheKeysBackwards()
    {
        using var database = Alphabet();

        await Assert.That(KeysFromLast(database.Db)).IsEquivalentTo(new[] { "e", "d", "c", "b", "a" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task AnIteratorOverAnEmptyDatabase_IsNeverValid()
    {
        using var database = TestDatabase.Create();
        using var iterator = database.Db.NewIterator();

        await Assert.That(iterator.SeekToFirst().Valid()).IsFalse();
    }

    [Test]
    public async Task Seek_LandsOnAnExactMatch()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("c");

        await Assert.That(iterator.StringKey()).IsEqualTo("c");
    }

    [Test]
    public async Task Seek_LandsOnTheFirstKeyAtOrAfterTheTarget()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("bb");

        await Assert.That(iterator.StringKey()).IsEqualTo("c");
    }

    [Test]
    public async Task Seek_PastTheLastKey_IsNotValid()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("z");

        await Assert.That(iterator.Valid()).IsFalse();
    }

    [Test]
    public async Task Seek_AcceptsByteArrayKeys()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("c"u8.ToArray());

        await Assert.That(iterator.StringKey()).IsEqualTo("c");
    }

    [Test]
    public async Task Seek_AcceptsSpanKeys()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("c"u8);

        await Assert.That(iterator.StringKey()).IsEqualTo("c");
    }

    [Test]
    public async Task Seek_HonoursAnExplicitKeyLength()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("cx"u8.ToArray(), klen: 1);

        await Assert.That(iterator.StringKey()).IsEqualTo("c");
    }

    [Test]
    public async Task SeekForPrev_LandsOnTheLastKeyAtOrBeforeTheTarget()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.SeekForPrev("bb");

        await Assert.That(iterator.StringKey()).IsEqualTo("b");
    }

    [Test]
    public async Task SeekForPrev_BeforeTheFirstKey_IsNotValid()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.SeekForPrev("A");

        await Assert.That(iterator.Valid()).IsFalse();
    }

    [Test]
    public async Task SeekForPrev_AcceptsByteArrayKeys()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.SeekForPrev("bb"u8.ToArray());

        await Assert.That(iterator.StringKey()).IsEqualTo("b");
    }

    [Test]
    public async Task SeekForPrev_AcceptsSpanKeys()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.SeekForPrev("bb"u8);

        await Assert.That(iterator.StringKey()).IsEqualTo("b");
    }

    [Test]
    public async Task SeekMethods_AreFluent()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        await Assert.That(iterator.SeekToFirst().Next().Prev()).IsSameReferenceAs(iterator);
    }

    [Test]
    public async Task KeyAndValue_ReturnTheRawBytes()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("c");

        using (Assert.Multiple())
        {
            await Assert.That(iterator.Key()).IsEquivalentTo("c"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(iterator.Value()).IsEquivalentTo("C"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task StringKeyAndStringValue_DecodeAsUtf8()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("kü", "vé");
        using var iterator = database.Db.NewIterator();

        iterator.SeekToFirst();

        using (Assert.Multiple())
        {
            await Assert.That(iterator.StringKey()).IsEqualTo("kü");
            await Assert.That(iterator.StringValue()).IsEqualTo("vé");
        }
    }

    [Test]
    public async Task GetKeySpanAndGetValueSpan_ViewTheSameBytes()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("c");

        using (Assert.Multiple())
        {
            await Assert.That(KeySpan(iterator)).IsEquivalentTo("c"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(ValueSpan(iterator)).IsEquivalentTo("C"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task KeyAndValue_AcceptASpanDeserializer()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();
        var deserializer = new Utf8Deserializer();

        iterator.Seek("c");

        using (Assert.Multiple())
        {
            await Assert.That(iterator.Key(deserializer)).IsEqualTo("c");
            await Assert.That(iterator.Value(deserializer)).IsEqualTo("C");
        }
    }

    [Test]
    public async Task KeyAndValue_AcceptAStreamDeserializer()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();

        iterator.Seek("c");

        using (Assert.Multiple())
        {
            await Assert.That(iterator.Key(stream => new StreamReader(stream).ReadToEnd())).IsEqualTo("c");
            await Assert.That(iterator.Value(stream => new StreamReader(stream).ReadToEnd())).IsEqualTo("C");
        }
    }

    [Test]
    public async Task IterateUpperBound_StopsBeforeTheBound()
    {
        using var database = Alphabet();
        var readOptions = new ReadOptions().SetIterateUpperBound("c"u8.ToArray());

        await Assert.That(KeysFromFirst(database.Db, readOptions)).IsEquivalentTo(new[] { "a", "b" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task IterateLowerBound_StartsAtTheBound()
    {
        using var database = Alphabet();
        var readOptions = new ReadOptions().SetIterateLowerBound("c"u8.ToArray());

        await Assert.That(KeysFromFirst(database.Db, readOptions)).IsEquivalentTo(new[] { "c", "d", "e" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task IterateBounds_CanBeGivenAsStrings()
    {
        using var database = Alphabet();
        var readOptions = new ReadOptions().SetIterateLowerBound("b").SetIterateUpperBound("d");

        await Assert.That(KeysFromFirst(database.Db, readOptions)).IsEquivalentTo(new[] { "b", "c" }, CollectionOrdering.Matching);
    }

    /// <remarks>
    /// The bound is copied into memory the read options own, so replacing it must not leave the
    /// native side pointing at the previous copy.
    /// </remarks>
    [Test]
    public async Task ReplacingAnIterateBound_TakesEffect()
    {
        using var database = Alphabet();
        var readOptions = new ReadOptions().SetIterateUpperBound("b"u8.ToArray());

        readOptions.SetIterateUpperBound("d"u8.ToArray());

        await Assert.That(KeysFromFirst(database.Db, readOptions)).IsEquivalentTo(new[] { "a", "b", "c" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task AnIterator_DoesNotSeeWritesMadeAfterItWasCreated()
    {
        using var database = Alphabet();
        using var iterator = database.Db.NewIterator();
        iterator.SeekToFirst();

        database.Db.Put("f", "F");

        var keys = new List<string>();
        for (; iterator.Valid(); iterator.Next())
            keys.Add(iterator.StringKey());

        await Assert.That(keys).IsEquivalentTo(new[] { "a", "b", "c", "d", "e" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task NewIterators_ReturnsOneIteratorPerColumnFamily()
    {
        using var database = TestDatabase.Create(
            new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies(),
            BuildFamilies());

        var blocks = database.Db.GetColumnFamily("blocks");
        var receipts = database.Db.GetColumnFamily("receipts");
        database.Db.Put("b"u8.ToArray(), "in blocks"u8.ToArray(), blocks);
        database.Db.Put("r"u8.ToArray(), "in receipts"u8.ToArray(), receipts);

        var iterators = database.Db.NewIterators([blocks, receipts]);

        try
        {
            using (Assert.Multiple())
            {
                await Assert.That(iterators.Length).IsEqualTo(2);
                await Assert.That(iterators[0].SeekToFirst().StringKey()).IsEqualTo("b");
                await Assert.That(iterators[1].SeekToFirst().StringKey()).IsEqualTo("r");
            }
        }
        finally
        {
            foreach (var iterator in iterators)
                iterator.Dispose();
        }

        static ColumnFamilies BuildFamilies()
        {
            var families = new ColumnFamilies();
            families.Add("blocks", new ColumnFamilyOptions());
            families.Add("receipts", new ColumnFamilyOptions());
            return families;
        }
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        using var database = Alphabet();
        var iterator = database.Db.NewIterator();

        iterator.Dispose();

        await Assert.That(iterator.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        using var database = Alphabet();
        var iterator = database.Db.NewIterator();
        iterator.Dispose();

        await Assert.That(iterator.Dispose).ThrowsNothing();
    }

    /// <remarks>
    /// Detaching hands ownership of the native iterator to the caller, so disposing the wrapper
    /// afterwards must not destroy it a second time.
    /// </remarks>
    [Test]
    public async Task Detach_GivesUpTheHandleWithoutDestroyingIt()
    {
        using var database = Alphabet();
        var iterator = database.Db.NewIterator();

        var detached = iterator.Detach();
        iterator.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(detached).IsNotEqualTo(nint.Zero);
            await Assert.That(iterator.Handle).IsEqualTo(nint.Zero);
        }

        Destroy(detached);
    }
}
