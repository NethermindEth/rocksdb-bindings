// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Nethermind.RocksDbBindings;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void PutDelegate(IntPtr state, IntPtr key, UIntPtr keyLength, IntPtr value, UIntPtr valueLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void DeletedDelegate(IntPtr state, IntPtr key, UIntPtr keyLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void PutCfDelegate(IntPtr state, uint cfid, IntPtr key, UIntPtr keyLength, IntPtr value, UIntPtr valueLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void DeletedCfDelegate(IntPtr state, uint cfid, IntPtr key, UIntPtr keyLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void MergeCfDelegate(IntPtr state, uint cfid, IntPtr key, UIntPtr keyLength, IntPtr value, UIntPtr valueLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate UIntPtr GetTsSizeDelegate(IntPtr state, uint cfid);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void DestructorDelegate(IntPtr state);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void Destructor_Delegate(IntPtr state);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr NameDelegate(IntPtr state);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate byte FilterDelegate(IntPtr state, int level, IntPtr key, UIntPtr keyLength, IntPtr existingValue, UIntPtr valueLength, IntPtr newValue, IntPtr newValueLength, out byte valueChanged);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr CreateCompactionFilterDelegate(IntPtr state, IntPtr context);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate int CompareDelegate(IntPtr state, IntPtr a, UIntPtr aLength, IntPtr b, UIntPtr bLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate int CompareTsDelegate(IntPtr state, IntPtr aTimestamp, UIntPtr aTimestampLength, IntPtr bTimestamp, UIntPtr bTimestampLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate int CompareWithoutTsDelegate(IntPtr state, IntPtr a, UIntPtr aLength, byte aHasTimestamp, IntPtr b, UIntPtr bLength, byte bHasTimestamp);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr FullMergeDelegate(IntPtr state, IntPtr key, UIntPtr keyLength, IntPtr existingValue, UIntPtr existingValueLength, IntPtr operandsList, IntPtr operandsListLength, int operandsCount, out byte success, out IntPtr newValueLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr PartialMergeDelegate(IntPtr state, IntPtr key, UIntPtr keyLength, IntPtr operandsList, IntPtr operandsListLength, int operandsCount, out byte success, out IntPtr newValueLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void DeleteValueDelegate(IntPtr state, IntPtr value, UIntPtr valueLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate IntPtr TransformDelegate(IntPtr state, IntPtr key, UIntPtr length, IntPtr destinationLength);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate byte InDomainDelegate(IntPtr state, IntPtr key, UIntPtr length);
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate byte InRangeDelegate(IntPtr state, IntPtr key, UIntPtr length);

internal static unsafe class RocksDbInterop
{
    public static byte Bool(bool value) => value ? (byte)1 : (byte)0;

    public static void ThrowIfError(sbyte* errptr)
    {
        if (errptr != null)
            throw new RocksDbNativeException((IntPtr)errptr);
    }

    public static string PtrToStringAndFree(sbyte* value, nuint length, Encoding encoding = null)
    {
        if (value == null)
            return null;

        encoding ??= Encoding.UTF8;
        try
        {
            return encoding.GetString((byte*)value, checked((int)length));
        }
        finally
        {
            RocksDbNative.rocksdb_free(value);
        }
    }

    public static string NullTerminatedStringAndFree(sbyte* value)
    {
        if (value == null)
            return null;

        try
        {
            return Marshal.PtrToStringAnsi((IntPtr)value);
        }
        finally
        {
            RocksDbNative.rocksdb_free(value);
        }
    }

    public static byte[] BytesAndFree(sbyte* value, nuint length)
    {
        if (value == null)
            return null;

        try
        {
            var result = new byte[checked((int)length)];
            Marshal.Copy((IntPtr)value, result, 0, result.Length);
            return result;
        }
        finally
        {
            RocksDbNative.rocksdb_free(value);
        }
    }

    public static byte[] Bytes(IntPtr value, nuint length)
    {
        if (value == IntPtr.Zero)
            return null;

        var result = new byte[checked((int)length)];
        Marshal.Copy(value, result, 0, result.Length);
        return result;
    }

    public static T Deserialize<T>(IntPtr value, nuint length, Func<Stream, T> deserializer)
    {
        if (value == IntPtr.Zero)
            return default;

        using var stream = new UnmanagedMemoryStream((byte*)value, checked((long)length));
        return deserializer(stream);
    }

#if !NETSTANDARD2_0
    public static T Deserialize<T>(IntPtr value, nuint length, ISpanDeserializer<T> deserializer)
        => value == IntPtr.Zero ? default : deserializer.Deserialize(new ReadOnlySpan<byte>((void*)value, checked((int)length)));
#endif

    public static rocksdb_t* Db(IntPtr value) => (rocksdb_t*)value;
    public static rocksdb_backup_engine_t* BackupEngine(IntPtr value) => (rocksdb_backup_engine_t*)value;
    public static rocksdb_block_based_table_options_t* BlockBasedTableOptions(IntPtr value) => (rocksdb_block_based_table_options_t*)value;
    public static rocksdb_cache_t* Cache(IntPtr value) => (rocksdb_cache_t*)value;
    public static rocksdb_checkpoint_t* Checkpoint(IntPtr value) => (rocksdb_checkpoint_t*)value;
    public static rocksdb_column_family_handle_t* ColumnFamily(IntPtr value) => (rocksdb_column_family_handle_t*)value;
    public static rocksdb_compactionfilter_t* CompactionFilter(IntPtr value) => (rocksdb_compactionfilter_t*)value;
    public static rocksdb_compactionfilterfactory_t* CompactionFilterFactory(IntPtr value) => (rocksdb_compactionfilterfactory_t*)value;
    public static rocksdb_comparator_t* Comparator(IntPtr value) => (rocksdb_comparator_t*)value;
    public static rocksdb_cuckoo_table_options_t* CuckooTableOptions(IntPtr value) => (rocksdb_cuckoo_table_options_t*)value;
    public static rocksdb_env_t* Env(IntPtr value) => (rocksdb_env_t*)value;
    public static rocksdb_envoptions_t* EnvOptions(IntPtr value) => (rocksdb_envoptions_t*)value;
    public static rocksdb_filterpolicy_t* FilterPolicy(IntPtr value) => (rocksdb_filterpolicy_t*)value;
    public static rocksdb_flushoptions_t* FlushOptions(IntPtr value) => (rocksdb_flushoptions_t*)value;
    public static rocksdb_fifo_compaction_options_t* FifoCompactionOptions(IntPtr value) => (rocksdb_fifo_compaction_options_t*)value;
    public static rocksdb_ingestexternalfileoptions_t* IngestExternalFileOptions(IntPtr value) => (rocksdb_ingestexternalfileoptions_t*)value;
    public static rocksdb_iterator_t* Iterator(IntPtr value) => (rocksdb_iterator_t*)value;
    public static rocksdb_livefiles_t* LiveFiles(IntPtr value) => (rocksdb_livefiles_t*)value;
    public static rocksdb_logger_t* Logger(IntPtr value) => (rocksdb_logger_t*)value;
    public static rocksdb_mergeoperator_t* MergeOperator(IntPtr value) => (rocksdb_mergeoperator_t*)value;
    public static rocksdb_options_t* Options(IntPtr value) => (rocksdb_options_t*)value;
    public static rocksdb_readoptions_t* ReadOptions(IntPtr value) => (rocksdb_readoptions_t*)value;
    public static rocksdb_slicetransform_t* SliceTransform(IntPtr value) => (rocksdb_slicetransform_t*)value;
    public static rocksdb_snapshot_t* Snapshot(IntPtr value) => (rocksdb_snapshot_t*)value;
    public static rocksdb_sstfilewriter_t* SstFileWriter(IntPtr value) => (rocksdb_sstfilewriter_t*)value;
    public static rocksdb_wal_iterator_t* WalIterator(IntPtr value) => (rocksdb_wal_iterator_t*)value;
    public static rocksdb_writebatch_t* WriteBatch(IntPtr value) => (rocksdb_writebatch_t*)value;
    public static rocksdb_writebatch_wi_t* WriteBatchWithIndex(IntPtr value) => (rocksdb_writebatch_wi_t*)value;
    public static rocksdb_writeoptions_t* WriteOptions(IntPtr value) => (rocksdb_writeoptions_t*)value;
    public static rocksdb_universal_compaction_options_t* UniversalCompactionOptions(IntPtr value) => (rocksdb_universal_compaction_options_t*)value;
}

internal sealed unsafe class NativeUtf8StringArray : IDisposable
{
    private readonly RocksSafePath[] values;
    private readonly IntPtr buffer;

    public NativeUtf8StringArray(string[] strings)
    {
        if (strings is null)
        {
            Pointer = null;
            values = Array.Empty<RocksSafePath>();
            return;
        }

        values = new RocksSafePath[strings.Length];
        buffer = Marshal.AllocHGlobal(IntPtr.Size * strings.Length);
        for (int i = 0; i < strings.Length; i++)
        {
            values[i] = new RocksSafePath(strings[i]);
            Marshal.WriteIntPtr(buffer, i * IntPtr.Size, values[i].Handle);
        }

        Pointer = (sbyte**)buffer;
    }

    public sbyte** Pointer { get; }

    public void Dispose()
    {
        for (int i = 0; i < values.Length; i++)
            values[i]?.Dispose();

        if (buffer != IntPtr.Zero)
            Marshal.FreeHGlobal(buffer);
    }
}
