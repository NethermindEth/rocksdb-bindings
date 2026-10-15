// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers;
using System.Runtime.CompilerServices;
using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed partial class RocksDb
{
    [SkipLocalsInit]
    public void MultiGetDecoded<T, TDeserializer>(ReadOnlySpan<byte> keys, int keyLength, Span<T> values,
        TDeserializer deserializer, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null, bool sortedInput = false)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if ((long)keys.Length != (long)values.Length * keyLength)
            throw new ArgumentException("The key buffer length must match the value count and fixed key length.", nameof(keys));
        values.Clear();
        if (values.IsEmpty) return;

        if (values.Length == 1)
        {
            using HandleLease singleLease = Lease();
            values[0] = ReadDecodedValue<T, TDeserializer>(keys, ref deserializer, cf, readOptions);
            return;
        }

        int count = values.Length;
        rocksdb_slice_t[]? rentedKeys = null;
        nint[]? rentedHandles = null;
        HandleLease lease = default;
        HandleLease optionsLease = default;
        int completed = 0;

        try
        {
            lease = Lease();
            ReadOptions options = readOptions ?? DefaultReadOptions;
            optionsLease = options.Lease(out nint optionsHandle);
            if (cf is null && rocksdb_readoptions_get_read_tier(RocksDbInterop.ReadOptions(optionsHandle)) != 0)
            {
                for (; completed < count; completed++)
                    values[completed] = ReadDecodedValue<T, TDeserializer>(keys.Slice(completed * keyLength, keyLength), ref deserializer, cf, options);
                return;
            }
            int handleCount = checked(count * 2);
            Span<rocksdb_slice_t> keySlices = count <= StackMultiGetCapacity
                ? stackalloc rocksdb_slice_t[count]
                : (rentedKeys = ArrayPool<rocksdb_slice_t>.Shared.Rent(count)).AsSpan(0, count);
            scoped Span<nint> handles = count <= StackMultiGetCapacity
                ? stackalloc nint[handleCount]
                : (rentedHandles = ArrayPool<nint>.Shared.Rent(handleCount)).AsSpan(0, handleCount);
            Span<nint> valueHandles = handles[..count];
            Span<nint> errorHandles = handles[count..];
            bool initialized = false;
            rocksdb_column_family_handle_t* defaultFamily = null;

            try
            {
                rocksdb_column_family_handle_t* family = cf is null
                    ? defaultFamily = rocksdb_get_default_column_family_handle(RocksDbInterop.Db(NativeHandle))
                    : RocksDbInterop.ColumnFamily(cf.Handle);

                fixed (byte* keyData = keys)
                fixed (rocksdb_slice_t* keySlicesPtr = keySlices)
                fixed (nint* valueHandlesPtr = valueHandles)
                fixed (nint* errorHandlesPtr = errorHandles)
                {
                    for (int i = 0; i < count; i++)
                        keySlicesPtr[i] = new rocksdb_slice_t { data = (sbyte*)(keyData + i * keyLength), size = (nuint)keyLength };

                    rocksdb_batched_multi_get_cf_slice(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                        family, (nuint)count, keySlicesPtr, (rocksdb_pinnableslice_t**)valueHandlesPtr, (sbyte**)errorHandlesPtr, sortedInput);
                    initialized = true;

                    sbyte* firstError = null;
                    for (int i = 0; i < count; i++)
                    {
                        sbyte* error = (sbyte*)errorHandlesPtr[i];
                        if (error is null) continue;
                        errorHandlesPtr[i] = 0;
                        if (firstError is null) firstError = error;
                        else rocksdb_free(error);
                    }
                    if (firstError is not null) RocksDbInterop.ThrowIfError(firstError);

                    for (; completed < count; completed++)
                    {
                        rocksdb_pinnableslice_t* valueHandle = (rocksdb_pinnableslice_t*)valueHandlesPtr[completed];
                        if (valueHandle is null)
                        {
                            values[completed] = default!;
                            continue;
                        }

                        nuint valueLength;
                        sbyte* data = rocksdb_pinnableslice_value(valueHandle, &valueLength);
                        if (valueLength > int.MaxValue)
                            throw new NotSupportedException($"The value is {valueLength} bytes; values over {int.MaxValue} bytes cannot be exposed as a span.");
                        values[completed] = deserializer.Deserialize(new ReadOnlySpan<byte>(data, (int)valueLength));
                        rocksdb_pinnableslice_destroy(valueHandle);
                    }
                }
            }
            finally
            {
                if (initialized)
                {
                    for (int i = completed; i < count; i++)
                    {
                        if (valueHandles[i] != 0) rocksdb_pinnableslice_destroy((rocksdb_pinnableslice_t*)valueHandles[i]);
                        if (errorHandles[i] != 0) rocksdb_free((void*)errorHandles[i]);
                    }
                }
                if (defaultFamily is not null) rocksdb_column_family_handle_destroy(defaultFamily);
            }
        }
        finally
        {
            if (rentedKeys is not null) ArrayPool<rocksdb_slice_t>.Shared.Return(rentedKeys);
            if (rentedHandles is not null) ArrayPool<nint>.Shared.Return(rentedHandles);
            optionsLease.Dispose();
            lease.Dispose();
        }
    }

    private T ReadDecodedValue<T, TDeserializer>(ReadOnlySpan<byte> key, ref TDeserializer deserializer,
        IColumnFamilyHandle? cf, ReadOptions? readOptions) where TDeserializer : ISpanDeserializer<T>
    {
        if (!TryGetPinned(key, out PinnedSlice slice, cf, readOptions)) return default!;
        try
        {
            return deserializer.Deserialize(slice.Value);
        }
        finally
        {
            slice.Dispose();
        }
    }
}
