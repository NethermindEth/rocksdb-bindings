// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Dynamic;

namespace Nethermind.RocksDbBindings;

public unsafe class BlockBasedTableOptions
{
    public nint Handle { get; protected set; }

    // The following exists only to retain a reference to those types which are used in-place by rocksdb
    // and not copied (or reference things that are used in-place).  The idea is to have managed references
    // track the behavior of the unmanaged reference as much as possible.  This prevents access violations
    // when the garbage collector cleans up the last managed reference
    internal dynamic References { get; } = new ExpandoObject();

    public BlockBasedTableOptions()
    {
        this.Handle = (nint)RocksDbNative.rocksdb_block_based_options_create();
    }

    ~BlockBasedTableOptions()
    {
        if (Handle != nint.Zero)
        {
            RocksDbNative.rocksdb_block_based_options_destroy(RocksDbInterop.BlockBasedTableOptions(Handle));
            Handle = nint.Zero;
        }
    }

    public BlockBasedTableOptions SetBlockSize(ulong blockSize)
    {
        RocksDbNative.rocksdb_block_based_options_set_block_size(RocksDbInterop.BlockBasedTableOptions(Handle), (nuint)blockSize);
        return this;
    }

    public BlockBasedTableOptions SetBlockSizeDeviation(int blockSizeDeviation)
    {
        RocksDbNative.rocksdb_block_based_options_set_block_size_deviation(RocksDbInterop.BlockBasedTableOptions(Handle), blockSizeDeviation);
        return this;
    }

    public BlockBasedTableOptions SetBlockRestartInterval(int blockRestartInterval)
    {
        RocksDbNative.rocksdb_block_based_options_set_block_restart_interval(RocksDbInterop.BlockBasedTableOptions(Handle), blockRestartInterval);
        return this;
    }

    public BlockBasedTableOptions SetFilterPolicy(nint filterPolicy)
    {
        RocksDbNative.rocksdb_block_based_options_set_filter_policy(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.FilterPolicy(filterPolicy));
        return this;
    }

    public BlockBasedTableOptions SetFilterPolicy(BloomFilterPolicy filterPolicy)
    {
        // store a managed reference to prevent garbage collection
        References.FilterPolicy = filterPolicy;
        RocksDbNative.rocksdb_block_based_options_set_filter_policy(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.FilterPolicy(filterPolicy.Handle));
        return this;
    }

    public BlockBasedTableOptions SetNoBlockCache(bool noBlockCache)
    {
        RocksDbNative.rocksdb_block_based_options_set_no_block_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(noBlockCache));
        return this;
    }

    public BlockBasedTableOptions SetBlockCache(nint blockCache)
    {
        RocksDbNative.rocksdb_block_based_options_set_block_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Cache(blockCache));
        return this;
    }

    public BlockBasedTableOptions SetBlockCache(Cache blockCache)
    {
        References.BlockCache = blockCache;
        RocksDbNative.rocksdb_block_based_options_set_block_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Cache(blockCache.Handle));
        return this;
    }

    public BlockBasedTableOptions SetWholeKeyFiltering(bool wholeKeyFiltering)
    {
        RocksDbNative.rocksdb_block_based_options_set_whole_key_filtering(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(wholeKeyFiltering));
        return this;
    }

    public BlockBasedTableOptions SetFormatVersion(int formatVersion)
    {
        RocksDbNative.rocksdb_block_based_options_set_format_version(RocksDbInterop.BlockBasedTableOptions(Handle), formatVersion);
        return this;
    }

    public BlockBasedTableOptions SetIndexType(BlockBasedTableIndexType indexType)
    {
        RocksDbNative.rocksdb_block_based_options_set_index_type(RocksDbInterop.BlockBasedTableOptions(Handle), (int)indexType);
        return this;
    }

    public BlockBasedTableOptions SetCacheIndexAndFilterBlocks(bool cacheIndexAndFilterBlocks)
    {
        RocksDbNative.rocksdb_block_based_options_set_cache_index_and_filter_blocks(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(cacheIndexAndFilterBlocks));
        return this;
    }

    public BlockBasedTableOptions SetPinL0FilterAndIndexBlocksInCache(bool pinL0FilterAndIndexBlocksInCache)
    {
        RocksDbNative.rocksdb_block_based_options_set_pin_l0_filter_and_index_blocks_in_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(pinL0FilterAndIndexBlocksInCache));
        return this;
    }
}
