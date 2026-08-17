// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// Creates a compaction filter from callbacks that RocksDB invokes directly. Each one must be a
/// static method marked with <c>[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]</c>,
/// and <paramref name="state"/> is passed back to every call.
/// </summary>
/// <remarks>
/// Every callback must catch all managed exceptions. An exception cannot unwind through the
/// RocksDB frames that invoked it, so one that escapes terminates the process.
/// <para>
/// RocksDB stores a compaction filter as a non-owning pointer and never destroys it, so the
/// caller must keep <paramref name="state"/> and everything it reaches alive for as long as any
/// database using the filter is open.
/// </para>
/// </remarks>
public unsafe class CompactionFilter(
    void* state,
    delegate* unmanaged[Cdecl]<void*, void> destructor,
    delegate* unmanaged[Cdecl]<void*, int, sbyte*, nuint, sbyte*, nuint, sbyte**, nuint*, byte*, byte> filter,
    delegate* unmanaged[Cdecl]<void*, sbyte*> name)
{
    public nint Handle { get; } = (nint)rocksdb_compactionfilter_create(state, destructor, filter, name);
}
