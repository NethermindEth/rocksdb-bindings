// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class RocksDbTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Value = "value"u8.ToArray();

    /// <summary>Reads the first four bytes of a value as a little endian integer.</summary>
    private sealed class Int32Deserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => BitConverter.ToInt32(buffer);
    }

    private static bool TryGetFixedSize(RocksDb db, byte[] key, byte[] output)
        => db.GetFixedSizeValue(key, output);

    private static bool HasKey(RocksDb db, byte[] key) => db.HasKey(key.AsSpan());

    [Test]
    public async Task Open_WithoutCreateIfMissing_FailsOnAnEmptyDirectory()
    {
        using var directory = new TempDirectory();

        var exception = await Assert.That(() => RocksDb.Open(new DbOptions(), directory.Reserve("db")))
            .Throws<RocksDbException>();

        await Assert.That(exception!.Message).Contains("does not exist");
    }

    [Test]
    public async Task Open_NullPath_ThrowsInsteadOfPassingANullPointerToTheNativeCall()
    {
        using var options = new DbOptions();

        await Assert.That(() => RocksDb.Open(options, null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Open_RecordsThePathItWasGiven()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.Path).IsEqualTo(database.Path);
    }

    [Test]
    public async Task Open_ReopensAnExistingDatabaseWithItsData()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), path))
            db.Put(Key, Value);

        using var reopened = RocksDb.Open(new DbOptions(), path);

        await Assert.That(reopened.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task OpenReadOnly_SeesTheDataButRefusesWrites()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), path))
            db.Put(Key, Value);

        using var readOnly = RocksDb.OpenReadOnly(new DbOptions(), path, errorIfLogFileExists: false);

        using (Assert.Multiple())
        {
            await Assert.That(readOnly.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(() => readOnly.Put(Key, Value)).Throws<RocksDbException>();
        }
    }

    [Test]
    public async Task OpenWithTtl_BehavesLikeANormalDatabaseWhileTheDataIsFresh()
    {
        using var directory = new TempDirectory();

        using var db = RocksDb.OpenWithTtl(new DbOptions().SetCreateIfMissing(), directory.Reserve("db"), ttlSeconds: 3600);
        db.Put(Key, Value);

        await Assert.That(db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task OpenAsSecondary_CatchesUpWithThePrimary()
    {
        using var directory = new TempDirectory();
        var primaryPath = directory.Reserve("primary");

        using var primary = RocksDb.Open(new DbOptions().SetCreateIfMissing(), primaryPath);
        primary.Put(Key, Value);
        primary.Flush(new FlushOptions().SetWaitForFlush(true));

        using var secondary = RocksDb.OpenAsSecondary(new DbOptions(), primaryPath, directory.Reserve("secondary"));
        secondary.TryCatchUpWithPrimary();

        await Assert.That(secondary.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Get_MissingKey_ReturnsNull()
    {
        using var database = TestDatabase.Create();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key)).IsNull();
            await Assert.That(database.Db.Get("missing")).IsNull();
        }
    }

    [Test]
    public async Task Put_AndGet_RoundTripBytes()
    {
        using var database = TestDatabase.Create();

        database.Db.Put(Key, Value);

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_AndGet_RoundTripSpans()
    {
        using var database = TestDatabase.Create();

        database.Db.Put("k"u8, "v"u8);

        await Assert.That(database.Db.Get("k"u8)).IsEquivalentTo("v"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_AndGet_RoundTripStrings()
    {
        using var database = TestDatabase.Create();

        database.Db.Put("key", "value");

        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
    }

    [Test]
    public async Task Put_WithSlicedSpans_StoresOnlyThoseBytes()
    {
        using var database = TestDatabase.Create();

        database.Db.Put([1, 2, 3], value: ((byte[])[4, 5, 6]).AsSpan(0, 1));

        await Assert.That(database.Db.Get(new byte[] { 1, 2, 3 })).IsEquivalentTo(new byte[] { 4 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_AsAString_IsStoredAsUtf8()
    {
        using var database = TestDatabase.Create();

        database.Db.Put("kü", "vé");

        await Assert.That(database.Db.Get(Encoding.UTF8.GetBytes("kü"))).IsEquivalentTo(Encoding.UTF8.GetBytes("vé"), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_OverwritesAnExistingValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        database.Db.Put(Key, "second"u8.ToArray());

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo("second"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_StoresAnEmptyValue()
    {
        using var database = TestDatabase.Create();

        database.Db.Put(Key, Array.Empty<byte>());

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Array.Empty<byte>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task HasKey_DistinguishesPresentFromMissing()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        using (Assert.Multiple())
        {
            await Assert.That(HasKey(database.Db, Key)).IsTrue();
            await Assert.That(HasKey(database.Db, "absent"u8.ToArray())).IsFalse();
        }
    }

    /// <remarks>
    /// An empty value is still a value, so presence has to be decided by the returned pointer
    /// rather than by the length rocksdb reports.
    /// </remarks>
    [Test]
    public async Task HasKey_IsTrueForAKeyWithAnEmptyValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Array.Empty<byte>());

        await Assert.That(HasKey(database.Db, Key)).IsTrue();
    }

    [Test]
    public async Task HasKey_ForAString_FindsTheKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("key", "value");

        await Assert.That(database.Db.HasKey("key")).IsTrue();
    }

    [Test]
    public async Task Remove_DeletesTheKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        database.Db.Remove(Key);

        await Assert.That(database.Db.Get(Key)).IsNull();
    }

    [Test]
    public async Task Remove_OfAMissingKey_IsSilent()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.Remove(Key)).ThrowsNothing();
    }

    [Test]
    public async Task Remove_ByString_DeletesTheKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("key", "value");

        database.Db.Remove("key");

        await Assert.That(database.Db.Get("key")).IsNull();
    }

    [Test]
    public async Task GetFixedSizeValue_FillsTheBufferWhenTheLengthMatches()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, new byte[] { 1, 2, 3, 4 });
        var output = new byte[4];

        var found = TryGetFixedSize(database.Db, Key, output);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(output).IsEquivalentTo(new byte[] { 1, 2, 3, 4 }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task GetFixedSizeValue_RejectsAValueOfADifferentLength()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, new byte[] { 1, 2, 3 });

        await Assert.That(TryGetFixedSize(database.Db, Key, new byte[4])).IsFalse();
    }

    [Test]
    public async Task GetFixedSizeValue_ReportsAMissingKey()
    {
        using var database = TestDatabase.Create();

        await Assert.That(TryGetFixedSize(database.Db, Key, new byte[4])).IsFalse();
    }

    [Test]
    public async Task Get_WithASpanDeserializer_DecodesTheValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, BitConverter.GetBytes(1234));

        await Assert.That(database.Db.Get(Key, new Int32Deserializer())).IsEqualTo(1234);
    }

    [Test]
    public async Task Get_WithASpanDeserializer_ReturnsTheDefaultForAMissingKey()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.Get(Key, new Int32Deserializer())).IsEqualTo(0);
    }

    [Test]
    public async Task MultiGet_ReturnsOneResultPerKeyInOrder()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a"u8.ToArray(), "1"u8.ToArray());
        database.Db.Put("b"u8.ToArray(), "2"u8.ToArray());

        var results = database.Db.MultiGet([[.. "b"u8], [.. "a"u8]]);

        using (Assert.Multiple())
        {
            await Assert.That(results.Select(result => Encoding.UTF8.GetString(result.Key))).IsEquivalentTo(new[] { "b", "a" }, CollectionOrdering.Matching);
            await Assert.That(results.Select(result => Encoding.UTF8.GetString(result.Value!))).IsEquivalentTo(new[] { "2", "1" }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task MultiGet_ReportsMissingKeysAsNull()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a"u8.ToArray(), "1"u8.ToArray());

        var results = database.Db.MultiGet([[.. "a"u8], [.. "absent"u8]]);

        using (Assert.Multiple())
        {
            await Assert.That(results[0].Value).IsNotNull();
            await Assert.That(results[1].Value).IsNull();
        }
    }

    [Test]
    public async Task MultiGet_WithNoKeys_ReturnsNothing()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.MultiGet(Array.Empty<byte[]>())).IsEmpty();
    }

    [Test]
    public async Task MultiGet_NullKeyArray_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.MultiGet((byte[][])null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task MultiGet_NullKey_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.MultiGet(new byte[][] { null! })).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task MultiGet_WithFewerColumnFamiliesThanKeys_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.MultiGet([[.. "a"u8]], []))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Write_AppliesEveryOperationInTheBatch()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("stale"u8.ToArray(), Value);

        using var batch = new WriteBatch();
        batch.Put("fresh"u8.ToArray(), Value).Delete("stale"u8.ToArray());
        database.Db.Write(batch);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("fresh"u8.ToArray())).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(database.Db.Get("stale"u8.ToArray())).IsNull();
        }
    }

    [Test]
    public async Task Write_WithSyncWriteOptions_Succeeds()
    {
        using var database = TestDatabase.Create();

        using var batch = new WriteBatch();
        batch.Put(Key, Value);
        database.Db.Write(batch, new WriteOptions().SetSync(true));

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task SetOptions_AppliesAKnownOption()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.SetOptions([new("disable_auto_compactions", "true")])).ThrowsNothing();
    }

    [Test]
    public async Task SetOptions_RejectsAnUnknownOption()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.SetOptions([new("not_an_option", "1")])).Throws<RocksDbException>();
    }

    [Test]
    public async Task GetProperty_ReadsAKnownProperty()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(database.Db.GetProperty("rocksdb.num-entries-active-mem-table")).IsEqualTo("1");
    }

    [Test]
    public async Task GetProperty_ReturnsNullForAnUnknownProperty()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.GetProperty("rocksdb.not-a-property")).IsNull();
    }

    [Test]
    public async Task GetLatestSequenceNumber_AdvancesWithEveryWrite()
    {
        using var database = TestDatabase.Create();
        var before = database.Db.GetLatestSequenceNumber();

        database.Db.Put(Key, Value);

        await Assert.That(database.Db.GetLatestSequenceNumber()).IsGreaterThan(before);
    }

    [Test]
    public async Task Flush_MovesTheMemtableToDisk()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        // Both of these only hold once the flush has actually run to completion.
        using (Assert.Multiple())
        {
            await Assert.That(database.Db.GetProperty("rocksdb.num-entries-active-mem-table")).IsEqualTo("0");
            await Assert.That(database.Db.GetProperty("rocksdb.num-files-at-level0")).IsNotEqualTo("0");
        }
    }

    [Test]
    public async Task GetLiveFileNames_ListsTheFlushedTableFiles()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        await Assert.That(database.Db.GetLiveFileNames()).IsNotEmpty();
    }

    [Test]
    public async Task GetLiveFilesMetadata_DescribesTheKeyRangeOfEachFile()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("aaa"u8.ToArray(), Value);
        database.Db.Put("zzz"u8.ToArray(), Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        var metadata = database.Db.GetLiveFilesMetadata()!.Single();

        using (Assert.Multiple())
        {
            await Assert.That(metadata.FileMetadata.FileName).IsNotEmpty();
            await Assert.That(metadata.FileMetadata.FileSize).IsGreaterThan(0ul);
            await Assert.That(metadata.FileDataMetadata!.SmallestKeyInFile).IsEqualTo("aaa");
            await Assert.That(metadata.FileDataMetadata.LargestKeyInFile).IsEqualTo("zzz");
            await Assert.That(metadata.FileDataMetadata.NumEntriesInFile).IsEqualTo(2ul);
        }
    }

    [Test]
    public async Task GetLiveFilesMetadata_CanSkipTheKeyRange()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        var metadata = database.Db.GetLiveFilesMetadata(populateFileMetadataOnly: true)!.Single();

        using (Assert.Multiple())
        {
            await Assert.That(metadata.FileMetadata.FileName).IsNotEmpty();
            await Assert.That(metadata.FileDataMetadata).IsNull();
        }
    }

    [Test]
    public async Task GetLiveFileNames_OnAnUnflushedDatabase_IsEmpty()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(database.Db.GetLiveFileNames()).IsEmpty();
    }

    [Test]
    public async Task CompactRange_LeavesTheDataReadable()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a"u8.ToArray(), Value);
        database.Db.Put("z"u8.ToArray(), Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        database.Db.CompactRange("a", "z");

        await Assert.That(database.Db.Get("a"u8.ToArray())).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task CompactRange_AcceptsAnUnboundedRange()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(() => database.Db.CompactRange((byte[]?)null, null)).ThrowsNothing();
    }

    [Test]
    public async Task FileDeletions_CanBeDisabledAndReEnabled()
    {
        using var database = TestDatabase.Create();

        database.Db.DisableFileDeletions();

        await Assert.That(database.Db.EnableFileDeletions).ThrowsNothing();
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        using var directory = new TempDirectory();
        var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), directory.Reserve("db"));

        db.Dispose();

        await Assert.That(db.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        using var directory = new TempDirectory();
        var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), directory.Reserve("db"));
        db.Dispose();

        await Assert.That(db.Dispose).ThrowsNothing();
    }

    [Test]
    public async Task FlushWal_SucceedsAfterWrites()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(() => database.Db.FlushWal(sync: true)).ThrowsNothing();
    }

    [Test]
    public async Task TryGetIntProperty_ReadsAnIntegerProperty()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        var found = database.Db.TryGetIntProperty("rocksdb.estimate-num-keys", out var keys);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(keys).IsEqualTo(1ul);
        }
    }

    [Test]
    public async Task TryGetIntProperty_ReturnsFalseForAnUnknownProperty()
    {
        using var database = TestDatabase.Create();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.TryGetIntProperty("rocksdb.no-such-property", out var value)).IsFalse();
            await Assert.That(value).IsEqualTo(0ul);
        }
    }

    [Test]
    public async Task Repair_LeavesAFlushedDatabaseReopenableWithItsData()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        using (var db = RocksDb.Open(creatingOptions, path))
        {
            db.Put(Key, Value);
            using var flushOptions = new FlushOptions().SetWaitForFlush(true);
            db.Flush(flushOptions);
        }

        using var repairOptions = new DbOptions();
        RocksDb.Repair(repairOptions, path);

        using var reopenOptions = new DbOptions();
        using var reopened = RocksDb.Open(reopenOptions, path);
        await Assert.That(reopened.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    /// <summary>Counts its enumerations, like a non-replayable LINQ source would misbehave past one.</summary>
    private sealed class SingleEnumerationOptions : IEnumerable<KeyValuePair<string, string>>
    {
        public int EnumerationCount { get; private set; }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            EnumerationCount++;
            yield return new("write_buffer_size", "1048576");
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <remarks>
    /// The native call indexes the keys and values arrays through one count, so they must come
    /// from a single enumeration; a second pass over an unstable source could desynchronize them.
    /// </remarks>
    [Test]
    public async Task SetOptions_EnumeratesTheOptionsExactlyOnce()
    {
        using var database = TestDatabase.Create();
        var options = new SingleEnumerationOptions();

        database.Db.SetOptions(options);

        await Assert.That(options.EnumerationCount).IsEqualTo(1);
    }
}
