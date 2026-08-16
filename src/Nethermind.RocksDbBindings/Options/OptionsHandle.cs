// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// Owns a native options struct of some kind. Derived types decide which one they create and
/// destroy, so that no two unrelated option structs become assignable to each other.
/// </summary>
public abstract class NativeOptions : IDisposable
{
    public nint Handle { get; protected set; }

    ~NativeOptions() => ReleaseHandle();

    /// <summary>Destroys the native options deterministically; the finalizer is only a backstop.</summary>
    /// <remarks>
    /// Dispose only after every native call using these options has returned. See the derived
    /// types for their specific lifetime rules.
    /// </remarks>
    public void Dispose()
    {
        ReleaseHandle();
        GC.SuppressFinalize(this);
    }

    private void ReleaseHandle()
    {
        if (Handle != nint.Zero)
        {
            DestroyHandle();
            Handle = nint.Zero;
        }
    }

    protected abstract void DestroyHandle();
}

/*
Configure options for a RocksDb store.

Note on SetXXX() syntax:
   Why not syntax like new Options { XXX = ... } instead?  Two reasons
   1. The rocksdb C API does not support reading the options and so a class with properties is not an appropriate representation
   2. The API functions are named as imperatives and don't always begin with "set" so one like "OptimizeLevelStyleCompaction" wouldn't work right
*/
/// <summary>A <c>rocksdb_options_t</c>: the options a database or column family is opened with.</summary>
public unsafe abstract class OptionsHandle : NativeOptions
{
    // RocksDB uses these in place rather than copying them, so the managed wrappers are held here to
    // keep the garbage collector from running their finalizers while rocksdb still points at them.
    internal BlockBasedTableOptions? BlockBasedTableFactory { get; set; }
    internal SliceTransform? PrefixExtractor { get; set; }

    //Stores some path values for the RocksDb class
    internal string? WalPath { get; set; }
    internal string? LogPath { get; set; }

    protected OptionsHandle() => Handle = (nint)rocksdb_options_create();

    protected override void DestroyHandle() => rocksdb_options_destroy(RocksDbInterop.Options(Handle));
}
