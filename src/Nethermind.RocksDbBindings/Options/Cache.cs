// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class Cache
{
    public nint Handle { get; protected set; }

    private Cache(nint handle)
    {
        Handle = handle;
    }

    ~Cache()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_cache_destroy(RocksDbInterop.Cache(Handle));
            Handle = nint.Zero;
        }
    }

    public static Cache CreateLru(ulong capacity)
    {
        nint handle = (nint)rocksdb_cache_create_lru((nuint)capacity);
        return new Cache(handle);
    }

    public Cache SetCapacity(ulong capacity)
    {
        rocksdb_cache_set_capacity(RocksDbInterop.Cache(Handle), (nuint)capacity);
        return this;
    }

    public ulong GetUsage() => rocksdb_cache_get_usage(RocksDbInterop.Cache(Handle));

    public ulong GetPinnedUsage() => rocksdb_cache_get_pinned_usage(RocksDbInterop.Cache(Handle));
}
