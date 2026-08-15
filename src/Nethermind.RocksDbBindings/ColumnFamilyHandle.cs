// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

public interface IColumnFamilyHandle
{
    nint Handle { get; }
}

unsafe class ColumnFamilyHandleInternal(nint handle) : IColumnFamilyHandle, IDisposable
{
    public nint Handle { get; protected set; } = handle;

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_column_family_handle_destroy(RocksDbInterop.ColumnFamily(Handle));
            Handle = nint.Zero;
        }
    }
}
