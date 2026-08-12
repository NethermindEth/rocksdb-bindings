// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class SstFileWriter : IDisposable
{
    public nint Handle { get; protected set; }

    internal dynamic References { get; } = new ExpandoObject();

    public SstFileWriter(EnvOptions envOptions = null, ColumnFamilyOptions ioOptions = null)
    {
        if (envOptions == null)
            envOptions = new EnvOptions();
        var opts = ioOptions ?? new ColumnFamilyOptions();
        References.EnvOptions = envOptions;
        References.IoOptions = ioOptions;
        Handle = (nint)RocksDbNative.rocksdb_sstfilewriter_create(RocksDbInterop.EnvOptions(envOptions.Handle), RocksDbInterop.Options(opts.Handle));
    }

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            var handle = Handle;
            Handle = nint.Zero;
            RocksDbNative.rocksdb_sstfilewriter_destroy(RocksDbInterop.SstFileWriter(handle));
        }
    }

    public void Open(string filename)
    {
        using var nativeName = new RocksSafePath(filename);
        sbyte* errptr = null;
        RocksDbNative.rocksdb_sstfilewriter_open(RocksDbInterop.SstFileWriter(Handle), (sbyte*)nativeName.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Add(string key, string val)
    {
        Add(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(val));
    }

    public void Add(byte[] key, byte[] val)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            sbyte* errptr = null;
            RocksDbNative.rocksdb_sstfilewriter_add(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.GetLongLength(0), (sbyte*)valuePtr, (nuint)val.GetLongLength(0), &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }

    public void Finish()
    {
        sbyte* errptr = null;
        RocksDbNative.rocksdb_sstfilewriter_finish(RocksDbInterop.SstFileWriter(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Put(byte[] key, byte[] val)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            sbyte* errptr = null;
            RocksDbNative.rocksdb_sstfilewriter_put(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)val.Length, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }

    public void Merge(byte[] key, byte[] val)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            sbyte* errptr = null;
            RocksDbNative.rocksdb_sstfilewriter_merge(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)val.Length, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }

    public void Delete(byte[] key)
    {
        fixed (byte* keyPtr = key)
        {
            sbyte* errptr = null;
            RocksDbNative.rocksdb_sstfilewriter_delete(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.Length, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }
}
