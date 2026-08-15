// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

internal static unsafe class RocksDbInterop
{
    public static byte Bool(bool value) => value ? (byte)1 : (byte)0;

    public static void ThrowIfError(sbyte* errptr)
    {
        if (errptr != null)
            throw new RocksDbNativeException((nint)errptr);
    }

    public static string? PtrToStringAndFree(sbyte* value, nuint length, Encoding? encoding = null)
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
            rocksdb_free(value);
        }
    }

    public static string? NullTerminatedStringAndFree(sbyte* value)
    {
        if (value == null)
            return null;

        try
        {
            return Utf8StringMarshaller.ConvertToManaged((byte*)value);
        }
        finally
        {
            rocksdb_free(value);
        }
    }

    public static byte[]? BytesAndFree(sbyte* value, nuint length)
    {
        if (value == null)
            return null;

        try
        {
            var result = new byte[checked((int)length)];
            new ReadOnlySpan<byte>(value, result.Length).CopyTo(result);
            return result;
        }
        finally
        {
            rocksdb_free(value);
        }
    }

    public static byte[]? Bytes(nint value, nuint length)
    {
        if (value == nint.Zero)
            return null;

        var result = new byte[checked((int)length)];
        new ReadOnlySpan<byte>((void*)value, result.Length).CopyTo(result);
        return result;
    }

    public static T? Deserialize<T>(nint value, nuint length, Func<Stream, T> deserializer)
    {
        if (value == nint.Zero)
            return default;

        using var stream = new UnmanagedMemoryStream((byte*)value, checked((long)length));
        return deserializer(stream);
    }

    public static T? Deserialize<T>(nint value, nuint length, ISpanDeserializer<T> deserializer)
        => value == nint.Zero ? default : deserializer.Deserialize(new ReadOnlySpan<byte>((void*)value, checked((int)length)));

    public static rocksdb_t* Db(nint value) => (rocksdb_t*)value;
    public static rocksdb_backup_engine_t* BackupEngine(nint value) => (rocksdb_backup_engine_t*)value;
    public static rocksdb_block_based_table_options_t* BlockBasedTableOptions(nint value) => (rocksdb_block_based_table_options_t*)value;
    public static rocksdb_cache_t* Cache(nint value) => (rocksdb_cache_t*)value;
    public static rocksdb_checkpoint_t* Checkpoint(nint value) => (rocksdb_checkpoint_t*)value;
    public static rocksdb_column_family_handle_t* ColumnFamily(nint value) => (rocksdb_column_family_handle_t*)value;
    public static rocksdb_compactionfilter_t* CompactionFilter(nint value) => (rocksdb_compactionfilter_t*)value;
    public static rocksdb_compactionfilterfactory_t* CompactionFilterFactory(nint value) => (rocksdb_compactionfilterfactory_t*)value;
    public static rocksdb_comparator_t* Comparator(nint value) => (rocksdb_comparator_t*)value;
    public static rocksdb_cuckoo_table_options_t* CuckooTableOptions(nint value) => (rocksdb_cuckoo_table_options_t*)value;
    public static rocksdb_env_t* Env(nint value) => (rocksdb_env_t*)value;
    public static rocksdb_envoptions_t* EnvOptions(nint value) => (rocksdb_envoptions_t*)value;
    public static rocksdb_filterpolicy_t* FilterPolicy(nint value) => (rocksdb_filterpolicy_t*)value;
    public static rocksdb_flushoptions_t* FlushOptions(nint value) => (rocksdb_flushoptions_t*)value;
    public static rocksdb_fifo_compaction_options_t* FifoCompactionOptions(nint value) => (rocksdb_fifo_compaction_options_t*)value;
    public static rocksdb_ingestexternalfileoptions_t* IngestExternalFileOptions(nint value) => (rocksdb_ingestexternalfileoptions_t*)value;
    public static rocksdb_iterator_t* Iterator(nint value) => (rocksdb_iterator_t*)value;
    public static rocksdb_livefiles_t* LiveFiles(nint value) => (rocksdb_livefiles_t*)value;
    public static rocksdb_logger_t* Logger(nint value) => (rocksdb_logger_t*)value;
    public static rocksdb_mergeoperator_t* MergeOperator(nint value) => (rocksdb_mergeoperator_t*)value;
    public static rocksdb_options_t* Options(nint value) => (rocksdb_options_t*)value;
    public static rocksdb_readoptions_t* ReadOptions(nint value) => (rocksdb_readoptions_t*)value;
    public static rocksdb_slicetransform_t* SliceTransform(nint value) => (rocksdb_slicetransform_t*)value;
    public static rocksdb_snapshot_t* Snapshot(nint value) => (rocksdb_snapshot_t*)value;
    public static rocksdb_sstfilewriter_t* SstFileWriter(nint value) => (rocksdb_sstfilewriter_t*)value;
    public static rocksdb_wal_iterator_t* WalIterator(nint value) => (rocksdb_wal_iterator_t*)value;
    public static rocksdb_writebatch_t* WriteBatch(nint value) => (rocksdb_writebatch_t*)value;
    public static rocksdb_writebatch_wi_t* WriteBatchWithIndex(nint value) => (rocksdb_writebatch_wi_t*)value;
    public static rocksdb_writeoptions_t* WriteOptions(nint value) => (rocksdb_writeoptions_t*)value;
    public static rocksdb_universal_compaction_options_t* UniversalCompactionOptions(nint value) => (rocksdb_universal_compaction_options_t*)value;
}

internal sealed unsafe class NativeUtf8StringArray : IDisposable
{
    private readonly RocksSafePath[] values;
    private readonly nint* buffer;

    public NativeUtf8StringArray(string[] strings)
    {
        if (strings is null)
        {
            Pointer = null;
            values = [];
            return;
        }

        values = new RocksSafePath[strings.Length];
        buffer = (nint*)NativeMemory.Alloc((nuint)strings.Length, (nuint)sizeof(nint));
        for (int i = 0; i < strings.Length; i++)
        {
            values[i] = new RocksSafePath(strings[i]);
            buffer[i] = values[i].Handle;
        }

        Pointer = (sbyte**)buffer;
    }

    public sbyte** Pointer { get; }

    public void Dispose()
    {
        for (int i = 0; i < values.Length; i++)
            values[i]?.Dispose();

        NativeMemory.Free(buffer);
    }
}
