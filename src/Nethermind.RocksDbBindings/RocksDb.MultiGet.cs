// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;
using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe sealed partial class RocksDb
{
    private const int StackReadCapacity = 256;

    /// <summary>Decodes values directly from native memory into the caller's output buffer.</summary>
    /// <remarks>
    /// Missing keys produce default values without calling the decoder. Empty values still call it.
    /// The key and output spans must not overlap.
    /// The decoder must not retain the native value span after returning.
    /// A sorted-input hint requires keys in column-family comparator order; results are undefined otherwise.
    /// Native batch errors are checked before decoding. With a non-default read tier, keys are read and decoded one at a time;
    /// a read failure retains the decoded prefix. Decoder failures also retain the prefix and leave the tail cleared.
    /// </remarks>
    /// <exception cref="ArgumentNullException">The deserializer is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The key length is not positive.</exception>
    /// <exception cref="ArgumentException">The key buffer does not match the output count and key length.</exception>
    /// <exception cref="RocksDbNativeException">A native read fails.</exception>
    /// <exception cref="NotSupportedException">A native value exceeds the maximum span length.</exception>
    /// <exception cref="ObjectDisposedException">The database, read options or column family handle has been disposed.</exception>
    /// <exception cref="OverflowException">The batch exceeds the supported scratch size.</exception>
    /// <exception cref="InvalidOperationException">The native batch read fails without an error message.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MultiGet<T, TDeserializer>(ReadOnlySpan<byte> keys, int keyLength, Span<T> values,
        TDeserializer deserializer, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null, bool sortedInput = false)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (cf is not null && cf.Handle == 0)
            throw new ObjectDisposedException(nameof(cf), "The column family handle has been disposed.");
        if (values.Length > 2)
        {
            ReadBatchContext<T, TDeserializer>(keys, keyLength, values, deserializer, cf, readOptions, sortedInput);
            return;
        }
        if (values.Length == 1)
        {
            ReadSingleMultiGet<T, TDeserializer>(keys, keyLength, values, deserializer, cf, readOptions);
            return;
        }
        ReadSmallMultiGet<T, TDeserializer>(keys, keyLength, values, deserializer, cf, readOptions, sortedInput);
    }

    [SkipLocalsInit]
    private void ReadSmallMultiGet<T, TDeserializer>(ReadOnlySpan<byte> keys, int keyLength, Span<T> values,
        TDeserializer deserializer, IColumnFamilyHandle? cf, ReadOptions? readOptions, bool sortedInput)
        where TDeserializer : ISpanDeserializer<T>
    {
        if (deserializer is null) throw new ArgumentNullException(nameof(deserializer));
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if ((long)keys.Length != (long)values.Length * keyLength)
            throw new ArgumentException("The key buffer length must match the value count and fixed key length.", nameof(keys));
        values.Clear();
        if (values.IsEmpty) return;

        int count = values.Length;
        int completed = 0;
        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);

        if (rocksdb_readoptions_get_read_tier(RocksDbInterop.ReadOptions(optionsHandle)) != 0)
        {
            for (; completed < count; completed++)
                values[completed] = ReadDecodedValue<T, TDeserializer>(keys.Slice(completed * keyLength, keyLength), ref deserializer, cf, options);
            return;
        }
        int handleCount = checked(count * 2);
        Span<rocksdb_slice_t> keySlices = stackalloc rocksdb_slice_t[count];
        Span<nint> handles = stackalloc nint[handleCount];
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
                        continue;

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

    private T ReadSingleDecodedValue<T, TDeserializer>(ReadOnlySpan<byte> key, ref TDeserializer deserializer,
        IColumnFamilyHandle? cf, ReadOptions? readOptions) where TDeserializer : ISpanDeserializer<T>
    {
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);
        rocksdb_pinnableslice_t* pinned;
        sbyte* error = null;
        fixed (byte* keyData = key)
        {
            pinned = cf is null
                ? rocksdb_get_pinned(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                    (sbyte*)keyData, (nuint)key.Length, &error)
                : rocksdb_get_pinned_cf(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                    RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyData, (nuint)key.Length, &error);
        }
        RocksDbInterop.ThrowIfError(error);
        if (pinned is null) return default!;

        try
        {
            nuint length;
            sbyte* data = rocksdb_pinnableslice_value(pinned, &length);
            if (data is null) return default!;
            if (length > int.MaxValue)
                throw new NotSupportedException($"The value is {length} bytes; values over {int.MaxValue} bytes cannot be exposed as a span.");
            return deserializer.Deserialize(new ReadOnlySpan<byte>(data, (int)length));
        }
        finally
        {
            rocksdb_pinnableslice_destroy(pinned);
        }
    }

    private T ReadDecodedValue<T, TDeserializer>(ReadOnlySpan<byte> key, ref TDeserializer deserializer,
        IColumnFamilyHandle? cf, ReadOptions? readOptions) where TDeserializer : ISpanDeserializer<T>
    {
        if (cf is not null && cf.Handle == 0)
            throw new ObjectDisposedException(nameof(cf), "The column family handle has been disposed.");
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
