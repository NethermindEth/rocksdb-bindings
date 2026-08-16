// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using Nethermind.RocksDbBindings.Native;

namespace Nethermind.RocksDbBindings.Tests;

public class OptionsTests
{
    private static unsafe byte WaitForFlush(FlushOptions options)
        => RocksDbNative.rocksdb_flushoptions_get_wait((rocksdb_flushoptions_t*)options.Handle);

    private static unsafe byte Sync(WriteOptions options)
        => RocksDbNative.rocksdb_writeoptions_get_sync((rocksdb_writeoptions_t*)options.Handle);

    private static unsafe byte CreateIfMissing(DbOptions options)
        => RocksDbNative.rocksdb_options_get_create_if_missing((rocksdb_options_t*)options.Handle);

    /// <remarks>
    /// <see cref="FlushOptions" /> derives from <see cref="OptionsHandle" /> but must not use the
    /// <c>rocksdb_options_t</c> that base class would otherwise create. If it did, this read of
    /// <c>wait</c> would land on <c>DBOptions::create_if_missing</c> instead, whose default is
    /// false, and every setter would write into an unrelated struct.
    /// </remarks>
    [Test]
    public async Task FlushOptions_StartsOutWaitingLikeRocksDbDoes()
        => await Assert.That(WaitForFlush(new FlushOptions())).IsEqualTo((byte)1);

    [Test]
    public async Task FlushOptions_SetWaitForFlush_ReachesTheNativeFlushOptions()
    {
        var options = new FlushOptions();

        options.SetWaitForFlush(false);
        await Assert.That(WaitForFlush(options)).IsEqualTo((byte)0);

        options.SetWaitForFlush(true);
        await Assert.That(WaitForFlush(options)).IsEqualTo((byte)1);
    }

    [Test]
    public async Task FlushOptions_SettersAreFluent()
    {
        var options = new FlushOptions();

        await Assert.That(options.SetWaitForFlush(true)).IsSameReferenceAs(options);
    }

    [Test]
    public async Task WriteOptions_DefaultsToAnAsynchronousWrite()
        => await Assert.That(Sync(new WriteOptions())).IsEqualTo((byte)0);

    [Test]
    public async Task WriteOptions_SetSync_ReachesTheNativeWriteOptions()
    {
        var options = new WriteOptions();

        options.SetSync(true);

        await Assert.That(Sync(options)).IsEqualTo((byte)1);
    }

    [Test]
    public async Task WriteOptions_SettersAreFluent()
    {
        var options = new WriteOptions();

        using (Assert.Multiple())
        {
            await Assert.That(options.SetSync(false)).IsSameReferenceAs(options);
            await Assert.That(options.DisableWal(1)).IsSameReferenceAs(options);
        }
    }

    [Test]
    public async Task DbOptions_DefaultsToNotCreatingTheDatabase()
        => await Assert.That(CreateIfMissing(new DbOptions())).IsEqualTo((byte)0);

    [Test]
    public async Task DbOptions_SetCreateIfMissing_ReachesTheNativeOptions()
        => await Assert.That(CreateIfMissing(new DbOptions().SetCreateIfMissing())).IsEqualTo((byte)1);

    [Test]
    public async Task DbOptions_SettersReturnTheSameInstance()
    {
        var options = new DbOptions();

        using (Assert.Multiple())
        {
            await Assert.That(options.SetCreateIfMissing()).IsSameReferenceAs(options);
            await Assert.That(options.IncreaseParallelism(2)).IsSameReferenceAs(options);
        }
    }

    [Test]
    public async Task DbOptions_SetWalDir_IsRememberedForTheOpenedDatabase()
    {
        using var directory = new TempDirectory();
        var walDirectory = directory.Reserve("wal");

        var options = new DbOptions().SetCreateIfMissing().SetWalDir(walDirectory);
        using var db = RocksDb.Open(options, directory.Reserve("db"));

        await Assert.That(db.WalPath).IsEqualTo(walDirectory);
    }

    [Test]
    public async Task DbOptions_SetDbLogDir_IsRememberedForTheOpenedDatabase()
    {
        using var directory = new TempDirectory();
        var logDirectory = directory.Reserve("log");
        Directory.CreateDirectory(logDirectory);

        var options = new DbOptions().SetCreateIfMissing().SetDbLogDir(logDirectory);
        using var db = RocksDb.Open(options, directory.Reserve("db"));

        await Assert.That(db.LogPath).IsEqualTo(logDirectory);
    }

    [Test]
    public async Task DbOptions_WithoutDirectoryOverrides_LeavesThePathsUnset()
    {
        using var database = TestDatabase.Create();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.WalPath).IsNull();
            await Assert.That(database.Db.LogPath).IsNull();
        }
    }

    [Test]
    public async Task BlockBasedTableFactory_IsHeldByTheOptionsThatUseIt()
    {
        var tableOptions = new BlockBasedTableOptions().SetBlockSize(4096);
        var options = new ColumnFamilyOptions();

        // The reference has to be kept on the managed side: rocksdb reads the table options in
        // place, so a collected wrapper would destroy a handle rocksdb is still using.
        await Assert.That(options.SetBlockBasedTableFactory(tableOptions)).IsSameReferenceAs(options);

        GC.Collect();
        GC.WaitForPendingFinalizers();

        await Assert.That(tableOptions.Handle).IsNotEqualTo(nint.Zero);
    }

    [Test]
    public async Task Cache_LruCacheStartsEmpty()
    {
        var cache = Cache.CreateLru(8 * 1024 * 1024);

        using (Assert.Multiple())
        {
            await Assert.That(cache.Handle).IsNotEqualTo(nint.Zero);
            await Assert.That(cache.GetPinnedUsage()).IsEqualTo(0ul);
        }
    }

    [Test]
    public async Task Cache_AttachedAsABlockCache_ReportsUsageAfterReads()
    {
        var cache = Cache.CreateLru(8 * 1024 * 1024);
        var tableOptions = new BlockBasedTableOptions().SetBlockCache(cache);
        var options = new DbOptions().SetCreateIfMissing().SetBlockBasedTableFactory(tableOptions);

        using var database = TestDatabase.Create(options);
        database.Db.Put("key", "value");
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        // The read has to come off disk for the block to reach the cache.
        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
        await Assert.That(cache.GetUsage()).IsGreaterThan(0ul);
    }

    [Test]
    public async Task BloomFilterPolicy_AttachedToATable_LeavesReadsCorrect()
    {
        var tableOptions = new BlockBasedTableOptions().SetFilterPolicy(BloomFilterPolicy.Create(10, use_block_based_builder: false));
        var options = new DbOptions().SetCreateIfMissing().SetBlockBasedTableFactory(tableOptions);

        using var database = TestDatabase.Create(options);
        database.Db.Put("present", "value");
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("present")).IsEqualTo("value");
            await Assert.That(database.Db.Get("absent")).IsNull();
        }
    }

    /// <remarks>
    /// With a prefix extractor in place, a prefix seek that asks to stay inside its prefix must
    /// stop at the first key belonging to the next one.
    /// </remarks>
    [Test]
    public async Task PrefixExtractor_ConfinesAPrefixSeekToItsOwnPrefix()
    {
        var options = new DbOptions().SetCreateIfMissing().SetPrefixExtractor(SliceTransform.CreateFixedPrefix(2));

        using var database = TestDatabase.Create(options);
        database.Db.Put("aa1", "1");
        database.Db.Put("aa2", "2");
        database.Db.Put("bb1", "3");

        var keys = new List<string>();
        using (var iterator = database.Db.NewIterator(readOptions: new ReadOptions().SetPrefixSameAsStart(true)))
        {
            for (iterator.Seek("aa"); iterator.Valid(); iterator.Next())
                keys.Add(iterator.StringKey());
        }

        await Assert.That(keys).IsEquivalentTo(new[] { "aa1", "aa2" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Cache_SetCapacityIsFluent()
    {
        var cache = Cache.CreateLru(1024);

        await Assert.That(cache.SetCapacity(2048)).IsSameReferenceAs(cache);
    }

    [Test]
    public async Task Env_CreatesDistinctEnvironments()
    {
        var mem = Env.CreateMemEnv();
        var def = Env.CreateDefaultEnv();

        await Assert.That(mem.Handle).IsNotEqualTo(def.Handle);
    }

    [Test]
    public async Task Env_SettersAreFluent()
    {
        var env = Env.CreateDefaultEnv();

        using (Assert.Multiple())
        {
            await Assert.That(env.SetBackgroundThreads(2)).IsSameReferenceAs(env);
            await Assert.That(env.SetHighPriorityBackgroundThreads(1)).IsSameReferenceAs(env);
        }
    }

    [Test]
    public async Task BloomFilterPolicy_CreatesAHandleForBothBuilders()
    {
        using (Assert.Multiple())
        {
            await Assert.That(BloomFilterPolicy.Create(10, use_block_based_builder: true).Handle).IsNotEqualTo(nint.Zero);
            await Assert.That(BloomFilterPolicy.Create(10, use_block_based_builder: false).Handle).IsNotEqualTo(nint.Zero);
        }
    }

    [Test]
    public async Task SliceTransform_CreatesAHandleForBothTransforms()
    {
        using (Assert.Multiple())
        {
            await Assert.That(SliceTransform.CreateFixedPrefix(4).Handle).IsNotEqualTo(nint.Zero);
            await Assert.That(SliceTransform.CreateNoOp().Handle).IsNotEqualTo(nint.Zero);
        }
    }

    [Test]
    public async Task IngestExternalFileOptions_SettersAreFluent()
    {
        var options = new IngestExternalFileOptions();

        using (Assert.Multiple())
        {
            await Assert.That(options.SetMoveFiles(true)).IsSameReferenceAs(options);
            await Assert.That(options.SetSnapshotConsistency(true)).IsSameReferenceAs(options);
            await Assert.That(options.SetAllowGlobalSeqno(true)).IsSameReferenceAs(options);
            await Assert.That(options.SetAllowBlockingFlush(true)).IsSameReferenceAs(options);
        }
    }

    [Test]
    public async Task ReadOptions_SettersAreFluent()
    {
        var options = new ReadOptions();

        using (Assert.Multiple())
        {
            await Assert.That(options.SetVerifyChecksums(true)).IsSameReferenceAs(options);
            await Assert.That(options.SetFillCache(false)).IsSameReferenceAs(options);
            await Assert.That(options.SetTotalOrderSeek(true)).IsSameReferenceAs(options);
            await Assert.That(options.SetPrefixSameAsStart(true)).IsSameReferenceAs(options);
        }
    }

    /// <remarks>
    /// Each bound is copied into native memory the options object owns; setting it again frees
    /// the previous copy, and only the last bounds set may constrain iteration.
    /// </remarks>
    [Test]
    public async Task ReadOptions_IterateBoundsCanBeReplaced()
    {
        using var database = TestDatabase.Create();
        database.Db.Put([14], [1]);
        database.Db.Put([15], [2]);
        database.Db.Put([16], [3]);

        using var options = new ReadOptions();
        for (var i = 0; i < 16; i++)
        {
            options.SetIterateLowerBound([(byte)i]);
            options.SetIterateUpperBound([(byte)(i + 1)]);
        }

        var keys = new List<byte>();
        using (var iterator = database.Db.NewIterator(readOptions: options))
        {
            for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
                keys.Add(iterator.GetKeySpan()[0]);
        }

        await Assert.That(keys).IsEquivalentTo(new byte[] { 15 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ReadOptions_SetIterateBounds_ConfinesIterationToTheRange()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "1");
        database.Db.Put("b", "2");
        database.Db.Put("c", "3");
        database.Db.Put("d", "4");

        using var options = new ReadOptions().SetIterateBounds("b"u8, "d"u8);
        var keys = new List<string>();
        using (var iterator = database.Db.NewIterator(readOptions: options))
        {
            for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
                keys.Add(iterator.StringKey());
        }

        await Assert.That(keys).IsEquivalentTo(new[] { "b", "c" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ReadOptions_Dispose_IsIdempotentAndFreesTheBounds()
    {
        var options = new ReadOptions().SetIterateBounds("a"u8, "b"u8);

        options.Dispose();
        options.Dispose();

        await Assert.That(options.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task WriteOptions_CanBeDisposedOnceItsWritesHaveReturned()
    {
        using var database = TestDatabase.Create();
        var options = new WriteOptions();
        database.Db.Put("key"u8, "value"u8, writeOptions: options);

        options.Dispose();
        options.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(options.Handle).IsEqualTo(nint.Zero);
            await Assert.That(database.Db.Get("key")).IsEqualTo("value");
        }
    }

    [Test]
    public async Task FlushOptions_Dispose_IsIdempotent()
    {
        var options = new FlushOptions();

        options.Dispose();
        options.Dispose();

        await Assert.That(options.Handle).IsEqualTo(nint.Zero);
    }

    /// <remarks>
    /// Databases copy their options at open, so disposing the wrapper afterwards must leave the
    /// database fully usable.
    /// </remarks>
    [Test]
    public async Task DbOptions_CanBeDisposedOnceTheDatabaseIsOpen()
    {
        using var directory = new TempDirectory();
        var options = new DbOptions().SetCreateIfMissing();
        using var db = RocksDb.Open(options, directory.Reserve("db"));

        options.Dispose();

        db.Put("key", "value");
        await Assert.That(db.Get("key")).IsEqualTo("value");
    }

    private static unsafe uint MaxSubcompactions(DbOptions options)
        => RocksDbNative.rocksdb_options_get_max_subcompactions((rocksdb_options_t*)options.Handle);

    private static unsafe byte LowPriority(WriteOptions options)
        => RocksDbNative.rocksdb_writeoptions_get_low_pri((rocksdb_writeoptions_t*)options.Handle);

    [Test]
    public async Task DbOptions_SetMaxSubcompactions_ReachesTheNativeOptions()
    {
        using var options = new DbOptions().SetMaxSubcompactions(4);

        await Assert.That(MaxSubcompactions(options)).IsEqualTo(4u);
    }

    [Test]
    public async Task DbOptions_GetNumLevels_ReturnsTheRocksDbDefault()
    {
        using var options = new DbOptions();

        await Assert.That(options.GetNumLevels()).IsEqualTo(7);
    }

    [Test]
    public async Task DbOptions_ApplyFromString_AppliesRecognizedOptions()
    {
        using var options = new DbOptions().ApplyFromString("max_subcompactions=3");

        await Assert.That(MaxSubcompactions(options)).IsEqualTo(3u);
    }

    [Test]
    public async Task DbOptions_ApplyFromString_ThrowsOnAnUnknownOption()
    {
        using var options = new DbOptions();

        await Assert.That(() => options.ApplyFromString("no_such_option=1"))
            .Throws<RocksDbNativeException>();
    }

    [Test]
    public async Task DbOptions_SetRowCache_LeavesReadsCorrectEvenAfterTheWrapperIsDisposed()
    {
        using var cache = Cache.CreateLru(8 * 1024 * 1024);
        using var options = new DbOptions().SetCreateIfMissing().SetRowCache(cache);

        using var database = TestDatabase.Create(options);
        database.Db.Put("key", "value");

        // rocksdb holds its own reference, so the wrapper can go while the database is open.
        cache.Dispose();

        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
    }

    [Test]
    public async Task WriteOptions_SetLowPriority_ReachesTheNativeWriteOptions()
    {
        using var options = new WriteOptions();

        options.SetLowPriority(true);
        await Assert.That(LowPriority(options)).IsEqualTo((byte)1);

        options.SetLowPriority(false);
        await Assert.That(LowPriority(options)).IsEqualTo((byte)0);
    }

    [Test]
    public async Task Cache_CreateHyperClock_WorksAsABlockCache()
    {
        using var cache = Cache.CreateHyperClock(8 * 1024 * 1024);
        var tableOptions = new BlockBasedTableOptions().SetBlockCache(cache);
        using var options = new DbOptions().SetCreateIfMissing().SetBlockBasedTableFactory(tableOptions);

        using var database = TestDatabase.Create(options);
        database.Db.Put("key", "value");
        using var flushOptions = new FlushOptions().SetWaitForFlush(true);
        database.Db.Flush(flushOptions);

        // The read has to come off disk for the block to reach the cache.
        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
        await Assert.That(cache.GetUsage()).IsGreaterThan(0ul);

        // rocksdb holds its own reference, so the wrapper can go while the database is open.
        cache.Dispose();

        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
    }

    [Test]
    public async Task Cache_Dispose_IsIdempotent()
    {
        var cache = Cache.CreateLru(1024);

        cache.Dispose();
        cache.Dispose();

        await Assert.That(cache.Handle).IsEqualTo(nint.Zero);
    }
}
