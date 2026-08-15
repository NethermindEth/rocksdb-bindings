// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class EnvOptions
{
    public nint Handle { get; protected set; }

    public EnvOptions()
    {
        Handle = (nint)rocksdb_envoptions_create();
    }

    ~EnvOptions()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_envoptions_destroy(RocksDbInterop.EnvOptions(Handle));
            Handle = nint.Zero;
        }
    }
}
