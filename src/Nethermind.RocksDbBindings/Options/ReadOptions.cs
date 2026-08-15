// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;
using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class ReadOptions
{
    private nint iterateLowerBound;
    private nint iterateUpperBound;

    public ReadOptions()
    {
        Handle = (nint)rocksdb_readoptions_create();
    }

    public nint Handle { get; protected set; }

    ~ReadOptions()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_readoptions_destroy(RocksDbInterop.ReadOptions(Handle));
            NativeMemory.Free((void*)iterateLowerBound);
            NativeMemory.Free((void*)iterateUpperBound);
            Handle = nint.Zero;
        }
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
    /// Enforce that the iterator only iterates over the same prefix as the seek.
    /// This option is effective only for prefix seeks, i.e. prefix_extractor is
    /// non-null for the column family and total_order_seek is false.  Unlike
    /// iterate_upper_bound, prefix_same_as_start only works within a prefix
    /// but in both directions.
    /// Default: false
    /// </summary>
    /// <param name="prefixSameAsStart"></param>
    /// <returns></returns>
    public ReadOptions SetPrefixSameAsStart(bool prefixSameAsStart)
    {
        rocksdb_readoptions_set_prefix_same_as_start(RocksDbInterop.ReadOptions(Handle), RocksDbInterop.Bool(prefixSameAsStart));
        return this;
    }

    public unsafe ReadOptions SetIterateLowerBound(byte* key, ulong keylen)
    {
        nuint klen = (nuint)keylen;
        rocksdb_readoptions_set_iterate_lower_bound(RocksDbInterop.ReadOptions(Handle), (sbyte*)key, klen);
        return this;
    }

    public ReadOptions SetIterateLowerBound(byte[] key, ulong keyLen)
    {
        NativeMemory.Free((void*)iterateLowerBound);
        iterateLowerBound = (nint)NativeMemory.Alloc((nuint)key.Length);
        key.CopyTo(new Span<byte>((void*)iterateLowerBound, key.Length));
        nuint klen = (nuint)keyLen;
        rocksdb_readoptions_set_iterate_lower_bound(RocksDbInterop.ReadOptions(Handle), (sbyte*)iterateLowerBound, klen);
        return this;
    }

    public ReadOptions SetIterateLowerBound(byte[] key) => SetIterateLowerBound(key, (ulong)key.GetLongLength(0));

    public unsafe ReadOptions SetIterateLowerBound(string stringKey, Encoding? encoding = null)
    {
        var key = (encoding ?? Encoding.UTF8).GetBytes(stringKey);
        return SetIterateLowerBound(key);
    }

    public unsafe ReadOptions SetIterateUpperBound(byte* key, ulong keylen)
    {
        nuint klen = (nuint)keylen;
        rocksdb_readoptions_set_iterate_upper_bound(RocksDbInterop.ReadOptions(Handle), (sbyte*)key, klen);
        return this;
    }

    public ReadOptions SetIterateUpperBound(byte[] key, ulong keyLen)
    {
        NativeMemory.Free((void*)iterateUpperBound);
        iterateUpperBound = (nint)NativeMemory.Alloc((nuint)key.Length);
        key.CopyTo(new Span<byte>((void*)iterateUpperBound, key.Length));
        nuint klen = (nuint)keyLen;
        rocksdb_readoptions_set_iterate_upper_bound(RocksDbInterop.ReadOptions(Handle), (sbyte*)iterateUpperBound, klen);
        return this;
    }

    public ReadOptions SetIterateUpperBound(byte[] key) => SetIterateUpperBound(key, (ulong)key.GetLongLength(0));

    public unsafe ReadOptions SetIterateUpperBound(string stringKey, Encoding? encoding = null)
    {
        var key = (encoding ?? Encoding.UTF8).GetBytes(stringKey);
        return SetIterateUpperBound(key);
    }

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
