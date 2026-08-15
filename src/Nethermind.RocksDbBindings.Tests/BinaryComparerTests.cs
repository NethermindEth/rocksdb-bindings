// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class BinaryComparerTests
{
    private static readonly BinaryComparer Comparer = BinaryComparer.Default;

    [Test]
    public async Task Compare_TwoNulls_AreEqual()
        => await Assert.That(Comparer.Compare(null, null)).IsEqualTo(0);

    [Test]
    public async Task Compare_NullSortsBeforeAnyArray()
        => await Assert.That(Comparer.Compare(null, [])).IsLessThan(0);

    [Test]
    public async Task Compare_AnyArraySortsAfterNull()
        => await Assert.That(Comparer.Compare([], null)).IsGreaterThan(0);

    [Test]
    public async Task Compare_OrdersByFirstDifferingByte()
        => await Assert.That(Comparer.Compare([1, 2, 9], [1, 3, 0])).IsLessThan(0);

    [Test]
    public async Task Compare_TreatsBytesAsUnsigned()
        => await Assert.That(Comparer.Compare([0x7f], [0x80])).IsLessThan(0);

    [Test]
    public async Task Compare_ShorterPrefixSortsFirst()
        => await Assert.That(Comparer.Compare([1, 2], [1, 2, 0])).IsLessThan(0);

    [Test]
    public async Task Compare_IdenticalContent_AreEqual()
        => await Assert.That(Comparer.Compare([1, 2, 3], [1, 2, 3])).IsEqualTo(0);

    [Test]
    public async Task Equals_SameInstance_IsTrue()
    {
        byte[] value = [1, 2, 3];
        await Assert.That(Comparer.Equals(value, value)).IsTrue();
    }

    [Test]
    public async Task Equals_TwoNulls_IsTrue()
        => await Assert.That(Comparer.Equals(null, null)).IsTrue();

    [Test]
    public async Task Equals_OnlyOneNull_IsFalse()
        => await Assert.That(Comparer.Equals(null, [1])).IsFalse();

    [Test]
    public async Task Equals_EqualContentInDistinctArrays_IsTrue()
        => await Assert.That(Comparer.Equals([1, 2, 3], [1, 2, 3])).IsTrue();

    [Test]
    public async Task Equals_DifferentLengths_IsFalse()
        => await Assert.That(Comparer.Equals([1, 2], [1, 2, 3])).IsFalse();

    [Test]
    public async Task PrefixEquals_IgnoresBytesBeyondThePrefix()
        => await Assert.That(Comparer.PrefixEquals([1, 2, 9], [1, 2, 8], prefix: 2)).IsTrue();

    [Test]
    public async Task PrefixEquals_ComparesBytesInsideThePrefix()
        => await Assert.That(Comparer.PrefixEquals([1, 2, 9], [1, 2, 8], prefix: 3)).IsFalse();

    [Test]
    public async Task PrefixEquals_PrefixLongerThanBothArrays_ComparesWholeArrays()
        => await Assert.That(Comparer.PrefixEquals([1, 2], [1, 2], prefix: 99)).IsTrue();

    /// <remarks>
    /// The prefix is clamped to each array separately, so an oversized prefix ends up comparing
    /// two spans of different lengths and they can never match.
    /// </remarks>
    [Test]
    public async Task PrefixEquals_PrefixLongerThanOneArray_IsFalse()
        => await Assert.That(Comparer.PrefixEquals([1, 2], [1, 2, 3], prefix: 99)).IsFalse();

    [Test]
    public async Task PrefixEquals_SameInstance_IsTrueRegardlessOfPrefix()
    {
        byte[] value = [1, 2, 3];
        await Assert.That(Comparer.PrefixEquals(value, value, prefix: 99)).IsTrue();
    }

    [Test]
    public async Task GetHashCode_EqualContentInDistinctArrays_Matches()
        => await Assert.That(Comparer.GetHashCode([1, 2, 3])).IsEqualTo(Comparer.GetHashCode([1, 2, 3]));

    [Test]
    public async Task GetHashCode_EmptyArrays_Match()
        => await Assert.That(Comparer.GetHashCode([])).IsEqualTo(Comparer.GetHashCode([]));

    [Test]
    public async Task Default_IsASingleton()
        => await Assert.That(BinaryComparer.Default).IsSameReferenceAs(BinaryComparer.Default);
}
