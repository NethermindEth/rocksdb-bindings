// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using Nethermind.RocksDbBindings.Native;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe partial class RocksDb
{
    /// <summary>Reads fixed-length keys asynchronously so that later reads find their blocks in the cache.</summary>
    /// <remarks>
    /// The values are never copied out: unlike <see cref="MultiGetAsync"/>, a prefetch allocates nothing per key.
    /// Copies the keys before returning. Read options must not be modified until completion, and they decide
    /// whether the blocks enter the cache. The block cache read tier does not stop it: RocksDB reads such a key from
    /// its file. Missing keys are not an error. RocksDB may complete synchronously when native coroutine reads are
    /// unavailable.
    /// </remarks>
    /// <returns>A task that completes when every read has finished, or faults with the first read error.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The key length is not positive.</exception>
    /// <exception cref="ArgumentException">The key buffer is not a whole number of keys.</exception>
    /// <exception cref="ObjectDisposedException">The database, read options or column family handle has been disposed.</exception>
    public Task PrefetchAsync(ReadOnlySpan<byte> keys, int keyLength,
        IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(keyLength);
        if (keys.Length % keyLength != 0)
            throw new ArgumentException("The key buffer length must be a multiple of the key length.", nameof(keys));
        if (cf is not null && cf.Handle == 0)
            throw new ObjectDisposedException(nameof(cf), "The column family handle has been disposed.");

        using HandleLease lease = Lease();
        ReadOptions options = readOptions ?? DefaultReadOptions;
        using HandleLease optionsLease = options.Lease(out nint optionsHandle);
        if (keys.IsEmpty)
            return Task.CompletedTask;

        rocksdb_net_read_t* request;
        sbyte* error = null;
        fixed (byte* keyData = keys)
        {
            request = NativePrefetchCreate(RocksDbInterop.Db(NativeHandle), RocksDbInterop.ReadOptions(optionsHandle),
                RocksDbInterop.ColumnFamily(cf?.Handle ?? 0), (nuint)(keys.Length / keyLength), (sbyte*)keyData,
                (nuint)keyLength, &error);
        }
        RocksDbInterop.ThrowIfError(error);
        AsyncPrefetchOperation operation = new(OwnAsyncRead((nint)request, options));
        operation.Start(multi: true);
        return operation.Task;
    }

    [DllImport("rocksdb", EntryPoint = "rocksdb_net_prefetch_create", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    private static extern rocksdb_net_read_t* NativePrefetchCreate(rocksdb_t* db, rocksdb_readoptions_t* options,
        rocksdb_column_family_handle_t* family, nuint count, sbyte* keys, nuint keyLength, sbyte** error);
}
