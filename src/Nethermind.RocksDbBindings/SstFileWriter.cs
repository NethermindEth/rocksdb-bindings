// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public unsafe class SstFileWriter : IDisposable
{
    public nint Handle { get; protected set; }

    // Held so the garbage collector cannot finalize them while rocksdb still points at them.
    private EnvOptions EnvOptions { get; }
    private ColumnFamilyOptions IoOptions { get; }

    public SstFileWriter(EnvOptions? envOptions = null, ColumnFamilyOptions? ioOptions = null)
    {
        EnvOptions = envOptions ?? new EnvOptions();
        IoOptions = ioOptions ?? new ColumnFamilyOptions();
        Handle = (nint)rocksdb_sstfilewriter_create(RocksDbInterop.EnvOptions(EnvOptions.Handle), RocksDbInterop.Options(IoOptions.Handle));
    }

    public void Dispose()
    {
        if (Handle != nint.Zero)
        {
            var handle = Handle;
            Handle = nint.Zero;
            rocksdb_sstfilewriter_destroy(RocksDbInterop.SstFileWriter(handle));
        }
    }

    public void Open(string filename)
    {
        using var nativeName = new TransientUtf8String(filename);
        sbyte* errptr = null;
        rocksdb_sstfilewriter_open(RocksDbInterop.SstFileWriter(Handle), (sbyte*)nativeName.Handle, &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Add(string key, string val) => Add(Encoding.UTF8.GetBytes(key), Encoding.UTF8.GetBytes(val));

    public void Add(byte[] key, byte[] val)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            sbyte* errptr = null;
            rocksdb_sstfilewriter_add(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.GetLongLength(0), (sbyte*)valuePtr, (nuint)val.GetLongLength(0), &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }

    public void Finish()
    {
        sbyte* errptr = null;
        rocksdb_sstfilewriter_finish(RocksDbInterop.SstFileWriter(Handle), &errptr);
        RocksDbInterop.ThrowIfError(errptr);
    }

    public void Put(byte[] key, byte[] val)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            sbyte* errptr = null;
            rocksdb_sstfilewriter_put(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)val.Length, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }

    public void Merge(byte[] key, byte[] val)
    {
        fixed (byte* keyPtr = key)
        fixed (byte* valuePtr = val)
        {
            sbyte* errptr = null;
            rocksdb_sstfilewriter_merge(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.Length, (sbyte*)valuePtr, (nuint)val.Length, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }

    public void Delete(byte[] key)
    {
        fixed (byte* keyPtr = key)
        {
            sbyte* errptr = null;
            rocksdb_sstfilewriter_delete(RocksDbInterop.SstFileWriter(Handle), (sbyte*)keyPtr, (nuint)key.Length, &errptr);
            RocksDbInterop.ThrowIfError(errptr);
        }
    }
}
