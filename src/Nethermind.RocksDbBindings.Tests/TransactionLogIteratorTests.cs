// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class TransactionLogIteratorTests
{
    [Test]
    public async Task GetUpdatesSince_ReportsTheSequenceNumberOfEachBatch()
    {
        using var primary = TestDatabase.Create();
        primary.Db.Put("first", "1");
        primary.Db.Put("second", "2");

        var sequenceNumbers = new List<ulong>();
        var batchSizes = new List<int>();

        using (var iterator = primary.Db.GetUpdatesSince(0))
        {
            await Assert.That(iterator.Status).ThrowsNothing();

            for (; iterator.Valid(); iterator.Next())
            {
                using var batch = iterator.GetBatch(out var sequenceNumber);
                sequenceNumbers.Add(sequenceNumber);
                batchSizes.Add(batch.Count());
            }
        }

        // One batch per write, asserted before the reads below index into them, so a change in
        // batching is reported as such rather than as an index error.
        await Assert.That(batchSizes).IsEquivalentTo(new[] { 1, 1 }, CollectionOrdering.Matching);

        // Each batch is numbered after the one before it.
        using (Assert.Multiple())
        {
            await Assert.That(sequenceNumbers[0]).IsGreaterThan(0ul);
            await Assert.That(sequenceNumbers[1]).IsGreaterThan(sequenceNumbers[0]);
        }
    }

    [Test]
    public async Task GetUpdatesSince_OnAnUntouchedDatabase_HasNothingToReplay()
    {
        using var primary = TestDatabase.Create();

        using var iterator = primary.Db.GetUpdatesSince(0);

        await Assert.That(iterator.Valid()).IsFalse();
    }

    [Test]
    public async Task GetUpdatesSince_ReplayedOnAnEmptyDatabase_ReproducesThePrimary()
    {
        using var primary = TestDatabase.Create();
        primary.Db.Put("key", "value");
        using var replica = TestDatabase.Create();

        using (var iterator = primary.Db.GetUpdatesSince(0))
        {
            for (; iterator.Valid(); iterator.Next())
            {
                using var batch = iterator.GetBatch(out _);
                using var replayed = WriteBatch.FromSpan(batch.ToBytes());
                replica.Db.Write(replayed);
            }
        }

        await Assert.That(replica.Db.Get("key")).IsEqualTo("value");
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        using var primary = TestDatabase.Create();
        primary.Db.Put("key", "value");
        var iterator = primary.Db.GetUpdatesSince(0);

        iterator.Dispose();

        await Assert.That(iterator.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        using var primary = TestDatabase.Create();
        var iterator = primary.Db.GetUpdatesSince(0);
        iterator.Dispose();

        await Assert.That(iterator.Dispose).ThrowsNothing();
    }
}
