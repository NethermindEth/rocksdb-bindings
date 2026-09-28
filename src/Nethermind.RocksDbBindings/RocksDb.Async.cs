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
    {
        using var lease = Lease();
        var options = readOptions ?? DefaultReadOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        fixed (byte* keyPtr = key)
        {
            var keyData = (sbyte*)keyPtr;
            var keyLength = (nuint)key.Length;
            var family = RocksDbInterop.ColumnFamily(cf?.Handle ?? 0);
            sbyte* error = null;
            var request = rocksdb_net_read_create(RocksDbInterop.Db(NativeHandle),
                RocksDbInterop.ReadOptions(optionsHandle), 1, cf is null ? null : &family,
                &keyData, &keyLength, &error);
            RocksDbInterop.ThrowIfError(error);
            var handle = OwnAsyncRead((nint)request, options);
            var operation = new AsyncGetOperation(handle);
            operation.Start(multi: false);
            return new ValueTask<byte[]?>(operation.Task);
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
        using var lease = Lease();
        if (cf is not null && cf.Length != keys.Length)
            throw new ArgumentException("Column family handle count must match key count.", nameof(cf));

        var options = readOptions ?? DefaultReadOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);
        if (keys.Length == 0)
            return ValueTask.FromResult(Array.Empty<KeyValuePair<byte[], byte[]?>>());

        var keyCopies = new byte[keys.Length][];
        var pins = new PinnedGCHandle<byte[]>[keys.Length];
        var keyPointers = new nint[keys.Length];
        var keyLengths = new nuint[keys.Length];
        var families = cf is null ? null : new nint[keys.Length];
        try
        {
            for (var i = 0; i < keys.Length; i++)
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
                var request = rocksdb_net_read_create(RocksDbInterop.Db(NativeHandle),
                    RocksDbInterop.ReadOptions(optionsHandle), (nuint)keys.Length,
                    (Native.rocksdb_column_family_handle_t**)familyPointer,
                    (sbyte**)keyPointer, lengthPointer, &error);
                RocksDbInterop.ThrowIfError(error);
                var handle = OwnAsyncRead((nint)request, options);
                var operation = new AsyncMultiGetOperation(handle, keyCopies);
                operation.Start(multi: true);
                return new ValueTask<KeyValuePair<byte[], byte[]?>[]>(operation.Task);
            }
        }
        finally
        {
            foreach (var pin in pins)
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
}
