// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

public unsafe sealed partial class RocksDb
{
    /// <summary>
    /// Reads the value for <paramref name="key"/> into <paramref name="destination"/> and reports how the read ended.
    /// No outcome of the read throws: a missing key, a value longer than the destination, a read the read tier stops
    /// and a failed read are all results.
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
    /// <exception cref="ObjectDisposedException">The database or <paramref name="readOptions"/> is disposed.</exception>
    public GetResult TryGet(ReadOnlySpan<byte> key, Span<byte> destination, out long valueLength,
        IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);

        nuint length;
        GetResult result;
        fixed (byte* keyPtr = key)
        fixed (byte* destinationPtr = destination)
        {
            result = (GetResult)NativeGetIntoBufferStatus(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                cf is null ? null : RocksDbInterop.ColumnFamily(cf.Handle), (sbyte*)keyPtr, (nuint)key.Length,
                (sbyte*)destinationPtr, (nuint)destination.Length, &length);
        }

        // RocksDB bounds a value length far below long.MaxValue.
        valueLength = result == GetResult.Found ? (long)length : -1;
        return result;
    }

    [DllImport("rocksdb", EntryPoint = "rocksdb_net_get_into_buffer_status", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern byte NativeGetIntoBufferStatus(rocksdb_t* db, rocksdb_readoptions_t* options,
        rocksdb_column_family_handle_t* family, sbyte* key, nuint keyLength, sbyte* buffer, nuint bufferSize,
        nuint* valueLength);
}
