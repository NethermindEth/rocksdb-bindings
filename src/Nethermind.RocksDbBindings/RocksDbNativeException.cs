// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices.Marshalling;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public class RocksDbNativeException : RocksDbException
{
    public unsafe RocksDbNativeException(nint errptr)
        : base(Utf8StringMarshaller.ConvertToManaged((byte*)errptr)!)
    {
        rocksdb_free((void*)errptr);
    }
}
