// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class Checkpoint : IDisposable
{
    public nint Handle { get; }

    public Checkpoint(nint handle)
    {
        Handle = handle;
    }

    public void Save(string checkpointDir, ulong logSizeForFlush = 0)
    {
        using var path = new RocksSafePath(checkpointDir);
        sbyte* errptr = null;
        RocksDbNative.rocksdb_checkpoint_create(RocksDbInterop.Checkpoint(Handle), (sbyte*)path.Handle, (nuint)logSizeForFlush, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Dispose()
    {
        RocksDbNative.rocksdb_checkpoint_object_destroy(RocksDbInterop.Checkpoint(Handle));
    }
}
