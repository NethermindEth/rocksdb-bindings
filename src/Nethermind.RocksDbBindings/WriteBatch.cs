// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Buffers;

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Nethermind.RocksDbBindings;

public interface IWriteBatch : IDisposable
{
    IntPtr Handle { get; }
    IWriteBatch Clear();
    int Count();
    IWriteBatch Put(string key, string val, Encoding encoding = null);
    IWriteBatch Put(byte[] key, byte[] val, ColumnFamilyHandle cf = null);
    IWriteBatch Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf = null);

#if !NETSTANDARD2_0
    IWriteBatch Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null);
#endif
    unsafe void Put(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle cf = null);
    IWriteBatch Putv(int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes);
    IWriteBatch PutvCf(IntPtr columnFamily, int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes);
    IWriteBatch Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf = null);
    unsafe void Merge(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle cf = null);

#if !NETSTANDARD2_0
    IWriteBatch Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null);
#endif
    IWriteBatch MergeCf(IntPtr columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen);
    unsafe void MergeCf(IntPtr columnFamily, byte* key, ulong klen, byte* val, ulong vlen);
    IWriteBatch Mergev(int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes);
    IWriteBatch MergevCf(IntPtr columnFamily, int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes);
    IWriteBatch Delete(byte[] key, ColumnFamilyHandle cf = null);
    IWriteBatch Delete(byte[] key, ulong klen, ColumnFamilyHandle cf = null);

#if !NETSTANDARD2_0
    IWriteBatch Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle cf = null);
#endif
    unsafe void Delete(byte* key, ulong klen, ColumnFamilyHandle cf = null);
    unsafe void Deletev(int numKeys, IntPtr keysList, IntPtr keysListSizes, ColumnFamilyHandle cf = null);
    IWriteBatch DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle cf = null);
    unsafe void DeleteRange(byte* startKey, ulong sklen, byte* endKey, ulong eklen, ColumnFamilyHandle cf = null);
    unsafe void DeleteRangev(int numKeys, IntPtr startKeysList, IntPtr startKeysListSizes, IntPtr endKeysList, IntPtr endKeysListSizes, ColumnFamilyHandle cf = null);
    IWriteBatch PutLogData(byte[] blob, ulong len);
    IWriteBatch Iterate(IntPtr state, PutDelegate put, DeletedDelegate deleted);
    byte[] ToBytes();
    byte[] ToBytes(byte[] buffer, int offset = 0, int size = -1);
    void SetSavePoint();
    void RollbackToSavePoint();
}

public unsafe class WriteBatch : IWriteBatch, IDisposable
{
    private IntPtr handle;
    private Encoding defaultEncoding = Encoding.UTF8;

    public WriteBatch() : this((IntPtr)RocksDbNative.rocksdb_writebatch_create())
    {
    }

    public WriteBatch(byte[] rep, long size = -1)
    {
        fixed (byte* repPtr = rep)
        {
            handle = (IntPtr)RocksDbNative.rocksdb_writebatch_create_from((sbyte*)repPtr, size < 0 ? (nuint)rep.Length : (nuint)size);
        }
    }

#if !NETSTANDARD2_0
    public unsafe static WriteBatch FromSpan(ReadOnlySpan<byte> data)
    {
        fixed (byte* dataPtr = data)
        {
            var handle = (IntPtr)RocksDbNative.rocksdb_writebatch_create_from((sbyte*)dataPtr, (nuint)data.Length);
            return new WriteBatch(handle);
        }
    }
#endif

    public WriteBatch(IntPtr handle)
    {
        this.handle = handle;
    }

    public IntPtr Handle { get { return handle; } }

    public void Dispose()
    {
        if (handle != IntPtr.Zero)
        {
#if !NODESTROY
            RocksDbNative.rocksdb_writebatch_destroy(RocksDbInterop.WriteBatch(handle));
#endif
            handle = IntPtr.Zero;
        }
    }

    public WriteBatch Clear()
    {
        RocksDbNative.rocksdb_writebatch_clear(RocksDbInterop.WriteBatch(handle));
        return this;
    }

    public int Count()
    {
        return RocksDbNative.rocksdb_writebatch_count(RocksDbInterop.WriteBatch(handle));
    }

    public WriteBatch Put(string key, string val, Encoding encoding = null)
    {
        if (encoding is null)
        {
            encoding = defaultEncoding;
        }

        Put(encoding.GetBytes(key), encoding.GetBytes(val));
        return this;
    }

    public WriteBatch Put(byte[] key, byte[] val, ColumnFamilyHandle cf = null)
    {
        return Put(key, (ulong)key.Length, val, (ulong)val.Length, cf);
    }

    public WriteBatch Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf = null)
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
            RocksDbNative.rocksdb_writebatch_put(RocksDbInterop.WriteBatch(handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_put_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
    }

#if !NETSTANDARD2_0
    public unsafe WriteBatch Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_writebatch_put(RocksDbInterop.WriteBatch(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                RocksDbNative.rocksdb_writebatch_put_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }

    public unsafe WriteBatch Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_writebatch_merge(RocksDbInterop.WriteBatch(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                RocksDbNative.rocksdb_writebatch_merge_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }

    public WriteBatch PutVector(ColumnFamilyHandle columnFamily, ReadOnlyMemory<byte> key, params ReadOnlyMemory<byte>[] values)
    {
        var pool = ArrayPool<ReadOnlyMemory<byte>>.Shared;
        var keys = pool.Rent(1);
        try
        {
            keys[0] = key;
            PutVector(keys.AsSpan(0, 1), values, columnFamily);
            return this;
        }
        finally
        {
            pool.Return(keys);
        }
    }

    public WriteBatch PutVector(ReadOnlyMemory<byte> key, params ReadOnlyMemory<byte>[] values)
    {
        var pool = ArrayPool<ReadOnlyMemory<byte>>.Shared;
        var keys = pool.Rent(1);
        try
        {
            keys[0] = key;
            PutVector(keys.AsSpan(0, 1), values);
            return this;
        }
        finally
        {
            pool.Return(keys);
        }
    }

    public unsafe WriteBatch PutVector(ReadOnlySpan<ReadOnlyMemory<byte>> keys, ReadOnlySpan<ReadOnlyMemory<byte>> values, ColumnFamilyHandle columnFamily = null)
    {
        var intPtrPool = ArrayPool<IntPtr>.Shared;
        var uintPtrPool = ArrayPool<UIntPtr>.Shared;
        IntPtr[] keysListArray = null, valuesListArray = null;
        UIntPtr[] keysListSizesArray = null, valuesListSizesArray = null;

        try
        {
            var keysLength = keys.Length;
            (keysListArray, keysListSizesArray) = keysLength < 256 
                ? (null, null)
                : (intPtrPool.Rent(keysLength), uintPtrPool.Rent(keysLength));
            Span<IntPtr> keysList = keysLength < 256 
                ? stackalloc IntPtr[keysLength]
                : keysListArray.AsSpan(0, keysLength);
            Span<UIntPtr> keysListSizes = keysLength < 256 
                ? stackalloc UIntPtr[keysLength] 
                : keysListSizesArray.AsSpan(0, keysLength);
            using var keyHandles = CopyVector(keys, keysList, keysListSizes);

            var valuesLength = values.Length;
            (valuesListArray, valuesListSizesArray) = valuesLength < 256 
                ? (null, null)
                : (intPtrPool.Rent(valuesLength), uintPtrPool.Rent(valuesLength));
            Span<IntPtr> valuesList = valuesLength < 256
                ? stackalloc IntPtr[valuesLength]
                : valuesListArray.AsSpan(0, valuesLength);
            Span<UIntPtr> valuesListSizes = valuesLength < 256
                ? stackalloc UIntPtr[valuesLength] 
                : valuesListSizesArray.AsSpan(0, valuesLength);
            using var valuesDisposable = CopyVector(values, valuesList, valuesListSizes);

            fixed (void* keysListPtr = keysList,
                keysListSizesPtr = keysListSizes,
                valuesListPtr = valuesList,
                valuesListSizesPtr = valuesListSizes)
            {
                if (columnFamily is null)
                {
                    Putv(
                        keysLength, (IntPtr)keysListPtr, (IntPtr)keysListSizesPtr,
                        valuesLength, (IntPtr)valuesListPtr, (IntPtr)valuesListSizesPtr);
                }
                else
                {
                    PutvCf(columnFamily.Handle,
                        keysLength, (IntPtr)keysListPtr, (IntPtr)keysListSizesPtr,
                        valuesLength, (IntPtr)valuesListPtr, (IntPtr)valuesListSizesPtr);
                }
            }
            return this; 
        }
        finally
        {
            if (keysListArray is not null) intPtrPool.Return(keysListArray);
            if (keysListSizesArray is not null) uintPtrPool.Return(keysListSizesArray);
            if (valuesListArray is not null) intPtrPool.Return(valuesListArray);
            if (valuesListSizesArray is not null) uintPtrPool.Return(valuesListSizesArray);
        }
    }

    static unsafe IDisposable CopyVector(ReadOnlySpan<ReadOnlyMemory<byte>> items, Span<IntPtr> itemsList, Span<UIntPtr> itemsListSizes)
    {
        var disposable = new MemoryHandleManager(items.Length);
        for (var i = 0; i < items.Length; i++)
        {
            var handle = items[i].Pin();
            disposable.Add(handle);
            itemsList[i] = (IntPtr)handle.Pointer;
            itemsListSizes[i] = (UIntPtr)items[i].Length;
        }
        return disposable;
    }



    class MemoryHandleManager : IDisposable
    {
        readonly IList<MemoryHandle> handles;

        public MemoryHandleManager(int capacity)
        {
             handles = new List<MemoryHandle>(capacity);
        }

        public void Add(MemoryHandle handle) => handles.Add(handle);

        public void Dispose()
        {
            for (int i = 0; i < handles.Count; i++)
            {
                handles[i].Dispose();
            }
        }
    }
#endif

    public WriteBatch Putv(int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_putv(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch PutvCf(IntPtr columnFamily, int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_putv_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(columnFamily), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf = null)
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
            RocksDbNative.rocksdb_writebatch_merge(RocksDbInterop.WriteBatch(handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_merge_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
    }

    public WriteBatch MergeCf(IntPtr columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            MergeCf(columnFamily, keyPtr, klen, valuePtr, vlen);
        }
        return this;
    }

    public unsafe void MergeCf(IntPtr columnFamily, byte* key, ulong klen, byte* val, ulong vlen)
    {
        RocksDbNative.rocksdb_writebatch_merge_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(columnFamily), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
    }

    public WriteBatch Mergev(int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_mergev(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch MergevCf(IntPtr columnFamily, int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
    {
        RocksDbNative.rocksdb_writebatch_mergev_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(columnFamily), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch Delete(byte[] key, ColumnFamilyHandle cf = null)
    {
        return Delete(key, (ulong)key.Length, cf);
    }

    public WriteBatch Delete(byte[] key, ulong klen, ColumnFamilyHandle cf = null)
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
            RocksDbNative.rocksdb_writebatch_delete(RocksDbInterop.WriteBatch(handle), (sbyte*)key, (nuint)klen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_delete_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen);
        }
    }

#if !NETSTANDARD2_0
    public unsafe WriteBatch Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_writebatch_delete(RocksDbInterop.WriteBatch(handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
            else
            {
                RocksDbNative.rocksdb_writebatch_delete_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
        }
        return this;
    }
#endif

    public unsafe void Deletev(int numKeys, IntPtr keysList, IntPtr keysListSizes, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_deletev(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_deletev_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes);
        }
    }

    public WriteBatch DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle cf = null)
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
            RocksDbNative.rocksdb_writebatch_delete_range(RocksDbInterop.WriteBatch(handle), (sbyte*)startKey, (nuint)sklen, (sbyte*)endKey, (nuint)eklen);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_delete_range_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startKey, (nuint)sklen, (sbyte*)endKey, (nuint)eklen);
        }
    }

    public unsafe void DeleteRangev(int numKeys, IntPtr startKeysList, IntPtr startKeysListSizes, IntPtr endKeysList, IntPtr endKeysListSizes, ColumnFamilyHandle cf = null)
    {
        if (cf is null)
        {
            RocksDbNative.rocksdb_writebatch_delete_rangev(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)startKeysList, (nuint*)startKeysListSizes, (sbyte**)endKeysList, (nuint*)endKeysListSizes);
        }
        else
        {
            RocksDbNative.rocksdb_writebatch_delete_rangev_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), numKeys, (sbyte**)startKeysList, (nuint*)startKeysListSizes, (sbyte**)endKeysList, (nuint*)endKeysListSizes);
        }
    }

    public WriteBatch PutLogData(byte[] blob, ulong len)
    {
        fixed (byte* blobPtr = blob)
        {
            RocksDbNative.rocksdb_writebatch_put_log_data(RocksDbInterop.WriteBatch(handle), (sbyte*)blobPtr, (nuint)len);
        }
        return this;
    }

    public byte[] GetLogDataBytes()
    {
        return ToBytes();
    }

    public WriteBatch Iterate(IntPtr state, PutDelegate put, DeletedDelegate deleted)
    {
        RocksDbNative.rocksdb_writebatch_iterate(
            RocksDbInterop.WriteBatch(handle),
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
        var resultPtr = RocksDbNative.rocksdb_writebatch_data(RocksDbInterop.WriteBatch(handle), &size);
        return RocksDbInterop.Bytes((IntPtr)resultPtr, size);
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

    public byte[] ToBytesPooled(out int size)
    {
        nuint sizePtr;
        var resultPtr = RocksDbNative.rocksdb_writebatch_data(RocksDbInterop.WriteBatch(handle), &sizePtr);
        size = (int)sizePtr;
        var pooledBuffer = ArrayPool<byte>.Shared.Rent(size);
        Marshal.Copy((IntPtr)resultPtr, pooledBuffer, 0, size);
        return pooledBuffer;
    }

    public static void ReturnPooledBytes(byte[] bytes)
    {
        ArrayPool<byte>.Shared.Return(bytes);
    }

    public void SetSavePoint()
    {
        RocksDbNative.rocksdb_writebatch_set_save_point(RocksDbInterop.WriteBatch(handle));
    }

    public void RollbackToSavePoint()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_writebatch_rollback_to_save_point(RocksDbInterop.WriteBatch(handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void PopSavePoint()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_writebatch_pop_save_point(RocksDbInterop.WriteBatch(handle), &errptr);
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
    IWriteBatch IWriteBatch.Putv(int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
        => Putv(numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.PutvCf(IntPtr columnFamily, int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
        => PutvCf(columnFamily, numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle cf)
        => Merge(key, klen, val, vlen, cf);
    IWriteBatch IWriteBatch.MergeCf(IntPtr columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen)
        => MergeCf(columnFamily, key, klen, val, vlen);
    IWriteBatch IWriteBatch.Mergev(int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
        => Mergev(numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.MergevCf(IntPtr columnFamily, int numKeys, IntPtr keysList, IntPtr keysListSizes, int numValues, IntPtr valuesList, IntPtr valuesListSizes)
        => MergevCf(columnFamily, numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.Delete(byte[] key, ColumnFamilyHandle cf)
        => Delete(key, cf);
    IWriteBatch IWriteBatch.Delete(byte[] key, ulong klen, ColumnFamilyHandle cf)
        => Delete(key, klen, cf);
    IWriteBatch IWriteBatch.DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle cf)
        => DeleteRange(startKey, sklen, endKey, eklen, cf);
    IWriteBatch IWriteBatch.PutLogData(byte[] blob, ulong len)
        => PutLogData(blob, len);
    IWriteBatch IWriteBatch.Iterate(IntPtr state, PutDelegate put, DeletedDelegate deleted)
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
