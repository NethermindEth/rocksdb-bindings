// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices.Marshalling;

namespace Nethermind.RocksDbBindings;

public class RocksDbNativeException : RocksDbException
{
    public unsafe RocksDbNativeException(nint errptr)
        : base(Utf8StringMarshaller.ConvertToManaged((byte*)errptr)!)
    {
        RocksDbNative.rocksdb_free((void*)errptr);
    }
}
