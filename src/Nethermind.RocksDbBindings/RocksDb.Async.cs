// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe partial class RocksDb
{
    /// <summary>Reads a value asynchronously, returning null when the key does not exist.</summary>
    /// <remarks>
    /// Copies the key before returning. Read options must not be modified until completion.
    /// RocksDB may complete synchronously when native coroutine reads are unavailable.
    /// </remarks>
    public ValueTask<byte[]?> GetAsync(ReadOnlySpan<byte> key,
        IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
        => GetAsync<byte[], ByteArrayDeserializer>(key, default, cf, readOptions);

    /// <summary>Deserializes a value asynchronously, returning default when the key does not exist.</summary>
    /// <remarks>
    /// Deserializes directly from native memory on a thread-pool thread, without an intermediate byte array.
    /// Copies the key before returning. Read options must not be modified until completion.
    /// RocksDB may complete synchronously when native coroutine reads are unavailable.
    /// </remarks>
    public ValueTask<T?> GetAsync<T>(ReadOnlySpan<byte> key, ISpanDeserializer<T> deserializer,
        IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
        => GetAsync<T, ISpanDeserializer<T>>(key, deserializer, cf, readOptions);

    /// <summary>Deserializes a value asynchronously without boxing a struct deserializer.</summary>
    /// <remarks>
    /// Returns default when the key does not exist. Deserialization runs on a thread-pool thread
    /// directly over native memory. Copies the key before returning; read options must not be
    /// modified until completion. Native submission may complete synchronously.
    /// </remarks>
    public ValueTask<T?> GetAsync<T, TDeserializer>(ReadOnlySpan<byte> key, TDeserializer deserializer,
        IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (deserializer is null)
            throw new ArgumentNullException(nameof(deserializer));
        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);
        fixed (byte* keyPtr = key)
        {
            sbyte* keyData = (sbyte*)keyPtr;
            nuint keyLength = (nuint)key.Length;
            Native.rocksdb_column_family_handle_t* family = RocksDbInterop.ColumnFamily(cf?.Handle ?? 0);
            sbyte* error = null;
            Native.rocksdb_net_read_t* request = rocksdb_net_read_create(RocksDbInterop.Db(NativeHandle),
                RocksDbInterop.ReadOptions(optionsHandle), 1, cf is null ? null : &family,
                &keyData, &keyLength, &error);
            RocksDbInterop.ThrowIfError(error);
            AsyncReadHandle handle = OwnAsyncRead((nint)request, options);
            AsyncGetOperation<T, TDeserializer> operation = AsyncGetOperation<T, TDeserializer>.Rent(handle, deserializer);
            ValueTask<T?> result = operation.ValueTask;
            operation.Start(multi: false);
            return result;
        }
    }

    /// <summary>Reads values in input order, returning null values for missing keys.</summary>
    /// <remarks>
    /// Copies keys before returning. Read options must not be modified until completion.
    /// Any read error fails the operation, as with <see cref="MultiGet"/>.
    /// RocksDB may complete synchronously when native coroutine reads are unavailable.
    /// </remarks>
    public ValueTask<KeyValuePair<byte[], byte[]?>[]> MultiGetAsync(byte[][] keys,
        IColumnFamilyHandle[]? cf = null, ReadOptions? readOptions = null)
    {
        ArgumentNullException.ThrowIfNull(keys);
        using HandleLease lease = Lease();
        if (cf is not null && cf.Length != keys.Length)
            throw new ArgumentException("Column family handle count must match key count.", nameof(cf));

        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);
        if (keys.Length == 0)
            return ValueTask.FromResult(Array.Empty<KeyValuePair<byte[], byte[]?>>());

        byte[][] keyCopies = new byte[keys.Length][];
        PinnedGCHandle<byte[]>[] pins = new PinnedGCHandle<byte[]>[keys.Length];
        nint[] keyPointers = new nint[keys.Length];
        nuint[] keyLengths = new nuint[keys.Length];
        nint[]? families = cf is null ? null : new nint[keys.Length];
        try
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] is null)
                    throw new ArgumentException("Keys cannot contain null values.", nameof(keys));
                keyCopies[i] = keys[i].ToArray();
                pins[i] = new PinnedGCHandle<byte[]>(keyCopies[i]);
                keyPointers[i] = (nint)pins[i].GetAddressOfArrayData();
                keyLengths[i] = (nuint)keyCopies[i].Length;
                if (cf is not null)
                {
                    ArgumentNullException.ThrowIfNull(cf[i]);
                    families![i] = cf[i].Handle;
                }
            }

            fixed (nint* keyPointer = keyPointers)
            fixed (nuint* lengthPointer = keyLengths)
            fixed (nint* familyPointer = families)
            {
                sbyte* error = null;
                Native.rocksdb_net_read_t* request = rocksdb_net_read_create(RocksDbInterop.Db(NativeHandle),
                    RocksDbInterop.ReadOptions(optionsHandle), (nuint)keys.Length,
                    (Native.rocksdb_column_family_handle_t**)familyPointer,
                    (sbyte**)keyPointer, lengthPointer, &error);
                RocksDbInterop.ThrowIfError(error);
                AsyncReadHandle handle = OwnAsyncRead((nint)request, options);
                AsyncMultiGetOperation operation = new(handle, keyCopies);
                operation.Start(multi: true);
                return new ValueTask<KeyValuePair<byte[], byte[]?>[]>(operation.Task);
            }
        }
        finally
        {
            foreach (PinnedGCHandle<byte[]> pin in pins)
                pin.Dispose();
        }
    }

    private AsyncReadHandle OwnAsyncRead(nint request, ReadOptions options)
    {
        HandleLease dbLease = default, optionsLease = default, snapshotLease = default;
        try
        {
            dbLease = new HandleLease(_handle);
            optionsLease = new HandleLease(options.SafeHandle);
            snapshotLease = options.Snapshot?.Lease() ?? default;
            return new AsyncReadHandle(request, dbLease, optionsLease, snapshotLease);
        }
        catch
        {
            rocksdb_net_read_destroy((Native.rocksdb_net_read_t*)request);
            snapshotLease.Dispose();
            optionsLease.Dispose();
            dbLease.Dispose();
            throw;
        }
    }

    private readonly struct ByteArrayDeserializer : ISpanDeserializer<byte[]>
    {
        public byte[] Deserialize(ReadOnlySpan<byte> buffer) => buffer.ToArray();
    }
}
