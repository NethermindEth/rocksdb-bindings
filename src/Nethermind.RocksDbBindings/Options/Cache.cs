// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe class Cache : IDisposable
{
    private readonly CacheHandle _handle;

    public nint Handle => _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    private Cache(nint handle)
    {
        _handle = new CacheHandle(handle);
    }

    /// <summary>Destroys the native cache wrapper; the SafeHandle's critical finalizer is the backstop.</summary>
    /// <remarks>
    /// RocksDB holds its own reference on the cache once it is attached to options, so disposing
    /// this wrapper does not free memory still in use by an open database.
    /// </remarks>
    public void Dispose() => _handle.Dispose();

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
    /// The estimated size of a cache entry in bytes, or 0 to let RocksDB size and adjust
    /// automatically.
    /// </param>
    public static Cache CreateHyperClock(ulong capacity, ulong estimatedEntryCharge = 0)
    {
        nint handle = (nint)rocksdb_cache_create_hyper_clock((nuint)capacity, (nuint)estimatedEntryCharge);
        return new Cache(handle);
    }

    public Cache SetCapacity(ulong capacity)
    {
        using var lease = new HandleLease(_handle);
        rocksdb_cache_set_capacity(RocksDbInterop.Cache(_handle.DangerousGetHandle()), (nuint)capacity);
        return this;
    }

    public ulong GetUsage()
    {
        using var lease = new HandleLease(_handle);
        return rocksdb_cache_get_usage(RocksDbInterop.Cache(_handle.DangerousGetHandle()));
    }

    public ulong GetPinnedUsage()
    {
        using var lease = new HandleLease(_handle);
        return rocksdb_cache_get_pinned_usage(RocksDbInterop.Cache(_handle.DangerousGetHandle()));
    }
}
