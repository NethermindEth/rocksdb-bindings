// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class MergeOperatorTests
{
    /// <summary>What the merge callbacks saw, so a test can assert on the arguments rocksdb passed.</summary>
    private sealed class MergeLog
    {
        public int FullMergeCalls { get; set; }
        public bool? HadExistingValue { get; set; }
        public int LastOperandCount { get; set; }
    }

    /// <summary>A merge operator that appends every operand to whatever is already stored.</summary>
    private static IMergeOperator Concatenating(MergeLog? log = null) => MergeOperators.Create(
        "concatenating",
        (ReadOnlySpan<byte> key, MergeOperators.OperandsEnumerator operands, out bool success) =>
        {
            success = true;
            return Concatenate(default, operands);
        },
        (ReadOnlySpan<byte> key, bool hasExistingValue, ReadOnlySpan<byte> existingValue, MergeOperators.OperandsEnumerator operands, out bool success) =>
        {
            if (log is not null)
            {
                log.FullMergeCalls++;
                log.HadExistingValue = hasExistingValue;
                log.LastOperandCount = operands.Count;
            }

            success = true;
            return Concatenate(existingValue, operands);
        });

    private static byte[] Concatenate(ReadOnlySpan<byte> prefix, MergeOperators.OperandsEnumerator operands)
    {
        var result = new List<byte>(prefix.ToArray());

        for (var i = 0; i < operands.Count; i++)
            result.AddRange(operands.Get(i).ToArray());

        return [.. result];
    }

    private static TestDatabase Merging(IMergeOperator mergeOperator)
        => TestDatabase.Create(new DbOptions().SetCreateIfMissing().SetMergeOperator(mergeOperator));

    private static unsafe (int Count, string First, string Second) ReadOperands()
    {
        var first = "aa"u8.ToArray();
        var second = "bbb"u8.ToArray();

        fixed (byte* firstPtr = first)
        fixed (byte* secondPtr = second)
        {
            ReadOnlySpan<nint> pointers = [(nint)firstPtr, (nint)secondPtr];
            ReadOnlySpan<long> lengths = [first.Length, second.Length];
            var operands = new MergeOperators.OperandsEnumerator(pointers, lengths);

            return (
                operands.Count,
                Encoding.UTF8.GetString(operands.Get(0)),
                Encoding.UTF8.GetString(operands.Get(1)));
        }
    }

    [Test]
    public async Task OperandsEnumerator_ExposesEachOperandByIndex()
    {
        var (count, first, second) = ReadOperands();

        using (Assert.Multiple())
        {
            await Assert.That(count).IsEqualTo(2);
            await Assert.That(first).IsEqualTo("aa");
            await Assert.That(second).IsEqualTo("bbb");
        }
    }

    [Test]
    public async Task Create_KeepsTheNameItWasGiven()
        => await Assert.That(Concatenating().Name).IsEqualTo("concatenating");

    [Test]
    public async Task Merge_AppendsToAMissingKey()
    {
        using var database = Merging(Concatenating());

        database.Db.Merge("key"u8.ToArray(), "hello"u8.ToArray());
        database.Db.Merge("key"u8.ToArray(), "world"u8.ToArray());

        await Assert.That(database.Db.Get("key"u8.ToArray())).IsEquivalentTo("helloworld"u8.ToArray(), CollectionOrdering.Matching);
    }

    /// <remarks>
    /// The string overload has to reach <c>rocksdb_merge</c> like every other one. When it wrote
    /// through <c>rocksdb_put</c> instead, the merge operator never ran and the last value simply
    /// replaced the earlier ones.
    /// </remarks>
    [Test]
    public async Task Merge_OfStrings_ReachesTheMergeOperator()
    {
        using var database = Merging(Concatenating());

        database.Db.Merge("key", "hello");
        database.Db.Merge("key", "world");

        await Assert.That(database.Db.Get("key")).IsEqualTo("helloworld");
    }

    [Test]
    public async Task Merge_OfSpans_ReachesTheMergeOperator()
    {
        using var database = Merging(Concatenating());

        database.Db.Merge("key"u8, "hello"u8);
        database.Db.Merge("key"u8, "world"u8);

        await Assert.That(database.Db.Get("key"u8)).IsEquivalentTo("helloworld"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Merge_WithSlicedSpans_MergesOnlyThoseBytes()
    {
        using var database = Merging(Concatenating());

        database.Db.Merge("keyX"u8[..3], "helloX"u8[..5]);

        await Assert.That(database.Db.Get("key"u8.ToArray())).IsEquivalentTo("hello"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Merge_InAWriteBatch_ReachesTheMergeOperator()
    {
        using var database = Merging(Concatenating());
        database.Db.Merge("key"u8.ToArray(), "hello"u8.ToArray());

        using var batch = new WriteBatch();
        batch.Merge("key"u8, "world"u8);
        database.Db.Write(batch);

        await Assert.That(database.Db.Get("key"u8.ToArray())).IsEquivalentTo("helloworld"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Merge_OverAStoredValue_SeesItAsTheExistingValue()
    {
        var log = new MergeLog();
        using var database = Merging(Concatenating(log));
        database.Db.Put("key"u8.ToArray(), "base"u8.ToArray());

        database.Db.Merge("key"u8.ToArray(), "-more"u8.ToArray());
        var value = database.Db.Get("key"u8.ToArray());

        using (Assert.Multiple())
        {
            await Assert.That(value).IsEquivalentTo("base-more"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(log.HadExistingValue).IsTrue();
            await Assert.That(log.LastOperandCount).IsEqualTo(1);
        }
    }

    [Test]
    public async Task Merge_OverAMissingKey_ReportsNoExistingValue()
    {
        var log = new MergeLog();
        using var database = Merging(Concatenating(log));

        database.Db.Merge("key"u8.ToArray(), "only"u8.ToArray());
        var value = database.Db.Get("key"u8.ToArray());

        using (Assert.Multiple())
        {
            await Assert.That(value).IsEquivalentTo("only"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(log.HadExistingValue).IsFalse();
        }
    }

    /// <remarks>
    /// A stored empty value is still an existing value, and it arrives as an empty span just like
    /// a missing one does. Only the flag tells the two apart.
    /// </remarks>
    [Test]
    public async Task Merge_OverAnEmptyStoredValue_StillReportsAnExistingValue()
    {
        var log = new MergeLog();
        using var database = Merging(Concatenating(log));
        database.Db.Put("key"u8.ToArray(), Array.Empty<byte>());

        database.Db.Merge("key"u8.ToArray(), "added"u8.ToArray());
        var value = database.Db.Get("key"u8.ToArray());

        using (Assert.Multiple())
        {
            await Assert.That(value).IsEquivalentTo("added"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(log.HadExistingValue).IsTrue();
        }
    }

    [Test]
    public async Task FullMerge_IsNotCalledUntilTheValueIsRead()
    {
        var log = new MergeLog();
        using var database = Merging(Concatenating(log));

        database.Db.Merge("key"u8.ToArray(), "hello"u8.ToArray());

        await Assert.That(log.FullMergeCalls).IsEqualTo(0);
    }

    [Test]
    public async Task Uint64AddMergeOperator_SumsLittleEndianCounters()
    {
        using var database = TestDatabase.Create(new DbOptions().SetCreateIfMissing().SetUint64addMergeOperator());

        database.Db.Merge("counter"u8.ToArray(), BitConverter.GetBytes(5ul));
        database.Db.Merge("counter"u8.ToArray(), BitConverter.GetBytes(7ul));

        await Assert.That(BitConverter.ToUInt64(database.Db.Get("counter"u8.ToArray())!)).IsEqualTo(12ul);
    }

    [Test]
    public async Task Merge_WithoutAMergeOperator_Fails()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.Merge("key"u8.ToArray(), "value"u8.ToArray()))
            .Throws<RocksDbException>();
    }

    [Test]
    public async Task MergedValues_SurviveAFlushAndCompaction()
    {
        using var database = Merging(Concatenating());
        database.Db.Merge("key"u8.ToArray(), "hello"u8.ToArray());
        database.Db.Merge("key"u8.ToArray(), "world"u8.ToArray());

        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));
        database.Db.CompactRange((byte[]?)null, null);

        await Assert.That(database.Db.Get("key"u8.ToArray())).IsEquivalentTo("helloworld"u8.ToArray(), CollectionOrdering.Matching);
    }
}
