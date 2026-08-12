// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Dynamic;

namespace Nethermind.RocksDbBindings;

/*
Configure options for a RocksDb store.

Note on SetXXX() syntax:
   Why not syntax like new Options { XXX = ... } instead?  Two reasons
   1. The rocksdb C API does not support reading the options and so a class with properties is not an appropriate representation
   2. The API functions are named as imperatives and don't always begin with "set" so one like "OptimizeLevelStyleCompaction" wouldn't work right
*/
public unsafe abstract class OptionsHandle
{
    // The following exists only to retain a reference to those types which are used in-place by rocksdb
    // and not copied (or reference things that are used in-place).  The idea is to have managed references
    // track the behavior of the unmanaged reference as much as possible.  This prevents access violations
    // when the garbage collector cleans up the last managed reference
    internal dynamic References { get; } = new ExpandoObject();

    //Stores some path values for the RocksDb class
    internal string WalPath { get; set; }
    internal string LogPath { get; set; }

    public nint Handle { get; protected set; }

    public OptionsHandle()
        : this(createHandle: true)
    {
    }

    protected OptionsHandle(bool createHandle)
    {
        if (createHandle)
            Handle = (nint)RocksDbNative.rocksdb_options_create();
    }

    ~OptionsHandle()
    {
        if (Handle != nint.Zero)
        {
#if !NODESTROY
            DestroyHandle();
#endif
            Handle = nint.Zero;
        }
    }

    protected virtual void DestroyHandle()
    {
        RocksDbNative.rocksdb_options_destroy(RocksDbInterop.Options(Handle));
    }
}
