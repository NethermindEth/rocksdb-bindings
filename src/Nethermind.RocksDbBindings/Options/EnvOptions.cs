// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class EnvOptions
{
    public IntPtr Handle { get; protected set; }

    public EnvOptions()
    {
        Handle = (IntPtr)RocksDbNative.rocksdb_envoptions_create();
    }

    ~EnvOptions()
    {
        if (Handle != IntPtr.Zero)
        {
#if !NODESTROY
            RocksDbNative.rocksdb_envoptions_destroy(RocksDbInterop.EnvOptions(Handle));
#endif
            Handle = IntPtr.Zero;
        }
    }
}
