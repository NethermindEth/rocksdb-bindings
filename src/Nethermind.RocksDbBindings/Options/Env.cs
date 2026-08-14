// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class Env
{
    public nint Handle { get; protected set; }

    private Env(nint handle)
    {
        Handle = handle;
    }

    public static Env CreateDefaultEnv()
    {
        return new Env((nint)RocksDbNative.rocksdb_create_default_env());
    }

    public static Env CreateMemEnv()
    {
        return new Env((nint)RocksDbNative.rocksdb_create_mem_env());
    }

    public Env SetBackgroundThreads(int value)
    {
        RocksDbNative.rocksdb_env_set_background_threads(RocksDbInterop.Env(Handle), value);
        return this;
    }

    public Env SetHighPriorityBackgroundThreads(int value)
    {
        RocksDbNative.rocksdb_env_set_high_priority_background_threads(RocksDbInterop.Env(Handle), value);
        return this;
    }

    public void JoinAllThreads()
    {
        RocksDbNative.rocksdb_env_join_all_threads(RocksDbInterop.Env(Handle));
    }

    ~Env()
    {
        if (Handle != nint.Zero)
        {
            RocksDbNative.rocksdb_env_destroy(RocksDbInterop.Env(Handle));
            Handle = nint.Zero;
        }
    }
}
