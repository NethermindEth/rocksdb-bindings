// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// Owns a native options struct of some kind. Derived types decide which one they create and
/// destroy, so that no two unrelated option structs become assignable to each other.
/// </summary>
/// <remarks>
/// The native pointer is reachable only through <see cref="Lease"/>, which holds the handle open
/// for the duration of the call it is passed to. A call already under way therefore completes even
/// if another thread disposes these options, and one started afterwards throws instead of handing
/// RocksDB a freed pointer.
/// </remarks>
public abstract class NativeOptions : IDisposable
{
    private readonly SafeHandle _handle;

    private protected NativeOptions(SafeHandle handle)
    {
        _handle = handle;
    }

    /// <summary>Releases the native options once no leased call is still using them.</summary>
    /// <remarks>
    /// See the derived types for what else they keep alive, and for how long that has to outlive
    /// the options themselves.
    /// </remarks>
    public void Dispose()
    {
        _handle.Dispose();
        GC.SuppressFinalize(this);
    }

    /// <summary>Holds the options open and hands out the pointer to use for one native call.</summary>
    /// <exception cref="ObjectDisposedException">The options have been disposed.</exception>
    internal HandleLease Lease(out nint handle)
    {
        var lease = new HandleLease(_handle);

        handle = _handle.DangerousGetHandle();
        return lease;
    }

    /// <summary>The handle itself, for holding a reference that outlives a single call.</summary>
    internal SafeHandle SafeHandle => _handle;
}

/// <summary>A <c>rocksdb_options_t</c>: the options a database or column family is opened with.</summary>
/// <remarks>
/// Configured by calling methods rather than by assigning properties, because not every method is
/// a simple assignment: <see cref="Options{T}.OptimizeLevelStyleCompaction"/> and its like set
/// several options at once. Options are also close to write-only here — the C API can read most
/// of them back, but this type wraps almost none of those getters, and the ones RocksDB takes by
/// reference (the comparator, the environment, the table and compaction factories) cannot be read
/// back at all.
/// </remarks>
public unsafe abstract class OptionsHandle : NativeOptions
{
    // Not native options: the paths are kept here so an opened database can report where its
    // write-ahead log and its LOG file went.
    internal string? WalPath { get; set; }
    internal string? LogPath { get; set; }

    private protected readonly OptionsSafeHandle NativeHandle;

    private protected OptionsHandle() : this(new OptionsSafeHandle((nint)rocksdb_options_create())) { }

    private OptionsHandle(OptionsSafeHandle handle) : base(handle) => NativeHandle = handle;
}
