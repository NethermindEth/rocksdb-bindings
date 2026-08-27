// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// Reuses one database lease and one read-options lease across a sequence of reads.
/// </summary>
/// <remarks>
/// Disposing the database or read options while this session is alive is safe: their native
/// handles stay open until the session is disposed. Do not dispose the session concurrently with
/// its own reads, and do not mutate its read options while reads are in progress.
/// </remarks>
public sealed class RocksDbReadSession : IDisposable
{
    private readonly RocksDb _database;
    private readonly nint _databaseHandle;
    private readonly nint _readOptionsHandle;
    private RocksDbHandle? _databaseLease;
    private SafeHandle? _readOptionsLease;
    private int _disposed;

    internal RocksDbReadSession(RocksDb database, ReadOptions readOptions)
    {
        RocksDbHandle? databaseLease = null;
        SafeHandle? readOptionsLease = null;
        var readOptionsLeaseAdded = false;

        try
        {
            databaseLease = database.AcquireLifetimeLease(out _databaseHandle);
            readOptionsLease = readOptions.SafeHandle;
            readOptionsLease.DangerousAddRef(ref readOptionsLeaseAdded);
            _readOptionsHandle = readOptionsLease.DangerousGetHandle();
        }
        catch
        {
            if (readOptionsLeaseAdded)
                readOptionsLease!.DangerousRelease();
            databaseLease?.DangerousRelease();
            throw;
        }

        _database = database;
        _databaseLease = databaseLease;
        _readOptionsLease = readOptionsLease;
    }

    ~RocksDbReadSession() => ReleaseLeases();

    /// <summary>Releases the database and read-options leases.</summary>
    public void Dispose()
    {
        ReleaseLeases();
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc cref="RocksDb.Get(ReadOnlySpan{byte}, IColumnFamilyHandle?, ReadOptions?)"/>
    public byte[]? Get(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null)
    {
        ThrowIfDisposed();
        byte[]? result = _database.GetLeased(key, cf, _databaseHandle, _readOptionsHandle);
        GC.KeepAlive(this);
        return result;
    }

    /// <inheritdoc cref="RocksDb.GetFixedSizeValue(ReadOnlySpan{byte}, Span{byte}, IColumnFamilyHandle?, ReadOptions?)"/>
    public bool GetFixedSizeValue(ReadOnlySpan<byte> key, Span<byte> fixedSizeValueOutput, IColumnFamilyHandle? cf = null)
    {
        if (!TryGetPinned(key, out var slice, cf))
            return false;

        try
        {
            if (slice.Value.Length != fixedSizeValueOutput.Length)
                return false;

            slice.Value.CopyTo(fixedSizeValueOutput);
            return true;
        }
        finally
        {
            slice.Dispose();
        }
    }

    /// <inheritdoc cref="RocksDb.TryGetPinned(ReadOnlySpan{byte}, out PinnedSlice, IColumnFamilyHandle?, ReadOptions?)"/>
    /// <remarks>
    /// The returned slice owns its native pin and may outlive this session. Dispose it exactly
    /// once when its value is no longer needed.
    /// </remarks>
    public bool TryGetPinned(scoped ReadOnlySpan<byte> key, out PinnedSlice slice, IColumnFamilyHandle? cf = null)
    {
        ThrowIfDisposed();
        bool result = _database.TryGetPinnedLeased(key, out slice, cf, _databaseHandle, _readOptionsHandle);
        GC.KeepAlive(this);
        return result;
    }

    /// <inheritdoc cref="RocksDb.GetSpan(ReadOnlySpan{byte}, IColumnFamilyHandle?, ReadOptions?)"/>
    public Span<byte> GetSpan(scoped ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null)
    {
        ThrowIfDisposed();
        Span<byte> result = _database.GetSpanLeased(key, cf, _databaseHandle, _readOptionsHandle);
        GC.KeepAlive(this);
        return result;
    }

    /// <inheritdoc cref="RocksDb.DangerousReleaseMemory(in ReadOnlySpan{byte})"/>
    public void DangerousReleaseMemory(in ReadOnlySpan<byte> span) => _database.DangerousReleaseMemory(span);

    /// <inheritdoc cref="RocksDb.Get(ReadOnlySpan{byte}, Span{byte}, IColumnFamilyHandle?, ReadOptions?)"/>
    public int Get(ReadOnlySpan<byte> key, Span<byte> destination, IColumnFamilyHandle? cf = null)
    {
        if (!TryGetPinned(key, out var slice, cf))
            return -1;

        try
        {
            ReadOnlySpan<byte> value = slice.Value;
            if (value.Length > destination.Length)
                throw new ArgumentException($"The value is {value.Length} bytes but the destination holds {destination.Length}.", nameof(destination));

            value.CopyTo(destination);
            return value.Length;
        }
        finally
        {
            slice.Dispose();
        }
    }

    /// <inheritdoc cref="RocksDb.HasKey(ReadOnlySpan{byte}, IColumnFamilyHandle?, ReadOptions?)"/>
    public bool HasKey(ReadOnlySpan<byte> key, IColumnFamilyHandle? cf = null)
    {
        ThrowIfDisposed();
        bool result = _database.HasKeyLeased(key, cf, _databaseHandle, _readOptionsHandle);
        GC.KeepAlive(this);
        return result;
    }

    /// <inheritdoc cref="RocksDb.Get{T}(ReadOnlySpan{byte}, ISpanDeserializer{T}, IColumnFamilyHandle?, ReadOptions?)"/>
    public T? Get<T>(ReadOnlySpan<byte> key, ISpanDeserializer<T> deserializer, IColumnFamilyHandle? cf = null)
    {
        if (!TryGetPinned(key, out var slice, cf))
            return default;

        try
        {
            return deserializer.Deserialize(slice.Value);
        }
        finally
        {
            slice.Dispose();
        }
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(Volatile.Read(ref _disposed) != 0, this);

    private void ReleaseLeases()
    {
        if (Interlocked.Exchange(ref _disposed, 1) != 0)
            return;

        Interlocked.Exchange(ref _readOptionsLease, null)?.DangerousRelease();
        Interlocked.Exchange(ref _databaseLease, null)?.DangerousRelease();
    }
}
