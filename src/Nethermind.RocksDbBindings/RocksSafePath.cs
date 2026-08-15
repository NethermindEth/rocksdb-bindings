// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices.Marshalling;

namespace Nethermind.RocksDbBindings;

public class RocksSafePath : IDisposable
{
    public nint Handle { get; private set; }

    public unsafe RocksSafePath(string path)
    {
        ArgumentNullException.ThrowIfNull(path);

        Handle = (nint)Utf8StringMarshaller.ConvertToUnmanaged(path);
    }

    public void Dispose()
    {
        //Disabled disposing, as it seems RocksDB actually save some of these strings without copying
        //This should be tied to the lifetime of the RocksDB object
        //unsafe
        //{
        //Utf8StringMarshaller.Free((byte*)Handle);
        //Handle = nint.Zero;
        //}
    }
}
