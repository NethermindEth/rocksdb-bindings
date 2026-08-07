// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;

namespace Nethermind.RocksDbBindings;

public unsafe class CompactionFilter
{
    public IntPtr Handle;
    private readonly NameDelegate getNameDelegate;
    private readonly FilterDelegate filterDelegate;
    private readonly DestructorDelegate destroyDelegate;

    public CompactionFilter(NameDelegate nameDelegate, 
                            FilterDelegate filterDelegate, 
                            DestructorDelegate destroyDelegate, 
                            IntPtr state)
    {
        this.getNameDelegate = nameDelegate;
        this.filterDelegate = filterDelegate;
        this.destroyDelegate = destroyDelegate;
        Handle = (IntPtr)RocksDbNative.rocksdb_compactionfilter_create(
            (void*)state,
            (delegate* unmanaged[Cdecl]<void*, void>)(void*)System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(destroyDelegate),
            (delegate* unmanaged[Cdecl]<void*, int, sbyte*, nuint, sbyte*, nuint, sbyte**, nuint*, byte*, byte>)(void*)System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(filterDelegate),
            (delegate* unmanaged[Cdecl]<void*, sbyte*>)(void*)System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(getNameDelegate));
    }        
}
