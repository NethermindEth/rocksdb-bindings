// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Nethermind.RocksDbBindings;


public unsafe sealed class RocksDb : IDisposable
{
    private bool _disposed;
    internal static ReadOptions DefaultReadOptions { get; } = new ReadOptions();
    internal static OptionsHandle DefaultOptions { get; } = new DbOptions();
    internal static WriteOptions DefaultWriteOptions { get; } = new WriteOptions();
    internal static Encoding DefaultEncoding => Encoding.UTF8;
    private Dictionary<string, ColumnFamilyHandleInternal> columnFamilies;

    // Managed references to unmanaged resources that need to live at least as long as the db
    internal dynamic References { get; } = new ExpandoObject();

    public nint Handle { get; internal set; }
    public string Path { get; internal set; }
    public string WalPath { get; internal set; }
    public string LogPath { get; internal set; }

    private RocksDb(nint handle, dynamic optionsReferences, dynamic cfOptionsRefs, Dictionary<string, ColumnFamilyHandleInternal> columnFamilies = null)
    {
        this.Handle = handle;
        References.Options = optionsReferences;
        References.CfOptions = cfOptionsRefs;
        this.columnFamilies = columnFamilies;
    }

    ~RocksDb()
    {
        ReleaseUnmanagedResources();
    }

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
        if (columnFamilies is object)
        {
            foreach (var cfh in columnFamilies.Values)
            {
                cfh.Dispose();
            }
            columnFamilies = null;
        }

        if (Handle != nint.Zero)
        {
            var handle = Handle;
            Handle = nint.Zero;
            RocksDbNative.rocksdb_close(RocksDbInterop.Db(handle));
        }
    }

    public static RocksDb Open(OptionsHandle options, string path)
    {
        using (var pathSafe = new RocksSafePath(path))
        {
            sbyte* errptr = null;
            nint db = (nint)RocksDbNative.rocksdb_open(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return new RocksDb(db, optionsReferences: null, cfOptionsRefs: null)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenReadOnly(OptionsHandle options, string path, bool errorIfLogFileExists)
    {
        using (var pathSafe = new RocksSafePath(path))
        {
            sbyte* errptr = null;
            nint db = (nint)RocksDbNative.rocksdb_open_for_read_only(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, RocksDbInterop.Bool(errorIfLogFileExists), &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return new RocksDb(db, optionsReferences: null, cfOptionsRefs: null)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenAsSecondary(OptionsHandle options, string path, string secondaryPath)
    {
        using (var pathSafe = new RocksSafePath(path))
        using (var secondaryPathSafe = new RocksSafePath(secondaryPath))
        {
            sbyte* errptr = null;
            nint db = (nint)RocksDbNative.rocksdb_open_as_secondary(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, (sbyte*)secondaryPathSafe.Handle, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return new RocksDb(db, optionsReferences: null, cfOptionsRefs: null)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb OpenWithTtl(OptionsHandle options, string path, int ttlSeconds)
    {
        using (var pathSafe = new RocksSafePath(path))
        {
            sbyte* errptr = null;
            nint db = (nint)RocksDbNative.rocksdb_open_with_ttl(RocksDbInterop.Options(options.Handle), (sbyte*)pathSafe.Handle, ttlSeconds, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return new RocksDb(db, optionsReferences: null, cfOptionsRefs: null)
            {
                Path = path,
                LogPath = options.LogPath,
                WalPath = options.WalPath,
            };
        }
    }

    public static RocksDb Open(DbOptions options, string path, ColumnFamilies columnFamilies)
    {
        using (var pathSafe = new RocksSafePath(path))
        {
            string[] cfnames = columnFamilies.Names.ToArray();
            nint[] cfoptions = columnFamilies.OptionHandles.ToArray();
            nint[] cfhandles = new nint[cfnames.Length];
            using var cfNameArray = new NativeUtf8StringArray(cfnames);
            fixed (nint* cfOptionsPtr = cfoptions)
            fixed (nint* cfHandlesPtr = cfhandles)
            {
                sbyte* errptr = null;
                nint db = (nint)RocksDbNative.rocksdb_open_column_families(
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
                    optionsReferences: options.References,
                    cfOptionsRefs: columnFamilies.Select(cfd => cfd.Options.References).ToArray(),
                    columnFamilies: cfHandleMap)
                {
                    Path = path,
                    LogPath = options.LogPath,
                    WalPath = options.WalPath,
                };
            }
        }
    }

    public static RocksDb OpenReadOnly(DbOptions options, string path, ColumnFamilies columnFamilies, bool errIfLogFileExists)
    {
        using (var pathSafe = new RocksSafePath(path))
        {
            string[] cfnames = columnFamilies.Names.ToArray();
            nint[] cfoptions = columnFamilies.OptionHandles.ToArray();
            nint[] cfhandles = new nint[cfnames.Length];
            using var cfNameArray = new NativeUtf8StringArray(cfnames);
            fixed (nint* cfOptionsPtr = cfoptions)
            fixed (nint* cfHandlesPtr = cfhandles)
            {
                sbyte* errptr = null;
                nint db = (nint)RocksDbNative.rocksdb_open_for_read_only_column_families(
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
                    optionsReferences: options.References,
                    cfOptionsRefs: columnFamilies.Select(cfd => cfd.Options.References).ToArray(),
                    columnFamilies: cfHandleMap)
                {
                    Path = path,
                    LogPath = options.LogPath,
                    WalPath = options.WalPath,
                };
            }
        }
    }

    public static RocksDb OpenAsSecondary(DbOptions options, string path, string secondaryPath, ColumnFamilies columnFamilies)
    {
        using (var pathSafe = new RocksSafePath(path))
        using (var secondaryPathSafe = new RocksSafePath(secondaryPath))
        {
            string[] cfnames = columnFamilies.Names.ToArray();
            nint[] cfoptions = columnFamilies.OptionHandles.ToArray();
            nint[] cfhandles = new nint[cfnames.Length];
            using var cfNameArray = new NativeUtf8StringArray(cfnames);
            fixed (nint* cfOptionsPtr = cfoptions)
            fixed (nint* cfHandlesPtr = cfhandles)
            {
                sbyte* errptr = null;
                var db = (nint)RocksDbNative.rocksdb_open_as_secondary_column_families(
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
                    optionsReferences: options.References,
                    cfOptionsRefs: columnFamilies.Select(cfd => cfd.Options.References).ToArray(),
                    columnFamilies: cfHandleMap)
                {
                    Path = path,
                    LogPath = options.LogPath,
                    WalPath = options.WalPath,
                };
            }
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
        var checkpoint = (nint)RocksDbNative.rocksdb_checkpoint_object_create(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new Checkpoint(checkpoint);
    }

    public void SetOptions(IEnumerable<KeyValuePair<string, string>> options)
    {
        var keys = options.Select(e => e.Key).ToArray();
        var values = options.Select(e => e.Value).ToArray();
        using var nativeKeys = new NativeUtf8StringArray(keys);
        using var nativeValues = new NativeUtf8StringArray(values);
        sbyte* errptr = null;
        RocksDbNative.rocksdb_set_options(RocksDbInterop.Db(Handle), keys.Length, nativeKeys.Pointer, nativeValues.Pointer, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public string Get(string key, ColumnFamilyHandle cf = null, ReadOptions readOptions = null, Encoding encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            nuint valueLength;
            sbyte* errptr = null;
            var valuePtr = cf is null
                ? RocksDbNative.rocksdb_get(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), (sbyte*)keyPtr, (nuint)keyBytes.Length, &valueLength, &errptr)
                : RocksDbNative.rocksdb_get_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)keyBytes.Length, &valueLength, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
            return RocksDbInterop.PtrToStringAndFree(valuePtr, valueLength, encoding);
        }
    }

    public byte[] Get(byte[] key, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        return Get(key, key.GetLongLength(0), cf, readOptions);
    }

    public byte[] Get(ReadOnlySpan<byte> key, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return Get(keyPtr, (nuint)key.Length, cf, readOptions);
        }
    }

    public bool GetFixedSizeValue(ReadOnlySpan<byte> key, Span<byte> fixedSizeValueOutput, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        var value = Get(key, cf, readOptions);
        if (value is null || value.Length != fixedSizeValueOutput.Length)
            return false;
        value.CopyTo(fixedSizeValueOutput);
        return true;
    }

    public bool HasKey(ReadOnlySpan<byte> key, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return HasKey(keyPtr, (nuint)key.Length, cf, readOptions);
        }
    }

    public T Get<T>(ReadOnlySpan<byte> key, ISpanDeserializer<T> deserializer, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        var value = Get(key, cf, readOptions);
        return value is null ? default : deserializer.Deserialize(value);
    }

    public T Get<T>(ReadOnlySpan<byte> key, Func<Stream, T> deserializer, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        var value = Get(key, cf, readOptions);
        if (value is null)
            return default;
        using var stream = new MemoryStream(value, writable: false);
        return deserializer(stream);
    }

    public byte[] Get(byte[] key, long keyLength, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return Get(keyPtr, (nuint)keyLength, cf, readOptions);
        }
    }

    public bool HasKey(byte[] key, long keyLength, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        fixed (byte* keyPtr = key)
        {
            return HasKey(keyPtr, (nuint)keyLength, cf, readOptions);
        }
    }

    public bool HasKey(string key, ColumnFamilyHandle cf = null, ReadOptions readOptions = null, Encoding encoding = null)
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
    public long Get(byte[] key, byte[] buffer, long offset, long length, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        return Get(key, key.GetLongLength(0), buffer, offset, length, cf, readOptions);
    }

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
    public long Get(byte[] key, long keyLength, byte[] buffer, long offset, long length, ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        unsafe
        {
            nuint valueLength;
            sbyte* errptr = null;
            fixed (byte* keyPtr = key)
            {
                var ptr = cf is null
                    ? RocksDbNative.rocksdb_get(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), (sbyte*)keyPtr, (nuint)keyLength, &valueLength, &errptr)
                    : RocksDbNative.rocksdb_get_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)keyLength, &valueLength, &errptr);
                RocksDbInterop.ThrowIfError(errptr);
                if (ptr == null)
                {
                    return -1;
                }

                var copyLength = Math.Min(length, (long)valueLength);
                new ReadOnlySpan<byte>(ptr, (int)copyLength).CopyTo(buffer.AsSpan((int)offset, (int)copyLength));
                RocksDbNative.rocksdb_free(ptr);
                return (long)valueLength;
            }
        }
    }

    public KeyValuePair<byte[], byte[]>[] MultiGet(byte[][] keys, ColumnFamilyHandle[] cf = null, ReadOptions readOptions = null)
    {
        if (keys is null)
            throw new ArgumentNullException(nameof(keys));

        var count = keys.Length;
        if (cf is not null && cf.Length != count)
            throw new ArgumentException("Column family handle count must match key count.", nameof(cf));

        var result = new KeyValuePair<byte[], byte[]>[count];
        var keyHandles = new PinnedGCHandle<byte[]>[count];
        var keyPtrs = new sbyte*[count];
        var keyLengths = new nuint[count];
        var valuePtrs = new sbyte*[count];
        var valueLengths = new nuint[count];
        var errptrs = new sbyte*[count];
        var cfHandles = cf is null ? null : new rocksdb_column_family_handle_t*[count];

        try
        {
            for (var i = 0; i < count; i++)
            {
                if (keys[i] is null)
                    throw new ArgumentException("Keys cannot contain null values.", nameof(keys));

                keyHandles[i] = new PinnedGCHandle<byte[]>(keys[i]);
                keyPtrs[i] = (sbyte*)keyHandles[i].GetAddressOfArrayData();
                keyLengths[i] = (nuint)keys[i].Length;

                if (cfHandles is not null)
                    cfHandles[i] = RocksDbInterop.ColumnFamily(cf[i].Handle);
            }

            fixed (sbyte** keyPtrsPtr = keyPtrs)
            fixed (nuint* keyLengthsPtr = keyLengths)
            fixed (sbyte** valuePtrsPtr = valuePtrs)
            fixed (nuint* valueLengthsPtr = valueLengths)
            fixed (sbyte** errptrsPtr = errptrs)
            {
                if (cfHandles is null)
                {
                    RocksDbNative.rocksdb_multi_get(
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
                        RocksDbNative.rocksdb_multi_get_cf(
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
                result[i] = new KeyValuePair<byte[], byte[]>(keys[i], RocksDbInterop.BytesAndFree(valuePtrs[i], valueLengths[i]));
                if (errptrs[i] == null)
                    continue;

                if (firstError == null)
                    firstError = errptrs[i];
                else
                    RocksDbNative.rocksdb_free(errptrs[i]);
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

    private byte[] Get(byte* key, nuint keyLength, ColumnFamilyHandle cf, ReadOptions readOptions)
    {
        nuint valueLength;
        sbyte* errptr = null;
        var valuePtr = cf is null
            ? RocksDbNative.rocksdb_get(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), (sbyte*)key, keyLength, &valueLength, &errptr)
            : RocksDbNative.rocksdb_get_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &valueLength, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return RocksDbInterop.BytesAndFree(valuePtr, valueLength);
    }

    private bool HasKey(byte* key, nuint keyLength, ColumnFamilyHandle cf, ReadOptions readOptions)
    {
        nuint valueLength;
        sbyte* errptr = null;
        var valuePtr = cf is null
            ? RocksDbNative.rocksdb_get(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), (sbyte*)key, keyLength, &valueLength, &errptr)
            : RocksDbNative.rocksdb_get_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &valueLength, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        if (valuePtr == null)
            return false;
        RocksDbNative.rocksdb_free(valuePtr);
        return true;
    }

    private void Remove(byte* key, nuint keyLength, ColumnFamilyHandle cf, WriteOptions writeOptions)
    {
        sbyte* errptr = null;
        if (cf is null)
        {
            RocksDbNative.rocksdb_delete(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), (sbyte*)key, keyLength, &errptr);
        }
        else
        {
            RocksDbNative.rocksdb_delete_cf(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    private void Put(byte* key, nuint keyLength, byte* value, nuint valueLength, ColumnFamilyHandle cf, WriteOptions writeOptions)
    {
        sbyte* errptr = null;
        if (cf is null)
        {
            RocksDbNative.rocksdb_put(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        else
        {
            RocksDbNative.rocksdb_put_cf(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    private void Merge(byte* key, nuint keyLength, byte* value, nuint valueLength, ColumnFamilyHandle cf, WriteOptions writeOptions)
    {
        sbyte* errptr = null;
        if (cf is null)
        {
            RocksDbNative.rocksdb_merge(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        else
        {
            RocksDbNative.rocksdb_merge_cf(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)key, keyLength, (sbyte*)value, valueLength, &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    public KeyValuePair<string, string>[] MultiGet(string[] keys, ColumnFamilyHandle[] cf = null, ReadOptions readOptions = null)
    {
        if (keys is null)
            throw new ArgumentNullException(nameof(keys));

        var encodedKeys = new byte[keys.Length][];
        for (var i = 0; i < keys.Length; i++)
        {
            if (keys[i] is null)
                throw new ArgumentException("Keys cannot contain null values.", nameof(keys));

            encodedKeys[i] = DefaultEncoding.GetBytes(keys[i]);
        }

        var values = MultiGet(encodedKeys, cf, readOptions);
        var result = new KeyValuePair<string, string>[keys.Length];
        for (var i = 0; i < keys.Length; i++)
        {
            result[i] = new KeyValuePair<string, string>(
                keys[i],
                values[i].Value is null ? null : DefaultEncoding.GetString(values[i].Value));
        }

        return result;
    }

    public void Write(WriteBatch writeBatch, WriteOptions writeOptions = null)
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_write(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.WriteBatch(writeBatch.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Write(WriteBatchWithIndex writeBatch, WriteOptions writeOptions = null)
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_write_writebatch_wi(RocksDbInterop.Db(Handle), RocksDbInterop.WriteOptions((writeOptions ?? DefaultWriteOptions).Handle), RocksDbInterop.WriteBatchWithIndex(writeBatch.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Remove(string key, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        var keyBytes = DefaultEncoding.GetBytes(key);
        fixed (byte* keyPtr = keyBytes)
        {
            Remove(keyPtr, (nuint)keyBytes.Length, cf, writeOptions);
        }
    }

    public void Remove(byte[] key, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        Remove(key, key.Length, cf, writeOptions);
    }

    public unsafe void Remove(ReadOnlySpan<byte> key, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
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

    public void Remove(byte[] key, long keyLength, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
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

    public void Put(string key, string value, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null, Encoding encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        var valueBytes = encoding.GetBytes(value);
        Put(keyBytes, keyBytes.LongLength, valueBytes, valueBytes.LongLength, cf, writeOptions);
    }

    public void Put(byte[] key, byte[] value, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        Put(key, key.GetLongLength(0), value, value.GetLongLength(0), cf, writeOptions);
    }

    public void Put(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Put(keyPtr, (nuint)key.Length, valuePtr, (nuint)value.Length, cf, writeOptions);
        }
    }

    public void Put(byte[] key, long keyLength, byte[] value, long valueLength, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Put(keyPtr, (nuint)keyLength, valuePtr, (nuint)valueLength, cf, writeOptions);
        }
    }

    public void Merge(string key, string value, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null, Encoding encoding = null)
    {
        encoding ??= DefaultEncoding;
        var keyBytes = encoding.GetBytes(key);
        var valueBytes = encoding.GetBytes(value);
        Merge(keyBytes, keyBytes.LongLength, valueBytes, valueBytes.LongLength, cf, writeOptions);
    }

    public void Merge(byte[] key, byte[] value, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        Merge(key, key.GetLongLength(0), value, value.GetLongLength(0), cf, writeOptions);
    }

    public void Merge(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Merge(keyPtr, (nuint)key.Length, valuePtr, (nuint)value.Length, cf, writeOptions);
        }
    }

    public void Merge(byte[] key, long keyLength, byte[] value, long valueLength, ColumnFamilyHandle cf = null, WriteOptions writeOptions = null)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = value)
        {
            Merge(keyPtr, (nuint)keyLength, valuePtr, (nuint)valueLength, cf, writeOptions);
        }
    }

    public Iterator NewIterator(ColumnFamilyHandle cf = null, ReadOptions readOptions = null)
    {
        nint iteratorHandle = cf is null
            ? (nint)RocksDbNative.rocksdb_create_iterator(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle))
            : (nint)RocksDbNative.rocksdb_create_iterator_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ReadOptions((readOptions ?? DefaultReadOptions).Handle), RocksDbInterop.ColumnFamily(cf.Handle));
        // Note: passing in read options here only to ensure that it is not collected before the iterator
        return new Iterator(iteratorHandle, readOptions);
    }

    public Iterator[] NewIterators(ColumnFamilyHandle[] cfs, ReadOptions[] readOptions)
    {
        throw new NotImplementedException("TODO: Implement NewIterators()");
        // See rocksdb_create_iterators
    }

    public Snapshot CreateSnapshot()
    {
        nint snapshotHandle = (nint)RocksDbNative.rocksdb_create_snapshot(RocksDbInterop.Db(Handle));
        return new Snapshot(Handle, snapshotHandle);
    }

    public static IEnumerable<string> ListColumnFamilies(DbOptions options, string name)
    {
        return TryListColumnFamilies(options, name, out var columnFamilies)
            ? columnFamilies
            : Array.Empty<string>();
    }

    public static bool TryListColumnFamilies(DbOptions options, string name, out string[] columnFamilies)
    {
        using var path = new RocksSafePath(name);
        nuint lencf;
        sbyte* errptr = null;
        var result = RocksDbNative.rocksdb_list_column_families(RocksDbInterop.Options(options.Handle), (sbyte*)path.Handle, &lencf, &errptr);
        if (errptr != null)
        {
            columnFamilies = Array.Empty<string>();
            RocksDbNative.rocksdb_free(errptr);
            return false;
        }

        var count = checked((int)lencf);
        columnFamilies = new string[count];
        for (var i = 0; i < count; i++)
        {
            columnFamilies[i] = Utf8StringMarshaller.ConvertToManaged((byte*)result[i]);
        }

        RocksDbNative.rocksdb_list_column_families_destroy(result, lencf);
        return true;
    }

    public ColumnFamilyHandle CreateColumnFamily(ColumnFamilyOptions cfOptions, string name)
    {
        using var nativeName = new RocksSafePath(name);
        sbyte* errptr = null;
        var cfh = (nint)RocksDbNative.rocksdb_create_column_family(RocksDbInterop.Db(Handle), RocksDbInterop.Options(cfOptions.Handle), (sbyte*)nativeName.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        var cfhw = new ColumnFamilyHandleInternal(cfh);
        columnFamilies.Add(name, cfhw);
        return cfhw;
    }

    public void DropColumnFamily(string name)
    {
        var cf = GetColumnFamily(name);
        sbyte* errptr = null;
        RocksDbNative.rocksdb_drop_column_family(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        columnFamilies.Remove(name);
    }

    public ColumnFamilyHandle GetDefaultColumnFamily()
    {
        return GetColumnFamily(ColumnFamilies.DefaultName);
    }

    public ColumnFamilyHandle GetColumnFamily(string name)
    {
        if (columnFamilies is null)
        {
            throw new RocksDbException("Database not opened for column families");
        }

        return columnFamilies[name];
    }

    public bool TryGetColumnFamily(string name, out ColumnFamilyHandle handle)
    {
        if (columnFamilies is null)
        {
            throw new RocksDbException("Database not opened for column families");
        }

        if (columnFamilies.TryGetValue(name, out var internalHandle))
        {
            handle = internalHandle;
            return true;
        }

        handle = null;
        return false;
    }

    public string GetProperty(string propertyName)
    {
        using var property = new RocksSafePath(propertyName);
        return RocksDbInterop.NullTerminatedStringAndFree(RocksDbNative.rocksdb_property_value(RocksDbInterop.Db(Handle), (sbyte*)property.Handle));
    }

    public string GetProperty(string propertyName, ColumnFamilyHandle cf)
    {
        using var property = new RocksSafePath(propertyName);
        return RocksDbInterop.NullTerminatedStringAndFree(RocksDbNative.rocksdb_property_value_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)property.Handle));
    }

    public void IngestExternalFiles(string[] files, IngestExternalFileOptions ingestOptions, ColumnFamilyHandle cf = null)
    {
        using var nativeFiles = new NativeUtf8StringArray(files);
        sbyte* errptr = null;
        if (cf is null)
        {
            RocksDbNative.rocksdb_ingest_external_file(RocksDbInterop.Db(Handle), nativeFiles.Pointer, (nuint)files.GetLongLength(0), RocksDbInterop.IngestExternalFileOptions(ingestOptions.Handle), &errptr);
        }
        else
        {
            RocksDbNative.rocksdb_ingest_external_file_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), nativeFiles.Pointer, (nuint)files.GetLongLength(0), RocksDbInterop.IngestExternalFileOptions(ingestOptions.Handle), &errptr);
        }
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void CompactRange(byte[] start, byte[] limit, ColumnFamilyHandle cf = null)
    {
        fixed (byte* startPtr = start)
        fixed (byte* limitPtr = limit)
        {
            if (cf is null)
            {
                RocksDbNative.rocksdb_compact_range(RocksDbInterop.Db(Handle), (sbyte*)startPtr, (nuint)(start?.GetLongLength(0) ?? 0L), (sbyte*)limitPtr, (nuint)(limit?.GetLongLength(0) ?? 0L));
            }
            else
            {
                RocksDbNative.rocksdb_compact_range_cf(RocksDbInterop.Db(Handle), RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)startPtr, (nuint)(start?.GetLongLength(0) ?? 0L), (sbyte*)limitPtr, (nuint)(limit?.GetLongLength(0) ?? 0L));
            }
        }
    }

    public void CompactRange(string start, string limit, ColumnFamilyHandle cf = null, Encoding encoding = null)
    {
        if (encoding is null)
        {
            encoding = Encoding.UTF8;
        }

        CompactRange(start is null ? null : encoding.GetBytes(start), limit is null ? null : encoding.GetBytes(limit), cf);
    }

    public void TryCatchUpWithPrimary()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_try_catch_up_with_primary(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void DisableFileDeletions()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_disable_file_deletions(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void EnableFileDeletions()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_enable_file_deletions(RocksDbInterop.Db(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public TransactionLogIterator GetUpdatesSince(ulong sequenceNumber)
    {
        // options is null for now as we don't have a wrapper and pass null to C API
        sbyte* errptr = null;
        nint iteratorHandle = (nint)RocksDbNative.rocksdb_get_updates_since(RocksDbInterop.Db(Handle), (nuint)sequenceNumber, null, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
        return new TransactionLogIterator(iteratorHandle);
    }

    public ulong GetLatestSequenceNumber()
    {
        return (ulong)RocksDbNative.rocksdb_get_latest_sequence_number(RocksDbInterop.Db(Handle));
    }

    public void Flush(FlushOptions flushOptions)
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_flush(RocksDbInterop.Db(Handle), RocksDbInterop.FlushOptions(flushOptions.Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }


    /// <summary>
    /// Returns metadata about the file and data in the file. 
    /// </summary>
    /// <param name="populateFileMetadataOnly">setting it to true only populates FileName, 
    /// Filesize and filelevel; By default it is false</param>
    /// <returns><c>LiveFilesMetadata</c> or null in case of failure</returns>
    public List<LiveFileMetadata> GetLiveFilesMetadata(bool populateFileMetadataOnly = false)
    {
        nint buffer = (nint)RocksDbNative.rocksdb_livefiles(RocksDbInterop.Db(Handle));
        if (buffer == nint.Zero)
        {
            return null;
        }

        try
        {
            List<LiveFileMetadata> filesMetadata = new List<LiveFileMetadata>();

            int fileCount = RocksDbNative.rocksdb_livefiles_count(RocksDbInterop.LiveFiles(buffer));
            for (int index = 0; index < fileCount; index++)
            {
                LiveFileMetadata liveFileMetadata = new LiveFileMetadata();

                FileMetadata metadata = new FileMetadata();
                var fileMetadata = RocksDbNative.rocksdb_livefiles_name(RocksDbInterop.LiveFiles(buffer), index);
                string fileName = Utf8StringMarshaller.ConvertToManaged((byte*)fileMetadata);

                int level = RocksDbNative.rocksdb_livefiles_level(RocksDbInterop.LiveFiles(buffer), index);

                ulong fileSize = (ulong)RocksDbNative.rocksdb_livefiles_size(RocksDbInterop.LiveFiles(buffer), index);

                metadata.FileName = fileName;
                metadata.FileLevel = level;
                metadata.FileSize = fileSize;

                liveFileMetadata.FileMetadata = metadata;

                if (!populateFileMetadataOnly)
                {
                    FileDataMetadata fileDataMetadata = new FileDataMetadata();
                    nuint smallestKeySize;
                    var smallestKeyPtr = RocksDbNative.rocksdb_livefiles_smallestkey(RocksDbInterop.LiveFiles(buffer), index, &smallestKeySize);
                    // These keys are length-delimited rather than null-terminated, so decode by size
                    string smallestKey = DefaultEncoding.GetString((byte*)smallestKeyPtr, checked((int)smallestKeySize));

                    nuint largestKeySize;
                    var largestKeyPtr = RocksDbNative.rocksdb_livefiles_largestkey(RocksDbInterop.LiveFiles(buffer), index, &largestKeySize);
                    string largestKey = DefaultEncoding.GetString((byte*)largestKeyPtr, checked((int)largestKeySize));

                    ulong entries = (ulong)RocksDbNative.rocksdb_livefiles_entries(RocksDbInterop.LiveFiles(buffer), index);
                    ulong deletions = (ulong)RocksDbNative.rocksdb_livefiles_deletions(RocksDbInterop.LiveFiles(buffer), index);

                    fileDataMetadata.SmallestKeyInFile = smallestKey;
                    fileDataMetadata.LargestKeyInFile = largestKey;
                    fileDataMetadata.NumEntriesInFile = entries;
                    fileDataMetadata.NumDeletionsInFile = deletions;

                    liveFileMetadata.FileDataMetadata = fileDataMetadata;
                }

                filesMetadata.Add(liveFileMetadata);
            }

            return filesMetadata;
        }
        finally
        {
            RocksDbNative.rocksdb_livefiles_destroy(RocksDbInterop.LiveFiles(buffer));
            buffer = nint.Zero;
        }
    }

    /// <summary>
    /// Lean API to just get Live file names. 
    /// Refer to GetLiveFilesMetadata() for the complete metadata
    /// </summary>
    /// <returns></returns>
    public List<string> GetLiveFileNames()
    {
        nint buffer = (nint)RocksDbNative.rocksdb_livefiles(RocksDbInterop.Db(Handle));
        if (buffer == nint.Zero)
        {
            return new List<string>();
        }

        try
        {
            List<string> liveFiles = new List<string>();

            int fileCount = RocksDbNative.rocksdb_livefiles_count(RocksDbInterop.LiveFiles(buffer));

            for (int index = 0; index < fileCount; index++)
            {
                var fileMetadata = RocksDbNative.rocksdb_livefiles_name(RocksDbInterop.LiveFiles(buffer), index);
                string fileName = Utf8StringMarshaller.ConvertToManaged((byte*)fileMetadata);
                liveFiles.Add(fileName);
            }

            return liveFiles;
        }
        finally
        {
            RocksDbNative.rocksdb_livefiles_destroy(RocksDbInterop.LiveFiles(buffer));
            buffer = nint.Zero;
        }
    }
}
