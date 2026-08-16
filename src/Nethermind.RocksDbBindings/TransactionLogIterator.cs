// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

public sealed unsafe class TransactionLogIterator : IDisposable
{
    // Owns the native WAL iterator and a lease on the database: while the iterator lives, the
    // native close is deferred, and abandoning it is recovered by the critical finalizer.
    private readonly WalIteratorHandle _handle;

    public nint Handle => _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    private nint NativeHandle => _handle.DangerousGetHandle();

    internal TransactionLogIterator(nint handle, RocksDbHandle dbLease)
    {
        _handle = new WalIteratorHandle(handle, dbLease);
    }

    public bool Valid() => rocksdb_wal_iter_valid(RocksDbInterop.WalIterator(NativeHandle)) != 0;

    public void Next() => rocksdb_wal_iter_next(RocksDbInterop.WalIterator(NativeHandle));

    public void Status()
    {
        sbyte* errptr = null;
        rocksdb_wal_iter_status(RocksDbInterop.WalIterator(NativeHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public WriteBatch GetBatch(out ulong sequenceNumber)
    {
        ulong seq;
        nint writeBatchHandle = (nint)rocksdb_wal_iter_get_batch(RocksDbInterop.WalIterator(NativeHandle), &seq);
        sequenceNumber = seq;
        return new WriteBatch(writeBatchHandle);
    }

    public void Dispose() => _handle.Dispose();
}
