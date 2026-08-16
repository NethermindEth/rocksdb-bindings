// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Collections.Concurrent;
using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class ColumnFamilyTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Value = "value"u8.ToArray();

    private static DbOptions CreatingOptions()
        => new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();

    private static ColumnFamilies Families(params string[] names)
    {
        var families = new ColumnFamilies();

        foreach (var name in names)
            families.Add(name, new ColumnFamilyOptions());

        return families;
    }

    [Test]
    public async Task Open_CreatesTheRequestedFamilies()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks", "receipts"));

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.TryGetColumnFamily("blocks", out _)).IsTrue();
            await Assert.That(database.Db.TryGetColumnFamily("receipts", out _)).IsTrue();
        }
    }

    [Test]
    public async Task Values_AreScopedToTheirFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks", "receipts"));
        var blocks = database.Db.GetColumnFamily("blocks");
        var receipts = database.Db.GetColumnFamily("receipts");

        database.Db.Put(Key, "in blocks"u8.ToArray(), blocks);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key, blocks)).IsEquivalentTo("in blocks"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(database.Db.Get(Key, receipts)).IsNull();
            await Assert.That(database.Db.Get(Key)).IsNull();
        }
    }

    [Test]
    public async Task Remove_OnlyAffectsTheGivenFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, Value, blocks);
        database.Db.Put(Key, Value);

        database.Db.Remove(Key, blocks);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key, blocks)).IsNull();
            await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task HasKey_IsScopedToItsFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, Value, blocks);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.HasKey("key", blocks)).IsTrue();
            await Assert.That(database.Db.HasKey("key")).IsFalse();
        }
    }

    [Test]
    public async Task GetDefaultColumnFamily_IsTheSameHandleAsTheNamedLookup()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));

        await Assert.That(database.Db.GetDefaultColumnFamily())
            .IsSameReferenceAs(database.Db.GetColumnFamily(ColumnFamilies.DefaultName));
    }

    [Test]
    public async Task WritingThroughTheDefaultFamilyHandleMatchesWritingWithoutOne()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));

        database.Db.Put(Key, Value, database.Db.GetDefaultColumnFamily());

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task CreateColumnFamily_AddsAUsableFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families());

        var created = database.Db.CreateColumnFamily(new ColumnFamilyOptions(), "later");
        database.Db.Put(Key, Value, created);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.GetColumnFamily("later")).IsSameReferenceAs(created);
            await Assert.That(database.Db.Get(Key, created)).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task DropColumnFamily_ForgetsTheFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));

        database.Db.DropColumnFamily("blocks");

        await Assert.That(database.Db.TryGetColumnFamily("blocks", out _)).IsFalse();
    }

    [Test]
    public async Task DropColumnFamily_ThenRecreatingTheSameName_YieldsAFreshFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        var original = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, Value, original);

        database.Db.DropColumnFamily("blocks");
        using var cfOptions = new ColumnFamilyOptions();
        var recreated = database.Db.CreateColumnFamily(cfOptions, "blocks");
        database.Db.Put("other"u8, Value, recreated);

        using (Assert.Multiple())
        {
            // The lookup must point at the new family, and the dropped one's data must be gone.
            await Assert.That(recreated).IsNotSameReferenceAs(original);
            await Assert.That(database.Db.GetColumnFamily("blocks")).IsSameReferenceAs(recreated);
            await Assert.That(database.Db.Get(Key, recreated)).IsNull();
            await Assert.That(database.Db.Get("other"u8.ToArray(), recreated)).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task CreateColumnFamily_FromManyThreads_RegistersAndOwnsEveryFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families());
        var names = Enumerable.Range(0, 16).Select(i => $"family-{i}").ToArray();
        var created = new ConcurrentBag<IColumnFamilyHandle>();

        Parallel.ForEach(names, name =>
        {
            using var cfOptions = new ColumnFamilyOptions();
            var cf = database.Db.CreateColumnFamily(cfOptions, name);
            created.Add(cf);
            database.Db.Put(Key, Encoding.UTF8.GetBytes(name), cf);
        });

        using (Assert.Multiple())
        {
            foreach (var name in names)
            {
                // A torn insert would lose the entry; a torn handle would read another family's value.
                await Assert.That(database.Db.TryGetColumnFamily(name, out var cf)).IsTrue();
                await Assert.That(database.Db.Get(Key, cf!)).IsEquivalentTo(Encoding.UTF8.GetBytes(name), CollectionOrdering.Matching);
            }
        }

        database.Db.Dispose();

        // A lost ownership registration is invisible until close: only then is the handle destroyed
        // and zeroed, so every created family must be zeroed here or it leaked.
        await Assert.That(created.Select(cf => cf.Handle)).IsEquivalentTo(new nint[names.Length]);
    }

    [Test]
    public async Task TryGetColumnFamily_ForAnUnknownName_ReturnsFalse()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));

        var found = database.Db.TryGetColumnFamily("nope", out var handle);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsFalse();
            await Assert.That(handle).IsNull();
        }
    }

    [Test]
    public async Task GetColumnFamily_ForAnUnknownName_Throws()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));

        await Assert.That(() => database.Db.GetColumnFamily("nope")).Throws<KeyNotFoundException>();
    }

    [Test]
    public async Task GetColumnFamily_OnADatabaseOpenedWithoutFamilies_Throws()
    {
        using var database = TestDatabase.Create();

        var exception = await Assert.That(() => database.Db.GetColumnFamily(ColumnFamilies.DefaultName))
            .ThrowsExactly<RocksDbException>();

        await Assert.That(exception!.Message).IsEqualTo("Database not opened for column families");
    }

    [Test]
    public async Task TryGetColumnFamily_OnADatabaseOpenedWithoutFamilies_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.TryGetColumnFamily(ColumnFamilies.DefaultName, out _))
            .ThrowsExactly<RocksDbException>();
    }

    [Test]
    public async Task ListColumnFamilies_NamesEveryFamilyOnDisk()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(CreatingOptions(), path, Families("blocks", "receipts")))
        {
        }

        await Assert.That(RocksDb.ListColumnFamilies(new DbOptions(), path))
            .IsEquivalentTo(new[] { ColumnFamilies.DefaultName, "blocks", "receipts" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task TryListColumnFamilies_ForAMissingDatabase_ReturnsFalseAndNoNames()
    {
        using var directory = new TempDirectory();

        var listed = RocksDb.TryListColumnFamilies(new DbOptions(), directory.Reserve("absent"), out var names);

        using (Assert.Multiple())
        {
            await Assert.That(listed).IsFalse();
            await Assert.That(names).IsEmpty();
        }
    }

    [Test]
    public async Task ListColumnFamilies_ForAMissingDatabase_IsEmpty()
    {
        using var directory = new TempDirectory();

        await Assert.That(RocksDb.ListColumnFamilies(new DbOptions(), directory.Reserve("absent"))).IsEmpty();
    }

    [Test]
    public async Task MultiGet_ReadsEachKeyFromItsOwnFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");
        var @default = database.Db.GetDefaultColumnFamily();
        database.Db.Put(Key, "in blocks"u8.ToArray(), blocks);
        database.Db.Put(Key, "in default"u8.ToArray());

        var results = database.Db.MultiGet([Key, Key], [blocks, @default]);

        using (Assert.Multiple())
        {
            await Assert.That(results[0].Value).IsEquivalentTo("in blocks"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(results[1].Value).IsEquivalentTo("in default"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task GetProperty_CanBeScopedToAFamily()
    {
        using var database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, Value, blocks);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.GetProperty("rocksdb.num-entries-active-mem-table", blocks)).IsEqualTo("1");
            await Assert.That(database.Db.GetProperty("rocksdb.num-entries-active-mem-table")).IsEqualTo("0");
        }
    }

    [Test]
    public async Task ReopeningWithoutAllFamilies_Fails()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(CreatingOptions(), path, Families("blocks")))
        {
        }

        await Assert.That(() => RocksDb.Open(new DbOptions(), path, new ColumnFamilies()))
            .Throws<RocksDbException>();
    }

    [Test]
    public async Task OpenReadOnly_WithFamilies_SeesTheirData()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(CreatingOptions(), path, Families("blocks")))
            db.Put(Key, Value, db.GetColumnFamily("blocks"));

        using var readOnly = RocksDb.OpenReadOnly(new DbOptions(), path, Families("blocks"), errIfLogFileExists: false);

        await Assert.That(readOnly.Get(Key, readOnly.GetColumnFamily("blocks"))).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ColumnFamilyLookups_ThrowAfterDisposal()
    {
        using var directory = new TempDirectory();
        using var options = CreatingOptions();
        var db = RocksDb.Open(options, directory.Reserve("db"), Families("blocks"));
        db.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(() => db.GetColumnFamily("blocks")).Throws<ObjectDisposedException>();
            await Assert.That(() => db.TryGetColumnFamily("blocks", out _)).Throws<ObjectDisposedException>();
        }
    }

    [Test]
    public async Task Flush_CanBeScopedToAFamily()
    {
        using var options = CreatingOptions();
        using var database = TestDatabase.Create(options, Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, Value, blocks);

        using var flushOptions = new FlushOptions().SetWaitForFlush(true);
        database.Db.Flush(flushOptions, blocks);

        using (Assert.Multiple())
        {
            // The flushed family has SST data; the untouched default family has none.
            await Assert.That(database.Db.TryGetIntProperty("rocksdb.total-sst-files-size", blocks, out var flushedSize)).IsTrue();
            await Assert.That(flushedSize).IsGreaterThan(0ul);
            await Assert.That(database.Db.TryGetIntProperty("rocksdb.total-sst-files-size", database.Db.GetDefaultColumnFamily(), out var defaultSize)).IsTrue();
            await Assert.That(defaultSize).IsEqualTo(0ul);
            await Assert.That(database.Db.Get(Key, blocks)).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task SetOptions_CanBeScopedToAFamily()
    {
        using var options = CreatingOptions();
        using var database = TestDatabase.Create(options, Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");

        database.Db.SetOptions(blocks, [new KeyValuePair<string, string>("write_buffer_size", "65536")]);

        // Writing past the shrunken buffer forces a memtable switch, observable as an immutable
        // memtable or an already-flushed SST file — in that family only.
        var payload = new byte[8 * 1024];
        for (var i = 0; i < 40; i++)
            database.Db.Put([(byte)i], payload.AsSpan(), blocks);

        using (Assert.Multiple())
        {
            await Assert.That(MemtableSwitched(database.Db, blocks)).IsTrue();
            await Assert.That(MemtableSwitched(database.Db, database.Db.GetDefaultColumnFamily())).IsFalse();
        }

        static bool MemtableSwitched(RocksDb db, IColumnFamilyHandle cf)
            => (db.TryGetIntProperty("rocksdb.num-immutable-mem-table", cf, out var immutable) && immutable > 0)
                || (db.TryGetIntProperty("rocksdb.total-sst-files-size", cf, out var sstSize) && sstSize > 0);
    }

    [Test]
    public async Task SetOptions_ThrowsOnAnUnknownOption()
    {
        using var options = CreatingOptions();
        using var database = TestDatabase.Create(options, Families("blocks"));
        var blocks = database.Db.GetColumnFamily("blocks");

        await Assert.That(() => database.Db.SetOptions(blocks, [new KeyValuePair<string, string>("no_such_option", "1")]))
            .Throws<RocksDbNativeException>();
    }
}
