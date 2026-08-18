// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe class Iterator : IDisposable
{
    private readonly IteratorHandle _handle;

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
        : this(handle, readOptions?.SafeHandle, readOptions?.Snapshot, dbLease)
    {
    }

    // No managed ReadOptions wrapper is held: the handle takes a reference to the native options
    // instead, so a wrapper the caller has let go of stays collectible while the iterator works.
    internal Iterator(nint handle, SafeHandle? readOptions, Snapshot? snapshot, RocksDbHandle? dbLease)
        => _handle = new IteratorHandle(handle, dbLease, readOptions, snapshot);

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

    /// <summary>Transfers the read options reference to the caller, who must release it once.</summary>
    internal SafeHandle? TakeReadOptions() => _handle.TakeReadOptions();

    /// <summary>Transfers the snapshot to the caller, leaving this iterator holding nothing.</summary>
    internal Snapshot? TakeSnapshot() => _handle.TakeSnapshot();

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

    /// <summary>
    /// Throws the error that ended the iteration, if any. An iterator reports a read failure by
    /// going invalid, which is otherwise indistinguishable from reaching the end of the range.
    /// </summary>
    /// <exception cref="RocksDbNativeException">The iteration failed.</exception>
    public void ThrowIfError()
    {
        sbyte* errptr = null;
        rocksdb_iter_get_error(RocksDbInterop.Iterator(NativeHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }
}
