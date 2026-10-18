// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

/// <summary>How a <see cref="RocksDb.TryGet"/> read ended.</summary>
public enum GetResult
{
    /// <summary>The key has a value.</summary>
    Found = 1,

    /// <summary>The key has no value.</summary>
    NotFound = 2,

    /// <summary>
    /// The read needs file IO that the read options do not allow: a read at the block-cache tier
    /// (<see cref="ReadOptions.SetReadTier"/> with 1) whose blocks are not in memory.
    /// </summary>
    Incomplete = 3,
}
