// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class Iterator : IDisposable
{
    private readonly IteratorHandle _handle;

    // The native iterator reads from the read options directly, so they are held here to keep the
    // garbage collector from finalizing them while the iterator is still alive.
    internal ReadOptions? ReadOptions { get; }

    internal Iterator(nint handle) : this(handle, readOptions: null)
    {
    }

    internal Iterator(nint handle, ReadOptions? readOptions) : this(handle, readOptions, dbLease: null)
    {
    }

    // The lease is an acquired ref on the database handle: while the iterator lives, the native
    // close is deferred, and if the iterator is abandoned the handle's critical finalizer both
    // destroys it and releases the lease.
    internal Iterator(nint handle, ReadOptions? readOptions, RocksDbHandle? dbLease)
    {
        _handle = new IteratorHandle(handle, dbLease);
        ReadOptions = readOptions;
    }

    public nint Handle => _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    // Iterator operations are deliberately unguarded: adding a lease per Seek/Next would tax the
    // hottest loops, so using a disposed iterator remains undefined, as it always was.
    private nint NativeHandle => _handle.DangerousGetHandle();

    public void Dispose() => _handle.Dispose();

    /// <summary>
    /// Hands native ownership of the iterator to the caller without destroying it. Take the
    /// database lease first if it must survive.
    /// </summary>
    internal nint Detach()
    {
        var r = _handle.DangerousGetHandle();
        _handle.SetHandleAsInvalid();
        return r;
    }

    /// <summary>Transfers the database lease to the caller, who must release it exactly once.</summary>
    internal RocksDbHandle? TakeDbLease() => _handle.TakeDbLease();

    public bool Valid() => rocksdb_iter_valid(RocksDbInterop.Iterator(NativeHandle)) != 0;

    public Iterator SeekToFirst()
    {
        rocksdb_iter_seek_to_first(RocksDbInterop.Iterator(NativeHandle));
        return this;
    }

    public Iterator SeekToLast()
    {
        rocksdb_iter_seek_to_last(RocksDbInterop.Iterator(NativeHandle));
        return this;
    }

    public Iterator Seek(ReadOnlySpan<byte> key)
    {
        fixed (byte* keyPtr = key)
        {
            rocksdb_iter_seek(RocksDbInterop.Iterator(NativeHandle), (sbyte*)keyPtr, (nuint)key.Length);
            return this;
        }
    }

    public Iterator SeekForPrev(ReadOnlySpan<byte> key)
    {
        fixed (byte* keyPtr = key)
        {
            rocksdb_iter_seek_for_prev(RocksDbInterop.Iterator(NativeHandle), (sbyte*)keyPtr, (nuint)key.Length);
            return this;
        }
    }

    public Iterator Next()
    {
        rocksdb_iter_next(RocksDbInterop.Iterator(NativeHandle));
        return this;
    }

    public Iterator Prev()
    {
        rocksdb_iter_prev(RocksDbInterop.Iterator(NativeHandle));
        return this;
    }

    public byte[]? Key()
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(NativeHandle), &keyLength);
        return RocksDbInterop.Bytes((nint)keyPtr, keyLength);
    }

    public byte[]? Value()
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(NativeHandle), &valueLength);
        return RocksDbInterop.Bytes((nint)valuePtr, valueLength);
    }

    public T? Key<T>(ISpanDeserializer<T> deserializer)
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(NativeHandle), &keyLength);
        return RocksDbInterop.Deserialize((nint)keyPtr, keyLength, deserializer);
    }

    public T? Value<T>(ISpanDeserializer<T> deserializer)
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(NativeHandle), &valueLength);
        return RocksDbInterop.Deserialize((nint)valuePtr, valueLength, deserializer);
    }

    public ReadOnlySpan<byte> GetKeySpan()
    {
        nuint keyLength;
        var keyPtr = rocksdb_iter_key(RocksDbInterop.Iterator(NativeHandle), &keyLength);
        return new ReadOnlySpan<byte>((byte*)keyPtr, (int)keyLength);
    }

    public ReadOnlySpan<byte> GetValueSpan()
    {
        nuint valueLength;
        var valuePtr = rocksdb_iter_value(RocksDbInterop.Iterator(NativeHandle), &valueLength);
        return new ReadOnlySpan<byte>((byte*)valuePtr, (int)valueLength);
    }

    // TODO: figure out how to best implement rocksdb_iter_get_error
}
