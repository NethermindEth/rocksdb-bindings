// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>Controls how a single write reaches the database.</summary>
/// <remarks>
/// Each <c>Set</c> method is paired with a <c>Get</c> that asks RocksDB for the option rather than
/// reporting a managed copy of the last value set. RocksDB has nine write options; three are
/// wrapped here. Dispose only after every write using these options has returned.
/// </remarks>
public sealed unsafe class WriteOptions : NativeOptions
{
    public WriteOptions() : base(new WriteOptionsHandle((nint)rocksdb_writeoptions_create())) { }

    /// <summary>
    /// Flushes the write-ahead log to disk before a write returns, so the write survives losing
    /// the machine rather than only losing the process. Off by default.
    /// </summary>
    public WriteOptions SetSync(bool value = true)
    {
        using var lease = Lease(out nint handle);

        rocksdb_writeoptions_set_sync(RocksDbInterop.WriteOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    /// <summary>Reports whether <see cref="SetSync"/> is in effect.</summary>
    public bool GetSync()
    {
        using var lease = Lease(out nint handle);

        return rocksdb_writeoptions_get_sync(RocksDbInterop.WriteOptions(handle)) != 0;
    }

    /// <summary>
    /// Skips the write-ahead log entirely, so writes live only in the memtable until it is
    /// flushed and are lost if the process ends before then.
    /// </summary>
    public WriteOptions SetDisableWal(bool value = true)
    {
        using var lease = Lease(out nint handle);

        rocksdb_writeoptions_disable_WAL(RocksDbInterop.WriteOptions(handle), value ? 1 : 0);
        return this;
    }

    /// <summary>Reports whether <see cref="SetDisableWal"/> is in effect.</summary>
    public bool GetDisableWal()
    {
        using var lease = Lease(out nint handle);

        return rocksdb_writeoptions_get_disable_WAL(RocksDbInterop.WriteOptions(handle)) != 0;
    }

    /// <summary>
    /// Marks writes with these options as low priority: they may be throttled or delayed in
    /// favor of regular writes when compaction falls behind.
    /// </summary>
    public WriteOptions SetLowPriority(bool value = true)
    {
        using var lease = Lease(out nint handle);

        rocksdb_writeoptions_set_low_pri(RocksDbInterop.WriteOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    /// <summary>Reports whether <see cref="SetLowPriority"/> is in effect.</summary>
    public bool GetLowPriority()
    {
        using var lease = Lease(out nint handle);

        return rocksdb_writeoptions_get_low_pri(RocksDbInterop.WriteOptions(handle)) != 0;
    }
}
