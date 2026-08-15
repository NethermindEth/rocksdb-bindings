// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class SstFileWriterTests
{
    private static string WriteSst(TempDirectory directory, Action<SstFileWriter> write, ColumnFamilyOptions? ioOptions = null)
    {
        var path = directory.Reserve("ingest.sst");

        using var writer = new SstFileWriter(ioOptions: ioOptions);
        writer.Open(path);
        write(writer);
        writer.Finish();

        return path;
    }

    [Test]
    public async Task AWrittenFile_CanBeIngestedAndRead()
    {
        using var directory = new TempDirectory();
        var path = WriteSst(directory, writer =>
        {
            writer.Put("a"u8.ToArray(), "A"u8.ToArray());
            writer.Put("b"u8.ToArray(), "B"u8.ToArray());
        });

        using var database = TestDatabase.Create();
        database.Db.IngestExternalFiles([path], new IngestExternalFileOptions());

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("a"u8.ToArray())).IsEquivalentTo("A"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(database.Db.Get("b"u8.ToArray())).IsEquivalentTo("B"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Add_WritesTheSameEntriesAsPut()
    {
        using var directory = new TempDirectory();
        var path = WriteSst(directory, writer => writer.Add("key", "value"));

        using var database = TestDatabase.Create();
        database.Db.IngestExternalFiles([path], new IngestExternalFileOptions());

        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
    }

    [Test]
    public async Task Delete_WritesATombstoneThatHidesTheKey()
    {
        using var directory = new TempDirectory();
        var path = WriteSst(directory, writer =>
        {
            writer.Put("a"u8.ToArray(), "A"u8.ToArray());
            writer.Delete("b"u8.ToArray());
            writer.Put("c"u8.ToArray(), "C"u8.ToArray());
        });

        using var database = TestDatabase.Create();
        database.Db.IngestExternalFiles([path], new IngestExternalFileOptions());

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("a"u8.ToArray())).IsEquivalentTo("A"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(database.Db.Get("b"u8.ToArray())).IsNull();
            await Assert.That(database.Db.Get("c"u8.ToArray())).IsEquivalentTo("C"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Merge_WritesAnOperandTheDatabaseMergeOperatorResolves()
    {
        using var directory = new TempDirectory();
        var writerOptions = new ColumnFamilyOptions().SetUint64addMergeOperator();
        var path = WriteSst(
            directory,
            writer => writer.Merge("counter"u8.ToArray(), BitConverter.GetBytes(7ul)),
            writerOptions);

        using var database = TestDatabase.Create(new DbOptions().SetCreateIfMissing().SetUint64addMergeOperator());
        database.Db.Merge("counter"u8.ToArray(), BitConverter.GetBytes(5ul));
        database.Db.IngestExternalFiles([path], new IngestExternalFileOptions());

        await Assert.That(BitConverter.ToUInt64(database.Db.Get("counter"u8.ToArray())!)).IsEqualTo(12ul);
    }

    [Test]
    public async Task IngestExternalFiles_CanTargetAColumnFamily()
    {
        using var directory = new TempDirectory();
        var path = WriteSst(directory, writer => writer.Add("key", "value"));

        var families = new ColumnFamilies();
        families.Add("blocks", new ColumnFamilyOptions());
        using var database = TestDatabase.Create(
            new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies(),
            families);
        var blocks = database.Db.GetColumnFamily("blocks");

        database.Db.IngestExternalFiles([path], new IngestExternalFileOptions(), blocks);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("key", blocks)).IsEqualTo("value");
            await Assert.That(database.Db.Get("key")).IsNull();
        }
    }

    [Test]
    public async Task IngestExternalFiles_WithMoveFiles_ConsumesTheSourceFile()
    {
        using var directory = new TempDirectory();
        var path = WriteSst(directory, writer => writer.Add("key", "value"));

        using var database = TestDatabase.Create();
        database.Db.IngestExternalFiles([path], new IngestExternalFileOptions().SetMoveFiles(true));

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("key")).IsEqualTo("value");
            await Assert.That(File.Exists(path)).IsFalse();
        }
    }

    [Test]
    public async Task IngestExternalFiles_RejectsAFileThatIsNotThere()
    {
        using var directory = new TempDirectory();
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.IngestExternalFiles([directory.Reserve("absent.sst")], new IngestExternalFileOptions()))
            .Throws<RocksDbException>();
    }

    [Test]
    public async Task Open_InADirectoryThatDoesNotExist_Fails()
    {
        using var directory = new TempDirectory();
        using var writer = new SstFileWriter();

        await Assert.That(() => writer.Open(Path.Combine(directory.Reserve("absent"), "out.sst")))
            .Throws<RocksDbException>();
    }

    [Test]
    public async Task Add_OutOfOrder_Fails()
    {
        using var directory = new TempDirectory();
        using var writer = new SstFileWriter();
        writer.Open(directory.Reserve("out.sst"));
        writer.Add("b", "B");

        await Assert.That(() => writer.Add("a", "A")).Throws<RocksDbException>();
    }

    [Test]
    public async Task Finish_WithoutAnyEntry_Fails()
    {
        using var directory = new TempDirectory();
        using var writer = new SstFileWriter();
        writer.Open(directory.Reserve("out.sst"));

        await Assert.That(writer.Finish).Throws<RocksDbException>();
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        var writer = new SstFileWriter();

        writer.Dispose();

        await Assert.That(writer.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        var writer = new SstFileWriter();
        writer.Dispose();

        await Assert.That(writer.Dispose).ThrowsNothing();
    }
}
