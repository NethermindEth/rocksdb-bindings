// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class ReplicationTests
{
    private static readonly string[] Keys = ["one", "two", "three"];

    private static TestDatabase Primary()
    {
        var database = TestDatabase.Create();

        foreach (var key in Keys)
            database.Db.Put(key, key.ToUpperInvariant());

        return database;
    }

    private static async Task AssertHasEveryKey(RocksDb db)
    {
        foreach (var key in Keys)
            await Assert.That(db.Get(key)).IsEqualTo(key.ToUpperInvariant());
    }

    private static void CopyTo(ReplicationSession session, string destination)
    {
        foreach (var file in session.Files)
        {
            using (file)
                ReplicationConsumer.IngestFile(file, destination);
        }
    }

    [Test]
    public async Task Checkpoint_ProducesAnOpenableCopyOfTheDatabase()
    {
        using var primary = Primary();
        using var directory = new TempDirectory();
        var checkpointPath = directory.Reserve("checkpoint");

        using (var checkpoint = primary.Db.Checkpoint())
            checkpoint.Save(checkpointPath);

        using var copy = RocksDb.Open(new DbOptions(), checkpointPath);
        await AssertHasEveryKey(copy);
    }

    [Test]
    public async Task Checkpoint_IntoAnExistingDirectory_Fails()
    {
        using var primary = Primary();
        using var directory = new TempDirectory();

        using var checkpoint = primary.Db.Checkpoint();

        await Assert.That(() => checkpoint.Save(directory.Path)).Throws<RocksDbException>();
    }

    [Test]
    public async Task Checkpoint_DoesNotDisturbThePrimary()
    {
        using var primary = Primary();
        using var directory = new TempDirectory();

        using (var checkpoint = primary.Db.Checkpoint())
            checkpoint.Save(directory.Reserve("checkpoint"));

        await AssertHasEveryKey(primary.Db);
    }

    [Test]
    public async Task Checkpoint_DisposedTwice_DoesNotDestroyTheHandleTwice()
    {
        using var primary = Primary();

        var checkpoint = primary.Db.Checkpoint();
        checkpoint.Dispose();
        checkpoint.Dispose();

        await Assert.That(checkpoint.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task ReplicationSession_DescribesEveryFileInItsDirectory()
    {
        using var directory = new TempDirectory();
        File.WriteAllBytes(Path.Combine(directory.Path, "000001.sst"), [1, 2, 3]);
        File.WriteAllBytes(Path.Combine(directory.Path, "CURRENT"), [4]);

        using var session = new ReplicationSession(directory.Path);
        var files = session.Files.ToList();

        try
        {
            using (Assert.Multiple())
            {
                await Assert.That(files.Select(file => file.FileName).Order())
                    .IsEquivalentTo(new[] { "000001.sst", "CURRENT" }, CollectionOrdering.Matching);
                await Assert.That(files.Single(file => file.FileName == "000001.sst").FileSize).IsEqualTo(3ul);
            }
        }
        finally
        {
            foreach (var file in files)
                file.Dispose();
        }
    }

    [Test]
    public async Task ReplicationSession_OpensAReadableStreamPerFile()
    {
        using var directory = new TempDirectory();
        File.WriteAllBytes(Path.Combine(directory.Path, "000001.sst"), [1, 2, 3]);

        using var session = new ReplicationSession(directory.Path);
        using var file = session.Files.Single();

        var buffer = new byte[3];
        file.FileStream.ReadExactly(buffer);

        await Assert.That(buffer).IsEquivalentTo(new byte[] { 1, 2, 3 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ReplicationSession_Dispose_RemovesTheTemporaryDirectory()
    {
        using var directory = new TempDirectory();
        var sessionPath = directory.Reserve("session");
        Directory.CreateDirectory(sessionPath);
        File.WriteAllBytes(Path.Combine(sessionPath, "000001.sst"), [1]);

        new ReplicationSession(sessionPath).Dispose();

        await Assert.That(Directory.Exists(sessionPath)).IsFalse();
    }

    [Test]
    public async Task ReplicationFile_Dispose_ClosesTheStream()
    {
        using var directory = new TempDirectory();
        var path = Path.Combine(directory.Path, "000001.sst");
        File.WriteAllBytes(path, [1]);

        var stream = File.OpenRead(path);
        new ReplicationFile { FileName = "000001.sst", FileStream = stream }.Dispose();

        await Assert.That(stream.CanRead).IsFalse();
    }

    [Test]
    public async Task IngestFile_WritesTheFileIntoTheDestinationDirectory()
    {
        using var directory = new TempDirectory();
        var destination = directory.Reserve("replica");

        using (var file = new ReplicationFile
        {
            FileName = "000001.sst",
            FileSize = 3,
            FileStream = new MemoryStream([1, 2, 3]),
        })
        {
            ReplicationConsumer.IngestFile(file, destination);
        }

        await Assert.That(File.ReadAllBytes(Path.Combine(destination, "000001.sst")))
            .IsEquivalentTo(new byte[] { 1, 2, 3 }, CollectionOrdering.Matching);
    }

    /// <remarks>
    /// The whole point of the initial state: a checkpoint of the primary, streamed file by file,
    /// has to open on the other side as a database holding the same data.
    /// </remarks>
    [Test]
    public async Task InitialState_RebuildsThePrimaryOnTheReplica()
    {
        using var primary = Primary();
        primary.Db.DisableFileDeletions();
        using var directory = new TempDirectory();
        var replicaPath = directory.Reserve("replica");

        using (var session = new ReplicationSource(primary.Db).GetInitialState(directory.Reserve("checkpoint")))
            CopyTo(session, replicaPath);

        using var replica = RocksDb.Open(new DbOptions(), replicaPath);
        await AssertHasEveryKey(replica);
    }

    [Test]
    public async Task InitialState_Dispose_RemovesTheCheckpoint()
    {
        using var primary = Primary();
        using var directory = new TempDirectory();
        var checkpointPath = directory.Reserve("checkpoint");

        new ReplicationSource(primary.Db).GetInitialState(checkpointPath).Dispose();

        await Assert.That(Directory.Exists(checkpointPath)).IsFalse();
    }

    /// <remarks>
    /// A fresh database numbers each single-key write in turn, so replaying the log of the three
    /// writes <see cref="Primary" /> makes must yield exactly those three sequence numbers in that
    /// order. Comparing the list against its own sorted copy would prove nothing.
    /// </remarks>
    [Test]
    public async Task WalUpdates_AreNumberedInIncreasingSequenceOrder()
    {
        using var primary = Primary();

        var sequenceNumbers = new ReplicationSource(primary.Db)
            .GetWalUpdates(0)
            .Select(batch => batch.SequenceNumber)
            .ToList();

        await Assert.That(sequenceNumbers).IsEquivalentTo(new ulong[] { 1, 2, 3 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task WalUpdates_CarryTheWriteBatchBytes()
    {
        using var primary = Primary();

        var batch = new ReplicationSource(primary.Db).GetWalUpdates(0).First();

        using var restored = WriteBatch.FromSpan(batch.Data);
        await Assert.That(restored.Count()).IsEqualTo(1);
    }

    /// <remarks>
    /// The other half of replication: replaying the primary's write-ahead log onto an empty
    /// database has to leave it holding exactly the primary's data.
    /// </remarks>
    [Test]
    public async Task WalUpdates_ReplayedOnAnEmptyDatabase_ReproduceThePrimary()
    {
        using var primary = Primary();
        using var replica = TestDatabase.Create();
        var consumer = new ReplicationConsumer(replica.Db);

        foreach (var batch in new ReplicationSource(primary.Db).GetWalUpdates(0))
            consumer.IngestBatch(batch);

        await AssertHasEveryKey(replica.Db);
    }

    [Test]
    public async Task WalUpdates_FromALaterSequenceNumber_SkipTheEarlierWrites()
    {
        using var primary = TestDatabase.Create();
        primary.Db.Put("first", "1");
        var afterFirst = primary.Db.GetLatestSequenceNumber();
        primary.Db.Put("second", "2");

        using var replica = TestDatabase.Create();
        var consumer = new ReplicationConsumer(replica.Db);

        foreach (var batch in new ReplicationSource(primary.Db).GetWalUpdates(afterFirst + 1))
            consumer.IngestBatch(batch);

        using (Assert.Multiple())
        {
            await Assert.That(replica.Db.Get("second")).IsEqualTo("2");
            await Assert.That(replica.Db.Get("first")).IsNull();
        }
    }

    [Test]
    public async Task PooledWalUpdates_CarryTheSameBytesAsTheUnpooledOnes()
    {
        using var primary = Primary();
        var source = new ReplicationSource(primary.Db);

        var expected = source.GetWalUpdates(0).First();
        var pooled = source.GetPooledWalUpdates(0).First();

        try
        {
            using (Assert.Multiple())
            {
                await Assert.That(pooled.SequenceNumber).IsEqualTo(expected.SequenceNumber);
                await Assert.That(pooled.Length).IsEqualTo(expected.Data.Length);
                await Assert.That(pooled.PooledData.AsSpan(0, pooled.Length).ToArray()).IsEquivalentTo(expected.Data, CollectionOrdering.Matching);
            }
        }
        finally
        {
            WriteBatch.ReturnPooledBytes(pooled.PooledData);
        }
    }

    [Test]
    public async Task IngestBatch_AcceptsRawBatchBytes()
    {
        using var primary = Primary();
        using var replica = TestDatabase.Create();
        var consumer = new ReplicationConsumer(replica.Db);

        foreach (var batch in new ReplicationSource(primary.Db).GetWalUpdates(0))
            consumer.IngestBatch(batch.SequenceNumber, batch.Data);

        await AssertHasEveryKey(replica.Db);
    }

    [Test]
    public async Task IngestBatch_WithoutADatabase_Throws()
    {
        var consumer = new ReplicationConsumer(null!);

        await Assert.That(() => consumer.IngestBatch(new ReplicationBatch { Data = [] }))
            .ThrowsExactly<InvalidOperationException>();
    }

    [Test]
    public async Task GetUpdatesSince_ReportsTheSequenceNumberOfEachBatch()
    {
        using var primary = TestDatabase.Create();
        primary.Db.Put("key", "value");

        using var iterator = primary.Db.GetUpdatesSince(0);
        iterator.Status();

        using var batch = iterator.GetBatch(out var sequenceNumber);

        using (Assert.Multiple())
        {
            await Assert.That(iterator.Valid()).IsTrue();
            await Assert.That(sequenceNumber).IsGreaterThan(0ul);
            await Assert.That(batch.Count()).IsEqualTo(1);
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
    public async Task TransactionLogIterator_Dispose_ClearsTheHandle()
    {
        using var primary = TestDatabase.Create();
        primary.Db.Put("key", "value");
        var iterator = primary.Db.GetUpdatesSince(0);

        iterator.Dispose();

        await Assert.That(iterator.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task TransactionLogIterator_Dispose_IsIdempotent()
    {
        using var primary = TestDatabase.Create();
        var iterator = primary.Db.GetUpdatesSince(0);
        iterator.Dispose();

        await Assert.That(iterator.Dispose).ThrowsNothing();
    }

    [Test]
    public async Task ReplicationBatch_HoldsTheDataItWasGiven()
    {
        var batch = new ReplicationBatch { SequenceNumber = 7, Data = Encoding.UTF8.GetBytes("payload") };

        using (Assert.Multiple())
        {
            await Assert.That(batch.SequenceNumber).IsEqualTo(7ul);
            await Assert.That(batch.Data).IsEquivalentTo("payload"u8.ToArray(), CollectionOrdering.Matching);
        }
    }
}
