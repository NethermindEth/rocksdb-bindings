// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe interface IWriteBatch : IDisposable
{
    nint Handle { get; }
    IWriteBatch Clear();
    int Count();
    IWriteBatch Put(string key, string val, Encoding? encoding = null);
    IWriteBatch Put(byte[] key, byte[] val, ColumnFamilyHandle? cf = null);
    IWriteBatch Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle? cf = null);

    IWriteBatch Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle? cf = null);
    void Put(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle? cf = null);
    IWriteBatch Putv(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes);
    IWriteBatch PutvCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes);
    IWriteBatch Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle? cf = null);
    void Merge(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle? cf = null);

    IWriteBatch Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle? cf = null);
    IWriteBatch MergeCf(nint columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen);
    void MergeCf(nint columnFamily, byte* key, ulong klen, byte* val, ulong vlen);
    IWriteBatch Mergev(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes);
    IWriteBatch MergevCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes);
    IWriteBatch Delete(byte[] key, ColumnFamilyHandle? cf = null);
    IWriteBatch Delete(byte[] key, ulong klen, ColumnFamilyHandle? cf = null);

    IWriteBatch Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle? cf = null);
    void Delete(byte* key, ulong klen, ColumnFamilyHandle? cf = null);
    void Deletev(int numKeys, nint keysList, nint keysListSizes, ColumnFamilyHandle? cf = null);
    IWriteBatch DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle? cf = null);
    void DeleteRange(byte* startKey, ulong sklen, byte* endKey, ulong eklen, ColumnFamilyHandle? cf = null);
    void DeleteRangev(int numKeys, nint startKeysList, nint startKeysListSizes, nint endKeysList, nint endKeysListSizes, ColumnFamilyHandle? cf = null);
    IWriteBatch PutLogData(byte[] blob, ulong len);
    /// <summary>
    /// Replays the batch into <paramref name="put"/> and <paramref name="deleted"/>, each of which
    /// must be a static method marked with
    /// <c>[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]</c> that catches all managed
    /// exceptions; an exception cannot unwind through the rocksdb frames that invoked it, so one
    /// that escapes terminates the process.
    /// </summary>
    IWriteBatch Iterate(void* state, delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted);
    byte[] ToBytes();
    byte[]? ToBytes(byte[] buffer, int offset = 0, int size = -1);
    void SetSavePoint();
    void RollbackToSavePoint();
}

public unsafe class WriteBatch : IWriteBatch, IDisposable
{
    private nint handle;
    private Encoding defaultEncoding = Encoding.UTF8;

    public WriteBatch() : this((nint)rocksdb_writebatch_create())
    {
    }

    public WriteBatch(byte[] rep, long size = -1)
    {
        fixed (byte* repPtr = rep)
        {
            handle = (nint)rocksdb_writebatch_create_from((sbyte*)repPtr, size < 0 ? (nuint)rep.Length : (nuint)size);
        }
    }

    public static WriteBatch FromSpan(ReadOnlySpan<byte> data)
    {
        fixed (byte* dataPtr = data)
        {
            var handle = (nint)rocksdb_writebatch_create_from((sbyte*)dataPtr, (nuint)data.Length);
            return new WriteBatch(handle);
        }
    }

    public WriteBatch(nint handle)
    {
        this.handle = handle;
    }

    public nint Handle { get { return handle; } }

    public void Dispose()
    {
        if (handle != nint.Zero)
        {
            rocksdb_writebatch_destroy(RocksDbInterop.WriteBatch(handle));
            handle = nint.Zero;
        }
    }

    public WriteBatch Clear()
    {
        rocksdb_writebatch_clear(RocksDbInterop.WriteBatch(handle));
        return this;
    }

    public int Count() => rocksdb_writebatch_count(RocksDbInterop.WriteBatch(handle));

    public WriteBatch Put(string key, string val, Encoding? encoding = null)
    {
        if (encoding is null)
        {
            encoding = defaultEncoding;
        }

        Put(encoding.GetBytes(key), encoding.GetBytes(val));
        return this;
    }

    public WriteBatch Put(byte[] key, byte[] val, ColumnFamilyHandle? cf = null) => Put(key, (ulong)key.Length, val, (ulong)val.Length, cf);

    public WriteBatch Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            Put(keyPtr, klen, valuePtr, vlen, cf);
        }

        return this;
    }

    public void Put(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle? cf = null)
    {
        if (cf is null)
        {
            rocksdb_writebatch_put(RocksDbInterop.WriteBatch(handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
        else
        {
            rocksdb_writebatch_put_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
    }

    public WriteBatch Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                rocksdb_writebatch_put(RocksDbInterop.WriteBatch(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                rocksdb_writebatch_put_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
        }
        return this;
    }

    public WriteBatch Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        fixed (byte* valuePtr = &MemoryMarshal.GetReference(value))
        {
            if (cf is null)
            {
                rocksdb_writebatch_merge(RocksDbInterop.WriteBatch(handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
            }
            else
            {
                rocksdb_writebatch_merge_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)value.Length);
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

    public WriteBatch PutVector(ReadOnlySpan<ReadOnlyMemory<byte>> keys, ReadOnlySpan<ReadOnlyMemory<byte>> values, ColumnFamilyHandle? columnFamily = null)
    {
        var intPtrPool = ArrayPool<nint>.Shared;
        var uintPtrPool = ArrayPool<nuint>.Shared;
        nint[]? keysListArray = null, valuesListArray = null;
        nuint[]? keysListSizesArray = null, valuesListSizesArray = null;

        try
        {
            var keysLength = keys.Length;
            (keysListArray, keysListSizesArray) = keysLength < 256
                ? (null, null)
                : (intPtrPool.Rent(keysLength), uintPtrPool.Rent(keysLength));
            Span<nint> keysList = keysLength < 256
                ? stackalloc nint[keysLength]
                : keysListArray!.AsSpan(0, keysLength);
            Span<nuint> keysListSizes = keysLength < 256
                ? stackalloc nuint[keysLength]
                : keysListSizesArray!.AsSpan(0, keysLength);
            using var keyHandles = CopyVector(keys, keysList, keysListSizes);

            var valuesLength = values.Length;
            (valuesListArray, valuesListSizesArray) = valuesLength < 256
                ? (null, null)
                : (intPtrPool.Rent(valuesLength), uintPtrPool.Rent(valuesLength));
            Span<nint> valuesList = valuesLength < 256
                ? stackalloc nint[valuesLength]
                : valuesListArray!.AsSpan(0, valuesLength);
            Span<nuint> valuesListSizes = valuesLength < 256
                ? stackalloc nuint[valuesLength]
                : valuesListSizesArray!.AsSpan(0, valuesLength);
            using var valuesDisposable = CopyVector(values, valuesList, valuesListSizes);

            fixed (void* keysListPtr = keysList,
                keysListSizesPtr = keysListSizes,
                valuesListPtr = valuesList,
                valuesListSizesPtr = valuesListSizes)
            {
                if (columnFamily is null)
                {
                    Putv(
                        keysLength, (nint)keysListPtr, (nint)keysListSizesPtr,
                        valuesLength, (nint)valuesListPtr, (nint)valuesListSizesPtr);
                }
                else
                {
                    PutvCf(columnFamily.Handle,
                        keysLength, (nint)keysListPtr, (nint)keysListSizesPtr,
                        valuesLength, (nint)valuesListPtr, (nint)valuesListSizesPtr);
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

    static IDisposable CopyVector(ReadOnlySpan<ReadOnlyMemory<byte>> items, Span<nint> itemsList, Span<nuint> itemsListSizes)
    {
        var disposable = new MemoryHandleManager(items.Length);
        for (var i = 0; i < items.Length; i++)
        {
            var handle = items[i].Pin();
            disposable.Add(handle);
            itemsList[i] = (nint)handle.Pointer;
            itemsListSizes[i] = (nuint)items[i].Length;
        }
        return disposable;
    }

    class MemoryHandleManager(int capacity) : IDisposable
    {
        readonly IList<MemoryHandle> handles = new List<MemoryHandle>(capacity);

        public void Add(MemoryHandle handle) => handles.Add(handle);

        public void Dispose()
        {
            for (int i = 0; i < handles.Count; i++)
            {
                handles[i].Dispose();
            }
        }
    }

    public WriteBatch Putv(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        rocksdb_writebatch_putv(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch PutvCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        rocksdb_writebatch_putv_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(columnFamily), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            Merge(keyPtr, klen, valuePtr, vlen, cf);
        }

        return this;
    }

    public void Merge(byte* key, ulong klen, byte* val, ulong vlen, ColumnFamilyHandle? cf = null)
    {
        if (cf is null)
        {
            rocksdb_writebatch_merge(RocksDbInterop.WriteBatch(handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
        else
        {
            rocksdb_writebatch_merge_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);
        }
    }

    public WriteBatch MergeCf(nint columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            MergeCf(columnFamily, keyPtr, klen, valuePtr, vlen);
        }
        return this;
    }

    public void MergeCf(nint columnFamily, byte* key, ulong klen, byte* val, ulong vlen) => rocksdb_writebatch_merge_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(columnFamily), (sbyte*)key, (nuint)klen, (sbyte*)val, (nuint)vlen);

    public WriteBatch Mergev(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        rocksdb_writebatch_mergev(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch MergevCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
    {
        rocksdb_writebatch_mergev_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(columnFamily), numKeys, (sbyte**)keysList, (nuint*)keysListSizes, numValues, (sbyte**)valuesList, (nuint*)valuesListSizes);
        return this;
    }

    public WriteBatch Delete(byte[] key, ColumnFamilyHandle? cf = null) => Delete(key, (ulong)key.Length, cf);

    public WriteBatch Delete(byte[] key, ulong klen, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = key)
        {
            Delete(keyPtr, klen, cf);
        }

        return this;
    }

    public void Delete(byte* key, ulong klen, ColumnFamilyHandle? cf = null)
    {
        if (cf is null)
        {
            rocksdb_writebatch_delete(RocksDbInterop.WriteBatch(handle), (sbyte*)key, (nuint)klen);
        }
        else
        {
            rocksdb_writebatch_delete_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, (nuint)klen);
        }
    }

    public WriteBatch Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        {
            if (cf is null)
            {
                rocksdb_writebatch_delete(RocksDbInterop.WriteBatch(handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
            else
            {
                rocksdb_writebatch_delete_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length);
            }
        }
        return this;
    }

    public void Deletev(int numKeys, nint keysList, nint keysListSizes, ColumnFamilyHandle? cf = null)
    {
        if (cf is null)
        {
            rocksdb_writebatch_deletev(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes);
        }
        else
        {
            rocksdb_writebatch_deletev_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), numKeys, (sbyte**)keysList, (nuint*)keysListSizes);
        }
    }

    public WriteBatch DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle? cf = null)
    {
        fixed (byte* startKeyPtr = startKey)
        fixed (byte* endKeyPtr = endKey)
        {
            DeleteRange(startKeyPtr, sklen, endKeyPtr, eklen, cf);
        }

        return this;
    }

    public void DeleteRange(byte* startKey, ulong sklen, byte* endKey, ulong eklen, ColumnFamilyHandle? cf = null)
    {
        if (cf is null)
        {
            rocksdb_writebatch_delete_range(RocksDbInterop.WriteBatch(handle), (sbyte*)startKey, (nuint)sklen, (sbyte*)endKey, (nuint)eklen);
        }
        else
        {
            rocksdb_writebatch_delete_range_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startKey, (nuint)sklen, (sbyte*)endKey, (nuint)eklen);
        }
    }

    public void DeleteRangev(int numKeys, nint startKeysList, nint startKeysListSizes, nint endKeysList, nint endKeysListSizes, ColumnFamilyHandle? cf = null)
    {
        if (cf is null)
        {
            rocksdb_writebatch_delete_rangev(RocksDbInterop.WriteBatch(handle), numKeys, (sbyte**)startKeysList, (nuint*)startKeysListSizes, (sbyte**)endKeysList, (nuint*)endKeysListSizes);
        }
        else
        {
            rocksdb_writebatch_delete_rangev_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), numKeys, (sbyte**)startKeysList, (nuint*)startKeysListSizes, (sbyte**)endKeysList, (nuint*)endKeysListSizes);
        }
    }

    public WriteBatch PutLogData(byte[] blob, ulong len)
    {
        fixed (byte* blobPtr = blob)
        {
            rocksdb_writebatch_put_log_data(RocksDbInterop.WriteBatch(handle), (sbyte*)blobPtr, (nuint)len);
        }
        return this;
    }

    public byte[] GetLogDataBytes() => ToBytes();

    /// <inheritdoc cref="IWriteBatch.Iterate" />
    public WriteBatch Iterate(void* state, delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted)
    {
        rocksdb_writebatch_iterate(
            RocksDbInterop.WriteBatch(handle),
            state,
            put,
            deleted);
        return this;
    }

    /// <summary>
    /// Get the write batch as bytes
    /// </summary>
    /// <returns></returns>
    public byte[] ToBytes()
    {
        nuint size;
        var resultPtr = rocksdb_writebatch_data(RocksDbInterop.WriteBatch(handle), &size);
        return RocksDbInterop.Bytes((nint)resultPtr, size)!;
    }

    /// <summary>
    /// Get the write batch as bytes
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="size"></param>
    /// <returns>null if size was not large enough to hold the data</returns>
    public byte[]? ToBytes(byte[] buffer, int offset = 0, int size = -1)
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
        var resultPtr = rocksdb_writebatch_data(RocksDbInterop.WriteBatch(handle), &sizePtr);
        size = (int)sizePtr;
        var pooledBuffer = ArrayPool<byte>.Shared.Rent(size);
        new ReadOnlySpan<byte>(resultPtr, size).CopyTo(pooledBuffer);
        return pooledBuffer;
    }

    public static void ReturnPooledBytes(byte[] bytes) => ArrayPool<byte>.Shared.Return(bytes);

    public void SetSavePoint() => rocksdb_writebatch_set_save_point(RocksDbInterop.WriteBatch(handle));

    public void RollbackToSavePoint()
    {
        sbyte* errptr = null;
        rocksdb_writebatch_rollback_to_save_point(RocksDbInterop.WriteBatch(handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void PopSavePoint()
    {
        sbyte* errptr = null;
        rocksdb_writebatch_pop_save_point(RocksDbInterop.WriteBatch(handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    IWriteBatch IWriteBatch.Clear()
        => Clear();
    IWriteBatch IWriteBatch.Put(string key, string val, Encoding? encoding)
        => Put(key, val, encoding);
    IWriteBatch IWriteBatch.Put(byte[] key, byte[] val, ColumnFamilyHandle? cf)
        => Put(key, val, cf);
    IWriteBatch IWriteBatch.Put(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle? cf)
        => Put(key, klen, val, vlen, cf);
    IWriteBatch IWriteBatch.Putv(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => Putv(numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.PutvCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => PutvCf(columnFamily, numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.Merge(byte[] key, ulong klen, byte[] val, ulong vlen, ColumnFamilyHandle? cf)
        => Merge(key, klen, val, vlen, cf);
    IWriteBatch IWriteBatch.MergeCf(nint columnFamily, byte[] key, ulong klen, byte[] val, ulong vlen)
        => MergeCf(columnFamily, key, klen, val, vlen);
    IWriteBatch IWriteBatch.Mergev(int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => Mergev(numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.MergevCf(nint columnFamily, int numKeys, nint keysList, nint keysListSizes, int numValues, nint valuesList, nint valuesListSizes)
        => MergevCf(columnFamily, numKeys, keysList, keysListSizes, numValues, valuesList, valuesListSizes);
    IWriteBatch IWriteBatch.Delete(byte[] key, ColumnFamilyHandle? cf)
        => Delete(key, cf);
    IWriteBatch IWriteBatch.Delete(byte[] key, ulong klen, ColumnFamilyHandle? cf)
        => Delete(key, klen, cf);
    IWriteBatch IWriteBatch.DeleteRange(byte[] startKey, ulong sklen, byte[] endKey, ulong eklen, ColumnFamilyHandle? cf)
        => DeleteRange(startKey, sklen, endKey, eklen, cf);
    IWriteBatch IWriteBatch.PutLogData(byte[] blob, ulong len)
        => PutLogData(blob, len);
    IWriteBatch IWriteBatch.Iterate(void* state, delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted)
        => Iterate(state, put, deleted);

    IWriteBatch IWriteBatch.Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle? cf)
        => Put(key, value, cf);
    IWriteBatch IWriteBatch.Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle? cf)
        => Merge(key, value, cf);
    IWriteBatch IWriteBatch.Delete(ReadOnlySpan<byte> key, ColumnFamilyHandle? cf)
        => Delete(key, cf);
}
