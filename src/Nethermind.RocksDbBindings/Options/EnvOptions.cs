// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe class EnvOptions
{
    public nint Handle { get; }

    public EnvOptions() => Handle = (nint)rocksdb_envoptions_create();

    // No Dispose and so no second actor: the finalizer runs at most once, and nothing can read
    // the handle afterwards.
    ~EnvOptions() => rocksdb_envoptions_destroy(RocksDbInterop.EnvOptions(Handle));
}
