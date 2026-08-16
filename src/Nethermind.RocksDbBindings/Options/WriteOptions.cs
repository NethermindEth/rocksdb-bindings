// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class WriteOptions : IDisposable
{
    public WriteOptions()
    {
        Handle = (nint)rocksdb_writeoptions_create();
    }

    public nint Handle { get; protected set; }

    ~WriteOptions() => ReleaseHandle();

    /// <summary>Destroys the native options deterministically; the finalizer is only a backstop.</summary>
    /// <remarks>Dispose only after every write using these options has returned.</remarks>
    public void Dispose()
    {
        ReleaseHandle();
        GC.SuppressFinalize(this);
    }

    private void ReleaseHandle()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_writeoptions_destroy(RocksDbInterop.WriteOptions(Handle));
            Handle = nint.Zero;
        }
    }

    public WriteOptions SetSync(bool value)
    {
        rocksdb_writeoptions_set_sync(RocksDbInterop.WriteOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }

    public WriteOptions DisableWal(int disable)
    {
        rocksdb_writeoptions_disable_WAL(RocksDbInterop.WriteOptions(Handle), disable);
        return this;
    }

    /// <summary>
    /// Marks writes with these options as low priority: they may be throttled or delayed in
    /// favor of regular writes when compaction falls behind.
    /// </summary>
    public WriteOptions SetLowPriority(bool value)
    {
        rocksdb_writeoptions_set_low_pri(RocksDbInterop.WriteOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }


}
