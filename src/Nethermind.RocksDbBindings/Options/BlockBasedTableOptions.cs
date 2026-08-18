// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe class BlockBasedTableOptions
{
    public nint Handle { get; }

    public BlockBasedTableOptions() => Handle = (nint)rocksdb_block_based_options_create();

    // No Dispose and so no second actor: the finalizer runs at most once, and nothing can read
    // the handle afterwards.
    ~BlockBasedTableOptions() => rocksdb_block_based_options_destroy(RocksDbInterop.BlockBasedTableOptions(Handle));

    public BlockBasedTableOptions SetBlockSize(ulong blockSize)
    {
        rocksdb_block_based_options_set_block_size(RocksDbInterop.BlockBasedTableOptions(Handle), (nuint)blockSize);
        return this;
    }

    public BlockBasedTableOptions SetBlockSizeDeviation(int blockSizeDeviation)
    {
        rocksdb_block_based_options_set_block_size_deviation(RocksDbInterop.BlockBasedTableOptions(Handle), blockSizeDeviation);
        return this;
    }

    public BlockBasedTableOptions SetBlockRestartInterval(int blockRestartInterval)
    {
        rocksdb_block_based_options_set_block_restart_interval(RocksDbInterop.BlockBasedTableOptions(Handle), blockRestartInterval);
        return this;
    }

    public BlockBasedTableOptions SetFilterPolicy(nint filterPolicy)
    {
        rocksdb_block_based_options_set_filter_policy(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.FilterPolicy(filterPolicy));
        return this;
    }

    public BlockBasedTableOptions SetFilterPolicy(BloomFilterPolicy filterPolicy)
    {
        rocksdb_block_based_options_set_filter_policy(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.FilterPolicy(filterPolicy.Handle));
        return this;
    }

    public BlockBasedTableOptions SetNoBlockCache(bool noBlockCache)
    {
        rocksdb_block_based_options_set_no_block_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(noBlockCache));
        return this;
    }

    public BlockBasedTableOptions SetBlockCache(nint blockCache)
    {
        rocksdb_block_based_options_set_block_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Cache(blockCache));
        return this;
    }

    public BlockBasedTableOptions SetBlockCache(Cache blockCache)
    {
        rocksdb_block_based_options_set_block_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Cache(blockCache.Handle));
        // RocksDB takes its own reference to the cache during the call, so the wrapper has to survive
        // the call but not outlive it.
        GC.KeepAlive(blockCache);
        return this;
    }

    public BlockBasedTableOptions SetWholeKeyFiltering(bool wholeKeyFiltering)
    {
        rocksdb_block_based_options_set_whole_key_filtering(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(wholeKeyFiltering));
        return this;
    }

    public BlockBasedTableOptions SetFormatVersion(int formatVersion)
    {
        rocksdb_block_based_options_set_format_version(RocksDbInterop.BlockBasedTableOptions(Handle), formatVersion);
        return this;
    }

    public BlockBasedTableOptions SetIndexType(BlockBasedTableIndexType indexType)
    {
        rocksdb_block_based_options_set_index_type(RocksDbInterop.BlockBasedTableOptions(Handle), (int)indexType);
        return this;
    }

    public BlockBasedTableOptions SetCacheIndexAndFilterBlocks(bool cacheIndexAndFilterBlocks)
    {
        rocksdb_block_based_options_set_cache_index_and_filter_blocks(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(cacheIndexAndFilterBlocks));
        return this;
    }

    public BlockBasedTableOptions SetPinL0FilterAndIndexBlocksInCache(bool pinL0FilterAndIndexBlocksInCache)
    {
        rocksdb_block_based_options_set_pin_l0_filter_and_index_blocks_in_cache(RocksDbInterop.BlockBasedTableOptions(Handle), RocksDbInterop.Bool(pinL0FilterAndIndexBlocksInCache));
        return this;
    }
}
