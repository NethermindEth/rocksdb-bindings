// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class EnvOptions
{
    public nint Handle { get; protected set; }

    public EnvOptions()
    {
        Handle = (nint)RocksDbNative.rocksdb_envoptions_create();
    }

    ~EnvOptions()
    {
        if (Handle != nint.Zero)
        {
#if !NODESTROY
            RocksDbNative.rocksdb_envoptions_destroy(RocksDbInterop.EnvOptions(Handle));
#endif
            Handle = nint.Zero;
        }
    }
}
