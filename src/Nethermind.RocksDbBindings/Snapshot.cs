// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

/// <summary>
/// A Snapshot is an immutable object and can therefore be safely
/// accessed from multiple threads without any external synchronization.
/// </summary>
public class Snapshot : IDisposable
{
    // Owns the native snapshot and a lease on the database: while the snapshot lives, the
    // native close is deferred, and abandoning it is recovered by the critical finalizer.
    private readonly SnapshotHandle _handle;

    public nint Handle => _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    internal Snapshot(RocksDbHandle dbLease, nint snapshotHandle)
    {
        _handle = new SnapshotHandle(snapshotHandle, dbLease);
    }

    public void Dispose() => _handle.Dispose();
}
