// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class Cache
{
    public IntPtr Handle { get; protected set; }

    private Cache(IntPtr handle)
    {
        this.Handle = handle;
    }

    ~Cache()
    {
        if (Handle != IntPtr.Zero)
        {
            RocksDbNative.rocksdb_cache_destroy(RocksDbInterop.Cache(Handle));
            Handle = IntPtr.Zero;
        }
    }

    public static Cache CreateLru(ulong capacity)
    {
        IntPtr handle = (IntPtr)RocksDbNative.rocksdb_cache_create_lru((nuint)capacity);
        return new Cache(handle);
    }

    public Cache SetCapacity(ulong capacity)
    {
        RocksDbNative.rocksdb_cache_set_capacity(RocksDbInterop.Cache(Handle), (nuint)capacity);
        return this;
    }

    public ulong GetUsage()
    {
        return (ulong)RocksDbNative.rocksdb_cache_get_usage(RocksDbInterop.Cache(Handle));
    }

    public ulong GetPinnedUsage()
    {
        return (ulong)RocksDbNative.rocksdb_cache_get_pinned_usage(RocksDbInterop.Cache(Handle));
    }
}
