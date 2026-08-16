// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class CheckpointTests
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
}
