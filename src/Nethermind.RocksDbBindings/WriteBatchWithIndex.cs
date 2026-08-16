// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed class WriteBatchWithIndex : IDisposable
{
    private nint handle;

    public WriteBatchWithIndex(ulong reservedBytes = 0, bool overwriteKeys = true)
        : this((nint)rocksdb_writebatch_wi_create((nuint)reservedBytes, RocksDbInterop.Bool(overwriteKeys)))
    {
    }

    private WriteBatchWithIndex(nint handle)
    {
        this.handle = handle;
    }

    public nint Handle { get { return handle; } }

    public void Dispose()
    {
        if (handle != nint.Zero)
        {
            rocksdb_writebatch_wi_destroy(RocksDbInterop.WriteBatchWithIndex(handle));
            handle = nint.Zero;
        }
    }

    public WriteBatchWithIndex Clear()
    {
        rocksdb_writebatch_wi_clear(RocksDbInterop.WriteBatchWithIndex(handle));
        return this;
    }

    public int Count() => rocksdb_writebatch_wi_count(RocksDbInterop.WriteBatchWithIndex(handle));

    /// <inheritdoc cref="NewIterator(Iterator, IColumnFamilyHandle?)"/>
    public Iterator CreateIteratorWithBase(Iterator baseIterator, IColumnFamilyHandle? cf = null) =>
        NewIterator(baseIterator, cf);

    /// <summary>Reads a key as the batch alone would leave it, ignoring the database.</summary>
    public byte[]? Get(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, OptionsHandle? options = null)
    {
        fixed (byte* keyPtr = key)
        {
            nuint valueLength;
            sbyte* errptr = null;
            var valuePtr = cf is null
                ? rocksdb_writebatch_wi_get_from_batch(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Options((options ?? RocksDb.DefaultOptions).Handle), (sbyte*)keyPtr, (nuint)key.Length, &valueLength, &errptr)
                : rocksdb_writebatch_wi_get_from_batch_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Options((options ?? RocksDb.DefaultOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, &valueLength, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return RocksDbInterop.BytesAndFree(valuePtr, valueLength);
        }
    }

    /// <summary>Reads a key as the database would look with the batch applied on top.</summary>
    /// <exception cref="ObjectDisposedException"><paramref name="db"/> has been disposed.</exception>
    public byte[]? Get(RocksDb db, ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, ReadOptions? options = null)
    {
        using var dbLease = db.LeaseHandle(out nint dbHandle);
        fixed (byte* keyPtr = key)
        {
            nuint valueLength;
            sbyte* errptr = null;
            var valuePtr = cf is null
                ? rocksdb_writebatch_wi_get_from_batch_and_db(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Db(dbHandle), RocksDbInterop.ReadOptions((options ?? RocksDb.DefaultReadOptions).Handle), (sbyte*)keyPtr, (nuint)key.Length, &valueLength, &errptr)
                : rocksdb_writebatch_wi_get_from_batch_and_db_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Db(dbHandle), RocksDbInterop.ReadOptions((options ?? RocksDb.DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, &valueLength, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return RocksDbInterop.BytesAndFree(valuePtr, valueLength);
        }
    }

    /// <summary>
    /// Creates an iterator over the database view with the batch applied on top, taking
    /// ownership of <paramref name="baseIterator"/>.
    /// </summary>
    /// <remarks>
    /// The overlay borrows this batch's index and storage, so the batch must outlive the
    /// returned iterator.
    /// </remarks>
    public Iterator NewIterator(Iterator baseIterator, IColumnFamilyHandle? cf = null)
    {
        nint iteratorHandle = cf is null
            ? (nint)rocksdb_writebatch_wi_create_iterator_with_base(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Iterator(baseIterator.Handle))
            : (nint)rocksdb_writebatch_wi_create_iterator_with_base_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Iterator(baseIterator.Handle), RocksDbInterop.ColumnFamily(cf.Handle));
        // The returned iterator owns the base one, so detach it to avoid a double destroy. The
        // base's read options and database lease move over too: the native base iterator keeps
        // reading the options (and any iterate bounds) in place, and the database must not close
        // while the overlay is alive. The lease is taken before detaching, as detaching disarms
        // the base handle's release path.
        var dbLease = baseIterator.TakeDbLease();
        baseIterator.Detach();
        return new Iterator(iteratorHandle, baseIterator.ReadOptions, dbLease);
    }

    public WriteBatchWithIndex Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                rocksdb_writebatch_wi_put(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                rocksdb_writebatch_wi_put_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }

    public WriteBatchWithIndex Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                rocksdb_writebatch_wi_merge(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                rocksdb_writebatch_wi_merge_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }

    public WriteBatchWithIndex Delete(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        {
            if (cf is null)
            {
                rocksdb_writebatch_wi_delete(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
            else
            {
                rocksdb_writebatch_wi_delete_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
        }
        return this;
    }

    /// <summary>
    /// Get the write batch as bytes
    /// </summary>
    /// <returns></returns>
    public byte[] ToBytes()
    {
        nuint size;
        var data = rocksdb_writebatch_wi_data(RocksDbInterop.WriteBatchWithIndex(handle), &size);
        return RocksDbInterop.Bytes((nint)data, size)!;
    }

    public void SetSavePoint() => rocksdb_writebatch_wi_set_save_point(RocksDbInterop.WriteBatchWithIndex(handle));

    public void RollbackToSavePoint()
    {
        sbyte* errptr = null;
        rocksdb_writebatch_wi_rollback_to_save_point(RocksDbInterop.WriteBatchWithIndex(handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }
}
