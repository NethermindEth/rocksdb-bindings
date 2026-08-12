// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

public unsafe class TransactionLogIterator : IDisposable
{
    public nint Handle { get; private set; }

    internal TransactionLogIterator(nint handle)
    {
        Handle = handle;
    }

    public bool Valid()
    {
        return RocksDbNative.rocksdb_wal_iter_valid(RocksDbInterop.WalIterator(Handle)) != 0;
    }

    public void Next()
    {
        RocksDbNative.rocksdb_wal_iter_next(RocksDbInterop.WalIterator(Handle));
    }

    public void Status()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_wal_iter_status(RocksDbInterop.WalIterator(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public unsafe WriteBatch GetBatch(out ulong sequenceNumber)
    {
        ulong seq;
        nint writeBatchHandle = (nint)RocksDbNative.rocksdb_wal_iter_get_batch(RocksDbInterop.WalIterator(Handle), &seq);
        sequenceNumber = seq;
        return new WriteBatch(writeBatchHandle);
    }

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            RocksDbNative.rocksdb_wal_iter_destroy(RocksDbInterop.WalIterator(Handle));
            Handle = nint.Zero;
        }
    }
}
