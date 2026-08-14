// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Nethermind.RocksDbBindings;

public unsafe class Iterator : IDisposable
{
    private nint handle;
#pragma warning disable CS0414
    private ReadOptions? readOptions;
#pragma warning restore CS0414

    internal Iterator(nint handle)
    {
        this.handle = handle;
    }

    internal Iterator(nint handle, ReadOptions? readOptions) : this(handle)
    {
        // Note: passing readOptions in here has no actual effect except to keep readOptions
        // from being garbage collected whilst the Iterator is still alive because the
        // the iterator on the native side will actually read things from some of the readOptions
        // directly
        this.readOptions = readOptions;
    }

    public nint Handle { get { return handle; } }

    public void Dispose()
    {
        if (handle != nint.Zero)
        {
            RocksDbNative.rocksdb_iter_destroy(RocksDbInterop.Iterator(handle));
            handle = nint.Zero;
        }
    }

    /// <summary>
    /// Detach the iterator from its handle but don't dispose the handle
    /// </summary>
    /// <returns></returns>
    public nint Detach()
    {
        var r = handle;
        handle = nint.Zero;
        return r;
    }

    public bool Valid()
    {
        return RocksDbNative.rocksdb_iter_valid(RocksDbInterop.Iterator(handle)) != 0;
    }

    public Iterator SeekToFirst()
    {
        RocksDbNative.rocksdb_iter_seek_to_first(RocksDbInterop.Iterator(handle));
        return this;
    }

    public Iterator SeekToLast()
    {
        RocksDbNative.rocksdb_iter_seek_to_last(RocksDbInterop.Iterator(handle));
        return this;
    }

    public unsafe Iterator Seek(byte* key, ulong klen)
    {
        RocksDbNative.rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)key, (nuint)klen);
        return this;
    }

    public Iterator Seek(byte[] key)
    {
        return Seek(key, (ulong)key.GetLongLength(0));
    }

    public Iterator Seek(byte[] key, ulong klen)
    {
        fixed (byte* keyPtr = key)
        {
            RocksDbNative.rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)klen);
        }
        return this;
    }

    public Iterator Seek(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            RocksDbNative.rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)keyBytes.Length);
        }
        return this;
    }

    public unsafe Iterator Seek(ReadOnlySpan<byte> key)
    {
        fixed (byte* keyPtr = key)
        {
            RocksDbNative.rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)key.Length);
            return this;
        }
    }

    public unsafe Iterator SeekForPrev(byte* key, ulong klen)
    {
        RocksDbNative.rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)key, (nuint)klen);
        return this;
    }

    public Iterator SeekForPrev(byte[] key)
    {
        SeekForPrev(key, (ulong)key.Length);
        return this;
    }

    public Iterator SeekForPrev(byte[] key, ulong klen)
    {
        fixed (byte* keyPtr = key)
        {
            RocksDbNative.rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)klen);
        }
        return this;
    }

    public Iterator SeekForPrev(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            RocksDbNative.rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)keyBytes.Length);
        }
        return this;
    }

    public unsafe Iterator SeekForPrev(ReadOnlySpan<byte> key)
    {
        fixed (byte* keyPtr = key)
        {
            RocksDbNative.rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)key.Length);
            return this;
        }
    }

    public Iterator Next()
    {
        RocksDbNative.rocksdb_iter_next(RocksDbInterop.Iterator(handle));
        return this;
    }

    public Iterator Prev()
    {
        RocksDbNative.rocksdb_iter_prev(RocksDbInterop.Iterator(handle));
        return this;
    }

    public byte[]? Key()
    {
        nuint keyLength;
        var keyPtr = RocksDbNative.rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return RocksDbInterop.Bytes((nint)keyPtr, keyLength);
    }

    public byte[]? Value()
    {
        nuint valueLength;
        var valuePtr = RocksDbNative.rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return RocksDbInterop.Bytes((nint)valuePtr, valueLength);
    }

    public T? Key<T>(ISpanDeserializer<T> deserializer)
    {
        nuint keyLength;
        var keyPtr = RocksDbNative.rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return RocksDbInterop.Deserialize((nint)keyPtr, keyLength, deserializer);
    }

    public T? Value<T>(ISpanDeserializer<T> deserializer)
    {
        nuint valueLength;
        var valuePtr = RocksDbNative.rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return RocksDbInterop.Deserialize((nint)valuePtr, valueLength, deserializer);
    }

    public unsafe ReadOnlySpan<byte> GetKeySpan()
    {
        nuint keyLength;
        var keyPtr = RocksDbNative.rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return new ReadOnlySpan<byte>((byte*)keyPtr, (int)keyLength);
    }

    public unsafe ReadOnlySpan<byte> GetValueSpan()
    {
        nuint valueLength;
        var valuePtr = RocksDbNative.rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return new ReadOnlySpan<byte>((byte*)valuePtr, (int)valueLength);
    }

    public T? Key<T>(Func<Stream, T> deserializer)
    {
        nuint keyLength;
        var keyPtr = RocksDbNative.rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return RocksDbInterop.Deserialize((nint)keyPtr, keyLength, deserializer);
    }

    public T? Value<T>(Func<Stream, T> deserializer)
    {
        nuint valueLength;
        var valuePtr = RocksDbNative.rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return RocksDbInterop.Deserialize((nint)valuePtr, valueLength, deserializer);
    }

    public string StringKey()
    {
        nuint keyLength;
        var keyPtr = RocksDbNative.rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return Encoding.UTF8.GetString((byte*)keyPtr, (int)keyLength);
    }

    public string StringValue()
    {
        nuint valueLength;
        var valuePtr = RocksDbNative.rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return Encoding.UTF8.GetString((byte*)valuePtr, (int)valueLength);
    }

    // TODO: figure out how to best implement rocksdb_iter_get_error
}
