// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Nethermind.RocksDbBindings.Native;
using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed partial class RocksDb
{
    private void ReadSingleMultiGet<T, TDeserializer>(ReadOnlySpan<byte> keys, int keyLength, Span<T> values,
        TDeserializer deserializer, IColumnFamilyHandle? cf, ReadOptions? readOptions)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if (keys.Length != keyLength)
            throw new ArgumentException("The key buffer length must match the value count and fixed key length.", nameof(keys));
        values.Clear();
        using HandleLease lease = Lease();
        values[0] = ReadSingleDecodedValue<T, TDeserializer>(keys, ref deserializer, cf, readOptions);
    }

    [SkipLocalsInit]
    private void ReadBatchContext<T, TDeserializer>(ReadOnlySpan<byte> keys, int keyLength, Span<T> values,
        TDeserializer deserializer, IColumnFamilyHandle? cf, ReadOptions? readOptions, bool sortedInput)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if ((long)keys.Length != (long)values.Length * keyLength)
            throw new ArgumentException("The key buffer length must match the value count and fixed key length.", nameof(keys));
        int count = values.Length;
        int scratchCount = checked(count * 2);
        values.Clear();

        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);
        if (rocksdb_readoptions_get_read_tier(RocksDbInterop.ReadOptions(optionsHandle)) != 0)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = ReadDecodedValue<T, TDeserializer>(keys.Slice(i * keyLength, keyLength), ref deserializer, cf, options);
            return;
        }

        rocksdb_slice_t[]? rented = null;
        Span<rocksdb_slice_t> scratch = count <= StackReadCapacity
            ? stackalloc rocksdb_slice_t[scratchCount]
            : (rented = ArrayPool<rocksdb_slice_t>.Shared.Rent(scratchCount)).AsSpan(0, scratchCount);
        Span<rocksdb_slice_t> keySlices = scratch[..count];
        Span<rocksdb_slice_t> valueSlices = scratch[count..];
        try
        {
            fixed (byte* keyData = keys)
            fixed (rocksdb_slice_t* keySlicesPtr = keySlices)
            fixed (rocksdb_slice_t* valueSlicesPtr = valueSlices)
            {
                for (int i = 0; i < count; i++)
                    keySlicesPtr[i] = new rocksdb_slice_t { data = (sbyte*)(keyData + i * keyLength), size = (nuint)keyLength };

                sbyte* error = null;
                nint batch = 0;
                byte succeeded = NativeBatchRead(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                    RocksDbInterop.ColumnFamily(cf?.Handle ?? 0), (nuint)count, keySlicesPtr, valueSlicesPtr,
                    sortedInput ? (byte)1 : (byte)0, &batch, &error);
                try
                {
                    RocksDbInterop.ThrowIfError(error);
                    if (succeeded == 0) throw new InvalidOperationException("Native batch read failed without an error.");
                    if (batch == 0) return;
                    for (int i = 0; i < count; i++)
                    {
                        nuint length = valueSlicesPtr[i].size;
                        if (length == nuint.MaxValue) continue;
                        if (length > int.MaxValue)
                            throw new NotSupportedException($"The value is {length} bytes; values over {int.MaxValue} bytes cannot be exposed as a span.");
                        values[i] = deserializer.Deserialize(new ReadOnlySpan<byte>(valueSlicesPtr[i].data, (int)length));
                    }
                }
                finally
                {
                    if (batch != 0) NativeBatchDestroy(batch);
                }
            }
        }
        finally
        {
            if (rented is not null) ArrayPool<rocksdb_slice_t>.Shared.Return(rented);
        }
    }

    [DllImport("rocksdb", EntryPoint = "rocksdb_net_batch_read", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern byte NativeBatchRead(rocksdb_t* db, rocksdb_readoptions_t* options,
        rocksdb_column_family_handle_t* family, nuint count, rocksdb_slice_t* keys,
        rocksdb_slice_t* values, byte sorted, nint* batch, sbyte** error);

    [DllImport("rocksdb", EntryPoint = "rocksdb_net_batch_destroy", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern void NativeBatchDestroy(nint batch);
}
