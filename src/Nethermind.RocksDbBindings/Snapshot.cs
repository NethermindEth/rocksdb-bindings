// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

/// <summary>
/// A Snapshot is an immutable object and can therefore be safely
/// accessed from multiple threads without any external synchronization.
/// </summary>
public unsafe class Snapshot : IDisposable
{
    private nint dbHandle;
    public nint Handle { get; private set; }

    internal Snapshot(nint dbHandle, nint snapshotHandle)
    {
        this.dbHandle = dbHandle;
        Handle = snapshotHandle;
    }

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_release_snapshot(RocksDbInterop.Db(dbHandle), RocksDbInterop.Snapshot(Handle));
            Handle = nint.Zero;
        }
    }
}
