// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed class RocksDb : IDisposable
{
    internal static ReadOptions DefaultReadOptions { get; } = new ReadOptions();
    internal static DbOptions DefaultOptions { get; } = new DbOptions();
    internal static WriteOptions DefaultWriteOptions { get; } = new WriteOptions();
    internal static Encoding DefaultEncoding => Encoding.UTF8;
    private Dictionary<string, ColumnFamilyHandleInternal>? columnFamilies;

    // Serializes column family creation, dropping, and lookup: a lease keeps the database open
    // but does not serialize callers, and RocksDB documents the DB as safe for concurrent use.
    private readonly Lock _columnFamilyLock = new();

    private readonly RocksDbHandle _handle;

    // SafeHandle.Dispose does not mark the handle closed while children still hold references,
    // so logical disposal is tracked separately to make later calls throw immediately.
    private volatile bool _disposed;

    public nint Handle => _disposed || _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    // Used under an acquired lease, where the raw pointer is valid by construction; the public
    // property must not be used there, because its zero-after-dispose check races a concurrent
    // Dispose even while the lease keeps the native handle alive.
    private nint NativeHandle => _handle.DangerousGetHandle();

    public string Path { get; internal set; } = null!;
    public string? WalPath { get; internal set; }
    public string? LogPath { get; internal set; }

    private RocksDb(nint handle, DbOptions? options, Dictionary<string, ColumnFamilyHandleInternal>? columnFamilies = null)
    {
        _handle = new RocksDbHandle(handle);
        // Snapshotted rather than reached through the options later: those can be pointed at another
        // environment once this database is open, and RocksDB holds a pointer to this one. Nothing
        // else about the options has to outlive the open, because RocksDB copies them.
        _handle.Env = options?.Env;
        this.columnFamilies = columnFamilies;

        // The handle owns and destroys every column family handle right before the native close,
        // which is the first moment no leased call can still be using them. The dictionary here
        // stays lookup-only.
        if (columnFamilies is not null)
        {
            foreach (ColumnFamilyHandleInternal cfh in columnFamilies.Values)
            {
                _handle.RegisterOwnedColumnFamily(cfh);
            }
        }
    }

    /// <summary>
    /// Releases this handle on the database; calls made after this throw
    /// <see cref="ObjectDisposedException"/>. The native close itself runs once the last child —
    /// iterator, snapshot, checkpoint, or WAL iterator — has also been released.
    /// </summary>
    public void Dispose()
    {
        _disposed = true;
        _handle.Dispose();
    }

    /// <summary>
    /// Guards a native call: the database cannot close while the lease is held, and a disposed
    /// database throws <see cref="ObjectDisposedException"/> instead of reaching native code.
    /// </summary>
    private HandleLease Lease()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        return new HandleLease(_handle);
    }

    // Acquires the ref a child object holds for its lifetime; the child must DangerousRelease
    // exactly once when disposed.
    private RocksDbHandle AcquireChildLease()
    {
        var added = false;
        _handle.DangerousAddRef(ref added);
        return _handle;
    }

    /// <summary>
    /// Guards a call another wrapper makes against this database; the raw pointer is valid for
    /// exactly as long as the returned lease is held.
    /// </summary>
    internal HandleLease LeaseHandle(out nint nativeHandle)
    {
        HandleLease lease = Lease();
        nativeHandle = _handle.DangerousGetHandle();
        return lease;
    }

    public static RocksDb Open(DbOptions options, string path)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open(RocksDbInterop.Options(optionsHandle), (sbyte*)pathSafe.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb OpenReadOnly(DbOptions options, string path, bool errorIfLogFileExists)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open_for_read_only(RocksDbInterop.Options(optionsHandle), (sbyte*)pathSafe.Handle, RocksDbInterop.Bool(errorIfLogFileExists), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb OpenAsSecondary(DbOptions options, string path, string secondaryPath)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        using var secondaryPathSafe = new TransientUtf8String(secondaryPath);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open_as_secondary(RocksDbInterop.Options(optionsHandle), (sbyte*)pathSafe.Handle, (sbyte*)secondaryPathSafe.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb OpenWithTtl(DbOptions options, string path, int ttlSeconds)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open_with_ttl(RocksDbInterop.Options(optionsHandle), (sbyte*)pathSafe.Handle, ttlSeconds, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb Open(DbOptions options, string path, ColumnFamilies columnFamilies)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        string[] cfnames = [.. columnFamilies.Names];
        using var cfOptionsLease = columnFamilies.LeaseOptions();
        nint[] cfoptions = cfOptionsLease.Handles;
        nint[] cfhandles = new nint[cfnames.Length];
        using var cfNameArray = new NativeUtf8StringArray(cfnames);
        fixed (nint* cfOptionsPtr = cfoptions)
        fixed (nint* cfHandlesPtr = cfhandles)
        {
            sbyte* errptr = null;
            nint db = (nint)rocksdb_open_column_families(
                RocksDbInterop.Options(optionsHandle),
                (sbyte*)pathSafe.Handle,
                cfnames.Length,
                cfNameArray.Pointer,
                (rocksdb_options_t**)cfOptionsPtr,
                (rocksdb_column_family_handle_t**)cfHandlesPtr,
                &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            var cfHandleMap = new Dictionary<string, ColumnFamilyHandleInternal>();
            foreach (var pair in cfnames.Zip(cfhandles.Select(cfh => new ColumnFamilyHandleInternal(cfh)), (name, cfh) => new { Name = name, Handle = cfh }))
            {
                cfHandleMap.Add(pair.Name, pair.Handle);
            }

            return new RocksDb(db, options, cfHandleMap)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenReadOnly(DbOptions options, string path, ColumnFamilies columnFamilies, bool errIfLogFileExists)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        string[] cfnames = [.. columnFamilies.Names];
        using var cfOptionsLease = columnFamilies.LeaseOptions();
        nint[] cfoptions = cfOptionsLease.Handles;
        nint[] cfhandles = new nint[cfnames.Length];
        using var cfNameArray = new NativeUtf8StringArray(cfnames);
        fixed (nint* cfOptionsPtr = cfoptions)
        fixed (nint* cfHandlesPtr = cfhandles)
        {
            sbyte* errptr = null;
            nint db = (nint)rocksdb_open_for_read_only_column_families(
                RocksDbInterop.Options(optionsHandle),
                (sbyte*)pathSafe.Handle,
                cfnames.Length,
                cfNameArray.Pointer,
                (rocksdb_options_t**)cfOptionsPtr,
                (rocksdb_column_family_handle_t**)cfHandlesPtr,
                RocksDbInterop.Bool(errIfLogFileExists),
                &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            var cfHandleMap = new Dictionary<string, ColumnFamilyHandleInternal>();
            foreach (var pair in cfnames.Zip(cfhandles.Select(cfh => new ColumnFamilyHandleInternal(cfh)), (name, cfh) => new { Name = name, Handle = cfh }))
            {
                cfHandleMap.Add(pair.Name, pair.Handle);
            }

            return new RocksDb(db, options, cfHandleMap)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenAsSecondary(DbOptions options, string path, string secondaryPath, ColumnFamilies columnFamilies)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        using var secondaryPathSafe = new TransientUtf8String(secondaryPath);
        string[] cfnames = [.. columnFamilies.Names];
        using var cfOptionsLease = columnFamilies.LeaseOptions();
        nint[] cfoptions = cfOptionsLease.Handles;
        nint[] cfhandles = new nint[cfnames.Length];
        using var cfNameArray = new NativeUtf8StringArray(cfnames);
        fixed (nint* cfOptionsPtr = cfoptions)
        fixed (nint* cfHandlesPtr = cfhandles)
        {
            sbyte* errptr = null;
            var db = (nint)rocksdb_open_as_secondary_column_families(
                RocksDbInterop.Options(optionsHandle),
                (sbyte*)pathSafe.Handle,
                (sbyte*)secondaryPathSafe.Handle,
                cfnames.Length,
                cfNameArray.Pointer,
                (rocksdb_options_t**)cfOptionsPtr,
                (rocksdb_column_family_handle_t**)cfHandlesPtr,
                &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            var cfHandleMap = new Dictionary<string, ColumnFamilyHandleInternal>();
            foreach (var pair in cfnames.Zip(cfhandles.Select(cfh => new ColumnFamilyHandleInternal(cfh)), (name, cfh) => new { Name = name, Handle = cfh }))
            {
                cfHandleMap.Add(pair.Name, pair.Handle);
            }
            return new RocksDb(db, options, cfHandleMap)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    /// <summary>
    /// Starts a checkpoint, which <see cref="RocksDbBindings.Checkpoint.Save"/> writes out. The
    /// database stays open until the returned checkpoint is disposed.
    /// </summary>
    public Checkpoint Checkpoint()
    {
        using var lease = Lease();
        sbyte* errptr = null;
        var checkpoint = (nint)rocksdb_checkpoint_object_create(RocksDbInterop.Db(NativeHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new Checkpoint(checkpoint, AcquireChildLease());
    }

    // Enumerates the options exactly once: the native call indexes both arrays through one
    // count, so keys and values must come from the same stable snapshot.
    private static (string[] Keys, string[] Values) SplitOptions(IEnumerable<KeyValuePair<string, string>> options)
    {
        KeyValuePair<string, string>[] pairs = [.. options];
        var keys = new string[pairs.Length];
        var values = new string[pairs.Length];
        for (var i = 0; i < pairs.Length; i++)
            (keys[i], values[i]) = pairs[i];

        return (keys, values);
    }

    public void SetOptions(IEnumerable<KeyValuePair<string, string>> options)
    {
        using var lease = Lease();
        var (keys, values) = SplitOptions(options);
        using var nativeKeys = new NativeUtf8StringArray(keys);
        using var nativeValues = new NativeUtf8StringArray(values);
        sbyte* errptr = null;
        rocksdb_set_options(RocksDbInterop.Db(NativeHandle), keys.Length, nativeKeys.Pointer, nativeValues.Pointer, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Changes mutable options of a single column family at runtime.</summary>
    public void SetOptions(IColumnFamilyHandle cf, IEnumerable<KeyValuePair<string, string>> options)
    {
        using var lease = Lease();
        var (keys, values) = SplitOptions(options);
        using var nativeKeys = new NativeUtf8StringArray(keys);
        using var nativeValues = new NativeUtf8StringArray(values);
        sbyte* errptr = null;
        rocksdb_set_options_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ColumnFamily(cf.Handle), keys.Length, nativeKeys.Pointer, nativeValues.Pointer, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public byte[]? Get(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return Get(keyPtr, (nuint)key.Length, cf, readOptions);
        }
    }

    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public bool GetFixedSizeValue(ReadOnlySpan<byte> key, Span<byte> fixedSizeValueOutput, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        if (!TryGetPinned(key, out var slice, cf, readOptions))
            return false;

        try
        {
            if (slice.Value.Length != fixedSizeValueOutput.Length)
                return false;
            slice.Value.CopyTo(fixedSizeValueOutput);
            return true;
        }
        finally
        {
            slice.Dispose();
        }
    }

    /// <summary>
    /// Reads the value for <paramref name="key"/> without copying it out of RocksDB-owned memory.
    /// </summary>
    /// <returns>
    /// True when the key exists; <paramref name="slice"/> then holds the value and must be disposed.
    /// </returns>
    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public bool TryGetPinned(scoped ReadOnlySpan<byte> key, out PinnedSlice slice, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        rocksdb_pinnableslice_t* pinned;
        fixed (byte* keyPtr = key)
        {
            pinned = GetPinned(keyPtr, (nuint)key.Length, cf, readOptions);
        }

        if (pinned is null)
        {
            slice = default;
            return false;
        }

        nuint valueLength;
        var valuePtr = rocksdb_pinnableslice_value(pinned, &valueLength);
        if (valuePtr is null)
        {
            rocksdb_pinnableslice_destroy(pinned);
            slice = default;
            return false;
        }

        // Destroy before throwing: nothing owns the handle until the PinnedSlice is constructed.
        if (valueLength > int.MaxValue)
        {
            rocksdb_pinnableslice_destroy(pinned);
            throw new NotSupportedException($"The value is {valueLength} bytes; values over {int.MaxValue} bytes cannot be exposed as a span.");
        }

        slice = new PinnedSlice((nint)pinned, (nint)valuePtr, (int)valueLength);
        return true;
    }

    /// <summary>
    /// Reads the value for <paramref name="key"/> into a native allocation independent of the
    /// database, returned as a span the caller owns.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="TryGetPinned(ReadOnlySpan{byte}, out PinnedSlice, IColumnFamilyHandle?, ReadOptions?)"/>,
    /// which pins block-cache or memtable memory until released, this copies the value into its
    /// own allocation, so the span may be held for a long time without affecting the database.
    /// Every non-empty returned span must be released exactly once with
    /// <see cref="DangerousReleaseMemory"/>. An empty span means the key does not exist or its
    /// value is empty, and needs no release; use
    /// <see cref="TryGetPinned(ReadOnlySpan{byte}, out PinnedSlice, IColumnFamilyHandle?, ReadOptions?)"/>
    /// to distinguish the two.
    /// </remarks>
    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public Span<byte> GetSpan(scoped ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        using var lease = Lease();
        var options = readOptions ?? DefaultReadOptions;
        nint optionsHandle = options.DangerousGetHandle();
        nuint valueLength;
        sbyte* errptr = null;
        sbyte* valuePtr;
        fixed (byte* keyPtr = key)
        {
            valuePtr = cf is null
                ? rocksdb_get(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle), (sbyte*)keyPtr, (nuint)key.Length, &valueLength, &errptr)
                : rocksdb_get_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length, &valueLength, &errptr);
        }
        GC.KeepAlive(options);
        RocksDbInterop.ThrowIfError(errptr);

        if (valuePtr is null)
            return default;

        if (valueLength > int.MaxValue)
        {
            rocksdb_free(valuePtr);
            throw new NotSupportedException($"The value is {valueLength} bytes; values over {int.MaxValue} bytes cannot be exposed as a span.");
        }

        // An empty span is never released by the caller, so its allocation is freed here.
        if (valueLength == 0)
        {
            rocksdb_free(valuePtr);
            return default;
        }

        return new Span<byte>(valuePtr, (int)valueLength);
    }

    /// <summary>
    /// Frees a native allocation returned by <see cref="GetSpan"/>. Call exactly once per
    /// non-empty span, and never touch the span afterwards.
    /// </summary>
    [SuppressMessage("Performance", "CA1822:Mark members as static",
        Justification = "Instance method to pair with the instance GetSpan and to fit the caller's own instance abstractions.")]
    public void DangerousReleaseMemory(in ReadOnlySpan<byte> span)
    {
        if (!span.IsEmpty)
            rocksdb_free(Unsafe.AsPointer(ref MemoryMarshal.GetReference(span)));
    }

    /// <summary>
    /// Copies the value for <paramref name="key"/> into <paramref name="destination"/>.
    /// </summary>
    /// <returns>The value length, or -1 when the key does not exist.</returns>
    /// <exception cref="ArgumentException">The value does not fit in <paramref name="destination"/>.</exception>
    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public int Get(ReadOnlySpan<byte> key, Span<byte> destination, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        if (!TryGetPinned(key, out var slice, cf, readOptions))
            return -1;

        try
        {
            var value = slice.Value;
            if (value.Length > destination.Length)
                throw new ArgumentException($"The value is {value.Length} bytes but the destination holds {destination.Length}.", nameof(destination));

            value.CopyTo(destination);
            return value.Length;
        }
        finally
        {
            slice.Dispose();
        }
    }

    public bool HasKey(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return HasKey(keyPtr, (nuint)key.Length, cf, readOptions);
        }
    }

    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public T? Get<T>(ReadOnlySpan<byte> key, ISpanDeserializer<T> deserializer, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        if (!TryGetPinned(key, out var slice, cf, readOptions))
            return default;

        try
        {
            return deserializer.Deserialize(slice.Value);
        }
        finally
        {
            slice.Dispose();
        }
    }

    public KeyValuePair<byte[], byte[]?>[] MultiGet(byte[][] keys, IColumnFamilyHandle[]? cf = null, ReadOptions? readOptions = null)
    {
        ArgumentNullException.ThrowIfNull(keys);
        using var lease = Lease();

        var count = keys.Length;
        if (cf is not null && cf.Length != count)
            throw new ArgumentException("Column family handle count must match key count.", nameof(cf));

        var options = readOptions ?? DefaultReadOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        var result = new KeyValuePair<byte[], byte[]?>[count];
        var keyHandles = new PinnedGCHandle<byte[]>[count];
        var keyPtrs = new sbyte*[count];
        var keyLengths = new nuint[count];
        var valuePtrs = new sbyte*[count];
        var valueLengths = new nuint[count];
        var errptrs = new sbyte*[count];
        rocksdb_column_family_handle_t*[]? cfHandles = cf is null
            ? null : new rocksdb_column_family_handle_t*[count];

        try
        {
            for (var i = 0; i < count; i++)
            {
                if (keys[i] is null)
                    throw new ArgumentException("Keys cannot contain null values.", nameof(keys));

                keyHandles[i] = new PinnedGCHandle<byte[]>(keys[i]);
                keyPtrs[i] = (sbyte*)keyHandles[i].GetAddressOfArrayData();
                keyLengths[i] = (nuint)keys[i].Length;

                cfHandles?[i] = RocksDbInterop.ColumnFamily(cf![i].Handle);
            }

            fixed (sbyte** keyPtrsPtr = keyPtrs)
            fixed (nuint* keyLengthsPtr = keyLengths)
            fixed (sbyte** valuePtrsPtr = valuePtrs)
            fixed (nuint* valueLengthsPtr = valueLengths)
            fixed (sbyte** errptrsPtr = errptrs)
            {
                if (cfHandles is null)
                {
                    rocksdb_multi_get(
                        RocksDbInterop.Db(NativeHandle),
                        RocksDbInterop.ReadOptions(optionsHandle),
                        (nuint)count,
                        keyPtrsPtr,
                        keyLengthsPtr,
                        valuePtrsPtr,
                        valueLengthsPtr,
                        errptrsPtr);
                }
                else
                {
                    fixed (rocksdb_column_family_handle_t** cfHandlesPtr = cfHandles)
                    {
                        rocksdb_multi_get_cf(
                            RocksDbInterop.Db(NativeHandle),
                            RocksDbInterop.ReadOptions(optionsHandle),
                            cfHandlesPtr,
                            (nuint)count,
                            keyPtrsPtr,
                            keyLengthsPtr,
                            valuePtrsPtr,
                            valueLengthsPtr,
                            errptrsPtr);
                    }
                }
            }


            sbyte* firstError = null;
            for (var i = 0; i < count; i++)
            {
                result[i] = new KeyValuePair<byte[], byte[]?>(keys[i], RocksDbInterop.BytesAndFree(valuePtrs[i], valueLengths[i]));
                if (errptrs[i] == null)
                    continue;

                if (firstError == null)
                    firstError = errptrs[i];
                else
                    rocksdb_free(errptrs[i]);
            }

            RocksDbInterop.ThrowIfError(firstError);
            return result;
        }
        finally
        {
            for (var i = 0; i < keyHandles.Length; i++)
                keyHandles[i].Dispose();
        }
    }

    private rocksdb_pinnableslice_t* GetPinned(byte* key, nuint keyLength, IColumnFamilyHandle? cf, ReadOptions? readOptions)
    {
        using var lease = Lease();
        var options = readOptions ?? DefaultReadOptions;
        nint optionsHandle = options.DangerousGetHandle();
        sbyte* errptr = null;
        var pinned = cf is null
            ? rocksdb_get_pinned(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle), (sbyte*)key, keyLength, &errptr)
            : rocksdb_get_pinned_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &errptr);
        GC.KeepAlive(options);
        RocksDbInterop.ThrowIfError(errptr);
        return pinned;
    }

    // Reads through a pinned slice instead of rocksdb_get: one copy into the managed array
    // rather than a native malloc, a copy, and a free.
    private byte[]? Get(byte* key, nuint keyLength, IColumnFamilyHandle? cf, ReadOptions? readOptions)
    {
        var pinned = GetPinned(key, keyLength, cf, readOptions);
        if (pinned is null)
            return null;

        try
        {
            nuint valueLength;
            var valuePtr = rocksdb_pinnableslice_value(pinned, &valueLength);
            if (valuePtr is null)
                return null;

            var result = new byte[checked((int)valueLength)];
            new ReadOnlySpan<byte>(valuePtr, result.Length).CopyTo(result);
            return result;
        }
        finally
        {
            rocksdb_pinnableslice_destroy(pinned);
        }
    }

    private bool HasKey(byte* key, nuint keyLength, IColumnFamilyHandle? cf, ReadOptions? readOptions)
    {
        var pinned = GetPinned(key, keyLength, cf, readOptions);
        if (pinned is null)
            return false;
        rocksdb_pinnableslice_destroy(pinned);
        return true;
    }

    private void Remove(byte* key, nuint keyLength, IColumnFamilyHandle? cf, WriteOptions? writeOptions)
    {
        using var lease = Lease();
        var options = writeOptions ?? DefaultWriteOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_delete(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), (sbyte*)key, keyLength, &errptr);
        }
        else
        {
            rocksdb_delete_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    private void Put(byte* key, nuint keyLength, byte* value, nuint valueLength, IColumnFamilyHandle? cf, WriteOptions? writeOptions)
    {
        using var lease = Lease();
        var options = writeOptions ?? DefaultWriteOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_put(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        else
        {
            rocksdb_put_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    private void Merge(byte* key, nuint keyLength, byte* value, nuint valueLength, IColumnFamilyHandle? cf, WriteOptions? writeOptions)
    {
        using var lease = Lease();
        var options = writeOptions ?? DefaultWriteOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_merge(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        else
        {
            rocksdb_merge_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Write(WriteBatch writeBatch, WriteOptions? writeOptions = null)
    {
        using var lease = Lease();
        sbyte* errptr = null;
        var options = writeOptions ?? DefaultWriteOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        rocksdb_write(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), RocksDbInterop.WriteBatch(writeBatch.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Write(WriteBatchWithIndex writeBatch, WriteOptions? writeOptions = null)
    {
        using var lease = Lease();
        sbyte* errptr = null;
        var options = writeOptions ?? DefaultWriteOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        rocksdb_write_writebatch_wi(RocksDbInterop.Db(NativeHandle), RocksDbInterop.WriteOptions(optionsHandle), RocksDbInterop.WriteBatchWithIndex(writeBatch.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Remove(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        {
            Remove(keyPtr, (nuint)key.Length, cf, writeOptions);
        }
    }

    public void Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Put(keyPtr, (nuint)key.Length, valuePtr, (nuint)value.Length, cf, writeOptions);
        }
    }

    public void Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Merge(keyPtr, (nuint)key.Length, valuePtr, (nuint)value.Length, cf, writeOptions);
        }
    }

    public Iterator NewIterator(IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        using var lease = Lease();
        var options = readOptions ?? DefaultReadOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        nint iteratorHandle = cf is null
            ? (nint)rocksdb_create_iterator(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle))
            : (nint)rocksdb_create_iterator_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle), RocksDbInterop.ColumnFamily(cf.Handle));
        return new Iterator(iteratorHandle, options, AcquireChildLease());
    }

    /// <remarks>
    /// The returned iterators read from a single consistent view of the database, which is what
    /// separate <see cref="NewIterator" /> calls do not give you. They share one
    /// <paramref name="readOptions" />, because that is all the C API accepts.
    /// </remarks>
    public Iterator[] NewIterators(IColumnFamilyHandle[] cfs, ReadOptions? readOptions = null)
    {
        using var lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        var cfHandles = new nint[cfs.Length];
        var iteratorHandles = new nint[cfs.Length];

        for (int i = 0; i < cfs.Length; i++)
            cfHandles[i] = cfs[i].Handle;

        fixed (nint* cfHandlesPtr = cfHandles)
        fixed (nint* iteratorHandlesPtr = iteratorHandles)
        {
            sbyte* errptr = null;
            rocksdb_create_iterators(
                RocksDbInterop.Db(NativeHandle),
                RocksDbInterop.ReadOptions(optionsHandle),
                (rocksdb_column_family_handle_t**)cfHandlesPtr,
                (rocksdb_iterator_t**)iteratorHandlesPtr,
                (nuint)cfs.Length,
                &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }

        var iterators = new Iterator[cfs.Length];

        for (int i = 0; i < cfs.Length; i++)
            iterators[i] = new Iterator(iteratorHandles[i], options, AcquireChildLease());

        return iterators;
    }

    public Snapshot CreateSnapshot()
    {
        using var lease = Lease();
        nint snapshotHandle = (nint)rocksdb_create_snapshot(RocksDbInterop.Db(NativeHandle));
        return new Snapshot(AcquireChildLease(), snapshotHandle);
    }

    public static IEnumerable<string> ListColumnFamilies(DbOptions options, string name) => TryListColumnFamilies(options, name, out var columnFamilies)
            ? columnFamilies
            : [];

    public static bool TryListColumnFamilies(DbOptions options, string name, out string[] columnFamilies)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var path = new TransientUtf8String(name);
        nuint lencf;
        sbyte* errptr = null;
        var result = rocksdb_list_column_families(RocksDbInterop.Options(optionsHandle), (sbyte*)path.Handle, &lencf, &errptr);
        if (errptr != null)
        {
            columnFamilies = [];
            rocksdb_free(errptr);
            return false;
        }

        var count = checked((int)lencf);
        columnFamilies = new string[count];
        for (var i = 0; i < count; i++)
        {
            columnFamilies[i] = Utf8StringMarshaller.ConvertToManaged((byte*)result[i])!;
        }

        rocksdb_list_column_families_destroy(result, lencf);
        return true;
    }

    public IColumnFamilyHandle CreateColumnFamily(ColumnFamilyOptions cfOptions, string name)
    {
        using var cfOptionsLease = cfOptions.Lease(out nint cfOptionsHandle);
        using var lease = Lease();
        // The lock spans the native call and the bookkeeping: interleaving a same-name drop and
        // create would otherwise let the create observe the dropped entry still in the lookup.
        lock (_columnFamilyLock)
        {
            using var nativeName = new TransientUtf8String(name);
            sbyte* errptr = null;
            var cfh = (nint)rocksdb_create_column_family(RocksDbInterop.Db(NativeHandle), RocksDbInterop.Options(cfOptionsHandle), (sbyte*)nativeName.Handle, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            var cfhw = new ColumnFamilyHandleInternal(cfh);
            // Ownership before lookup, so a failed dictionary insert cannot leak the handle.
            _handle.RegisterOwnedColumnFamily(cfhw);
            (columnFamilies ??= []).Add(name, cfhw);
            return cfhw;
        }
    }

    public void DropColumnFamily(string name)
    {
        using var lease = Lease();
        // The lock spans the native call and the bookkeeping; the lookup is done directly here
        // rather than through GetColumnFamily to keep the whole transition one critical section.
        lock (_columnFamilyLock)
        {
            if (columnFamilies is null)
            {
                throw new RocksDbException("Database not opened for column families");
            }

            ColumnFamilyHandleInternal cf = columnFamilies[name];
            sbyte* errptr = null;
            rocksdb_drop_column_family(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ColumnFamily(cf.Handle), &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            // Lookup only: the handle stays in the ownership registry and is destroyed at close.
            columnFamilies.Remove(name);
        }
    }

    public IColumnFamilyHandle GetDefaultColumnFamily() => GetColumnFamily(ColumnFamilies.DefaultName);

    public IColumnFamilyHandle GetColumnFamily(string name)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        lock (_columnFamilyLock)
        {
            if (columnFamilies is null)
            {
                throw new RocksDbException("Database not opened for column families");
            }

            return columnFamilies[name];
        }
    }

    public bool TryGetColumnFamily(string name, [MaybeNullWhen(false)] out IColumnFamilyHandle handle)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        lock (_columnFamilyLock)
        {
            if (columnFamilies is null)
            {
                throw new RocksDbException("Database not opened for column families");
            }

            if (columnFamilies.TryGetValue(name, out ColumnFamilyHandleInternal? internalHandle))
            {
                handle = internalHandle;
                return true;
            }

            handle = null;
            return false;
        }
    }

    public string? GetProperty(string propertyName)
    {
        using var lease = Lease();
        using var property = new TransientUtf8String(propertyName);
        return RocksDbInterop.NullTerminatedStringAndFree(rocksdb_property_value(RocksDbInterop.Db(NativeHandle), (sbyte*)property.Handle));
    }

    public string? GetProperty(string propertyName, IColumnFamilyHandle cf)
    {
        using var lease = Lease();
        using var property = new TransientUtf8String(propertyName);
        return RocksDbInterop.NullTerminatedStringAndFree(rocksdb_property_value_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)property.Handle));
    }

    public void IngestExternalFiles(string[] files, IngestExternalFileOptions ingestOptions, IColumnFamilyHandle? cf = null)
    {
        using var lease = Lease();
        using var nativeFiles = new NativeUtf8StringArray(files);
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_ingest_external_file(RocksDbInterop.Db(NativeHandle), nativeFiles.Pointer, (nuint)files.GetLongLength(0), RocksDbInterop.IngestExternalFileOptions(ingestOptions.Handle), &errptr);
        }
        else
        {
            rocksdb_ingest_external_file_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ColumnFamily(cf.Handle), nativeFiles.Pointer, (nuint)files.GetLongLength(0), RocksDbInterop.IngestExternalFileOptions(ingestOptions.Handle), &errptr);
        }
        // The ingest options are not leased, so keep them from being finalized mid-call.
        GC.KeepAlive(ingestOptions);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void CompactRange(byte[]? start, byte[]? limit, IColumnFamilyHandle? cf = null)
    {
        using var lease = Lease();
        fixed (byte* startPtr = start)
        fixed (byte* limitPtr = limit)
        {
            if (cf is null)
            {
                rocksdb_compact_range(RocksDbInterop.Db(NativeHandle), (sbyte*)startPtr, (nuint)(start?.GetLongLength(0) ?? 0L), (sbyte*)limitPtr, (nuint)(limit?.GetLongLength(0) ?? 0L));
            }
            else
            {
                rocksdb_compact_range_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startPtr, (nuint)(start?.GetLongLength(0) ?? 0L), (sbyte*)limitPtr, (nuint)(limit?.GetLongLength(0) ?? 0L));
            }
        }
    }

    public void TryCatchUpWithPrimary()
    {
        using var lease = Lease();
        sbyte* errptr = null;
        rocksdb_try_catch_up_with_primary(RocksDbInterop.Db(NativeHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void DisableFileDeletions()
    {
        using var lease = Lease();
        sbyte* errptr = null;
        rocksdb_disable_file_deletions(RocksDbInterop.Db(NativeHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void EnableFileDeletions()
    {
        using var lease = Lease();
        sbyte* errptr = null;
        rocksdb_enable_file_deletions(RocksDbInterop.Db(NativeHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public TransactionLogIterator GetUpdatesSince(ulong sequenceNumber)
    {
        using var lease = Lease();
        // options is null for now as we don't have a wrapper and pass null to C API
        sbyte* errptr = null;
        nint iteratorHandle = (nint)rocksdb_get_updates_since(RocksDbInterop.Db(NativeHandle), (nuint)sequenceNumber, null, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new TransactionLogIterator(iteratorHandle, AcquireChildLease());
    }

    public ulong GetLatestSequenceNumber()
    {
        using var lease = Lease();
        return rocksdb_get_latest_sequence_number(RocksDbInterop.Db(NativeHandle));
    }

    public void Flush(FlushOptions flushOptions)
    {
        using var flushOptionsLease = flushOptions.Lease(out nint flushOptionsHandle);
        using var lease = Lease();
        sbyte* errptr = null;
        rocksdb_flush(RocksDbInterop.Db(NativeHandle), RocksDbInterop.FlushOptions(flushOptionsHandle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Flushes the memtable of a single column family into SST files.</summary>
    public void Flush(FlushOptions flushOptions, IColumnFamilyHandle cf)
    {
        using var flushOptionsLease = flushOptions.Lease(out nint flushOptionsHandle);
        using var lease = Lease();
        sbyte* errptr = null;
        rocksdb_flush_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.FlushOptions(flushOptionsHandle), RocksDbInterop.ColumnFamily(cf.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Flushes the write-ahead log, optionally syncing it to disk.</summary>
    public void FlushWal(bool sync)
    {
        using var lease = Lease();
        sbyte* errptr = null;
        rocksdb_flush_wal(RocksDbInterop.Db(NativeHandle), RocksDbInterop.Bool(sync), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>
    /// Reads an integer-valued property such as <c>rocksdb.estimate-num-keys</c> without the
    /// string round-trip of <see cref="GetProperty(string)"/>.
    /// </summary>
    /// <returns>False when the property does not exist or is not integer-valued.</returns>
    public bool TryGetIntProperty(string propertyName, out ulong value)
    {
        using var lease = Lease();
        using var property = new TransientUtf8String(propertyName);
        ulong result;
        var found = rocksdb_property_int(RocksDbInterop.Db(NativeHandle), (sbyte*)property.Handle, &result) == 0;
        value = found ? result : 0;
        return found;
    }

    /// <inheritdoc cref="TryGetIntProperty(string, out ulong)"/>
    public bool TryGetIntProperty(string propertyName, IColumnFamilyHandle cf, out ulong value)
    {
        using var lease = Lease();
        using var property = new TransientUtf8String(propertyName);
        ulong result;
        var found = rocksdb_property_int_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)property.Handle, &result) == 0;
        value = found ? result : 0;
        return found;
    }

    /// <summary>
    /// Attempts to repair a database that cannot be opened, rebuilding what is salvageable from
    /// its files. Data may be lost; take a copy first if the files matter.
    /// </summary>
    public static void Repair(DbOptions options, string path)
    {
        using var optionsLease = options.Lease(out nint optionsHandle);
        using var pathSafe = new TransientUtf8String(path);
        sbyte* errptr = null;
        rocksdb_repair_db(RocksDbInterop.Options(optionsHandle), (sbyte*)pathSafe.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Describes the files currently making up the database.</summary>
    /// <param name="populateFileMetadataOnly">
    /// Skips the per-file key range and entry counts, leaving only the name, level and size.
    /// </param>
    /// <returns>
    /// The metadata, empty when the database has no live files, or null when RocksDB declines to
    /// report them at all.
    /// </returns>
    public List<LiveFileMetadata>? GetLiveFilesMetadata(bool populateFileMetadataOnly = false)
    {
        using var lease = Lease();
        nint buffer = (nint)rocksdb_livefiles(RocksDbInterop.Db(NativeHandle));
        if (buffer == nint.Zero)
        {
            return null;
        }

        try
        {
            List<LiveFileMetadata> filesMetadata = [];

            int fileCount = rocksdb_livefiles_count(RocksDbInterop.LiveFiles(buffer));
            for (int index = 0; index < fileCount; index++)
            {
                var fileMetadata = rocksdb_livefiles_name(RocksDbInterop.LiveFiles(buffer), index);
                string fileName = Utf8StringMarshaller.ConvertToManaged((byte*)fileMetadata)!;

                int level = rocksdb_livefiles_level(RocksDbInterop.LiveFiles(buffer), index);

                ulong fileSize = rocksdb_livefiles_size(RocksDbInterop.LiveFiles(buffer), index);

                LiveFileMetadata liveFileMetadata = new()
                {
                    FileMetadata = new FileMetadata
                    {
                        FileName = fileName,
                        FileLevel = level,
                        FileSize = fileSize,
                    },
                };

                if (!populateFileMetadataOnly)
                {
                    nuint smallestKeySize;
                    var smallestKeyPtr = rocksdb_livefiles_smallestkey(RocksDbInterop.LiveFiles(buffer), index, &smallestKeySize);
                    // These keys are length-delimited rather than null-terminated, so decode by size
                    string smallestKey = DefaultEncoding.GetString((byte*)smallestKeyPtr, checked((int)smallestKeySize));

                    nuint largestKeySize;
                    var largestKeyPtr = rocksdb_livefiles_largestkey(RocksDbInterop.LiveFiles(buffer), index, &largestKeySize);
                    string largestKey = DefaultEncoding.GetString((byte*)largestKeyPtr, checked((int)largestKeySize));

                    ulong entries = rocksdb_livefiles_entries(RocksDbInterop.LiveFiles(buffer), index);
                    ulong deletions = rocksdb_livefiles_deletions(RocksDbInterop.LiveFiles(buffer), index);

                    liveFileMetadata.FileDataMetadata = new FileDataMetadata
                    {
                        SmallestKeyInFile = smallestKey,
                        LargestKeyInFile = largestKey,
                        NumEntriesInFile = entries,
                        NumDeletionsInFile = deletions,
                    };
                }

                filesMetadata.Add(liveFileMetadata);
            }

            return filesMetadata;
        }
        finally
        {
            rocksdb_livefiles_destroy(RocksDbInterop.LiveFiles(buffer));
        }
    }

    /// <summary>
    /// Names the live files, without the rest of what
    /// <see cref="GetLiveFilesMetadata"/> collects.
    /// </summary>
    public List<string> GetLiveFileNames()
    {
        using var lease = Lease();
        nint buffer = (nint)rocksdb_livefiles(RocksDbInterop.Db(NativeHandle));
        if (buffer == nint.Zero)
        {
            return [];
        }

        try
        {
            List<string> liveFiles = [];

            int fileCount = rocksdb_livefiles_count(RocksDbInterop.LiveFiles(buffer));

            for (int index = 0; index < fileCount; index++)
            {
                var fileMetadata = rocksdb_livefiles_name(RocksDbInterop.LiveFiles(buffer), index);
                string fileName = Utf8StringMarshaller.ConvertToManaged((byte*)fileMetadata)!;
                liveFiles.Add(fileName);
            }

            return liveFiles;
        }
        finally
        {
            rocksdb_livefiles_destroy(RocksDbInterop.LiveFiles(buffer));
        }
    }
}
