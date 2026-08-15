// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class Iterator : IDisposable
{
    private nint handle;

    // The native iterator reads from the read options directly, so they are held here to keep the
    // garbage collector from finalizing them while the iterator is still alive.
    private ReadOptions? ReadOptions { get; }

    internal Iterator(nint handle)
    {
        this.handle = handle;
    }

    internal Iterator(nint handle, ReadOptions? readOptions) : this(handle)
    {
        ReadOptions = readOptions;
    }

    public nint Handle { get { return handle; } }

    public void Dispose()
    {
        if (handle != nint.Zero)
        {
            rocksdb_iter_destroy(RocksDbInterop.Iterator(handle));
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

    public bool Valid() => rocksdb_iter_valid(RocksDbInterop.Iterator(handle)) != 0;

    public Iterator SeekToFirst()
    {
        rocksdb_iter_seek_to_first(RocksDbInterop.Iterator(handle));
        return this;
    }

    public Iterator SeekToLast()
    {
        rocksdb_iter_seek_to_last(RocksDbInterop.Iterator(handle));
        return this;
    }

    public unsafe Iterator Seek(byte* key, ulong klen)
    {
        rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)key, (nuint)klen);
        return this;
    }

    public Iterator Seek(byte[] key) => Seek(key, (ulong)key.GetLongLength(0));

    public Iterator Seek(byte[] key, ulong klen)
    {
        fixed (byte* keyPtr = key)
        {
            rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)klen);
        }
        return this;
    }

    public Iterator Seek(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)keyBytes.Length);
        }
        return this;
    }

    public unsafe Iterator Seek(ReadOnlySpan<byte> key)
    {
        fixed (byte* keyPtr = key)
        {
            rocksdb_iter_seek(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)key.Length);
            return this;
        }
    }

    public unsafe Iterator SeekForPrev(byte* key, ulong klen)
    {
        rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)key, (nuint)klen);
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
            rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)klen);
        }
        return this;
    }

    public Iterator SeekForPrev(string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)keyBytes.Length);
        }
        return this;
    }

    public unsafe Iterator SeekForPrev(ReadOnlySpan<byte> key)
    {
        fixed (byte* keyPtr = key)
        {
            rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(handle), (sbyte*)keyPtr, (nuint)key.Length);
            return this;
        }
    }

    public Iterator Next()
    {
        rocksdb_iter_next(RocksDbInterop.Iterator(handle));
        return this;
    }

    public Iterator Prev()
    {
        rocksdb_iter_prev(RocksDbInterop.Iterator(handle));
        return this;
    }

    public byte[]? Key()
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return RocksDbInterop.Bytes((nint)keyPtr, keyLength);
    }

    public byte[]? Value()
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return RocksDbInterop.Bytes((nint)valuePtr, valueLength);
    }

    public T? Key<T>(ISpanDeserializer<T> deserializer)
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return RocksDbInterop.Deserialize((nint)keyPtr, keyLength, deserializer);
    }

    public T? Value<T>(ISpanDeserializer<T> deserializer)
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return RocksDbInterop.Deserialize((nint)valuePtr, valueLength, deserializer);
    }

    public unsafe ReadOnlySpan<byte> GetKeySpan()
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return new ReadOnlySpan<byte>((byte*)keyPtr, (int)keyLength);
    }

    public unsafe ReadOnlySpan<byte> GetValueSpan()
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return new ReadOnlySpan<byte>((byte*)valuePtr, (int)valueLength);
    }

    public T? Key<T>(Func<Stream, T> deserializer)
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return RocksDbInterop.Deserialize((nint)keyPtr, keyLength, deserializer);
    }

    public T? Value<T>(Func<Stream, T> deserializer)
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return RocksDbInterop.Deserialize((nint)valuePtr, valueLength, deserializer);
    }

    public string StringKey()
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(handle), &keyLength);
        return Encoding.UTF8.GetString((byte*)keyPtr, (int)keyLength);
    }

    public string StringValue()
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(handle), &valueLength);
        return Encoding.UTF8.GetString((byte*)valuePtr, (int)valueLength);
    }

    // TODO: figure out how to best implement rocksdb_iter_get_error
}
