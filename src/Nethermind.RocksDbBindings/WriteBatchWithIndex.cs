// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class WriteBatchWithIndex : IWriteBatch
{
    private nint handle;
    private Encoding defaultEncoding = Encoding.UTF8;

    public WriteBatchWithIndex(ulong reservedBytes = 0, bool overwriteKeys = true)
        : this((nint)RocksDbNative.rocksdb_writebatch_wi_create((nuint)reservedBytes, RocksDbInterop.Bool(overwriteKeys)))
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
#if !NODESTROY
            RocksDbNative.rocksdb_writebatch_wi_destroy(RocksDbInterop.WriteBatchWithIndex(handle));
#endif
            handle = nint.Zero;
        }
    }

    public WriteBatchWithIndex Clear()
    {
        RocksDbNative.rocksdb_writebatch_wi_clear(RocksDbInterop.WriteBatchWithIndex(handle));
        return this;
    }

    public int Count()
    {
        return RocksDbNative.rocksdb_writebatch_wi_count(RocksDbInterop.WriteBatchWithIndex(handle));
    }

    public Iterator CreateIteratorWithBase(Iterator baseIterator, ColumnFamilyHandle cf = null)
    {
        var handle = cf is null
            ? (nint)RocksDbNative.rocksdb_writebatch_wi_create_iterator_with_base(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Iterator(baseIterator.Handle))
            : (nint)RocksDbNative.rocksdb_writebatch_wi_create_iterator_with_base_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Iterator(baseIterator.Handle), RocksDbInterop.ColumnFamily(cf.Handle));
        return new Iterator(handle);
    }

    public string Get(string key, ColumnFamilyHandle cf = null, OptionsHandle options = null, Encoding encoding = null)
    {
        encoding ??= defaultEncoding;
        var value = Get(encoding.GetBytes(key), cf, options);
        return value is null ? null : encoding.GetString(value);
    }

    public byte[] Get(byte[] key, ColumnFamilyHandle cf = null, OptionsHandle options = null)
    {
        return Get(key, (ulong)key.GetLongLength(0), cf, options);
    }

    public byte[] Get(byte[] key, ulong keyLength, ColumnFamilyHandle cf = null, OptionsHandle options = null)
    {
        fixed (byte* keyPtr = key)
        {
            return GetFromBatch(keyPtr, (nuint)keyLength, cf, options);
        }
    }

    public ulong Get(byte[] key, byte[] buffer, ulong offset, ulong length, ColumnFamilyHandle cf = null, OptionsHandle options = null)
    {
        return Get(key, (ulong)key.GetLongLength(0), buffer, offset, length, cf, options);
    }

    public ulong Get(byte[] key, ulong keyLength, byte[] buffer, ulong offset, ulong length, ColumnFamilyHandle cf = null, OptionsHandle options = null)
    {
        unsafe
        {
            fixed (byte* keyPtr = key)
            {
                var value = GetFromBatch(keyPtr, (nuint)keyLength, cf, options);
                if (value is null)
                    return 0;
                var valLength = Math.Min(length, (ulong)value.Length);
                Buffer.BlockCopy(value, 0, buffer, (int)offset, (int)valLength);
                return valLength;
            }
        }
    }

    public string Get(RocksDb db, string key, ColumnFamilyHandle cf = null, ReadOptions options = null, Encoding encoding = null)
    {
        encoding ??= defaultEncoding;
        var value = Get(db, encoding.GetBytes(key), cf, options);
        return value is null ? null : encoding.GetString(value);
    }

    public byte[] Get(RocksDb db, byte[] key, ColumnFamilyHandle cf = null, ReadOptions options = null)
    {
        return Get(db, key, (ulong)key.GetLongLength(0), cf, options);
    }

    public byte[] Get(RocksDb db, byte[] key, ulong keyLength, ColumnFamilyHandle cf = null, ReadOptions options = null)
    {
        fixed (byte* keyPtr = key)
        {
            return GetFromBatchAndDb(db, keyPtr, (nuint)keyLength, cf, options);
        }
    }

    public ulong Get(RocksDb db, byte[] key, byte[] buffer, ulong offset, ulong length, ColumnFamilyHandle cf = null, ReadOptions options = null)
    {
        return Get(db, key, (ulong)key.GetLongLength(0), buffer, offset, length, cf, options);
    }

    public ulong Get(RocksDb db, byte[] key, ulong keyLength, byte[] buffer, ulong offset, ulong length, ColumnFamilyHandle cf = null, ReadOptions options = null)
    {
        unsafe
        {
            fixed (byte* keyPtr = key)
            {
                var value = GetFromBatchAndDb(db, keyPtr, (nuint)keyLength, cf, options);
                if (value is null)
                    return 0;
                var valLength = Math.Min(length, (ulong)value.Length);
                Buffer.BlockCopy(value, 0, buffer, (int)offset, (int)valLength);
                return valLength;
            }
        }
    }

    public Iterator NewIterator(Iterator baseIterator, ColumnFamilyHandle cf = null)
    {
        nint iteratorHandle = cf is null
            ? (nint)RocksDbNative.rocksdb_writebatch_wi_create_iterator_with_base(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Iterator(baseIterator.Handle))
            : (nint)RocksDbNative.rocksdb_writebatch_wi_create_iterator_with_base_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Iterator(baseIterator.Handle), RocksDbInterop.ColumnFamily(cf.Handle));
        baseIterator.Detach();
        // Note: passing in base iterator here only to ensure that it is not collected before the iterator
        return new Iterator(iteratorHandle);
    }

    public WriteBatchWithIndex Put(string key, string val, Encoding encoding = null)
    {
        if (encoding is null)
        {
            encoding = defaultEncoding;
        }

        Put(encoding.GetBytes(key), encoding.GetBytes(val));
        return this;
    }

    private byte[] GetFromBatch(byte* key, nuint keyLength, ColumnFamilyHandle cf, OptionsHandle options)
    {
        nuint valueLength;
        sbyte* errptr = null;
        var valuePtr = cf is null
            ? RocksDbNative.rocksdb_writebatch_wi_get_from_batch(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Options((options ?? RocksDb.DefaultOptions).Handle), (sbyte*)key, keyLength, &valueLength, &errptr)
            : RocksDbNative.rocksdb_writebatch_wi_get_from_batch_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Options((options ?? RocksDb.DefaultOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &valueLength, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return RocksDbInterop.BytesAndFree(valuePtr, valueLength);
    }

    private byte[] GetFromBatchAndDb(RocksDb db, byte* key, nuint keyLength, ColumnFamilyHandle cf, ReadOptions options)
    {
        nuint valueLength;
        sbyte* errptr = null;
        var valuePtr = cf is null
            ? RocksDbNative.rocksdb_writebatch_wi_get_from_batch_and_db(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Db(db.Handle), RocksDbInterop.ReadOptions((options ?? RocksDb.DefaultReadOptions).Handle), (sbyte*)key, keyLength, &valueLength, &errptr)
            : RocksDbNative.rocksdb_writebatch_wi_get_from_batch_and_db_cf(RocksDbInterop.WriteBatchWithIndex(Handle), RocksDbInterop.Db(db.Handle), RocksDbInterop.ReadOptions((options ?? RocksDb.DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &valueLength, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return RocksDbInterop.BytesAndFree(valuePtr, valueLength);
    }

    public WriteBatchWithIndex Put(byte[] key, byte[] val, ColumnFamilyHandle cf = null)
    {
        return Put(key, (ulong)key.Length, val, (ulong)val.Length, cf);
    }

    public WriteBatchWithIndex Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            Put(keyPtr, klen, valuePtr, vlen, cf);
        }

        return this;
    }

    public unsafe void Put(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_wi_put(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_wi_put_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
    }

#if !NETSTANDARD2_0
    public unsafe WriteBatchWithIndex Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_writebatch_wi_put(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                RocksDbNative.rocksdb_writebatch_wi_put_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }

    public unsafe WriteBatchWithIndex Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_writebatch_wi_merge(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                RocksDbNative.rocksdb_writebatch_wi_merge_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }
#endif

    public WriteBatchWithIndex Putv(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_wi_putv(RocksDbInterop.WriteBatchWithIndex(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatchWithIndex PutvCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_wi_putv_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(columnFamily), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatchWithIndex Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            Merge(keyPtr, klen, valuePtr, vlen, cf);
        }

        return this;
    }

    public unsafe void Merge(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_wi_merge(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_wi_merge_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
    }

    public WriteBatchWithIndex MergeCf(nint columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            MergeCf(columnFamily, keyPtr, klen, valuePtr, vlen);
        }
        return this;
    }

    public unsafe void MergeCf(nint columnFamily, byte* key, ulong klen, byte* val, ulong vlen)
    {
        RocksDbNative.rocksdb_writebatch_wi_merge_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(columnFamily), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
    }

    public WriteBatchWithIndex Mergev(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_wi_mergev(RocksDbInterop.WriteBatchWithIndex(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatchWithIndex MergevCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_wi_mergev_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(columnFamily), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatchWithIndex Delete(byte[] key, ColumnFamilyHandle cf = null)
    {
        return Delete(key, (ulong)key.Length, cf);
    }

    public WriteBatchWithIndex Delete(byte[] key, ulong klen, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = key)
        {
            Delete(keyPtr, klen, cf);
        }

        return this;
    }

    public unsafe void Delete(byte* key, ulong klen, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_wi_delete(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)key, (nuint)klen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_wi_delete_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen);
        }
    }

#if !NETSTANDARD2_0
    public unsafe WriteBatchWithIndex Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_writebatch_wi_delete(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
            else
            {
                RocksDbNative.rocksdb_writebatch_wi_delete_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
        }
        return this;
    }
#endif

    public unsafe void Deletev(int numKeys, nint keysList, nint keysListSizes, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_wi_deletev(RocksDbInterop.WriteBatchWithIndex(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_wi_deletev_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes);
        }
    }

    public WriteBatchWithIndex DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle cf = null)
    {
        fixed (byte* startKeyPtr = startKey)
        fixed (byte* endKeyPtr = endKey)
        {
            DeleteRange(startKeyPtr, sklen, endKeyPtr, eklen, cf);
        }

        return this;
    }

    public unsafe void DeleteRange(byte* startKey, ulong sklen, byte* endKey, ulong eklen, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_wi_delete_range(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)startKey, (nuint)sklen, (sbyte*)endKey, (nuint)eklen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_wi_delete_range_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startKey, (nuint)sklen, (sbyte*)endKey, (nuint)eklen);
        }
    }

    public unsafe void DeleteRangev(int numKeys, nint startKeysList, nint startKeysListSizes, nint endKeysList, nint endKeysListSizes, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_wi_delete_rangev(RocksDbInterop.WriteBatchWithIndex(handle), numKeys, (sbyte**)startKeysList, (nuint*)startKeysListSizes, (sbyte**)endKeysList, (nuint*)endKeysListSizes);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_wi_delete_rangev_cf(RocksDbInterop.WriteBatchWithIndex(handle), RocksDbInterop.ColumnFamily(cf.Handle), numKeys, (sbyte**)startKeysList, (nuint*)startKeysListSizes, (sbyte**)endKeysList, (nuint*)endKeysListSizes);
        }
    }

    public WriteBatchWithIndex PutLogData(byte[] blob, ulong len)
    {
        fixed (byte* blobPtr = blob) { RocksDbNative.rocksdb_writebatch_wi_put_log_data(RocksDbInterop.WriteBatchWithIndex(handle), (sbyte*)blobPtr, (nuint)len); }
        return this;
    }

    public WriteBatchWithIndex Iterate(nint state, PutDelegate put, DeletedDelegate deleted)
    {
        RocksDbNative.rocksdb_writebatch_wi_iterate(
            RocksDbInterop.WriteBatchWithIndex(handle),
            (void*)state,
            (delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void>)(void*)Marshal.GetFunctionPointerForDelegate(put),
            (delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void>)(void*)Marshal.GetFunctionPointerForDelegate(deleted));
        return this;
    }

    /// <summary>
    /// Get the write batch as bytes
    /// </summary>
    /// <returns></returns>
    public byte[] ToBytes()
    {
        nuint size;
        var data = RocksDbNative.rocksdb_writebatch_wi_data(RocksDbInterop.WriteBatchWithIndex(handle), &size);
        return RocksDbInterop.Bytes((nint)data, size);
    }

    /// <summary>
    /// Get the write batch as bytes
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <returns>null if size was not large enough to hold the data</returns>
    public byte[] ToBytes(byte[] buffer, int offset = 0, int size = -1)
    {
        if (size < 0)
        {
            size = buffer.Length;
        }

        var bytes = ToBytes();
        if (bytes is not null && bytes.Length <= size)
        {
            Buffer.BlockCopy(bytes, 0, buffer, offset, bytes.Length);
            return buffer;
        }

        return null;
    }

    public void SetSavePoint()
    {
        RocksDbNative.rocksdb_writebatch_wi_set_save_point(RocksDbInterop.WriteBatchWithIndex(handle));
    }

    public void RollbackToSavePoint()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_writebatch_wi_rollback_to_save_point(RocksDbInterop.WriteBatchWithIndex(handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }


    IWriteBatch IWriteBatch.Clear()
        => Clear();
    IWriteBatch IWriteBatch.Put(string key, string val, Encoding encoding)
        => Put(key, val, encoding);
    IWriteBatch IWriteBatch.Put(byte[] key, byte[] val, ColumnFamilyHandle cf)
        => Put(key, val, cf);
    IWriteBatch IWriteBatch.Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf)
        => Put(key, klen, val, vlen, cf);
    IWriteBatch IWriteBatch.Putv(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => Putv(numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.PutvCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => PutvCf(columnFamily, numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf)
        => Merge(key, klen, val, vlen, cf);
    IWriteBatch IWriteBatch.MergeCf(nint columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen)
        => MergeCf(columnFamily, key, klen, val, vlen);
    IWriteBatch IWriteBatch.Mergev(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => Mergev(numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.MergevCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => MergevCf(columnFamily, numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.Delete(byte[] key, ColumnFamilyHandle cf)
        => Delete(key, cf);
    IWriteBatch IWriteBatch.Delete(byte[] key, ulong klen, ColumnFamilyHandle cf)
        => Delete(key, klen, cf);
    IWriteBatch IWriteBatch.DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle cf)
        => DeleteRange(startKey, sklen, endKey, eklen, cf);
    IWriteBatch IWriteBatch.PutLogData(byte[] blob, ulong len)
        => PutLogData(blob, len);
    IWriteBatch IWriteBatch.Iterate(nint state, PutDelegate put, DeletedDelegate deleted)
        => Iterate(state, put, deleted);

#if !NETSTANDARD2_0
    IWriteBatch IWriteBatch.Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf)
        => Put(key, value, cf);
    IWriteBatch IWriteBatch.Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf)
        => Merge(key, value, cf);
    IWriteBatch IWriteBatch.Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle cf)
        => Delete(key, cf);
#endif
}
