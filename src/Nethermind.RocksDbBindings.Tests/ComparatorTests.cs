// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class ComparatorTests
{
    /// <summary>Orders keys back to front, so a database using it iterates in descending order.</summary>
    private sealed class DescendingComparator : IComparator
    {
        public string Name => "descending";

        public unsafe int Compare(nint a, nuint alen, nint b, nuint blen)
            => new ReadOnlySpan<byte>((void*)b, (int)blen).SequenceCompareTo(new ReadOnlySpan<byte>((void*)a, (int)alen));
    }

    private sealed class DescendingStringComparator : StringComparatorBase
    {
        public override int Compare(string a, string b) => string.CompareOrdinal(b, a);
    }

    private static List<string> Keys(RocksDb db)
    {
        using var iterator = db.NewIterator();
        var keys = new List<string>();

        for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
            keys.Add(iterator.StringKey());

        return keys;
    }

    private static TestDatabase Ordered(IComparator comparator)
    {
        var database = TestDatabase.Create(new DbOptions().SetCreateIfMissing().SetComparator(comparator));

        foreach (var key in new[] { "a", "b", "c" })
            database.Db.Put(key, key);

        return database;
    }

    /// <summary>Calls the native-facing overload the way rocksdb does, with raw pointers.</summary>
    private static unsafe int CompareEncoded(StringComparatorBase comparator, string a, string b)
    {
        var left = comparator.Encoding.GetBytes(a);
        var right = comparator.Encoding.GetBytes(b);

        fixed (byte* leftPtr = left)
        fixed (byte* rightPtr = right)
        {
            return comparator.Compare((nint)leftPtr, (nuint)left.Length, (nint)rightPtr, (nuint)right.Length);
        }
    }

    [Test]
    public async Task ACustomComparator_DrivesTheIterationOrder()
    {
        using var database = Ordered(new DescendingComparator());

        await Assert.That(Keys(database.Db)).IsEquivalentTo(new[] { "c", "b", "a" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ACustomComparator_DrivesSeekToo()
    {
        using var database = Ordered(new DescendingComparator());
        using var iterator = database.Db.NewIterator();

        // Under a descending order the key after "c" is "b".
        iterator.Seek("c");

        await Assert.That(iterator.Next().StringKey()).IsEqualTo("b");
    }

    [Test]
    public async Task ACustomComparator_LeavesPointLookupsWorking()
    {
        using var database = Ordered(new DescendingComparator());

        await Assert.That(database.Db.Get("b")).IsEqualTo("b");
    }

    [Test]
    public async Task AStringComparatorSubclass_DrivesTheIterationOrder()
    {
        using var database = Ordered(new DescendingStringComparator());

        await Assert.That(Keys(database.Db)).IsEquivalentTo(new[] { "c", "b", "a" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ReopeningWithADifferentComparator_Fails()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(new DbOptions().SetCreateIfMissing().SetComparator(new DescendingComparator()), path))
            db.Put("a", "a");

        await Assert.That(() => RocksDb.Open(new DbOptions(), path)).Throws<RocksDbException>();
    }

    /// <remarks>
    /// "z" precedes "ä" by code point but follows it under every culture-aware collation, so this
    /// pair distinguishes an ordinal ordering from one that depends on where the process runs.
    /// </remarks>
    [Test]
    public async Task StringComparator_DefaultsToOrdinalOrder()
        => await Assert.That(new StringComparator("ordinal").Compare("z", "ä")).IsLessThan(0);

    [Test]
    public async Task StringComparator_UsesTheComparerItWasGiven()
        => await Assert.That(new StringComparator("ci", StringComparer.OrdinalIgnoreCase).Compare("A", "a")).IsEqualTo(0);

    [Test]
    public async Task StringComparator_CanIgnoreCase()
        => await Assert.That(new StringComparator("ci", ignoreCase: true).Compare("A", "a")).IsEqualTo(0);

    [Test]
    public async Task StringComparator_IsCaseSensitiveWhenAskedNotToIgnoreCase()
        => await Assert.That(new StringComparator("cs", ignoreCase: false).Compare("A", "a")).IsNotEqualTo(0);

    /// <remarks>
    /// The case-insensitive shorthand has to stay culture independent too, or it reintroduces the
    /// machine-dependent ordering the ordinal default exists to avoid. Erasing case does not erase
    /// the difference: culture-aware collation still orders "ä" before "z".
    /// </remarks>
    [Test]
    public async Task StringComparator_IgnoringCase_IsStillOrdinal()
        => await Assert.That(new StringComparator("ci", ignoreCase: true).Compare("z", "ä")).IsLessThan(0);

    [Test]
    public async Task StringComparator_DefaultsToUtf8()
        => await Assert.That(new StringComparator("x").Encoding).IsEqualTo(Encoding.UTF8);

    [Test]
    public async Task StringComparator_UsesTheEncodingItWasGivenToDecodeKeys()
    {
        var comparator = new StringComparator("uni", StringComparer.Ordinal, Encoding.Unicode);

        // Decoded as UTF-8 these bytes would not compare as the strings they stand for.
        await Assert.That(CompareEncoded(comparator, "ä", "ö")).IsLessThan(0);
    }

    [Test]
    public async Task StringComparator_KeepsTheNameItWasGiven()
        => await Assert.That(new StringComparator("my-comparator").Name).IsEqualTo("my-comparator");

    [Test]
    [Arguments(null)]
    [Arguments("")]
    public async Task StringComparator_RejectsAnUnusableName(string? name)
        => await Assert.That(() => new StringComparator(name!)).Throws<ArgumentException>();

    [Test]
    public async Task AStringComparatorSubclass_WithoutAName_IsNamedAfterItself()
        => await Assert.That(new DescendingStringComparator().Name).IsEqualTo(nameof(DescendingStringComparator));

    /// <remarks>
    /// RocksDB tells orderings apart by this name alone, so two comparators that sort keys
    /// differently must never report the same one.
    /// </remarks>
    [Test]
    public async Task ComparatorsOfDifferentTypes_DoNotShareAName()
        => await Assert.That(new DescendingStringComparator().Name).IsNotEqualTo(new StringComparator("ordinal").Name);

    /// <remarks>
    /// A database created under one ordering must not open under another, which only works if
    /// the two comparators report different names.
    /// </remarks>
    [Test]
    public async Task ReopeningUnderADifferentlyOrderedComparatorOfAnotherType_Fails()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(new DbOptions().SetCreateIfMissing().SetComparator(new DescendingStringComparator()), path))
            db.Put("a", "a");

        await Assert.That(() => RocksDb.Open(new DbOptions().SetComparator(new StringComparator("ordinal")), path))
            .Throws<RocksDbException>();
    }

    /// <remarks>
    /// The reason the name is a required argument: two <see cref="StringComparator" /> instances
    /// differ only in the comparer they were handed, so without distinct names rocksdb would
    /// accept this reopen and read every key back in the wrong order.
    /// </remarks>
    [Test]
    public async Task ReopeningUnderADifferentlyConfiguredStringComparator_Fails()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(
            new DbOptions().SetCreateIfMissing().SetComparator(new StringComparator("ordinal", StringComparer.Ordinal)),
            path))
        {
            db.Put("a", "a");
        }

        await Assert.That(() => RocksDb.Open(
                new DbOptions().SetComparator(new StringComparator("ordinal-ignore-case", StringComparer.OrdinalIgnoreCase)),
                path))
            .Throws<RocksDbException>();
    }

    [Test]
    public async Task ReopeningUnderTheSameComparator_Succeeds()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(
            new DbOptions().SetCreateIfMissing().SetComparator(new StringComparator("ordinal")),
            path))
        {
            db.Put("a", "a");
        }

        using var reopened = RocksDb.Open(new DbOptions().SetComparator(new StringComparator("ordinal")), path);

        await Assert.That(reopened.Get("a")).IsEqualTo("a");
    }
}
