// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers;
using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed partial class RocksDb
{
    private const int Reference17Capacity = 256;
    public void MultiGetReference17<T>(
        ReadOnlySpan<byte> keys,
        int keyLength,
        Span<T> values,
        ISpanDeserializer<T> deserializer,
        IColumnFamilyHandle? cf = null,
        ReadOptions? readOptions = null)
    {
        ArgumentNullException.ThrowIfNull(deserializer);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if ((long)keys.Length != (long)values.Length * keyLength)
            throw new ArgumentException("The key buffer length must match the value count and fixed key length.", nameof(keys));

        values.Clear();
        if (values.IsEmpty)
            return;

        if (cf is null)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = Get(keys.Slice(i * keyLength, keyLength), deserializer, readOptions: readOptions)!;

            return;
        }

        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);

        int count = values.Length;
        rocksdb_slice_t[]? rentedKeys = null;
        nint[]? rentedValueHandles = null;
        nint[]? rentedErrorHandles = null;
        scoped Span<rocksdb_slice_t> keySlices;
        scoped Span<nint> valueHandles;
        scoped Span<nint> errorHandles;

        if (count <= Reference17Capacity)
        {
            keySlices = stackalloc rocksdb_slice_t[count];
            valueHandles = stackalloc nint[count];
            errorHandles = stackalloc nint[count];
        }
        else
        {
            rentedKeys = ArrayPool<rocksdb_slice_t>.Shared.Rent(count);
            rentedValueHandles = ArrayPool<nint>.Shared.Rent(count);
            rentedErrorHandles = ArrayPool<nint>.Shared.Rent(count);
            keySlices = rentedKeys.AsSpan(0, count);
            valueHandles = rentedValueHandles.AsSpan(0, count);
            errorHandles = rentedErrorHandles.AsSpan(0, count);
        }

        valueHandles.Clear();
        errorHandles.Clear();

        try
        {
            fixed (byte* keyData = keys)
            fixed (rocksdb_slice_t* keySlicesPtr = keySlices)
            fixed (nint* valueHandlesPtr = valueHandles)
            fixed (nint* errorHandlesPtr = errorHandles)
            {
                for (int i = 0; i < count; i++)
                {
                    keySlicesPtr[i] = new rocksdb_slice_t
                    {
                        data = (sbyte*)(keyData + (i * keyLength)),
                        size = (nuint)keyLength,
                    };
                }

                rocksdb_batched_multi_get_cf_slice(
                    RocksDbInterop.Db(NativeHandle),
                    RocksDbInterop.ReadOptions(optionsHandle),
                    RocksDbInterop.ColumnFamily(cf.Handle),
                    (nuint)count,
                    keySlicesPtr,
                    (rocksdb_pinnableslice_t**)valueHandlesPtr,
                    (sbyte**)errorHandlesPtr,
                    sorted_input: false);

                sbyte* firstError = null;
                for (int i = 0; i < count; i++)
                {
                    sbyte* error = (sbyte*)errorHandlesPtr[i];
                    if (error is null)
                        continue;

                    errorHandlesPtr[i] = nint.Zero;
                    if (firstError is null)
                        firstError = error;
                    else
                        rocksdb_free(error);
                }

                if (firstError is not null)
                    RocksDbInterop.ThrowIfError(firstError);

                for (int i = 0; i < count; i++)
                {
                    rocksdb_pinnableslice_t* valueHandle = (rocksdb_pinnableslice_t*)valueHandlesPtr[i];
                    if (valueHandle is null)
                        continue;

                    try
                    {
                        nuint valueLength;
                        sbyte* valueData = rocksdb_pinnableslice_value(valueHandle, &valueLength);
                        if (valueLength > int.MaxValue)
                            throw new NotSupportedException($"The value is {valueLength} bytes; values over {int.MaxValue} bytes cannot be exposed as a span.");

                        ReadOnlySpan<byte> value = valueLength == 0
                            ? ReadOnlySpan<byte>.Empty
                            : new ReadOnlySpan<byte>((void*)valueData, (int)valueLength);
                        values[i] = deserializer.Deserialize(value);
                    }
                    finally
                    {
                        rocksdb_pinnableslice_destroy(valueHandle);
                        valueHandlesPtr[i] = nint.Zero;
                    }
                }
            }
        }
        finally
        {
            for (int i = 0; i < count; i++)
            {
                if (valueHandles[i] is not 0)
                {
                    rocksdb_pinnableslice_destroy((rocksdb_pinnableslice_t*)valueHandles[i]);
                    valueHandles[i] = nint.Zero;
                }

                if (errorHandles[i] is not 0)
                {
                    rocksdb_free((void*)errorHandles[i]);
                    errorHandles[i] = nint.Zero;
                }
            }

            if (rentedKeys is not null)
                ArrayPool<rocksdb_slice_t>.Shared.Return(rentedKeys);
            if (rentedValueHandles is not null)
                ArrayPool<nint>.Shared.Return(rentedValueHandles);
            if (rentedErrorHandles is not null)
                ArrayPool<nint>.Shared.Return(rentedErrorHandles);
        }
    }
}
