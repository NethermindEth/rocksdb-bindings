// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class FlushOptions : OptionsHandle
{
    public FlushOptions() : base(createHandle: false)
    {
        Handle = (nint)rocksdb_flushoptions_create();
    }

    public FlushOptions SetWaitForFlush(bool waitForFlush)
    {
        rocksdb_flushoptions_set_wait(RocksDbInterop.FlushOptions(Handle), RocksDbInterop.Bool(waitForFlush));
        return this;
    }

    protected override void DestroyHandle() => rocksdb_flushoptions_destroy(RocksDbInterop.FlushOptions(Handle));
}
