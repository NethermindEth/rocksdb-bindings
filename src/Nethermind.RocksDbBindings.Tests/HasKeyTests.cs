// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class HasKeyTests
{
    [Test]
    [Arguments(0, false, false)]
    [Arguments(0, false, true)]
    [Arguments(0, true, false)]
    [Arguments(0, true, true)]
    [Arguments(128, false, false)]
    [Arguments(128, false, true)]
    [Arguments(128, true, false)]
    [Arguments(128, true, true)]
    [Arguments(8192, false, false)]
    [Arguments(8192, false, true)]
    [Arguments(8192, true, false)]
    [Arguments(8192, true, true)]
    public async Task HasKey_AfterSnapshotMutations_PreservesExactPresence(int valueLength, bool columnFamily, bool flush)
    {
        using DbOptions options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using ColumnFamilyOptions defaultOptions = new();
        using ColumnFamilyOptions dataOptions = new();
        ColumnFamilies families = new(defaultOptions);
        families.Add("data", dataOptions);
        using TestDatabase database = TestDatabase.Create(options, families);
        IColumnFamilyHandle? family = columnFamily ? database.Db.GetColumnFamily("data") : null;
        byte[] key = [0, 255, 1];
        byte[] addedKey = [0, 255, 2];
        database.Db.Put(key, new byte[valueLength], family);
        database.Db.Put([], [], family);
        using FlushOptions flushOptions = new FlushOptions().SetWaitForFlush(true);
        FlushIfNeeded();

        using Snapshot snapshot = database.Db.CreateSnapshot();
        using ReadOptions readOptions = new ReadOptions().SetSnapshot(snapshot).SetFillCache(false);
        await Assert.That(database.Db.HasKey(key, family)).IsTrue();
        database.Db.Remove(key, family);
        database.Db.Put(addedKey, new byte[valueLength], family);
        FlushIfNeeded();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.HasKey(key, family)).IsFalse();
            await Assert.That(database.Db.HasKey(key, family, readOptions)).IsTrue();
            await Assert.That(database.Db.HasKey(addedKey, family)).IsTrue();
            await Assert.That(database.Db.HasKey(addedKey, family, readOptions)).IsFalse();
            await Assert.That(database.Db.HasKey([], family)).IsTrue();
            await Assert.That(database.Db.HasKey([], family, readOptions)).IsTrue();
            await Assert.That(database.Db.HasKey("missing"u8, family, readOptions)).IsFalse();
        }

        void FlushIfNeeded()
        {
            if (!flush) return;
            if (family is null) database.Db.Flush(flushOptions);
            else database.Db.Flush(flushOptions, family);
        }
    }
}
