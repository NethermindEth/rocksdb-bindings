// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <remarks>
/// The native flush reads these options in place, so dispose only after every flush using them
/// has returned.
/// </remarks>
public unsafe class FlushOptions : NativeOptions
{
    public FlushOptions() => Handle = (nint)rocksdb_flushoptions_create();

    public FlushOptions SetWaitForFlush(bool waitForFlush)
    {
        rocksdb_flushoptions_set_wait(RocksDbInterop.FlushOptions(Handle), RocksDbInterop.Bool(waitForFlush));
        return this;
    }

    protected override void DestroyHandle(nint handle) => rocksdb_flushoptions_destroy(RocksDbInterop.FlushOptions(handle));
}
