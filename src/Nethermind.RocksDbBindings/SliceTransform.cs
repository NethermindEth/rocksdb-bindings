// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

/// <remarks>
/// Attaching the transform to options hands ownership to rocksdb, which destroys it with them, so
/// there is nothing to release here. A transform that is never attached leaks; see
/// <see href="https://github.com/facebook/rocksdb/issues/1095">rocksdb issue #1095</see>.
/// </remarks>
public sealed unsafe class SliceTransform
{
    public nint Handle { get; }

    private SliceTransform(nint handle)
    {
        Handle = handle;
    }

    /// <summary>Extracts the first <paramref name="prefixLength"/> bytes of a key.</summary>
    public static SliceTransform CreateFixedPrefix(ulong prefixLength) =>
        new((nint)rocksdb_slicetransform_create_fixed_prefix((nuint)prefixLength));

    /// <summary>Extracts the whole key.</summary>
    public static SliceTransform CreateNoOp() => new((nint)rocksdb_slicetransform_create_noop());
}
