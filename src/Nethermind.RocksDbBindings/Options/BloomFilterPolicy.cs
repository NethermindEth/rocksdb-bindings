// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

/// <remarks>
/// Attaching the policy to options hands ownership to rocksdb, which destroys it with them, so
/// there is nothing to release here. A policy that is never attached leaks; see
/// <see href="https://github.com/facebook/rocksdb/issues/1095">rocksdb issue #1095</see>.
/// </remarks>
public sealed unsafe class BloomFilterPolicy
{
    public nint Handle { get; }

    private BloomFilterPolicy(nint handle)
    {
        Handle = handle;
    }

    /// <param name="bitsPerKey">
    /// Bits of filter per key. 10 gives roughly a 1% false positive rate.
    /// </param>
    /// <param name="useBlockBasedBuilder">Selects the legacy block-based filter over a full one.</param>
    /// <remarks>
    /// A filter that ignores part of a key must be paired with a comparator that ignores the same
    /// part, or lookups will miss keys the filter rules out.
    /// </remarks>
    public static BloomFilterPolicy Create(int bitsPerKey = 10, bool useBlockBasedBuilder = true)
    {
        nint handle = useBlockBasedBuilder
            ? (nint)rocksdb_filterpolicy_create_bloom(bitsPerKey)
            : (nint)rocksdb_filterpolicy_create_bloom_full(bitsPerKey);
        return new BloomFilterPolicy(handle);
    }
}
