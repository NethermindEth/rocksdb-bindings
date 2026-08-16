// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class Cache : IDisposable
{
    public nint Handle { get; protected set; }

    private Cache(nint handle)
    {
        Handle = handle;
    }

    ~Cache() => ReleaseHandle();

    /// <summary>Destroys the native cache wrapper deterministically; the finalizer is only a backstop.</summary>
    /// <remarks>
    /// rocksdb holds its own reference on the cache once it is attached to options, so disposing
    /// this wrapper does not free memory still in use by an open database.
    /// </remarks>
    public void Dispose()
    {
        ReleaseHandle();
        GC.SuppressFinalize(this);
    }

    private void ReleaseHandle()
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

    /// <summary>
    /// Creates a HyperClockCache, a lock-free block cache that outperforms the LRU cache under
    /// high concurrency.
    /// </summary>
    /// <param name="capacity">The cache capacity in bytes.</param>
    /// <param name="estimatedEntryCharge">
    /// The estimated size of a cache entry in bytes, or 0 to let rocksdb size and adjust
    /// automatically.
    /// </param>
    public static Cache CreateHyperClock(ulong capacity, ulong estimatedEntryCharge = 0)
    {
        nint handle = (nint)rocksdb_cache_create_hyper_clock((nuint)capacity, (nuint)estimatedEntryCharge);
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
