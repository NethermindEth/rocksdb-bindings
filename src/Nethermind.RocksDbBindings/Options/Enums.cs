// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;

namespace Nethermind.RocksDbBindings;

public enum BlockBasedTableIndexType
{
    Binary = 0,
    Hash = 1,
    TwoLevelIndex = 2,
}

public enum BlockBasedTableDataBlockIndexType
{
    BinarySearch = 0,
    BinarySearchAndHash = 1,
}

public enum BlockBasedK
{
    Fallback = 0,
    None = 1,
    FlushAndSimilar = 2,
    All = 3,
}

[SuppressMessage("Design", "CA1069:Enums values should not be duplicated",
    Justification = "Mirrors the RocksDB StatsLevel enum, where kExceptTickers is an alias for kDisableAll.")]
public enum StatisticsLevel
{
    DisableAll = 0,
    ExceptTickers = 0,
    ExceptHistogramOrTimers = 1,
    ExceptTimers = 2,
    ExceptDetailedTimers = 3,
    ExceptTimeForMutex = 4,
    All = 5,
}

public enum PrepopulateBlob
{
    Disable = 0,
    FlushOnly = 1,
}

public enum Recovery
{
    TolerateCorruptedTailRecords = 0,
    AbsoluteConsistency = 1,
    PointInTime = 2,
    SkipAnyCorruptedRecords = 3,
}

public enum Compression
{
    No = 0,
    Snappy = 1,
    Zlib = 2,
    Bz2 = 3,
    Lz4 = 4,
    Lz4hc = 5,
    Xpress = 6,
    Zstd = 7,
}

public enum Compaction
{
    Level = 0,
    Universal = 1,
    Fifo = 2,
}

public enum CompactionPri
{
    ByCompensatedSize = 0,
    OldestLargestSeqFirst = 1,
    OldestSmallestSeqFirst = 2,
    MinOverlappingRatio = 3,
    RoundRobin = 4,
}
