// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class ReadOptions : IDisposable
{
    private nint _handle;
    private nint iterateLowerBound;
    private nint iterateUpperBound;

    public ReadOptions()
    {
        _handle = (nint)rocksdb_readoptions_create();
    }

    public nint Handle
    {
        get => _handle;
        protected set => _handle = value;
    }

    ~ReadOptions() => ReleaseHandle();

    /// <summary>Destroys the native options deterministically; the finalizer is only a backstop.</summary>
    /// <remarks>
    /// Iterators read these options and any iterate bounds in place for their whole lifetime, so
    /// dispose only after every iterator and read using these options is done.
    /// </remarks>
    public void Dispose()
    {
        ReleaseHandle();
        GC.SuppressFinalize(this);
    }

    // The handle is taken away rather than tested and then cleared, so that two callers disposing
    // at once cannot both reach the destroy. The bounds go with it, freed only by whoever won the
    // handle, so that a loser cannot free them while the winner is still in the native destroy.
    private void ReleaseHandle()
    {
        nint handle = Interlocked.Exchange(ref _handle, nint.Zero);

        if (handle != nint.Zero)
        {
            rocksdb_readoptions_destroy(RocksDbInterop.ReadOptions(handle));
            FreeBound(ref iterateLowerBound);
            FreeBound(ref iterateUpperBound);
        }
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

    public ReadOptions SetSnapshot(Snapshot snapshot)
    {
        rocksdb_readoptions_set_snapshot(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Snapshot(snapshot.Handle));
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
