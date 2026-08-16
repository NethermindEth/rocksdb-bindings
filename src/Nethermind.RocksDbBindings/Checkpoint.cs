// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

public unsafe class Checkpoint(nint handle) : IDisposable
{
    public nint Handle { get; private set; } = handle;

    public void Save(string checkpointDir, ulong logSizeForFlush = 0)
    {
        using var path = new TransientUtf8String(checkpointDir);
        sbyte* errptr = null;
        rocksdb_checkpoint_create(RocksDbInterop.Checkpoint(Handle), (sbyte*)path.Handle, (nuint)logSizeForFlush, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_checkpoint_object_destroy(RocksDbInterop.Checkpoint(Handle));
            Handle = nint.Zero;
        }
    }
}
