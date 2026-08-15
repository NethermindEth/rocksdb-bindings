// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class Env
{
    public nint Handle { get; protected set; }

    private Env(nint handle)
    {
        Handle = handle;
    }

    public static Env CreateDefaultEnv() => new Env((nint)rocksdb_create_default_env());

    public static Env CreateMemEnv() => new Env((nint)rocksdb_create_mem_env());

    public Env SetBackgroundThreads(int value)
    {
        rocksdb_env_set_background_threads(RocksDbInterop.Env(Handle), value);
        return this;
    }

    public Env SetHighPriorityBackgroundThreads(int value)
    {
        rocksdb_env_set_high_priority_background_threads(RocksDbInterop.Env(Handle), value);
        return this;
    }

    public void JoinAllThreads() => rocksdb_env_join_all_threads(RocksDbInterop.Env(Handle));

    ~Env()
    {
        if (Handle != nint.Zero)
        {
            rocksdb_env_destroy(RocksDbInterop.Env(Handle));
            Handle = nint.Zero;
        }
    }
}
