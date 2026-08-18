// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <inheritdoc/>
public class DbOptions : Options<DbOptions>
{
    // Read by an opening database, which takes its own reference: this slot can be pointed at
    // another environment afterwards, and that must not unroot the one already in use.
    internal Env? Env { get; private set; }

    /// <summary>
    /// Sets the environment the database runs its file and thread operations through.
    /// </summary>
    /// <remarks>
    /// Only a database's own options carry one: RocksDB builds its <c>DBOptions</c> from these
    /// alone and drops the field from every column family's options, which is why this setter is
    /// here rather than on <see cref="Options{T}"/>. It is also the one option a database does
    /// not copy — RocksDB keeps a bare pointer to the environment — so the wrapper is held here,
    /// and held again by any database opened from these options. See
    /// <see cref="Nethermind.RocksDbBindings.Env"/> for what that still leaves to the caller.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"><paramref name="env"/> has been disposed.</exception>
    public DbOptions SetEnv(Env env)
    {
        env.AttachTo(Handle);
        Env = env;
        return this;
    }
}

/// <summary>
/// Configures a database or column family. Each setter maps to the identically named RocksDB
/// option, whose meaning, default and tuning advice are documented by RocksDB itself; only
/// behaviour specific to these bindings is described here.
/// </summary>
/// <remarks>
/// Databases copy these options when they are opened, so disposing after the open call returns
/// is safe even while the database is in use.
/// </remarks>
public unsafe abstract partial class Options<T> : OptionsHandle where T : Options<T>
{
    internal bool CreateIfMissing { get; set; }

    /// <summary>
    /// Sizes the background thread pools for the given total number of threads; the core count
    /// is a reasonable starting point.
    /// </summary>
    public T IncreaseParallelism(int totalThreads)
    {
        rocksdb_options_increase_parallelism(RocksDbInterop.Options(Handle), totalThreads);
        return (T)this;
    }

    /// <summary>
    /// Creates the database instead of failing the open when it does not exist yet.
    /// </summary>
    public T SetCreateIfMissing(bool value = true)
    {
        // Remembered so that opening can create the column families too.
        CreateIfMissing = value;
        rocksdb_options_set_create_if_missing(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Creates the column families named at open that do not exist yet.
    /// </summary>
    public T SetCreateMissingColumnFamilies(bool value = true)
    {
        rocksdb_options_set_create_missing_column_families(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Fails the open when the database already exists.
    /// </summary>
    public T SetErrorIfExists(bool value = true)
    {
        rocksdb_options_set_error_if_exists(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Checks the database's own data aggressively and stops early when it looks damaged.
    /// </summary>
    public T SetParanoidChecks(bool value = true)
    {
        rocksdb_options_set_paranoid_checks(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Sends the database's log output to a custom logger.
    /// </summary>
    public T SetInfoLog(nint logger)
    {
        rocksdb_options_set_info_log(RocksDbInterop.Options(Handle), RocksDbInterop.Logger(logger));
        return (T)this;
    }

    /// <summary>
    /// Caps the file handles kept for open SST files; -1 keeps every file open.
    /// </summary>
    public T SetMaxOpenFiles(int value)
    {
        rocksdb_options_set_max_open_files(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Threads used to open files while the database starts.
    /// </summary>
    public T SetMaxFileOpeningThreads(int value)
    {
        rocksdb_options_set_max_file_opening_threads(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Total size of the write-ahead logs that forces a flush of the memtables holding the
    /// oldest data.
    /// </summary>
    public T SetMaxTotalWalSize(ulong n)
    {
        rocksdb_options_set_max_total_wal_size(RocksDbInterop.Options(Handle), (nuint)n);
        return (T)this;
    }

    /// <summary>
    /// How much of a truncated or corrupt write-ahead log to tolerate when recovering.
    /// </summary>
    public T SetWalRecoveryMode(Recovery mode)
    {
        rocksdb_options_set_wal_recovery_mode(RocksDbInterop.Options(Handle), (int)mode);
        return (T)this;
    }

    /// <summary>
    /// Selects the algorithm write-ahead log records are compressed with.
    /// </summary>
    public T SetWalCompression(Compression compression)
    {
        rocksdb_options_set_wal_compression(RocksDbInterop.Options(Handle), (int)compression);
        return (T)this;
    }

    /// <summary>
    /// Starts collecting the counters that <see cref="GetStatisticsString"/> reports.
    /// </summary>
    public T EnableStatistics()
    {
        rocksdb_options_enable_statistics(RocksDbInterop.Options(Handle));
        return (T)this;
    }

    /// <summary>
    /// Skips recomputing file statistics at open, which shortens the open of a large database.
    /// </summary>
    public T SkipStatsUpdateOnOpen(bool value = true)
    {
        rocksdb_options_set_skip_stats_update_on_db_open(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// The counters collected since <see cref="EnableStatistics"/>, formatted by RocksDB, or null
    /// when statistics are off.
    /// </summary>
    public string? GetStatisticsString()
    {
        var statistics = RocksDbInterop.NullTerminatedStringAndFree(rocksdb_options_statistics_get_string(RocksDbInterop.Options(Handle)));
        // Without this, the finalizer could destroy the options mid-call.
        GC.KeepAlive(this);
        return statistics;
    }

    /// <summary>
    /// Threads available to run compactions in the background.
    /// </summary>
    public T SetMaxBackgroundCompactions(int value)
    {
        rocksdb_options_set_max_background_compactions(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// The maximum number of threads that will concurrently perform a compaction job by breaking
    /// it into multiple, smaller ones that are run simultaneously.
    /// </summary>
    public T SetMaxSubcompactions(uint value)
    {
        rocksdb_options_set_max_subcompactions(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// A global cache for table-level rows. RocksDB holds its own reference on the cache, so it
    /// may be disposed once no options wrapper is being configured with it.
    /// </summary>
    public T SetRowCache(Cache cache)
    {
        rocksdb_options_set_row_cache(RocksDbInterop.Options(Handle), RocksDbInterop.Cache(cache.Handle));
        // Without this, the cache's finalizer could destroy the handle mid-call.
        GC.KeepAlive(cache);
        return (T)this;
    }

    /// <summary>The number of levels in the LSM tree.</summary>
    public int GetNumLevels()
    {
        var levels = rocksdb_options_get_num_levels(RocksDbInterop.Options(Handle));
        // Without this, the finalizer could destroy the options mid-call.
        GC.KeepAlive(this);
        return levels;
    }

    /// <summary>
    /// Applies a RocksDB options string of <c>name=value</c> pairs separated by semicolons, as
    /// accepted by <c>GetOptionsFromString</c>, on top of the current options.
    /// </summary>
    /// <exception cref="RocksDbNativeException">The string contains an unknown or malformed option.</exception>
    public T ApplyFromString(string optionsString)
    {
        using var nativeString = new TransientUtf8String(optionsString);
        sbyte* errptr = null;
        rocksdb_get_options_from_string(RocksDbInterop.Options(Handle), (sbyte*)nativeString.Handle, RocksDbInterop.Options(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return (T)this;
    }

    /// <summary>
    /// Threads available to flush memtables in the background.
    /// </summary>
    public T SetMaxBackgroundFlushes(int value)
    {
        rocksdb_options_set_max_background_flushes(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Size at which the LOG file is rolled over.
    /// </summary>
    public T SetMaxLogFileSize(ulong value)
    {
        rocksdb_options_set_max_log_file_size(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Seconds after which the LOG file is rolled over.
    /// </summary>
    public T SetLogFileTimeToRoll(ulong value)
    {
        rocksdb_options_set_log_file_time_to_roll(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// How many rolled-over LOG files to keep.
    /// </summary>
    public T SetKeepLogFileNum(ulong value)
    {
        rocksdb_options_set_keep_log_file_num(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// How many write-ahead log files to reuse rather than create anew.
    /// </summary>
    public T SetRecycleLogFileNum(ulong value)
    {
        rocksdb_options_set_recycle_log_file_num(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Size at which the manifest is rolled over.
    /// </summary>
    public T SetMaxManifestFileSize(ulong value)
    {
        rocksdb_options_set_max_manifest_file_size(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Shards the table cache into 2^n parts, which reduces contention between readers.
    /// </summary>
    public T SetTableCacheNumShardbits(int value)
    {
        rocksdb_options_set_table_cache_numshardbits(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Syncs files with fsync rather than fdatasync when non-zero.
    /// </summary>
    public T SetUseFsync(int value)
    {
        rocksdb_options_set_use_fsync(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Directory for the LOG files; they sit beside the data when this is empty.
    /// </summary>
    public T SetDbLogDir(string value)
    {
        using (var safePath = new TransientUtf8String(value))
        {
            rocksdb_options_set_db_log_dir(RocksDbInterop.Options(Handle), (sbyte*)safePath.Handle);
        }
        LogPath = value;
        return (T)this;
    }

    /// <summary>
    /// Directory for the write-ahead logs; they sit beside the data when this is empty.
    /// </summary>
    public T SetWalDir(string value)
    {
        using (var safePath = new TransientUtf8String(value))
        {
            rocksdb_options_set_wal_dir(RocksDbInterop.Options(Handle), (sbyte*)safePath.Handle);
        }
        WalPath = value;
        return (T)this;
    }

    /// <summary>
    /// Age at which an archived write-ahead log is deleted.
    /// </summary>
    public T SetWalTtlSeconds(ulong value)
    {
        rocksdb_options_set_WAL_ttl_seconds(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Total size of archived write-ahead logs that starts deleting the oldest.
    /// </summary>
    public T SetWalSizeLimitMB(ulong value)
    {
        rocksdb_options_set_WAL_size_limit_MB(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Bytes preallocated for the manifest file.
    /// </summary>
    public T SetManifestPreallocationSize(ulong value)
    {
        rocksdb_options_set_manifest_preallocation_size(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Reads data files through memory mapping.
    /// </summary>
    public T SetAllowMmapReads(bool value)
    {
        rocksdb_options_set_allow_mmap_reads(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Writes data files through memory mapping.
    /// </summary>
    public T SetAllowMmapWrites(bool value)
    {
        rocksdb_options_set_allow_mmap_writes(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Bypasses the operating system's page cache when reading data files.
    /// </summary>
    public T SetUseDirectReads(bool value)
    {
        rocksdb_options_set_use_direct_reads(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Bypasses the operating system's page cache during flush and compaction.
    /// </summary>
    public T SetUseDirectIoForFlushAndCompaction(bool value)
    {
        rocksdb_options_set_use_direct_io_for_flush_and_compaction(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Closes the database's file descriptors in processes it spawns.
    /// </summary>
    public T SetIsFdCloseOnExec(bool value)
    {
        rocksdb_options_set_is_fd_close_on_exec(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// How often the collected statistics are written to the LOG file.
    /// </summary>
    public T SetStatsDumpPeriodSec(uint value)
    {
        rocksdb_options_set_stats_dump_period_sec(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Tells the operating system at open that the data files are read at random.
    /// </summary>
    public T SetAdviseRandomOnOpen(bool value)
    {
        rocksdb_options_set_advise_random_on_open(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Total memtable bytes across all column families before the largest one is flushed.
    /// </summary>
    public T SetDbWriteBufferSize(ulong size)
    {
        rocksdb_options_set_db_write_buffer_size(RocksDbInterop.Options(Handle), (nuint)size);
        return (T)this;
    }

    /// <summary>
    /// Spins briefly before blocking on an internal mutex, which pays off when they are held
    /// only for short stretches.
    /// </summary>
    public T SetUseAdaptiveMutex(bool value)
    {
        rocksdb_options_set_use_adaptive_mutex(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Asks the operating system to flush a file this often while it is being written, rather
    /// than in one burst at the end.
    /// </summary>
    public T SetBytesPerSync(ulong value)
    {
        rocksdb_options_set_bytes_per_sync(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Lets several threads write into one memtable. Supported only by some memtable types, and
    /// incompatible with <see cref="SetInplaceUpdateSupport"/>.
    /// </summary>
    public T SetAllowConcurrentMemtableWrite(bool value)
    {
        rocksdb_options_set_allow_concurrent_memtable_write(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Lets a writer thread spin before yielding, which helps when many threads write at once.
    /// </summary>
    public T SetEnableWriteThreadAdaptiveYield(bool value)
    {
        rocksdb_options_set_enable_write_thread_adaptive_yield(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// How often files left behind by finished compactions are swept up.
    /// </summary>
    public T SetDeleteObsoleteFilesPeriodMicros(ulong value)
    {
        rocksdb_options_set_delete_obsolete_files_period_micros(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Applies RocksDB's bulk-load preset, which holds compaction back so data can be written
    /// as fast as possible and organized afterwards.
    /// </summary>
    public T PrepareForBulkLoad()
    {
        rocksdb_options_prepare_for_bulk_load(RocksDbInterop.Options(Handle));
        return (T)this;
    }

}
