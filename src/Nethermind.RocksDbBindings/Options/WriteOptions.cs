// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;

namespace Nethermind.RocksDbBindings;

public unsafe class WriteOptions
{
    public WriteOptions()
    {
        Handle = (IntPtr)RocksDbNative.rocksdb_writeoptions_create();
    }

    public IntPtr Handle { get; protected set; }

    ~WriteOptions()
    {
        if (Handle != IntPtr.Zero)
        {
#if !NODESTROY
            RocksDbNative.rocksdb_writeoptions_destroy(RocksDbInterop.WriteOptions(Handle));
#endif
            Handle = IntPtr.Zero;
        }
    }

    public WriteOptions SetSync(bool value)
    {
        RocksDbNative.rocksdb_writeoptions_set_sync(RocksDbInterop.WriteOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }

    public WriteOptions DisableWal(int disable)
    {
        RocksDbNative.rocksdb_writeoptions_disable_WAL(RocksDbInterop.WriteOptions(Handle), disable);
        return this;
    }


}
