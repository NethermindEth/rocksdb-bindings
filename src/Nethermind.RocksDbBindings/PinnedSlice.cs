// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// A value read without copying: <see cref="Value"/> exposes bytes that stay in RocksDB-owned
/// memory, typically the block cache or a memtable.
/// </summary>
/// <remarks>
/// The span is valid until <see cref="Dispose"/>, which releases the pin so RocksDB can reclaim
/// the memory. While pinned, the slice holds a reference on the underlying block or memtable, so
/// keep it short-lived; the ref struct constraint enforces that it cannot be stored on the heap.
/// The native handle is released with an unconditional delete, and copies of the slice alias the
/// same handle, so release it exactly once: either dispose a single copy, or detach and pass the
/// handle to <see cref="DangerousDestroy"/>. Obtained from
/// <see cref="RocksDb.TryGetPinned(ReadOnlySpan{byte}, out PinnedSlice, IColumnFamilyHandle?, ReadOptions?)"/>.
/// </remarks>
public unsafe ref struct PinnedSlice
{
    private nint handle;
    private readonly nint valuePtr;
    private readonly int valueLength;

    internal PinnedSlice(nint handle, nint valuePtr, int valueLength)
    {
        this.handle = handle;
        this.valuePtr = valuePtr;
        this.valueLength = valueLength;
    }

    /// <summary>
    /// Whether the read found a value. False only for a defaulted instance; detaching or disposing
    /// does not reset it, so it does not indicate that <see cref="Value"/> is still safe to read.
    /// </summary>
    public readonly bool HasValue => valuePtr != nint.Zero;

    /// <summary>
    /// The value bytes, empty when <see cref="HasValue"/> is false. Valid until the native handle
    /// is released through <see cref="Dispose"/> or <see cref="DangerousDestroy"/>.
    /// </summary>
    public readonly ReadOnlySpan<byte> Value => new((void*)valuePtr, valueLength);

    /// <summary>
    /// Transfers ownership of the native handle to the caller, who must eventually pass it to
    /// <see cref="DangerousDestroy"/>. The value bytes stay valid until then; disposing this
    /// instance afterwards is a no-op.
    /// </summary>
    public nint DangerousDetach()
    {
        var detached = handle;
        handle = nint.Zero;
        return detached;
    }

    /// <summary>Destroys a handle obtained from <see cref="DangerousDetach"/>. Ignores <c>0</c>.</summary>
    public static void DangerousDestroy(nint handle)
    {
        if (handle != nint.Zero)
            rocksdb_pinnableslice_destroy(RocksDbInterop.PinnableSlice(handle));
    }

    /// <summary>
    /// Releases the native handle. Safe to call repeatedly on the same copy, but copies do not
    /// observe each other's disposal — release through exactly one copy.
    /// </summary>
    public void Dispose()
    {
        DangerousDestroy(handle);
        handle = nint.Zero;
    }
}
