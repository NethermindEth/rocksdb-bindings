// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

public unsafe sealed partial class RocksDb
{
    private const byte NativeGetError = 0;

    /// <summary>
    /// Reads the value for <paramref name="key"/> into <paramref name="destination"/> and reports how the read ended,
    /// without an exception for a missing key, a value larger than the destination, or a read the read tier stops.
    /// </summary>
    /// <remarks>
    /// One native call with no allocation. Use it to probe the block cache: with read options at the block-cache tier
    /// and an empty <paramref name="destination"/>, the result tells whether a read is served from memory
    /// (<see cref="GetResult.Found"/> or <see cref="GetResult.NotFound"/>) or needs disk (<see cref="GetResult.Incomplete"/>).
    /// </remarks>
    /// <returns>
    /// How the read ended. On <see cref="GetResult.Found"/>, <paramref name="valueLength"/> is the value length, and
    /// the value is copied only when it is not longer than <paramref name="destination"/>. Otherwise it is -1.
    /// </returns>
    /// <exception cref="RocksDbException">The read failed for another reason, such as corruption or an IO error.</exception>
    /// <exception cref="NotSupportedException">The value is larger than <see cref="int.MaxValue"/> bytes.</exception>
    public GetResult TryGet(ReadOnlySpan<byte> key, Span<byte> destination, out int valueLength,
        IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        using var lease = Lease();
        var options = readOptions ?? DefaultReadOptions;
        using var optionsLease = options.Lease(out nint optionsHandle);

        nuint length;
        byte result;
        sbyte* errptr = null;
        fixed (byte* keyPtr = key)
        fixed (byte* destinationPtr = destination)
        {
            result = NativeGetIntoBufferStatus(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                cf is null ? null : RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length,
                (sbyte*)destinationPtr, (nuint)destination.Length, &length, &errptr);
        }

        if (result == NativeGetError)
        {
            RocksDbInterop.ThrowIfError(errptr);
            throw new RocksDbException("The read failed without an error message.");
        }

        var getResult = (GetResult)result;
        if (getResult != GetResult.Found)
        {
            valueLength = -1;
            return getResult;
        }

        if (length > int.MaxValue)
            throw new NotSupportedException($"The value is {length} bytes; values over {int.MaxValue} bytes cannot be copied into a span.");

        valueLength = (int)length;
        return getResult;
    }

    [DllImport("rocksdb", EntryPoint = "rocksdb_net_get_into_buffer_status", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern byte NativeGetIntoBufferStatus(rocksdb_t* db, rocksdb_readoptions_t* options,
        rocksdb_column_family_handle_t* family, sbyte* key, nuint keyLength, sbyte* buffer, nuint bufferSize,
        nuint* valueLength, sbyte** error);
}
