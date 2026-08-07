// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class FlushOptions : OptionsHandle
{
    public FlushOptions()
        : base(createHandle: false)
    {
        Handle = (IntPtr)RocksDbNative.rocksdb_flushoptions_create();
    }

    public FlushOptions SetWaitForFlush(bool waitForFlush)
    {
        RocksDbNative.rocksdb_flushoptions_set_wait(RocksDbInterop.FlushOptions(Handle), RocksDbInterop.Bool(waitForFlush));
        return this;
    }

    protected override void DestroyHandle()
    {
        RocksDbNative.rocksdb_flushoptions_destroy(RocksDbInterop.FlushOptions(Handle));
    }
}
