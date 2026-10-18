// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers;
using System.Runtime.CompilerServices;
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

    /// <summary>Reads fixed-length keys asynchronously and decodes the values into the caller's buffer.</summary>
    /// <remarks>
    /// Copies the keys before returning, so the key buffer may be reused at once. The value buffer must stay
    /// alive and untouched until the task completes. Missing keys produce default values without calling the
    /// decoder; empty values still call it. Decoding runs on a thread-pool thread directly over native memory
    /// and the decoder must not retain that span. On a read or decoder failure, values from the failing index
    /// on are cleared. Read options must not be modified until completion.
    /// RocksDB may complete synchronously when native coroutine reads are unavailable.
    /// </remarks>
    /// <exception cref="ArgumentNullException">The deserializer is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The key length is not positive.</exception>
    /// <exception cref="ArgumentException">The key buffer does not match the output count and key length.</exception>
    /// <exception cref="ObjectDisposedException">The database, read options or column family handle has been disposed.</exception>
    [SkipLocalsInit]
    public ValueTask MultiGetAsync<T, TDeserializer>(ReadOnlySpan<byte> keys, int keyLength, Memory<T> values,
        TDeserializer deserializer, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if ((long)keys.Length != (long)values.Length * keyLength)
            throw new ArgumentException("The key buffer length must match the value count and fixed key length.", nameof(keys));
        if (cf is not null && cf.Handle == 0)
            throw new ObjectDisposedException(nameof(cf), "The column family handle has been disposed.");
        int count = values.Length;
        if (count == 0) return ValueTask.CompletedTask;

        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);

        // Pointer and length scratch for the native create call, which copies the keys it is given.
        int scratchCount = cf is null ? checked(count * 2) : checked(count * 3);
        nint[]? rented = null;
        Span<nint> scratch = scratchCount <= StackReadCapacity * 3
            ? stackalloc nint[scratchCount]
            : (rented = ArrayPool<nint>.Shared.Rent(scratchCount)).AsSpan(0, scratchCount);
        try
        {
            fixed (byte* keyData = keys)
            fixed (nint* scratchPointer = scratch)
            {
                nint* keyPointers = scratchPointer;
                nuint* keyLengths = (nuint*)(scratchPointer + count);
                nint* families = cf is null ? null : scratchPointer + count * 2;
                for (int i = 0; i < count; i++)
                {
                    keyPointers[i] = (nint)(keyData + (nint)i * keyLength);
                    keyLengths[i] = (nuint)keyLength;
                    if (families is not null) families[i] = cf!.Handle;
                }

                sbyte* error = null;
                Native.rocksdb_net_read_t* request = rocksdb_net_read_create(RocksDbInterop.Db(NativeHandle),
                    RocksDbInterop.ReadOptions(optionsHandle), (nuint)count,
                    (Native.rocksdb_column_family_handle_t**)families, (sbyte**)keyPointers, keyLengths, &error);
                RocksDbInterop.ThrowIfError(error);
                AsyncReadHandle handle = OwnAsyncRead((nint)request, options);
                AsyncDecodedMultiGetOperation<T, TDeserializer> operation =
                    AsyncDecodedMultiGetOperation<T, TDeserializer>.Rent(handle, values, deserializer);
                ValueTask result = operation.ValueTask;
                operation.Start(multi: true);
                return result;
            }
        }
        finally
        {
            if (rented is not null) ArrayPool<nint>.Shared.Return(rented);
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
