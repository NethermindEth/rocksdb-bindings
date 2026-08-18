// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <remarks>
/// Iterators keep reading whatever these options point at — the iterate bounds, and the snapshot
/// if one is set — for their whole lifetime, so dispose only after every iterator and read using
/// these options is done.
/// </remarks>
public sealed unsafe class ReadOptions : NativeOptions
{
    private nint iterateLowerBound;
    private nint iterateUpperBound;

    // Held because RocksDB keeps only a bare pointer to it; see SetSnapshot.
    private Snapshot? Snapshot { get; set; }

    public ReadOptions() : base((nint)rocksdb_readoptions_create()) { }

    protected override void DestroyHandle(nint handle)
    {
        rocksdb_readoptions_destroy(RocksDbInterop.ReadOptions(handle));

        // Only after the destroy, and only here, so that a second disposer cannot free what this
        // one is still handing to RocksDB. The options no longer carry the snapshot's pointer, so
        // nothing needs to keep the snapshot, or the database lease behind it, alive.
        FreeBound(ref iterateLowerBound);
        FreeBound(ref iterateUpperBound);
        Snapshot = null;
    }

    private static void FreeBound(ref nint bound) => NativeMemory.Free((void*)Interlocked.Exchange(ref bound, nint.Zero));

    private static nint AllocateCopy(ReadOnlySpan<byte> key)
    {
        var buffer = (byte*)NativeMemory.Alloc((nuint)key.Length);
        key.CopyTo(new Span<byte>(buffer, key.Length));
        return (nint)buffer;
    }

    // The native setter stores the pointer without copying, so the old buffer must stay alive
    // until the new one is installed: allocate, point RocksDB at it, then free the old one. A
    // failed allocation thus leaves both the field and RocksDB on the still-valid old buffer.
    private static void InstallBound(ref nint bound, nint buffer)
    {
        var previous = bound;
        bound = buffer;
        NativeMemory.Free((void*)previous);
    }

    public ReadOptions SetBackgroundPurgeOnIteratorCleanup(bool value)
    {
        rocksdb_readoptions_set_background_purge_on_iterator_cleanup(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetVerifyChecksums(bool value)
    {
        rocksdb_readoptions_set_verify_checksums(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetFillCache(bool value)
    {
        rocksdb_readoptions_set_fill_cache(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(value));
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
        nint snapshotHandle = snapshot.Handle;
        ObjectDisposedException.ThrowIf(snapshotHandle == nint.Zero, snapshot);

        Snapshot = snapshot;
        rocksdb_readoptions_set_snapshot(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Snapshot(snapshotHandle));
        // The field alone is not enough: it is reachable only through this object, which the
        // fluent return does not keep alive.
        GC.KeepAlive(snapshot);
        return this;
    }

    /// <summary>Goes back to reading the current state, and lets go of any snapshot set.</summary>
    public ReadOptions ClearSnapshot()
    {
        Snapshot = null;
        rocksdb_readoptions_set_snapshot(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Snapshot(nint.Zero));
        return this;
    }

    /// <summary>
    /// Confines iteration, in both directions, to the prefix the seek started in. Requires a
    /// prefix extractor on the column family and has no effect under a total-order seek.
    /// </summary>
    public ReadOptions SetPrefixSameAsStart(bool prefixSameAsStart)
    {
        rocksdb_readoptions_set_prefix_same_as_start(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(prefixSameAsStart));
        return this;
    }

    /// <summary>
    /// Sets the inclusive lower bound for iteration, copying it into memory owned and freed by
    /// these options.
    /// </summary>
    /// <remarks>Do not change bounds while an iterator created from these options is alive.</remarks>
    public ReadOptions SetIterateLowerBound(ReadOnlySpan<byte> key)
    {
        var buffer = AllocateCopy(key);
        rocksdb_readoptions_set_iterate_lower_bound(RocksDbInterop.ReadOptions(Handle), (sbyte*)buffer, (nuint)key.Length);
        InstallBound(ref iterateLowerBound, buffer);
        return this;
    }

    /// <summary>
    /// Sets the exclusive upper bound for iteration, copying it into memory owned and freed by
    /// these options.
    /// </summary>
    /// <remarks>Do not change bounds while an iterator created from these options is alive.</remarks>
    public ReadOptions SetIterateUpperBound(ReadOnlySpan<byte> key)
    {
        var buffer = AllocateCopy(key);
        rocksdb_readoptions_set_iterate_upper_bound(RocksDbInterop.ReadOptions(Handle), (sbyte*)buffer, (nuint)key.Length);
        InstallBound(ref iterateUpperBound, buffer);
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
        rocksdb_readoptions_set_read_tier(RocksDbInterop.ReadOptions(Handle), value);
        return this;
    }

    public ReadOptions SetTailing(bool value)
    {
        rocksdb_readoptions_set_tailing(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetReadaheadSize(ulong size)
    {
        nuint readaheadSize = (nuint)size;
        rocksdb_readoptions_set_readahead_size(RocksDbInterop.ReadOptions(Handle), readaheadSize);
        return this;
    }
    public ReadOptions SetAutoReadaheadSize(bool value)
    {
        rocksdb_readoptions_set_auto_readahead_size(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }
    public ReadOptions SetAsyncIO(bool value)
    {
        rocksdb_readoptions_set_async_io(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(value));
        return this;
    }

    public ReadOptions SetPinData(bool enable)
    {
        rocksdb_readoptions_set_pin_data(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(enable));
        return this;
    }

    public ReadOptions SetTotalOrderSeek(bool enable)
    {
        rocksdb_readoptions_set_total_order_seek(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(enable));
        return this;
    }
}
