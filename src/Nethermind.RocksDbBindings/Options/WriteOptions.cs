// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;

namespace Nethermind.RocksDbBindings;

public unsafe class WriteOptions
{
    public WriteOptions()
    {
        Handle = (nint)RocksDbNative.rocksdb_writeoptions_create();
    }

    public nint Handle { get; protected set; }

    ~WriteOptions()
    {
        if (Handle != nint.Zero)
        {
            RocksDbNative.rocksdb_writeoptions_destroy(RocksDbInterop.WriteOptions(Handle));
            Handle = nint.Zero;
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
