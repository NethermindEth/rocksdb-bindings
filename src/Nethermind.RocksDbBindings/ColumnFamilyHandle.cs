// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethermind.RocksDbBindings;

#pragma warning disable IDE1006 // Naming (missing I) for backward source-compatibility reasons
public interface ColumnFamilyHandle
#pragma warning restore IDE1006
{
    nint Handle { get; }
}

unsafe class ColumnFamilyHandleInternal : ColumnFamilyHandle, IDisposable
{
    public ColumnFamilyHandleInternal(nint handle)
    {
        this.Handle = handle;
    }

    public nint Handle { get; protected set; }

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            RocksDbNative.rocksdb_column_family_handle_destroy(RocksDbInterop.ColumnFamily(Handle));
            Handle = nint.Zero;
        }
    }
}
