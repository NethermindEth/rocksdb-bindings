// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// A Snapshot is an immutable object and can therefore be safely
/// accessed from multiple threads without any external synchronization.
/// </summary>
public unsafe class Snapshot : IDisposable
{
    private IntPtr dbHandle;
    public IntPtr Handle { get; private set; }

    internal Snapshot(IntPtr dbHandle, IntPtr snapshotHandle)
    {
        this.dbHandle = dbHandle;
        Handle = snapshotHandle;
    }

    public void Dispose()
    {
        if (Handle != IntPtr.Zero)
        {
#if !NODESTROY
            RocksDbNative.rocksdb_release_snapshot(RocksDbInterop.Db(dbHandle), RocksDbInterop.Snapshot(Handle));
#endif
            Handle = IntPtr.Zero;
        }
    }
}
