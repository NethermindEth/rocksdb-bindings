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
    // RocksDB uses these in place rather than copying them, so the managed wrappers are held here to
    // keep the garbage collector from running their finalizers while RocksDB still points at them.
    internal BlockBasedTableOptions? BlockBasedTableFactory { get; set; }
    internal SliceTransform? PrefixExtractor { get; set; }

    // Not native options: the paths are kept here so an opened database can report where its
    // write-ahead log and its LOG file went.
    internal string? WalPath { get; set; }
    internal string? LogPath { get; set; }

    protected OptionsHandle() => Handle = (nint)rocksdb_options_create();

    protected override void DestroyHandle() => rocksdb_options_destroy(RocksDbInterop.Options(Handle));
}
