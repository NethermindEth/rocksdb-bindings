// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.CompatibilityTests;

internal sealed record DatabaseSpec(string Name, string Options, string[] Families, int MinValueSize, int MaxValueSize, int KeyCount)
{
    // Option strings mirror the defaults in Nethermind's DbConfig.
    private const string Base =
        "min_write_buffer_number_to_merge=1;write_buffer_size=16000000;max_write_buffer_number=2;"
        + "memtable_whole_key_filtering=true;memtable_prefix_bloom_size_ratio=0.02;level_compaction_dynamic_level_bytes=false;"
        + "max_compaction_bytes=4000000000;compression=kSnappyCompression;optimize_filters_for_hits=true;advise_random_on_open=true;"
        + "async_wal_precreate=true;avoid_unnecessary_blocking_io=true;target_file_size_base=64000000;max_bytes_for_level_base=256000000;"
        + "block_based_table_factory.block_size=16000;block_based_table_factory.pin_l0_filter_and_index_blocks_in_cache=true;"
        + "block_based_table_factory.cache_index_and_filter_blocks_with_high_priority=true;block_based_table_factory.format_version=5;"
        + "block_based_table_factory.index_type=kTwoLevelIndexSearch;block_based_table_factory.partition_filters=true;"
        + "block_based_table_factory.metadata_block_size=4096;block_based_table_factory.filter_policy=bloomfilter:10;"
        + "block_based_table_factory.block_cache=32000000;";

    private const string State =
        "compression=kLZ4Compression;max_bytes_for_level_multiplier=30;max_bytes_for_level_base=350000000;target_file_size_multiplier=2;"
        + "min_write_buffer_number_to_merge=2;block_based_table_factory.block_restart_interval=4;"
        + "block_based_table_factory.data_block_index_type=kDataBlockBinaryAndHash;block_based_table_factory.data_block_hash_table_util_ratio=0.5;"
        + "block_based_table_factory.block_size=32000;block_based_table_factory.filter_policy=bloomfilter:15;unordered_write=true;"
        + "max_write_batch_group_size_bytes=4000000;ttl=0;periodic_compaction_seconds=0;";

    // Binary-search index without partitioned filters.
    private const string StateLargeMemory = "block_based_table_factory={index_type=kBinarySearch;partition_filters=0;};";

    private const string Code =
        "compression=kLZ4Compression;write_buffer_size=16000000;block_based_table_factory.block_cache=16000000;optimize_filters_for_hits=false;"
        + "prefix_extractor=capped:8;block_based_table_factory.index_type=kHashSearch;block_based_table_factory.block_size=4096;"
        + "memtable=prefix_hash:1000000;block_based_table_factory.filter_policy=null;allow_concurrent_memtable_write=false;";

    private const string Blocks =
        "write_buffer_size=64000000;block_based_table_factory.block_cache=32000000;compaction_pri=kOldestLargestSeqFirst;"
        + "optimize_filters_for_hits=false;enable_blob_files=true;blob_compression_type=kSnappyCompression;max_bytes_for_level_base=4194304;";

    private const string Receipts = "write_buffer_size=2000000;block_based_table_factory.block_cache=8000000;optimize_filters_for_hits=false;";

    private const string Zstd = "compression=kZSTD;bottommost_compression=kZSTD;";

    public static readonly DatabaseSpec[] All =
    [
        new("base", Base, [], 50, 400, 60_000),
        new("state", Base + State, [], 30, 200, 80_000),
        new("state-large-memory", Base + State + StateLargeMemory, [], 30, 200, 40_000),
        new("code", Base + Code, [], 200, 6_000, 8_000),
        new("blocks", Base + Blocks, [], 2_000, 20_000, 4_000),
        new("zstd", Base + Zstd, [], 50, 400, 30_000),
        new("receipts", Base + Receipts, ["transactions", "blocks"], 40, 600, 20_000),
    ];

    public string[] AllFamilies => [ColumnFamilies.DefaultName, .. Families];

    public bool HasPrefixExtractor => Options.Contains("prefix_extractor", StringComparison.Ordinal);
}
