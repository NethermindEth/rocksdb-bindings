// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

public unsafe class Checkpoint(nint handle) : IDisposable
{
    public nint Handle { get; } = handle;

    public void Save(string checkpointDir, ulong logSizeForFlush = 0)
    {
        using var path = new RocksSafePath(checkpointDir);
        sbyte* errptr = null;
        rocksdb_checkpoint_create(RocksDbInterop.Checkpoint(Handle), (sbyte*)path.Handle, (nuint)logSizeForFlush, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Dispose() => rocksdb_checkpoint_object_destroy(RocksDbInterop.Checkpoint(Handle));
}
