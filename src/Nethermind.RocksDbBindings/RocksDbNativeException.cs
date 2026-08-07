// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

public class RocksDbNativeException : RocksDbException
{
    public unsafe RocksDbNativeException(IntPtr errptr)
        : base(Marshal.PtrToStringAnsi(errptr))
    {
        RocksDbNative.rocksdb_free((void*)errptr);
    }
}
