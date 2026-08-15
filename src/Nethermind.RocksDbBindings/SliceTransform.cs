// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

public unsafe class SliceTransform
{
    public nint Handle { get; protected set; }

    private SliceTransform(nint handle)
    {
        Handle = handle;
    }

    public static SliceTransform CreateFixedPrefix(/*(size_t)*/ ulong fixed_prefix_length)
    {
        nint handle = (nint)rocksdb_slicetransform_create_fixed_prefix((nuint)fixed_prefix_length);
        return new SliceTransform(handle);
    }

    public static SliceTransform CreateNoOp()
    {
        nint handle = (nint)rocksdb_slicetransform_create_noop();
        return new SliceTransform(handle);
    }

    ~SliceTransform()
    {
        if (Handle != nint.Zero)
        {
            // Commented out until a solution is found to rocksdb issue #1095 (https://github.com/facebook/rocksdb/issues/1095)
            // If you create one of these, use it in an Option which will destroy it when finished
            // Otherwise don't create one or it will leak
            // RocksDB owns this while attached to options; see rocksdb issue #1095.
            Handle = nint.Zero;
        }
    }
}
