// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers;
using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed class WriteBatch : IDisposable
{
    private nint handle;

    public WriteBatch() : this((nint)rocksdb_writebatch_create())
    {
    }

    internal WriteBatch(nint handle)
    {
        this.handle = handle;
    }

    public static WriteBatch FromSpan(ReadOnlySpan<byte> data)
    {
        fixed (byte* dataPtr = data)
        {
            var handle = (nint)rocksdb_writebatch_create_from((sbyte*)dataPtr, (nuint)data.Length);
            return new WriteBatch(handle);
        }
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

    /// <summary>The size in bytes of the batch's serialized representation.</summary>
    public nuint DataSize
    {
        get
        {
            nuint size;
            rocksdb_writebatch_data(RocksDbInterop.WriteBatch(handle), &size);
            return size;
        }
    }

    public WriteBatch Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null)
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

    public WriteBatch Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null)
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

    public WriteBatch Delete(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null)
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

    /// <summary>
    /// Records the deletion of every key in <c>[startKey, endKey)</c>.
    /// </summary>
    public WriteBatch DeleteRange(ReadOnlySpan<byte> startKey, ReadOnlySpan<byte> endKey, IColumnFamilyHandle? cf = null)
    {
        fixed (byte* startKeyPtr = &MemoryMarshal.GetReference(startKey))
        fixed (byte* endKeyPtr = &MemoryMarshal.GetReference(endKey))
        {
            if (cf is null)
            {
                rocksdb_writebatch_delete_range(RocksDbInterop.WriteBatch(handle), (sbyte*)startKeyPtr, (nuint)startKey.Length, (sbyte*)endKeyPtr, (nuint)endKey.Length);
            }
            else
            {
                rocksdb_writebatch_delete_range_cf(RocksDbInterop.WriteBatch(handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startKeyPtr, (nuint)startKey.Length, (sbyte*)endKeyPtr, (nuint)endKey.Length);
            }
        }
        return this;
    }

    /// <summary>
    /// Replays the batch into <paramref name="put"/> and <paramref name="deleted"/>, each of which
    /// must be a static method marked with
    /// <c>[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]</c> that catches all managed
    /// exceptions; an exception cannot unwind through the rocksdb frames that invoked it, so one
    /// that escapes terminates the process.
    /// </summary>
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
}
