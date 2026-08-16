// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed class RocksDb : IDisposable
{
    private bool _disposed;
    internal static ReadOptions DefaultReadOptions { get; } = new ReadOptions();
    internal static OptionsHandle DefaultOptions { get; } = new DbOptions();
    internal static WriteOptions DefaultWriteOptions { get; } = new WriteOptions();
    internal static Encoding DefaultEncoding => Encoding.UTF8;
    private Dictionary<string, ColumnFamilyHandleInternal>? columnFamilies;

    // Held so the garbage collector cannot finalize them while the db is still open.
    private OptionsHandle? Options { get; }
    private ColumnFamilyOptions[]? ColumnFamilyOptions { get; }

    public nint Handle { get; internal set; }
    public string Path { get; internal set; } = null!;
    public string? WalPath { get; internal set; }
    public string? LogPath { get; internal set; }

    private RocksDb(nint handle, OptionsHandle? options, ColumnFamilyOptions[]? columnFamilyOptions, Dictionary<string, ColumnFamilyHandleInternal>? columnFamilies = null)
    {
        Handle = handle;
        Options = options;
        ColumnFamilyOptions = columnFamilyOptions;
        this.columnFamilies = columnFamilies;
    }

    ~RocksDb() => ReleaseUnmanagedResources();

    public void Dispose()
    {
        if (_disposed) return;

        try
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }
        finally
        {
            _disposed = true;
        }
    }

    private void ReleaseUnmanagedResources()
    {
        if (columnFamilies is not null)
        {
            foreach (ColumnFamilyHandleInternal cfh in columnFamilies.Values)
            {
                cfh.Dispose();
            }
            columnFamilies = null;
        }

        if (Handle != nint.Zero)
        {
            var handle = Handle;
            Handle = nint.Zero;
            rocksdb_close(RocksDbInterop.Db(handle));
        }
    }

    public static RocksDb Open(OptionsHandle options, string path)
    {
        using var pathSafe = new RocksSafePath(path);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options, columnFamilyOptions: null)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb OpenReadOnly(OptionsHandle options, string path, bool errorIfLogFileExists)
    {
        using var pathSafe = new RocksSafePath(path);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open_for_read_only(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, RocksDbInterop.Bool(errorIfLogFileExists), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options, columnFamilyOptions: null)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb OpenAsSecondary(OptionsHandle options, string path, string secondaryPath)
    {
        using var pathSafe = new RocksSafePath(path);
        using var secondaryPathSafe = new RocksSafePath(secondaryPath);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open_as_secondary(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, (sbyte*)secondaryPathSafe.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options, columnFamilyOptions: null)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb OpenWithTtl(OptionsHandle options, string path, int ttlSeconds)
    {
        using var pathSafe = new RocksSafePath(path);
        sbyte* errptr = null;
        nint db = (nint)rocksdb_open_with_ttl(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, ttlSeconds, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new RocksDb(db, options, columnFamilyOptions: null)
        {
            Path = path,
            LogPath = options.LogPath,
            WalPath = options.WalPath,
        };
    }

    public static RocksDb Open(DbOptions options, string path, ColumnFamilies columnFamilies)
    {
        using var pathSafe = new RocksSafePath(path);
        string[] cfnames = [.. columnFamilies.Names];
        nint[] cfoptions = [.. columnFamilies.OptionHandles];
        nint[] cfhandles = new nint[cfnames.Length];
        using var cfNameArray = new NativeUtf8StringArray(cfnames);
        fixed (nint* cfOptionsPtr = cfoptions)
        fixed (nint* cfHandlesPtr = cfhandles)
        {
            sbyte* errptr = null;
            nint db = (nint)rocksdb_open_column_families(
                RocksDbInterop.Options(options.Handle),
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

            return new RocksDb(db,
                options,
                [.. columnFamilies.Select(cfd => cfd.Options)],
                columnFamilies: cfHandleMap)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenReadOnly(DbOptions options, string path, ColumnFamilies columnFamilies, bool errIfLogFileExists)
    {
        using var pathSafe = new RocksSafePath(path);
        string[] cfnames = [.. columnFamilies.Names];
        nint[] cfoptions = [.. columnFamilies.OptionHandles];
        nint[] cfhandles = new nint[cfnames.Length];
        using var cfNameArray = new NativeUtf8StringArray(cfnames);
        fixed (nint* cfOptionsPtr = cfoptions)
        fixed (nint* cfHandlesPtr = cfhandles)
        {
            sbyte* errptr = null;
            nint db = (nint)rocksdb_open_for_read_only_column_families(
                RocksDbInterop.Options(options.Handle),
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

            return new RocksDb(db,
                options,
                [.. columnFamilies.Select(cfd => cfd.Options)],
                columnFamilies: cfHandleMap)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenAsSecondary(DbOptions options, string path, string secondaryPath, ColumnFamilies columnFamilies)
    {
        using var pathSafe = new RocksSafePath(path);
        using var secondaryPathSafe = new RocksSafePath(secondaryPath);
        string[] cfnames = [.. columnFamilies.Names];
        nint[] cfoptions = [.. columnFamilies.OptionHandles];
        nint[] cfhandles = new nint[cfnames.Length];
        using var cfNameArray = new NativeUtf8StringArray(cfnames);
        fixed (nint* cfOptionsPtr = cfoptions)
        fixed (nint* cfHandlesPtr = cfhandles)
        {
            sbyte* errptr = null;
            var db = (nint)rocksdb_open_as_secondary_column_families(
                RocksDbInterop.Options(options.Handle),
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
            return new RocksDb(db,
                options,
                [.. columnFamilies.Select(cfd => cfd.Options)],
                columnFamilies: cfHandleMap)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    /// <summary>
    /// Usage:
    /// <code><![CDATA[
    /// using (var cp = db.Checkpoint())
    /// {
    ///     cp.Save("path/to/checkpoint");
    /// }
    /// ]]></code>
    /// </summary>
    /// <returns></returns>
    public Checkpoint Checkpoint()
    {
        sbyte* errptr = null;
        var checkpoint = (nint)rocksdb_checkpoint_object_create(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new Checkpoint(checkpoint);
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
        var (keys, values) = SplitOptions(options);
        using var nativeKeys = new NativeUtf8StringArray(keys);
        using var nativeValues = new NativeUtf8StringArray(values);
        sbyte* errptr = null;
        rocksdb_set_options(RocksDbInterop.Db(Handle), keys.Length, nativeKeys.Pointer, nativeValues.Pointer, &errptr);
        GC.KeepAlive(this);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Changes mutable options of a single column family at runtime.</summary>
    public void SetOptions(IColumnFamilyHandle cf, IEnumerable<KeyValuePair<string, string>> options)
    {
        var (keys, values) = SplitOptions(options);
        using var nativeKeys = new NativeUtf8StringArray(keys);
        using var nativeValues = new NativeUtf8StringArray(values);
        sbyte* errptr = null;
        rocksdb_set_options_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), keys.Length, nativeKeys.Pointer, nativeValues.Pointer, &errptr);
        GC.KeepAlive(this);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public string? Get(string key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null, Encoding? encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            nuint valueLength;
            sbyte* errptr = null;
            var valuePtr = cf is null
                ? rocksdb_get(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), (sbyte*)keyPtr, (nuint)keyBytes.Length, &valueLength, &errptr)
                : rocksdb_get_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)keyBytes.Length, &valueLength, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return RocksDbInterop.PtrToStringAndFree(valuePtr, valueLength, encoding);
        }
    }

    public byte[]? Get(byte[] key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null) => Get(key, key.GetLongLength(0), cf, readOptions);

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
    /// Reads the value for <paramref name="key"/> without copying it out of rocksdb-owned memory.
    /// </summary>
    /// <returns>
    /// True when the key exists; <paramref name="slice"/> then holds the value and must be disposed.
    /// </returns>
    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public bool TryGetPinned(ReadOnlySpan<byte> key, out PinnedSlice slice, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
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

    public T? Get<T>(ReadOnlySpan<byte> key, Func<Stream, T> deserializer, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        var value = Get(key, cf, readOptions);
        if (value is null)
            return default;
        using var stream = new MemoryStream(value, writable: false);
        return deserializer(stream);
    }

    public byte[]? Get(byte[] key, long keyLength, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return Get(keyPtr, (nuint)keyLength, cf, readOptions);
        }
    }

    public bool HasKey(byte[] key, long keyLength, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return HasKey(keyPtr, (nuint)keyLength, cf, readOptions);
        }
    }

    public bool HasKey(string key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null, Encoding? encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            return HasKey(keyPtr, (nuint)keyBytes.Length, cf, readOptions);
        }
    }

    /// <summary>
    /// Reads the contents of the database value associated with <paramref name="key"/>, if present, into the supplied
    /// <paramref name="buffer"/> at <paramref name="offset"/> up to <paramref name="length"/> bytes, returning the
    /// length of the value in the database, or -1 if the key is not present.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    /// <param name="cf"></param>
    /// <param name="readOptions"></param>
    /// <returns>The actual length of the database field if it exists, otherwise -1</returns>
    public long Get(byte[] key, byte[] buffer, long offset, long length, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null) => Get(key, key.GetLongLength(0), buffer, offset, length, cf, readOptions);

    /// <summary>
    /// Reads the contents of the database value associated with <paramref name="key"/>, if present, into the supplied
    /// <paramref name="buffer"/> at <paramref name="offset"/> up to <paramref name="length"/> bytes, returning the
    /// length of the value in the database, or -1 if the key is not present.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="keyLength"></param>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    /// <param name="cf"></param>
    /// <param name="readOptions"></param>
    /// <returns>The actual length of the database field if it exists, otherwise -1</returns>
    public long Get(byte[] key, long keyLength, byte[] buffer, long offset, long length, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        unsafe
        {
            nuint valueLength;
            sbyte* errptr = null;
            fixed (byte* keyPtr = key)
            {
                var ptr = cf is null
                    ? rocksdb_get(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), (sbyte*)keyPtr, (nuint)keyLength, &valueLength, &errptr)
                    : rocksdb_get_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)keyLength, &valueLength, &errptr);
                RocksDbInterop.ThrowIfError(errptr);
                if (ptr == null)
                {
                    return -1;
                }

                var copyLength = Math.Min(length, (long)valueLength);
                new ReadOnlySpan<byte>(ptr, (int)copyLength).CopyTo(buffer.AsSpan((int)offset, (int)copyLength));
                rocksdb_free(ptr);
                return (long)valueLength;
            }
        }
    }

    public KeyValuePair<byte[], byte[]?>[] MultiGet(byte[][] keys, IColumnFamilyHandle[]? cf = null, ReadOptions? readOptions = null)
    {
        ArgumentNullException.ThrowIfNull(keys);

        var count = keys.Length;
        if (cf is not null && cf.Length != count)
            throw new ArgumentException("Column family handle count must match key count.", nameof(cf));

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
                        RocksDbInterop.Db(Handle),
                        RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle),
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
                            RocksDbInterop.Db(Handle),
                            RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle),
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
        var options = readOptions ?? DefaultReadOptions;
        sbyte* errptr = null;
        var pinned = cf is null
            ? rocksdb_get_pinned(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions(options.Handle), (sbyte*)key, keyLength, &errptr)
            : rocksdb_get_pinned_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions(options.Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &errptr);
        // Without these, the finalizers could destroy the db or read options mid-call.
        GC.KeepAlive(this);
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
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_delete(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), (sbyte*)key, keyLength, &errptr);
        }
        else
        {
            rocksdb_delete_cf(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    private void Put(byte* key, nuint keyLength, byte* value, nuint valueLength, IColumnFamilyHandle? cf, WriteOptions? writeOptions)
    {
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_put(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        else
        {
            rocksdb_put_cf(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    private void Merge(byte* key, nuint keyLength, byte* value, nuint valueLength, IColumnFamilyHandle? cf, WriteOptions? writeOptions)
    {
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_merge(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        else
        {
            rocksdb_merge_cf(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    public KeyValuePair<string, string?>[] MultiGet(string[] keys, IColumnFamilyHandle[]? cf = null, ReadOptions? readOptions = null)
    {
        ArgumentNullException.ThrowIfNull(keys);

        var encodedKeys = new byte[keys.Length][];
        for (var i = 0; i < keys.Length; i++)
        {
            if (keys[i] is null)
                throw new ArgumentException("Keys cannot contain null values.", nameof(keys));

            encodedKeys[i] = DefaultEncoding.GetBytes(keys[i]);
        }

        KeyValuePair<byte[], byte[]?>[] values = MultiGet(encodedKeys, cf, readOptions);
        var result = new KeyValuePair<string, string?>[keys.Length];
        for (var i = 0; i < keys.Length; i++)
        {
            var value = values[i].Value;
            result[i] = new KeyValuePair<string, string?>(
                keys[i],
                value is null ? null : DefaultEncoding.GetString(value));
        }

        return result;
    }

    public void Write(WriteBatch writeBatch, WriteOptions? writeOptions = null)
    {
        sbyte* errptr = null;
        rocksdb_write(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.WriteBatch(writeBatch.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Write(WriteBatchWithIndex writeBatch, WriteOptions? writeOptions = null)
    {
        sbyte* errptr = null;
        rocksdb_write_writebatch_wi(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.WriteBatchWithIndex(writeBatch.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Remove(string key, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        var keyBytes = DefaultEncoding.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            Remove(keyPtr, (nuint)keyBytes.Length, cf, writeOptions);
        }
    }

    public void Remove(byte[] key, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null) => Remove(key, key.Length, cf, writeOptions);

    public unsafe void Remove(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = &MemoryMarshal.GetReference(key))
        {
            if (cf is null)
            {
                Remove(keyPtr, (nuint)key.Length, null, writeOptions);
            }
            else
            {
                Remove(keyPtr, (nuint)key.Length, cf, writeOptions);
            }
        }
    }

    public void Remove(byte[] key, long keyLength, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        if (cf is null)
        {
            fixed (byte* keyPtr = key)
            {
                Remove(keyPtr, (nuint)keyLength, null, writeOptions);
            }
        }
        else
        {
            fixed (byte* keyPtr = key)
            {
                Remove(keyPtr, (nuint)keyLength, cf, writeOptions);
            }
        }
    }

    public void Put(string key, string value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null, Encoding? encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        var valueBytes = encoding.GetBytes(value);
        Put(keyBytes, keyBytes.LongLength, valueBytes, valueBytes.LongLength, cf, writeOptions);
    }

    public void Put(byte[] key, byte[] value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null) => Put(key, key.GetLongLength(0), value, value.GetLongLength(0), cf, writeOptions);

    public void Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Put(keyPtr, (nuint)key.Length, valuePtr, (nuint)value.Length, cf, writeOptions);
        }
    }

    public void Put(byte[] key, long keyLength, byte[] value, long valueLength, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Put(keyPtr, (nuint)keyLength, valuePtr, (nuint)valueLength, cf, writeOptions);
        }
    }

    public void Merge(string key, string value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null, Encoding? encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        var valueBytes = encoding.GetBytes(value);
        Merge(keyBytes, keyBytes.LongLength, valueBytes, valueBytes.LongLength, cf, writeOptions);
    }

    public void Merge(byte[] key, byte[] value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null) => Merge(key, key.GetLongLength(0), value, value.GetLongLength(0), cf, writeOptions);

    public void Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Merge(keyPtr, (nuint)key.Length, valuePtr, (nuint)value.Length, cf, writeOptions);
        }
    }

    public void Merge(byte[] key, long keyLength, byte[] value, long valueLength, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Merge(keyPtr, (nuint)keyLength, valuePtr, (nuint)valueLength, cf, writeOptions);
        }
    }

    public Iterator NewIterator(IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        nint iteratorHandle = cf is null
            ? (nint)rocksdb_create_iterator(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle))
            : (nint)rocksdb_create_iterator_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle));
        // Note: passing in read options here only to ensure that it is not collected before the iterator
        return new Iterator(iteratorHandle, readOptions);
    }

    /// <remarks>
    /// The returned iterators read from a single consistent view of the database, which is what
    /// separate <see cref="NewIterator" /> calls do not give you. They share one
    /// <paramref name="readOptions" />, because that is all the C API accepts, and every one of
    /// them must be disposed before the database is closed.
    /// </remarks>
    public Iterator[] NewIterators(IColumnFamilyHandle[] cfs, ReadOptions? readOptions = null)
    {
        ReadOptions options = readOptions ?? DefaultReadOptions;
        var cfHandles = new nint[cfs.Length];
        var iteratorHandles = new nint[cfs.Length];

        for (int i = 0; i < cfs.Length; i++)
            cfHandles[i] = cfs[i].Handle;

        fixed (nint* cfHandlesPtr = cfHandles)
        fixed (nint* iteratorHandlesPtr = iteratorHandles)
        {
            sbyte* errptr = null;
            rocksdb_create_iterators(
                RocksDbInterop.Db(Handle),
                RocksDbInterop.ReadOptions(options.Handle),
                (rocksdb_column_family_handle_t**)cfHandlesPtr,
                (rocksdb_iterator_t**)iteratorHandlesPtr,
                (nuint)cfs.Length,
                &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }

        var iterators = new Iterator[cfs.Length];

        for (int i = 0; i < cfs.Length; i++)
            iterators[i] = new Iterator(iteratorHandles[i], options);

        return iterators;
    }

    public Snapshot CreateSnapshot()
    {
        nint snapshotHandle = (nint)rocksdb_create_snapshot(RocksDbInterop.Db(Handle));
        return new Snapshot(Handle, snapshotHandle);
    }

    public static IEnumerable<string> ListColumnFamilies(DbOptions options, string name) => TryListColumnFamilies(options, name, out var columnFamilies)
            ? columnFamilies
            : [];

    public static bool TryListColumnFamilies(DbOptions options, string name, out string[] columnFamilies)
    {
        using var path = new RocksSafePath(name);
        nuint lencf;
        sbyte* errptr = null;
        var result = rocksdb_list_column_families(RocksDbInterop.Options(options.Handle), (sbyte*)path.Handle, &lencf, &errptr);
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
        using var nativeName = new RocksSafePath(name);
        sbyte* errptr = null;
        var cfh = (nint)rocksdb_create_column_family(RocksDbInterop.Db(Handle), RocksDbInterop.Options(cfOptions.Handle), (sbyte*)nativeName.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        var cfhw = new ColumnFamilyHandleInternal(cfh);
        (columnFamilies ??= []).Add(name, cfhw);
        return cfhw;
    }

    public void DropColumnFamily(string name)
    {
        IColumnFamilyHandle cf = GetColumnFamily(name);
        sbyte* errptr = null;
        rocksdb_drop_column_family(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        columnFamilies?.Remove(name);
    }

    public IColumnFamilyHandle GetDefaultColumnFamily() => GetColumnFamily(ColumnFamilies.DefaultName);

    public IColumnFamilyHandle GetColumnFamily(string name)
    {
        if (columnFamilies is null)
        {
            throw new RocksDbException("Database not opened for column families");
        }

        return columnFamilies[name];
    }

    public bool TryGetColumnFamily(string name, [MaybeNullWhen(false)] out IColumnFamilyHandle handle)
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

    public string? GetProperty(string propertyName)
    {
        using var property = new RocksSafePath(propertyName);
        return RocksDbInterop.NullTerminatedStringAndFree(rocksdb_property_value(RocksDbInterop.Db(Handle), (sbyte*)property.Handle));
    }

    public string? GetProperty(string propertyName, IColumnFamilyHandle cf)
    {
        using var property = new RocksSafePath(propertyName);
        return RocksDbInterop.NullTerminatedStringAndFree(rocksdb_property_value_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)property.Handle));
    }

    public void IngestExternalFiles(string[] files, IngestExternalFileOptions ingestOptions, IColumnFamilyHandle? cf = null)
    {
        using var nativeFiles = new NativeUtf8StringArray(files);
        sbyte* errptr = null;
        if (cf is null)
        {
            rocksdb_ingest_external_file(RocksDbInterop.Db(Handle), nativeFiles.Pointer, (nuint)files.GetLongLength(0), RocksDbInterop.IngestExternalFileOptions(ingestOptions.Handle), &errptr);
        }
        else
        {
            rocksdb_ingest_external_file_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), nativeFiles.Pointer, (nuint)files.GetLongLength(0), RocksDbInterop.IngestExternalFileOptions(ingestOptions.Handle), &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void CompactRange(byte[]? start, byte[]? limit, IColumnFamilyHandle? cf = null)
    {
        fixed (byte* startPtr = start)
        fixed (byte* limitPtr = limit)
        {
            if (cf is null)
            {
                rocksdb_compact_range(RocksDbInterop.Db(Handle), (sbyte*)startPtr, (nuint)(start?.GetLongLength(0) ?? 0L), (sbyte*)limitPtr, (nuint)(limit?.GetLongLength(0) ?? 0L));
            }
            else
            {
                rocksdb_compact_range_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startPtr, (nuint)(start?.GetLongLength(0) ?? 0L), (sbyte*)limitPtr, (nuint)(limit?.GetLongLength(0) ?? 0L));
            }
        }
    }

    public void CompactRange(string? start, string? limit, IColumnFamilyHandle? cf = null, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;

        CompactRange(start is null ? null : encoding.GetBytes(start), limit is null ? null : encoding.GetBytes(limit), cf);
    }

    public void TryCatchUpWithPrimary()
    {
        sbyte* errptr = null;
        rocksdb_try_catch_up_with_primary(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void DisableFileDeletions()
    {
        sbyte* errptr = null;
        rocksdb_disable_file_deletions(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void EnableFileDeletions()
    {
        sbyte* errptr = null;
        rocksdb_enable_file_deletions(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public TransactionLogIterator GetUpdatesSince(ulong sequenceNumber)
    {
        // options is null for now as we don't have a wrapper and pass null to C API
        sbyte* errptr = null;
        nint iteratorHandle = (nint)rocksdb_get_updates_since(RocksDbInterop.Db(Handle), (nuint)sequenceNumber, null, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new TransactionLogIterator(iteratorHandle);
    }

    public ulong GetLatestSequenceNumber() => rocksdb_get_latest_sequence_number(RocksDbInterop.Db(Handle));

    public void Flush(FlushOptions flushOptions)
    {
        sbyte* errptr = null;
        rocksdb_flush(RocksDbInterop.Db(Handle), RocksDbInterop.FlushOptions(flushOptions.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Flushes the memtable of a single column family into SST files.</summary>
    public void Flush(FlushOptions flushOptions, IColumnFamilyHandle cf)
    {
        sbyte* errptr = null;
        rocksdb_flush_cf(RocksDbInterop.Db(Handle), RocksDbInterop.FlushOptions(flushOptions.Handle), RocksDbInterop.ColumnFamily(cf.Handle), &errptr);
        GC.KeepAlive(this);
        GC.KeepAlive(flushOptions);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>Flushes the write-ahead log, optionally syncing it to disk.</summary>
    public void FlushWal(bool sync)
    {
        sbyte* errptr = null;
        rocksdb_flush_wal(RocksDbInterop.Db(Handle), RocksDbInterop.Bool(sync), &errptr);
        GC.KeepAlive(this);
        RocksDbInterop.ThrowIfError(errptr);
    }

    /// <summary>
    /// Reads an integer-valued property such as <c>rocksdb.estimate-num-keys</c> without the
    /// string round-trip of <see cref="GetProperty(string)"/>.
    /// </summary>
    /// <returns>False when the property does not exist or is not integer-valued.</returns>
    public bool TryGetIntProperty(string propertyName, out ulong value)
    {
        using var property = new TransientUtf8String(propertyName);
        ulong result;
        var found = rocksdb_property_int(RocksDbInterop.Db(Handle), (sbyte*)property.Handle, &result) == 0;
        GC.KeepAlive(this);
        value = found ? result : 0;
        return found;
    }

    /// <inheritdoc cref="TryGetIntProperty(string, out ulong)"/>
    public bool TryGetIntProperty(string propertyName, IColumnFamilyHandle cf, out ulong value)
    {
        using var property = new TransientUtf8String(propertyName);
        ulong result;
        var found = rocksdb_property_int_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)property.Handle, &result) == 0;
        GC.KeepAlive(this);
        value = found ? result : 0;
        return found;
    }

    /// <summary>
    /// Attempts to repair a database that cannot be opened, rebuilding what is salvageable from
    /// its files. Data may be lost; take a copy first if the files matter.
    /// </summary>
    public static void Repair(DbOptions options, string path)
    {
        using var pathSafe = new TransientUtf8String(path);
        sbyte* errptr = null;
        rocksdb_repair_db(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, &errptr);
        GC.KeepAlive(options);
        RocksDbInterop.ThrowIfError(errptr);
    }


    /// <summary>
    /// Returns metadata about the file and data in the file. 
    /// </summary>
    /// <param name="populateFileMetadataOnly">setting it to true only populates FileName, 
    /// Filesize and filelevel; By default it is false</param>
    /// <returns><c>LiveFilesMetadata</c> or null in case of failure</returns>
    public List<LiveFileMetadata>? GetLiveFilesMetadata(bool populateFileMetadataOnly = false)
    {
        nint buffer = (nint)rocksdb_livefiles(RocksDbInterop.Db(Handle));
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
    /// Lean API to just get Live file names. 
    /// Refer to GetLiveFilesMetadata() for the complete metadata
    /// </summary>
    /// <returns></returns>
    public List<string> GetLiveFileNames()
    {
        nint buffer = (nint)rocksdb_livefiles(RocksDbInterop.Db(Handle));
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
