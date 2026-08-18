// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <remarks>
/// An iterator takes its own reference to these options, so disposing them under a live iterator
/// defers the release until the iterator is done rather than taking it away. It also captures
/// the snapshot they carried when it was created, so a later <see cref="SetSnapshot"/> or
/// <see cref="ClearSnapshot"/> leaves it reading the one it started with. The iterate bounds are
/// the exception: an iterator reads the buffers these options own, and the setters overwrite them
/// in place, so bounds must not be changed while an iterator created from them is alive.
/// </remarks>
public sealed unsafe class ReadOptions : NativeOptions
{
    private readonly ReadOptionsHandle _handle;

    public ReadOptions() : this(new ReadOptionsHandle((nint)rocksdb_readoptions_create())) { }

    private ReadOptions(ReadOptionsHandle handle) : base(handle) => _handle = handle;

    // The snapshot these options currently point at, captured by an iterator when it is created.
    internal Snapshot? Snapshot => _handle.Snapshot;

    private static nint AllocateCopy(ReadOnlySpan<byte> key)
    {
        var buffer = (byte*)NativeMemory.Alloc((nuint)key.Length);
        key.CopyTo(new Span<byte>(buffer, key.Length));
        return (nint)buffer;
    }

    public ReadOptions SetBackgroundPurgeOnIteratorCleanup(bool value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_background_purge_on_iterator_cleanup(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetVerifyChecksums(bool value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_verify_checksums(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetFillCache(bool value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_fill_cache(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    /// <summary>Reads from a fixed view of the database rather than from its current state.</summary>
    /// <remarks>
    /// RocksDB stores a non-owning pointer to the snapshot, so the wrapper is held here for as
    /// long as these options carry it — releasing it would leave them pointing at a freed snapshot
    /// and let the database it kept open close. Stop reading from it with
    /// <see cref="ClearSnapshot"/> or by setting another snapshot, not by disposing it.
    /// </remarks>
    /// <exception cref="ObjectDisposedException"><paramref name="snapshot"/> has been disposed.</exception>
    public ReadOptions SetSnapshot(Snapshot snapshot)
    {
        using var lease = Lease(out nint handle);

        nint snapshotHandle = snapshot.Handle;
        ObjectDisposedException.ThrowIf(snapshotHandle == nint.Zero, snapshot);

        // Stored before the call, so the lease on the handle is what keeps the snapshot reachable
        // through it.
        _handle.Snapshot = snapshot;
        rocksdb_readoptions_set_snapshot(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Snapshot(snapshotHandle));
        return this;
    }

    /// <summary>Goes back to reading the current state, and lets go of any snapshot set.</summary>
    public ReadOptions ClearSnapshot()
    {
        using var lease = Lease(out nint handle);

        _handle.Snapshot = null;
        rocksdb_readoptions_set_snapshot(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Snapshot(nint.Zero));
        return this;
    }

    /// <summary>
    /// Confines iteration, in both directions, to the prefix the seek started in. Requires a
    /// prefix extractor on the column family and has no effect under a total-order seek.
    /// </summary>
    public ReadOptions SetPrefixSameAsStart(bool prefixSameAsStart)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_prefix_same_as_start(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(prefixSameAsStart));
        return this;
    }

    /// <summary>
    /// Sets the inclusive lower bound for iteration, copying it into memory owned and freed by
    /// these options.
    /// </summary>
    /// <remarks>Do not change bounds while an iterator created from these options is alive.</remarks>
    public ReadOptions SetIterateLowerBound(ReadOnlySpan<byte> key)
    {
        using var lease = Lease(out nint handle);

        var buffer = AllocateCopy(key);
        rocksdb_readoptions_set_iterate_lower_bound(RocksDbInterop.ReadOptions(handle), (sbyte*)buffer, (nuint)key.Length);
        _handle.InstallLowerBound(buffer);
        return this;
    }

    /// <summary>
    /// Sets the exclusive upper bound for iteration, copying it into memory owned and freed by
    /// these options.
    /// </summary>
    /// <remarks>Do not change bounds while an iterator created from these options is alive.</remarks>
    public ReadOptions SetIterateUpperBound(ReadOnlySpan<byte> key)
    {
        using var lease = Lease(out nint handle);

        var buffer = AllocateCopy(key);
        rocksdb_readoptions_set_iterate_upper_bound(RocksDbInterop.ReadOptions(handle), (sbyte*)buffer, (nuint)key.Length);
        _handle.InstallUpperBound(buffer);
        return this;
    }

    /// <summary>
    /// Sets the inclusive lower and exclusive upper bounds for iteration, copying both into
    /// memory owned and freed by these options.
    /// </summary>
    /// <remarks>Do not change bounds while an iterator created from these options is alive.</remarks>
    public ReadOptions SetIterateBounds(ReadOnlySpan<byte> lowerBound, ReadOnlySpan<byte> upperBound)
        => SetIterateLowerBound(lowerBound).SetIterateUpperBound(upperBound);

    public ReadOptions SetReadTier(int value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_read_tier(RocksDbInterop.ReadOptions(handle), value);
        return this;
    }

    public ReadOptions SetTailing(bool value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_tailing(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetReadaheadSize(ulong size)
    {
        using var lease = Lease(out nint handle);

        nuint readaheadSize = (nuint)size;
        rocksdb_readoptions_set_readahead_size(RocksDbInterop.ReadOptions(handle), readaheadSize);
        return this;
    }
    public ReadOptions SetAutoReadaheadSize(bool value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_auto_readahead_size(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }
    public ReadOptions SetAsyncIO(bool value)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_async_io(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetPinData(bool enable)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_pin_data(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(enable));
        return this;
    }

    public ReadOptions SetTotalOrderSeek(bool enable)
    {
        using var lease = Lease(out nint handle);

        rocksdb_readoptions_set_total_order_seek(RocksDbInterop.ReadOptions(handle), RocksDbInterop.Bool(enable));
        return this;
    }
}
