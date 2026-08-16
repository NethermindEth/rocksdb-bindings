// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

public unsafe class Checkpoint : IDisposable
{
    // Owns the native checkpoint and a lease on the database: while the checkpoint lives, the
    // native close is deferred, and abandoning it is recovered by the critical finalizer.
    private readonly CheckpointHandle _handle;

    public nint Handle => _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    internal Checkpoint(nint handle, RocksDbHandle dbLease)
    {
        _handle = new CheckpointHandle(handle, dbLease);
    }

    public void Save(string checkpointDir, ulong logSizeForFlush = 0)
    {
        using var lease = new HandleLease(_handle);
        using var path = new TransientUtf8String(checkpointDir);
        sbyte* errptr = null;
        rocksdb_checkpoint_create(RocksDbInterop.Checkpoint(_handle.DangerousGetHandle()), (sbyte*)path.Handle, (nuint)logSizeForFlush, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Dispose() => _handle.Dispose();
}
