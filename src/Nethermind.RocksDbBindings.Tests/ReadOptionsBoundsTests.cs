// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class ReadOptionsBoundsTests
{
    [Test]
    public async Task UpdateIterateBounds_ReseeksAcrossNarrowerAndWiderRanges()
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);

        options.UpdateIterateBounds("b"u8, "d"u8);
        await Assert.That(Keys(iterator, "b")).IsEquivalentTo(new[] { "b", "c" }, CollectionOrdering.Matching);

        options.UpdateIterateBounds("a"u8, "f"u8);
        await Assert.That(Keys(iterator, "a")).IsEquivalentTo(new[] { "a", "b", "c", "d", "e" }, CollectionOrdering.Matching);

        options.UpdateIterateBounds("c"u8, "d"u8);
        await Assert.That(Keys(iterator, "c")).IsEquivalentTo(new[] { "c" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task UpdateIterateBounds_PreservesCapacityAfterShorterBounds()
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);

        options.UpdateIterateBounds("a"u8, "b"u8);
        await Assert.That(Keys(iterator, "a")).IsEquivalentTo(new[] { "a" }, CollectionOrdering.Matching);

        options.UpdateIterateBounds("aa"u8, "ffff"u8);
        await Assert.That(Keys(iterator, "aa")).IsEquivalentTo(new[] { "b", "c", "d", "e" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task UpdateIterateBounds_ChangesBackwardIterationBounds()
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);

        options.UpdateIterateBounds("b"u8, "e"u8);
        await Assert.That(KeysBackward(iterator, "d")).IsEquivalentTo(new[] { "d", "c", "b" }, CollectionOrdering.Matching);

        options.UpdateIterateBounds("a"u8, "d"u8);
        await Assert.That(KeysBackward(iterator, "c")).IsEquivalentTo(new[] { "c", "b", "a" }, CollectionOrdering.Matching);
    }

    [Test]
    [Arguments("xxxxx", "d")]
    [Arguments("c", "zzzzz")]
    public async Task UpdateIterateBounds_OversizedInputLeavesBothBoundsUnchanged(string lower, string upper)
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);
        byte[] lowerBytes = Encoding.UTF8.GetBytes(lower);
        byte[] upperBytes = Encoding.UTF8.GetBytes(upper);

        await Assert.That(() => options.UpdateIterateBounds(lowerBytes, upperBytes)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(Keys(iterator, "a")).IsEquivalentTo(new[] { "a", "b", "c", "d", "e" }, CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(false, false)]
    [Arguments(true, false)]
    [Arguments(false, true)]
    public async Task UpdateIterateBounds_RequiresBothBounds(bool lowerConfigured, bool upperConfigured)
    {
        using ReadOptions options = new();
        if (lowerConfigured) options.SetIterateLowerBound("a"u8);
        if (upperConfigured) options.SetIterateUpperBound("z"u8);

        await Assert.That(() => options.UpdateIterateBounds("b"u8, "d"u8)).Throws<InvalidOperationException>();
    }

    [Test]
    public async Task UpdateIterateBounds_PreservesTheIteratorSnapshot()
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);
        database.Db.Put("f"u8, "F"u8);

        options.UpdateIterateBounds("f"u8, "g"u8);
        await Assert.That(Keys(iterator, "f")).IsEmpty();
        using Iterator current = database.Db.NewIterator(readOptions: options);
        await Assert.That(Keys(current, "f")).IsEquivalentTo(new[] { "f" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task UpdateIterateBounds_SharedOptionsRequireEachIteratorToReseek()
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        using Iterator first = database.Db.NewIterator(readOptions: options);
        using Iterator second = database.Db.NewIterator(readOptions: options);

        options.UpdateIterateBounds("c"u8, "e"u8);

        using (Assert.Multiple())
        {
            await Assert.That(Keys(first, "c")).IsEquivalentTo(new[] { "c", "d" }, CollectionOrdering.Matching);
            await Assert.That(Keys(second, "c")).IsEquivalentTo(new[] { "c", "d" }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task UpdateIterateBounds_DisposedOptionsKeepTheIteratorBoundsAlive()
    {
        using TestDatabase database = Alphabet();
        ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);
        options.UpdateIterateBounds("b"u8, "d"u8);
        options.Dispose();

        await Assert.That(Keys(iterator, "b")).IsEquivalentTo(new[] { "b", "c" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task UpdateIterateBounds_RejectsReleasedOptions()
    {
        ReadOptions options = Bounds();
        options.Dispose();

        await Assert.That(() => options.UpdateIterateBounds("a"u8, "f"u8)).Throws<ObjectDisposedException>();
    }

    [Test]
    public async Task UpdateIterateBounds_UsesTheReplacementBufferCapacity()
    {
        using TestDatabase database = Alphabet();
        using ReadOptions options = Bounds();
        options.SetIterateBounds("a"u8, "f"u8);

        await Assert.That(() => options.UpdateIterateBounds("aa"u8, "f"u8)).Throws<ArgumentOutOfRangeException>();
        await Assert.That(() => options.UpdateIterateBounds("a"u8, "ff"u8)).Throws<ArgumentOutOfRangeException>();

        options.SetIterateBounds("0000"u8, "zzzz"u8);
        using Iterator iterator = database.Db.NewIterator(readOptions: options);
        options.UpdateIterateBounds("aaaa"u8, "ffff"u8);
        await Assert.That(Keys(iterator, "aaaa")).IsEquivalentTo(new[] { "b", "c", "d", "e" }, CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(false, false, false)]
    [Arguments(false, false, true)]
    [Arguments(false, true, false)]
    [Arguments(false, true, true)]
    [Arguments(true, false, false)]
    [Arguments(true, false, true)]
    [Arguments(true, true, false)]
    [Arguments(true, true, true)]
    public async Task UpdateIterateBounds_RespectsPointAndRangeDeletes(bool rangeDelete, bool flushBefore, bool flushAfter)
    {
        using TestDatabase database = Alphabet();
        using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
        if (flushBefore) database.Db.Flush(flush);
        using (WriteBatch batch = new())
        {
            if (rangeDelete)
                batch.DeleteRange("b"u8, "d"u8);
            else
            {
                batch.Delete("b"u8);
                batch.Delete("c"u8);
            }
            database.Db.Write(batch);
        }
        if (flushAfter) database.Db.Flush(flush);

        using ReadOptions options = Bounds();
        using Iterator iterator = database.Db.NewIterator(readOptions: options);
        options.UpdateIterateBounds("b"u8, "d"u8);
        await Assert.That(Keys(iterator, "b")).IsEmpty();

        options.UpdateIterateBounds("a"u8, "f"u8);
        await Assert.That(Keys(iterator, "a")).IsEquivalentTo(new[] { "a", "d", "e" }, CollectionOrdering.Matching);
    }

    private static ReadOptions Bounds() => new ReadOptions().SetIterateBounds("0000"u8, "zzzz"u8);

    private static TestDatabase Alphabet()
    {
        TestDatabase database = TestDatabase.Create();
        foreach (string key in new[] { "a", "b", "c", "d", "e" })
            database.Db.Put(key, key.ToUpperInvariant());
        return database;
    }

    private static List<string> Keys(Iterator iterator, string lower)
    {
        List<string> keys = [];
        for (iterator.Seek(Encoding.UTF8.GetBytes(lower)); iterator.Valid(); iterator.Next())
            keys.Add(Encoding.UTF8.GetString(iterator.GetKeySpan()));
        iterator.ThrowIfError();
        return keys;
    }

    private static List<string> KeysBackward(Iterator iterator, string last)
    {
        List<string> keys = [];
        for (iterator.SeekForPrev(Encoding.UTF8.GetBytes(last)); iterator.Valid(); iterator.Prev())
            keys.Add(Encoding.UTF8.GetString(iterator.GetKeySpan()));
        iterator.ThrowIfError();
        return keys;
    }
}
