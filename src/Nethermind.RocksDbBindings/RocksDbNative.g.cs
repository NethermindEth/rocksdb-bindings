// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: DisableRuntimeMarshalling]

namespace Nethermind.RocksDbBindings.Native;

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_status_ptr_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_backup_engine_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_backup_engine_info_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_backup_engine_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_create_backup_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_restore_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_memory_allocator_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_lru_cache_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_hyper_clock_cache_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_cache_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_write_buffer_manager_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_sst_file_manager_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionfilter_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionfiltercontext_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionfilterfactory_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_walfilter_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_file_checksum_gen_factory_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_sst_partitioner_factory_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_table_properties_collector_factory_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_comparator_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_dbpath_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_env_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_fifo_compaction_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_filelock_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_filterpolicy_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_flushoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_flushwaloptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_iterator_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_logger_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_mergeoperator_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compaction_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_block_based_table_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_block_cache_trace_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_block_cache_trace_writer_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_cuckoo_table_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_randomfile_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_readoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_seqfile_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_slicetransform_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_snapshot_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_writablefile_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_writebatch_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_writebatch_wi_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_writeoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_universal_compaction_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_livefile_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_livefiles_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_column_family_handle_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_column_family_metadata_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_import_column_family_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_export_import_files_metadata_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_level_metadata_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_sst_file_metadata_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_column_family_metadata_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_envoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_ingestexternalfileoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_trace_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_trace_reader_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_replay_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_replayer_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_size_approximation_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_livefiles_storage_info_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_livefiles_storage_info_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_sstfilewriter_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_ratelimiter_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_perfcontext_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_pinnableslice_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_transactiondb_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_transactiondb_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_transaction_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_optimistictransactiondb_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_optimistictransactiondb_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_optimistictransactiondb_lock_buckets_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_optimistictransaction_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_transaction_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_checkpoint_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_wal_iterator_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_wal_readoptions_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_wal_file_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_wal_files_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_memory_consumers_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_memory_usage_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_statistics_histogram_data_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_wait_for_compact_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public unsafe partial struct rocksdb_slice_t
{
    [NativeTypeName("const char *")]
    public sbyte* data;

    [NativeTypeName("size_t")]
    public nuint size;
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_flushjobinfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionjobinfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_subcompactionjobinfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_externalfileingestioninfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_table_properties_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compaction_job_stats_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compaction_file_info_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_blob_file_addition_info_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_blob_file_garbage_info_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_eventlistener_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_writestallinfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_writestallcondition_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_memtableinfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionservice_scheduleresponse_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionservice_jobinfo_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compactionservice_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_compaction_service_options_override_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_open_and_compact_options_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public partial struct rocksdb_pinnable_handle_t
{
}

[GeneratedCode("ClangSharp", "21.1.8.4")]
public static unsafe partial class RocksDbNative
{
    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_with_ttl([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int ttl, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_for_read_only([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("unsigned char")] byte error_if_wal_file_exists, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_as_secondary([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* secondary_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_backup_engine_t* rocksdb_backup_engine_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_backup_engine_t* rocksdb_backup_engine_open_opts([NativeTypeName("const rocksdb_backup_engine_options_t *")] rocksdb_backup_engine_options_t* options, rocksdb_env_t* env, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_create_backup_options_t* rocksdb_create_backup_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_destroy(rocksdb_create_backup_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_set_progress_callback(rocksdb_create_backup_options_t* options, void* state, [NativeTypeName("rocksdb_create_backup_options_progress_cb")] delegate* unmanaged[Cdecl]<void*, void> callback);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_set_exclude_files_callback(rocksdb_create_backup_options_t* options, void* state, [NativeTypeName("rocksdb_create_backup_options_exclude_files_cb")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, byte> callback);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_create_new_backup(rocksdb_backup_engine_t* be, rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_create_new_backup_flush(rocksdb_backup_engine_t* be, rocksdb_t* db, [NativeTypeName("unsigned char")] byte flush_before_backup, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_create_new_backup_with_options(rocksdb_backup_engine_t* be, rocksdb_t* db, [NativeTypeName("const rocksdb_create_backup_options_t *")] rocksdb_create_backup_options_t* options, [NativeTypeName("uint32_t *")] uint* backup_id, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_create_new_backup_with_metadata(rocksdb_backup_engine_t* be, rocksdb_t* db, [NativeTypeName("const rocksdb_create_backup_options_t *")] rocksdb_create_backup_options_t* options, [NativeTypeName("const char *")] sbyte* app_metadata, [NativeTypeName("size_t")] nuint app_metadata_len, [NativeTypeName("uint32_t *")] uint* backup_id, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_purge_old_backups(rocksdb_backup_engine_t* be, [NativeTypeName("uint32_t")] uint num_backups_to_keep, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_restore_options_t* rocksdb_restore_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_restore_options_destroy(rocksdb_restore_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_verify_backup(rocksdb_backup_engine_t* be, [NativeTypeName("uint32_t")] uint backup_id, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_restore_db_from_latest_backup(rocksdb_backup_engine_t* be, [NativeTypeName("const char *")] sbyte* db_dir, [NativeTypeName("const char *")] sbyte* wal_dir, [NativeTypeName("const rocksdb_restore_options_t *")] rocksdb_restore_options_t* restore_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_restore_db_from_backup(rocksdb_backup_engine_t* be, [NativeTypeName("const char *")] sbyte* db_dir, [NativeTypeName("const char *")] sbyte* wal_dir, [NativeTypeName("const rocksdb_restore_options_t *")] rocksdb_restore_options_t* restore_options, [NativeTypeName("const uint32_t")] uint backup_id, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_backup_engine_info_t *")]
    public static extern rocksdb_backup_engine_info_t* rocksdb_backup_engine_get_backup_info(rocksdb_backup_engine_t* be);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_info_count([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_backup_engine_info_timestamp([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_backup_engine_info_backup_id([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_backup_engine_info_size([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_backup_engine_info_number_files([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_backup_engine_info_app_metadata([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_info_destroy([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_stop_backup(rocksdb_backup_engine_t* be);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_close(rocksdb_backup_engine_t* be);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_merge(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_merge_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_t* batch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_range_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush(rocksdb_t* db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_wal(rocksdb_t* db, [NativeTypeName("unsigned char")] byte sync, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_pause_background_work(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_continue_background_work(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_disable_file_deletions(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_enable_file_deletions(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_verify_checksum(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_verify_file_checksums(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_destroy_db([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_repair_db([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_verify_checksum_with_options(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_verify_file_checksums_with_options(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_increase_full_history_ts_low(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* ts_low, [NativeTypeName("size_t")] nuint ts_lowlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_full_history_ts_low(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("size_t *")] nuint* ts_lowlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_backup_engine_options_t* rocksdb_backup_engine_options_create([NativeTypeName("const char *")] sbyte* backup_dir);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_env(rocksdb_backup_engine_options_t* options, rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_rate_limiter(rocksdb_backup_engine_options_t* options, rocksdb_ratelimiter_t* limiter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_restore_rate_limiter(rocksdb_backup_engine_options_t* options, rocksdb_ratelimiter_t* limiter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_destroy(rocksdb_backup_engine_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_checkpoint_t* rocksdb_checkpoint_object_create(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_checkpoint_create(rocksdb_checkpoint_t* checkpoint, [NativeTypeName("const char *")] sbyte* checkpoint_dir, [NativeTypeName("uint64_t")] ulong log_size_for_flush, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_export_import_files_metadata_t* rocksdb_checkpoint_export_column_family(rocksdb_checkpoint_t* checkpoint, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* export_dir, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_checkpoint_object_destroy(rocksdb_checkpoint_t* checkpoint);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_and_trim_history([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char *")] sbyte* trim_ts, [NativeTypeName("size_t")] nuint trim_tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_column_families_with_ttl([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("const int *")] int* ttls, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_for_read_only_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("unsigned char")] byte error_if_wal_file_exists, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_as_secondary_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* secondary_path, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char **")]
    public static extern sbyte** rocksdb_list_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("size_t *")] nuint* lencf, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_list_column_families_destroy([NativeTypeName("char **")] sbyte** list, [NativeTypeName("size_t")] nuint len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_create_column_family(rocksdb_t* db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t** rocksdb_create_column_families(rocksdb_t* db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("size_t *")] nuint* lencfs, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_column_families_destroy(rocksdb_column_family_handle_t** list);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_create_column_family_with_import(rocksdb_t* db, rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, rocksdb_import_column_family_options_t* import_options, rocksdb_export_import_files_metadata_t* metadata, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_create_column_family_with_ttl(rocksdb_t* db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, int ttl, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_drop_column_family(rocksdb_t* db, rocksdb_column_family_handle_t* handle, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_get_default_column_family_handle(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_handle_destroy(rocksdb_column_family_handle_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_column_family_handle_get_id(rocksdb_column_family_handle_t* handle);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_column_family_handle_get_name(rocksdb_column_family_handle_t* handle, [NativeTypeName("size_t *")] nuint* name_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_close(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** ts, [NativeTypeName("size_t *")] nuint* tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** ts, [NativeTypeName("size_t *")] nuint* tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_db_identity(rocksdb_t* db, [NativeTypeName("size_t *")] nuint* id_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** timestamp_list, [NativeTypeName("size_t *")] nuint* timestamp_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** timestamps_list, [NativeTypeName("size_t *")] nuint* timestamps_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_batched_multi_get_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, rocksdb_pinnableslice_t** values, [NativeTypeName("char **")] sbyte** errs, [NativeTypeName("const bool")] bool sorted_input);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_batched_multi_get_cf_slice(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const rocksdb_slice_t *")] rocksdb_slice_t* keys_list, rocksdb_pinnableslice_t** values, [NativeTypeName("char **")] sbyte** errs, [NativeTypeName("const bool")] bool sorted_input);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_key_may_exist(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint key_len, [NativeTypeName("char **")] sbyte** value, [NativeTypeName("size_t *")] nuint* val_len, [NativeTypeName("const char *")] sbyte* timestamp, [NativeTypeName("size_t")] nuint timestamp_len, [NativeTypeName("unsigned char *")] byte* value_found);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_key_may_exist_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint key_len, [NativeTypeName("char **")] sbyte** value, [NativeTypeName("size_t *")] nuint* val_len, [NativeTypeName("const char *")] sbyte* timestamp, [NativeTypeName("size_t")] nuint timestamp_len, [NativeTypeName("unsigned char *")] byte* value_found);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_create_iterator(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wal_iterator_t* rocksdb_get_updates_since(rocksdb_t* db, [NativeTypeName("uint64_t")] ulong seq_number, [NativeTypeName("const rocksdb_wal_readoptions_t *")] rocksdb_wal_readoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wal_files_t* rocksdb_get_sorted_wal_files(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wal_file_t* rocksdb_get_current_wal_file(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_create_iterator_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_iterators(rocksdb_t* db, rocksdb_readoptions_t* opts, rocksdb_column_family_handle_t** column_families, rocksdb_iterator_t** iterators, [NativeTypeName("size_t")] nuint size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_snapshot_t *")]
    public static extern rocksdb_snapshot_t* rocksdb_create_snapshot(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_release_snapshot(rocksdb_t* db, [NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* snapshot);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_snapshot_get_sequence_number([NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* snapshot);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_property_value(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_property_int(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] ulong* out_val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_property_int_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] ulong* out_val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_property_value_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes(rocksdb_t* db, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint64_t *")] ulong* sizes, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint64_t *")] ulong* sizes, [NativeTypeName("char **")] sbyte** errptr);

    public const uint rocksdb_size_approximation_flags_none = 0;
    public const uint rocksdb_size_approximation_flags_include_memtable = 1 << 0;
    public const uint rocksdb_size_approximation_flags_include_files = 1 << 1;
    public const uint rocksdb_size_approximation_flags_include_blob_files = 1 << 2;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes_cf_with_flags(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint8_t")] byte include_flags, [NativeTypeName("uint64_t *")] ulong* sizes, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes_with_options(rocksdb_t* db, [NativeTypeName("const rocksdb_size_approximation_options_t *")] rocksdb_size_approximation_options_t* options, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint64_t *")] ulong* sizes, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes_cf_with_options(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const rocksdb_size_approximation_options_t *")] rocksdb_size_approximation_options_t* options, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint64_t *")] ulong* sizes, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_size_approximation_options_t* rocksdb_size_approximation_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_size_approximation_options_destroy(rocksdb_size_approximation_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_suggest_compact_range(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_suggest_compact_range_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range_opt(rocksdb_t* db, rocksdb_compactoptions_t* opt, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range_cf_opt(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, rocksdb_compactoptions_t* opt, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_files(rocksdb_t* db, [NativeTypeName("const rocksdb_compaction_options_t *")] rocksdb_compaction_options_t* options, [NativeTypeName("const char *const *")] sbyte** input_file_names, [NativeTypeName("size_t")] nuint num_input_file_names, int output_level, int output_path_id, [NativeTypeName("char ***")] sbyte*** output_file_names, [NativeTypeName("size_t *")] nuint* num_output_file_names, rocksdb_compactionjobinfo_t* compaction_job_info, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_files_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const rocksdb_compaction_options_t *")] rocksdb_compaction_options_t* options, [NativeTypeName("const char *const *")] sbyte** input_file_names, [NativeTypeName("size_t")] nuint num_input_file_names, int output_level, int output_path_id, [NativeTypeName("char ***")] sbyte*** output_file_names, [NativeTypeName("size_t *")] nuint* num_output_file_names, rocksdb_compactionjobinfo_t* compaction_job_info, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_files_output_file_names_destroy([NativeTypeName("char **")] sbyte** output_file_names, [NativeTypeName("size_t")] nuint num_output_file_names);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_livefiles_t *")]
    public static extern rocksdb_livefiles_t* rocksdb_livefiles(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_cfs(rocksdb_t* db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t** column_family, int num_column_families, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_destroy(rocksdb_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_iter_valid([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek_to_first(rocksdb_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek_to_last(rocksdb_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek(rocksdb_iterator_t* param0, [NativeTypeName("const char *")] sbyte* k, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek_for_prev(rocksdb_iterator_t* param0, [NativeTypeName("const char *")] sbyte* k, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_next(rocksdb_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_prev(rocksdb_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_iter_key([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("size_t *")] nuint* klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_iter_value([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("size_t *")] nuint* vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_iter_timestamp([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("size_t *")] nuint* tslen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_get_error([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slice_t rocksdb_iter_key_slice([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slice_t rocksdb_iter_value_slice([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slice_t rocksdb_iter_timestamp_slice([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_refresh([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_iter_next(rocksdb_wal_iterator_t* iter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wal_iter_valid([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_iter_status([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* iter, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_wal_iter_get_batch([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* iter, [NativeTypeName("uint64_t *")] ulong* seq);

    public const uint rocksdb_wal_file_type_archived_log = 0;
    public const uint rocksdb_wal_file_type_alive_log = 1;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_get_latest_sequence_number(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_iter_destroy([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* iter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wal_readoptions_t* rocksdb_wal_readoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_readoptions_destroy(rocksdb_wal_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_readoptions_set_verify_checksums(rocksdb_wal_readoptions_t* options, [NativeTypeName("unsigned char")] byte verify_checksums);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wal_readoptions_get_verify_checksums(rocksdb_wal_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_wal_file_path_name([NativeTypeName("const rocksdb_wal_file_t *")] rocksdb_wal_file_t* file);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_wal_file_log_number([NativeTypeName("const rocksdb_wal_file_t *")] rocksdb_wal_file_t* file);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_wal_file_type([NativeTypeName("const rocksdb_wal_file_t *")] rocksdb_wal_file_t* file);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_wal_file_start_sequence([NativeTypeName("const rocksdb_wal_file_t *")] rocksdb_wal_file_t* file);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_wal_file_size_file_bytes([NativeTypeName("const rocksdb_wal_file_t *")] rocksdb_wal_file_t* file);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_file_destroy(rocksdb_wal_file_t* file);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_wal_files_count([NativeTypeName("const rocksdb_wal_files_t *")] rocksdb_wal_files_t* files);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_wal_file_t *")]
    public static extern rocksdb_wal_file_t* rocksdb_wal_files_get_wal_file([NativeTypeName("const rocksdb_wal_files_t *")] rocksdb_wal_files_t* files, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_files_destroy(rocksdb_wal_files_t* files);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_writebatch_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_writebatch_create_from([NativeTypeName("const char *")] sbyte* rep, [NativeTypeName("size_t")] nuint size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_writebatch_create_with_params([NativeTypeName("size_t")] nuint reserved_bytes, [NativeTypeName("size_t")] nuint max_bytes, [NativeTypeName("size_t")] nuint protection_bytes_per_key, [NativeTypeName("size_t")] nuint default_cf_ts_sz);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_destroy(rocksdb_writebatch_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_clear(rocksdb_writebatch_t* b);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put_log_data(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* blob, [NativeTypeName("size_t")] nuint len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_set_save_point(rocksdb_writebatch_t* b);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_rollback_to_save_point(rocksdb_writebatch_t* b, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_pop_save_point(rocksdb_writebatch_t* b, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_verify_checksum(rocksdb_writebatch_t* b, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_writebatch_count(rocksdb_writebatch_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put_cf_with_ts(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_putv(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_putv_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_merge(rocksdb_writebatch_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_merge_cf(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_mergev(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_mergev_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_singledelete(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_cf(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_cf_with_ts(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_singledelete_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_singledelete_cf_with_ts(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_deletev(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_deletev_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_range(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_range_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_rangev(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_rangev_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate_ld(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> log_data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate_cf(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> put_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, void> deleted_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> merge_cf);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate_cf_ld(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> put_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, void> deleted_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> merge_cf, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> log_data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_writebatch_data(rocksdb_writebatch_t* param0, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_update_timestamps(rocksdb_writebatch_t* wb, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, void* state, [NativeTypeName("size_t (*)(void *, uint32_t)")] delegate* unmanaged[Cdecl]<void*, uint, nuint> get_ts_size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_writebatch_wi_create([NativeTypeName("size_t")] nuint reserved_bytes, [NativeTypeName("unsigned char")] byte overwrite_keys);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_writebatch_wi_create_with_params(rocksdb_comparator_t* backup_index_comparator, [NativeTypeName("size_t")] nuint reserved_bytes, [NativeTypeName("unsigned char")] byte overwrite_key, [NativeTypeName("size_t")] nuint max_bytes, [NativeTypeName("size_t")] nuint protection_bytes_per_key);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_destroy(rocksdb_writebatch_wi_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_clear(rocksdb_writebatch_wi_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_writebatch_wi_count(rocksdb_writebatch_wi_t* b);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_put(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_put_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_putv(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_putv_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_merge(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_merge_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_mergev(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_mergev_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_singledelete(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_singledelete_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_deletev(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_deletev_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_range(rocksdb_writebatch_wi_t* b, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_range_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_rangev(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_rangev_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_put_log_data(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* blob, [NativeTypeName("size_t")] nuint len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_iterate(rocksdb_writebatch_wi_t* b, void* state, [NativeTypeName("void (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_writebatch_wi_data(rocksdb_writebatch_wi_t* b, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_set_save_point(rocksdb_writebatch_wi_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_rollback_to_save_point(rocksdb_writebatch_wi_t* param0, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch(rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch_cf(rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch_and_db(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_writebatch_wi_get_pinned_from_batch_and_db(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch_and_db_cf(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_writebatch_wi_get_pinned_from_batch_and_db_cf(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_writebatch_wi(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base_readopts(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base_cf(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator, rocksdb_column_family_handle_t* cf);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base_cf_readopts(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator, rocksdb_column_family_handle_t* cf, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_update_timestamps(rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, void* state, [NativeTypeName("size_t (*)(void *, uint32_t)")] delegate* unmanaged[Cdecl]<void*, uint, nuint> get_ts_size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_load_latest_options([NativeTypeName("const char *")] sbyte* db_path, rocksdb_env_t* env, bool ignore_unknown_options, rocksdb_cache_t* cache, rocksdb_options_t** db_options, [NativeTypeName("size_t *")] nuint* num_column_families, [NativeTypeName("char ***")] sbyte*** column_family_names, rocksdb_options_t*** column_family_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_load_latest_options_destroy(rocksdb_options_t* db_options, [NativeTypeName("char **")] sbyte** list_column_family_names, rocksdb_options_t** list_column_family_options, [NativeTypeName("size_t")] nuint len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_block_based_table_options_t* rocksdb_block_based_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_destroy(rocksdb_block_based_table_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_data_block_hash_ratio(rocksdb_block_based_table_options_t* options, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_top_level_index_pinning_tier(rocksdb_block_based_table_options_t* options, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_partition_pinning_tier(rocksdb_block_based_table_options_t* options, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_unpartitioned_pinning_tier(rocksdb_block_based_table_options_t* options, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_filter_policy(rocksdb_block_based_table_options_t* options, rocksdb_filterpolicy_t* filter_policy);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_cache(rocksdb_block_based_table_options_t* options, rocksdb_cache_t* block_cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_user_defined_index_factory_from_string(rocksdb_block_based_table_options_t* options, [NativeTypeName("const char *")] sbyte* value, [NativeTypeName("size_t")] nuint value_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_clear_user_defined_index_factory(rocksdb_block_based_table_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_block_based_options_get_user_defined_index_factory_name([NativeTypeName("const rocksdb_block_based_table_options_t *")] rocksdb_block_based_table_options_t* options, [NativeTypeName("size_t *")] nuint* name_len);

    public const uint rocksdb_block_based_table_index_type_binary_search = 0;
    public const uint rocksdb_block_based_table_index_type_hash_search = 1;
    public const uint rocksdb_block_based_table_index_type_two_level_index_search = 2;

    public const uint rocksdb_block_based_table_data_block_index_type_binary_search = 0;
    public const uint rocksdb_block_based_table_data_block_index_type_binary_search_and_hash = 1;

    public const uint rocksdb_block_based_table_index_block_search_type_binary = 0;
    public const uint rocksdb_block_based_table_index_block_search_type_interpolation = 1;
    public const uint rocksdb_block_based_table_index_block_search_type_auto = 2;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_block_based_table_factory(rocksdb_options_t* opt, rocksdb_block_based_table_options_t* table_options);

    public const uint rocksdb_block_based_k_fallback_pinning_tier = 0;
    public const uint rocksdb_block_based_k_none_pinning_tier = 1;
    public const uint rocksdb_block_based_k_flush_and_similar_pinning_tier = 2;
    public const uint rocksdb_block_based_k_all_pinning_tier = 3;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_buffer_manager(rocksdb_options_t* opt, rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_sst_file_manager(rocksdb_options_t* opt, rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_flushjobinfo_cf_id([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_flushjobinfo_cf_name([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_flushjobinfo_file_path([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_flushjobinfo_file_number([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_flushjobinfo_oldest_blob_file_number([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_flushjobinfo_thread_id([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_flushjobinfo_job_id([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushjobinfo_triggered_writes_slowdown([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushjobinfo_triggered_writes_stop([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_flushjobinfo_smallest_seqno([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_flushjobinfo_largest_seqno([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_flushjobinfo_flush_reason([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_flushjobinfo_blob_compression_type([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionjobinfo_cf_id([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_cf_name([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionjobinfo_status([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_thread_id([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionjobinfo_job_id([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionjobinfo_num_l0_files([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionjobinfo_base_input_level([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionjobinfo_output_level([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionjobinfo_compaction_reason([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionjobinfo_compression([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionjobinfo_blob_compression_type([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionjobinfo_aborted([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_subcompactionjobinfo_cf_id([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_subcompactionjobinfo_cf_name([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_subcompactionjobinfo_status([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_subcompactionjobinfo_thread_id([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_subcompactionjobinfo_job_id([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_subcompactionjobinfo_subcompaction_job_id([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_subcompactionjobinfo_base_input_level([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_subcompactionjobinfo_output_level([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_subcompactionjobinfo_compaction_reason([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_subcompactionjobinfo_compression([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_subcompactionjobinfo_blob_compression_type([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_externalfileingestioninfo_cf_name([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_externalfileingestioninfo_external_file_path([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_externalfileingestioninfo_internal_file_path([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_externalfileingestioninfo_global_seqno([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_memtableinfo_cf_name([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_memtableinfo_first_seqno([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_memtableinfo_earliest_seqno([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_memtableinfo_num_entries([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_memtableinfo_num_deletes([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_memtableinfo_newest_udt([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_input_files_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_output_files_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_elapsed_micros([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_num_corrupt_keys([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_input_records([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_output_records([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_total_input_bytes([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionjobinfo_total_output_bytes([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_num_input_files([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_num_input_files_at_output_level([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_writestallinfo_cf_name([NativeTypeName("const rocksdb_writestallinfo_t *")] rocksdb_writestallinfo_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_writestallcondition_t *")]
    public static extern rocksdb_writestallcondition_t* rocksdb_writestallinfo_cur([NativeTypeName("const rocksdb_writestallinfo_t *")] rocksdb_writestallinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_writestallcondition_t *")]
    public static extern rocksdb_writestallcondition_t* rocksdb_writestallinfo_prev([NativeTypeName("const rocksdb_writestallinfo_t *")] rocksdb_writestallinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_orig_file_number([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_data_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_uncompressed_data_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_index_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_index_partitions([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_top_level_index_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_index_key_is_user_key([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_index_value_is_delta_encoded([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_udi_is_primary_index([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_filter_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_raw_key_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_raw_value_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_data_blocks([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_data_blocks_compression_rejected([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_data_blocks_compression_bypassed([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_uniform_blocks([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_entries([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_filter_entries([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_deletions([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_merge_operands([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_num_range_deletions([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_format_version([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_fixed_key_len([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_column_family_id([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_creation_time([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_oldest_key_time([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_newest_key_time([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_file_creation_time([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_slow_compression_estimated_data_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_fast_compression_estimated_data_size([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_external_sst_file_global_seqno_offset([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_tail_start_offset([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_user_defined_timestamps_persisted([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_key_largest_seqno([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_key_smallest_seqno([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_data_block_restart_interval([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_index_block_restart_interval([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_table_properties_separate_key_value_in_data_block([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_db_id([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_db_session_id([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_db_host_id([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_column_family_name([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_filter_policy_name([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_comparator_name([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_merge_operator_name([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_prefix_extractor_name([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_property_collectors_names([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_compression_name([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_compression_options([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_seqno_to_time_mapping([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_elapsed_micros([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_cpu_micros([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compaction_job_stats_has_accurate_num_input_records([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_input_records([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_blobs_read([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_input_files([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_input_files_trivially_moved([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_input_files_at_output_level([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_filtered_input_files([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_filtered_input_files_at_output_level([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_output_records([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_output_files([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compaction_job_stats_num_output_files_blob([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compaction_job_stats_is_full_compaction([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compaction_job_stats_is_manual_compaction([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compaction_job_stats_is_remote_compaction([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_input_bytes([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_blob_bytes_read([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_output_bytes([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_output_bytes_blob([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_skipped_input_bytes([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_records_replaced([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_input_raw_key_bytes([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_total_input_raw_value_bytes([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_input_deletion_records([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_expired_deletion_records([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_corrupt_keys([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_file_write_nanos([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_file_range_sync_nanos([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_file_fsync_nanos([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_file_prepare_write_nanos([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compaction_job_stats_smallest_output_key_prefix([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compaction_job_stats_largest_output_key_prefix([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_single_del_fallthru([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_job_stats_num_single_del_mismatch([NativeTypeName("const rocksdb_compaction_job_stats_t *")] rocksdb_compaction_job_stats_t* stats);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compaction_file_info_level([NativeTypeName("const rocksdb_compaction_file_info_t *")] rocksdb_compaction_file_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_file_info_file_number([NativeTypeName("const rocksdb_compaction_file_info_t *")] rocksdb_compaction_file_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_file_info_oldest_blob_file_number([NativeTypeName("const rocksdb_compaction_file_info_t *")] rocksdb_compaction_file_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_blob_file_addition_info_blob_file_path([NativeTypeName("const rocksdb_blob_file_addition_info_t *")] rocksdb_blob_file_addition_info_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_blob_file_addition_info_blob_file_number([NativeTypeName("const rocksdb_blob_file_addition_info_t *")] rocksdb_blob_file_addition_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_blob_file_addition_info_total_blob_count([NativeTypeName("const rocksdb_blob_file_addition_info_t *")] rocksdb_blob_file_addition_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_blob_file_addition_info_total_blob_bytes([NativeTypeName("const rocksdb_blob_file_addition_info_t *")] rocksdb_blob_file_addition_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_blob_file_garbage_info_blob_file_path([NativeTypeName("const rocksdb_blob_file_garbage_info_t *")] rocksdb_blob_file_garbage_info_t* info, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_blob_file_garbage_info_blob_file_number([NativeTypeName("const rocksdb_blob_file_garbage_info_t *")] rocksdb_blob_file_garbage_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_blob_file_garbage_info_garbage_blob_count([NativeTypeName("const rocksdb_blob_file_garbage_info_t *")] rocksdb_blob_file_garbage_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_blob_file_garbage_info_garbage_blob_bytes([NativeTypeName("const rocksdb_blob_file_garbage_info_t *")] rocksdb_blob_file_garbage_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionjobinfo_t* rocksdb_compactionjobinfo_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionjobinfo_destroy(rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_reset_status(rocksdb_status_ptr_t* status_ptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_status_ptr_get_error(rocksdb_status_ptr_t* status, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_table_properties_has_key_largest_seqno([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_table_properties_has_key_smallest_seqno([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_table_properties_user_collected_properties_count([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_user_collected_properties_key_at([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_user_collected_properties_value_at([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* value_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_table_properties_readable_properties_count([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_readable_properties_key_at([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_table_properties_readable_properties_value_at([NativeTypeName("const rocksdb_table_properties_t *")] rocksdb_table_properties_t* props, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* value_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_table_properties_t *")]
    public static extern rocksdb_table_properties_t* rocksdb_flushjobinfo_table_properties([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_flushjobinfo_blob_file_addition_infos_count([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_blob_file_addition_info_t *")]
    public static extern rocksdb_blob_file_addition_info_t* rocksdb_flushjobinfo_blob_file_addition_info_at([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info, [NativeTypeName("size_t")] nuint pos);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_input_file_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_output_file_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_compaction_job_stats_t *")]
    public static extern rocksdb_compaction_job_stats_t* rocksdb_compactionjobinfo_stats([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_input_file_infos_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_compaction_file_info_t *")]
    public static extern rocksdb_compaction_file_info_t* rocksdb_compactionjobinfo_input_file_info_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_output_file_infos_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_compaction_file_info_t *")]
    public static extern rocksdb_compaction_file_info_t* rocksdb_compactionjobinfo_output_file_info_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_table_properties_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_table_properties_key_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_table_properties_t *")]
    public static extern rocksdb_table_properties_t* rocksdb_compactionjobinfo_table_properties_value_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_table_properties_t *")]
    public static extern rocksdb_table_properties_t* rocksdb_compactionjobinfo_table_properties_for_file([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("const char *")] sbyte* file_name, [NativeTypeName("size_t")] nuint file_name_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_blob_file_addition_infos_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_blob_file_addition_info_t *")]
    public static extern rocksdb_blob_file_addition_info_t* rocksdb_compactionjobinfo_blob_file_addition_info_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_blob_file_garbage_infos_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_blob_file_garbage_info_t *")]
    public static extern rocksdb_blob_file_garbage_info_t* rocksdb_compactionjobinfo_blob_file_garbage_info_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("size_t")] nuint pos);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_compaction_job_stats_t *")]
    public static extern rocksdb_compaction_job_stats_t* rocksdb_subcompactionjobinfo_stats([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_table_properties_t *")]
    public static extern rocksdb_table_properties_t* rocksdb_externalfileingestioninfo_table_properties([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_eventlistener_t* rocksdb_eventlistener_create(void* state_, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor_, [NativeTypeName("on_flush_begin_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_flushjobinfo_t*, void> on_flush_begin, [NativeTypeName("on_flush_completed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_flushjobinfo_t*, void> on_flush_completed, [NativeTypeName("on_compaction_begin_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_compactionjobinfo_t*, void> on_compaction_begin, [NativeTypeName("on_compaction_completed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_compactionjobinfo_t*, void> on_compaction_completed, [NativeTypeName("on_subcompaction_begin_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_subcompactionjobinfo_t*, void> on_subcompaction_begin, [NativeTypeName("on_subcompaction_completed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_subcompactionjobinfo_t*, void> on_subcompaction_completed, [NativeTypeName("on_external_file_ingested_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_externalfileingestioninfo_t*, void> on_external_file_ingested, [NativeTypeName("on_background_error_cb")] delegate* unmanaged[Cdecl]<void*, uint, rocksdb_status_ptr_t*, void> on_background_error, [NativeTypeName("on_stall_conditions_changed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_writestallinfo_t*, void> on_stall_conditions_changed, [NativeTypeName("on_memtable_sealed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_memtableinfo_t*, void> on_memtable_sealed);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_eventlistener_destroy(rocksdb_eventlistener_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_eventlistener(rocksdb_options_t* param0, rocksdb_eventlistener_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cuckoo_table_options_t* rocksdb_cuckoo_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_destroy(rocksdb_cuckoo_table_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_hash_ratio(rocksdb_cuckoo_table_options_t* options, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_cuckoo_table_factory(rocksdb_options_t* opt, rocksdb_cuckoo_table_options_t* table_options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_options(rocksdb_t* db, int count, [NativeTypeName("const char *const[]")] sbyte** keys, [NativeTypeName("const char *const[]")] sbyte** values, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_db_options(rocksdb_t* db, int count, [NativeTypeName("const char *const[]")] sbyte** keys, [NativeTypeName("const char *const[]")] sbyte** values, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_options_cf(rocksdb_t* db, rocksdb_column_family_handle_t* handle, int count, [NativeTypeName("const char *const[]")] sbyte** keys, [NativeTypeName("const char *const[]")] sbyte** values, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_options_t* rocksdb_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_destroy(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_options_t* rocksdb_options_create_copy(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_increase_parallelism(rocksdb_options_t* opt, int total_threads);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_optimize_for_point_lookup(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong block_cache_size_mb);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_optimize_level_style_compaction(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong memtable_memory_budget);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_optimize_universal_style_compaction(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong memtable_memory_budget);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_ingest_behind(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_ingest_behind(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_filter(rocksdb_options_t* param0, rocksdb_compactionfilter_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_filter_factory(rocksdb_options_t* param0, rocksdb_compactionfilterfactory_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_compaction_readahead_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_compaction_readahead_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_comparator(rocksdb_options_t* param0, rocksdb_comparator_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_merge_operator(rocksdb_options_t* param0, rocksdb_mergeoperator_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_filter(rocksdb_options_t* param0, rocksdb_walfilter_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_clear_wal_filter(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_uint64add_merge_operator(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_per_level(rocksdb_options_t* opt, [NativeTypeName("const int *")] int* level_values, [NativeTypeName("size_t")] nuint num_levels);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_create_if_missing(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_create_if_missing(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_create_missing_column_families(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_create_missing_column_families(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_error_if_exists(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_error_if_exists(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_paranoid_checks(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_paranoid_checks(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_open_files_async(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_open_files_async(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_paths(rocksdb_options_t* param0, [NativeTypeName("const rocksdb_dbpath_t **")] rocksdb_dbpath_t** path_values, [NativeTypeName("size_t")] nuint num_paths);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_cf_paths(rocksdb_options_t* param0, [NativeTypeName("const rocksdb_dbpath_t **")] rocksdb_dbpath_t** path_values, [NativeTypeName("size_t")] nuint num_paths);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_env(rocksdb_options_t* param0, rocksdb_env_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_info_log(rocksdb_options_t* param0, rocksdb_logger_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_logger_t* rocksdb_options_get_info_log(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_info_log_level(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_info_log_level(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_logger_t* rocksdb_logger_create_stderr_logger(int log_level, [NativeTypeName("const char *")] sbyte* prefix);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_logger_t* rocksdb_logger_create_callback_logger(int log_level, [NativeTypeName("void (*)(void *, unsigned int, char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, void> param1, void* priv);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_logger_destroy(rocksdb_logger_t* logger);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_file_checksum_gen_factory_t* rocksdb_file_checksum_gen_crc32c_factory_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_file_checksum_gen_factory_destroy(rocksdb_file_checksum_gen_factory_t* factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_file_checksum_gen_factory(rocksdb_options_t* param0, rocksdb_file_checksum_gen_factory_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_checksum_handoff_file_types_clear(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_checksum_handoff_file_types_add(rocksdb_options_t* opt, int file_type);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_checksum_handoff_file_types_remove(rocksdb_options_t* opt, int file_type);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_checksum_handoff_file_types_contains(rocksdb_options_t* opt, int file_type);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_checksum_handoff_file_types_count(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_calculate_sst_write_lifetime_hint_set_clear(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_calculate_sst_write_lifetime_hint_set_add(rocksdb_options_t* opt, int compaction_style);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_calculate_sst_write_lifetime_hint_set_remove(rocksdb_options_t* opt, int compaction_style);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_calculate_sst_write_lifetime_hint_set_contains(rocksdb_options_t* opt, int compaction_style);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_calculate_sst_write_lifetime_hint_set_count(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sst_partitioner_factory_t* rocksdb_sst_partitioner_fixed_prefix_factory_create([NativeTypeName("size_t")] nuint prefix_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_partitioner_factory_destroy(rocksdb_sst_partitioner_factory_t* factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_sst_partitioner_factory(rocksdb_options_t* param0, rocksdb_sst_partitioner_factory_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_table_properties_collector_factory_destroy(rocksdb_table_properties_collector_factory_t* factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_table_properties_collector_factory(rocksdb_options_t* param0, rocksdb_table_properties_collector_factory_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_buffer_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_write_buffer_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_write_buffer_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_db_write_buffer_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_open_files(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_open_files(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_file_opening_threads(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_file_opening_threads(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_total_wal_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong n);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_max_total_wal_size(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options(rocksdb_options_t* param0, int param1, int param2, int param3, int param4);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_zstd_max_train_bytes(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compression_options_zstd_max_train_bytes(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_use_zstd_dict_trainer(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_compression_options_use_zstd_dict_trainer(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_parallel_threads(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compression_options_parallel_threads(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_max_dict_buffer_bytes(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_compression_options_max_dict_buffer_bytes(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options(rocksdb_options_t* param0, int param1, int param2, int param3, int param4, [NativeTypeName("unsigned char")] byte param5);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options_zstd_max_train_bytes(rocksdb_options_t* param0, int param1, [NativeTypeName("unsigned char")] byte param2);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options_use_zstd_dict_trainer(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1, [NativeTypeName("unsigned char")] byte param2);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_bottommost_compression_options_use_zstd_dict_trainer(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options_max_dict_buffer_bytes(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1, [NativeTypeName("unsigned char")] byte param2);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_prefix_extractor(rocksdb_options_t* param0, rocksdb_slicetransform_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_num_levels(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_num_levels(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level0_file_num_compaction_trigger(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_level0_file_num_compaction_trigger(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level0_slowdown_writes_trigger(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_level0_slowdown_writes_trigger(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level0_stop_writes_trigger(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_level0_stop_writes_trigger(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_target_file_size_base(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_target_file_size_base(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_target_file_size_multiplier(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_target_file_size_multiplier(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bytes_for_level_base(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_max_bytes_for_level_base(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level_compaction_dynamic_level_bytes(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_level_compaction_dynamic_level_bytes(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bytes_for_level_multiplier(rocksdb_options_t* param0, double param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_max_bytes_for_level_multiplier(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bytes_for_level_multiplier_additional(rocksdb_options_t* param0, int* level_values, [NativeTypeName("size_t")] nuint num_levels);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_enable_statistics(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_ttl(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_ttl(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_periodic_compaction_seconds(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_periodic_compaction_seconds(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_op_scan_flush_trigger(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_memtable_op_scan_flush_trigger(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_avg_op_scan_flush_trigger(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_memtable_avg_op_scan_flush_trigger(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_tombstones_for_range_conversion(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_min_tombstones_for_range_conversion(rocksdb_options_t* param0);

    public const uint rocksdb_statistics_level_disable_all = 0;
    public const uint rocksdb_statistics_level_except_tickers = rocksdb_statistics_level_disable_all;
    public const uint rocksdb_statistics_level_except_histogram_or_timers = 1;
    public const uint rocksdb_statistics_level_except_timers = 2;
    public const uint rocksdb_statistics_level_except_detailed_timers = 3;
    public const uint rocksdb_statistics_level_except_time_for_mutex = 4;
    public const uint rocksdb_statistics_level_all = 5;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_statistics_level(rocksdb_options_t* param0, int level);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_statistics_level(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_skip_stats_update_on_db_open(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_skip_stats_update_on_db_open(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_blob_files(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_blob_files(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_blob_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_min_blob_size(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_file_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_blob_file_size(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_compression_type(rocksdb_options_t* opt, int val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_blob_compression_type(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_blob_gc(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_blob_gc(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_gc_age_cutoff(rocksdb_options_t* opt, double val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_blob_gc_age_cutoff(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_gc_force_threshold(rocksdb_options_t* opt, double val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_blob_gc_force_threshold(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_read_triggered_compaction_threshold(rocksdb_options_t* opt, double val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_read_triggered_compaction_threshold(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_compaction_trigger_wakeup_seconds(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_max_compaction_trigger_wakeup_seconds(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_compaction_readahead_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_blob_compaction_readahead_size(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_file_starting_level(rocksdb_options_t* opt, int val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_blob_file_starting_level(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_cache(rocksdb_options_t* opt, rocksdb_cache_t* blob_cache);

    public const uint rocksdb_prepopulate_blob_disable = 0;
    public const uint rocksdb_prepopulate_blob_flush_only = 1;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_prepopulate_blob_cache(rocksdb_options_t* opt, int val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_prepopulate_blob_cache(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_options_statistics_get_string(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_statistics_get_ticker_count(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint ticker_type);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_statistics_get_histogram_data(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint histogram_type, rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_write_buffer_number(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_write_buffer_number(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_write_buffer_number_to_merge(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_min_write_buffer_number_to_merge(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_write_buffer_size_to_maintain(rocksdb_options_t* param0, [NativeTypeName("int64_t")] long param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_options_get_max_write_buffer_size_to_maintain(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_pipelined_write(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_pipelined_write(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_unordered_write(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_unordered_write(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_subcompactions(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_max_subcompactions(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_background_jobs(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_background_jobs(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_background_compactions(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_background_compactions(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_background_flushes(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_background_flushes(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_log_file_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_max_log_file_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_log_file_time_to_roll(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_log_file_time_to_roll(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_keep_log_file_num(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_keep_log_file_num(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_recycle_log_file_num(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_recycle_log_file_num(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_soft_pending_compaction_bytes_limit(rocksdb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_soft_pending_compaction_bytes_limit(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_hard_pending_compaction_bytes_limit(rocksdb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_hard_pending_compaction_bytes_limit(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_manifest_file_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_max_manifest_file_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_table_cache_numshardbits(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_table_cache_numshardbits(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_arena_block_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_arena_block_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_fsync(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_use_fsync(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_log_dir(rocksdb_options_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_dir(rocksdb_options_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_WAL_ttl_seconds(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_WAL_ttl_seconds(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_WAL_size_limit_MB(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_WAL_size_limit_MB(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_manifest_preallocation_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_manifest_preallocation_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_mmap_reads(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_mmap_reads(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_mmap_writes(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_mmap_writes(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_direct_reads(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_direct_reads(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_direct_io_for_flush_and_compaction(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_direct_io_for_flush_and_compaction(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_is_fd_close_on_exec(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_is_fd_close_on_exec(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_stats_dump_period_sec(rocksdb_options_t* param0, [NativeTypeName("unsigned int")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint rocksdb_options_get_stats_dump_period_sec(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_stats_persist_period_sec(rocksdb_options_t* param0, [NativeTypeName("unsigned int")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint rocksdb_options_get_stats_persist_period_sec(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_advise_random_on_open(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_advise_random_on_open(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_adaptive_mutex(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_adaptive_mutex(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bytes_per_sync(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_bytes_per_sync(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_bytes_per_sync(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_wal_bytes_per_sync(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_writable_file_max_buffer_size(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_writable_file_max_buffer_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_concurrent_memtable_write(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_concurrent_memtable_write(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_write_thread_adaptive_yield(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_write_thread_adaptive_yield(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_sequential_skip_in_iterations(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_max_sequential_skip_in_iterations(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_disable_auto_compactions(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_disable_auto_compactions(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_optimize_filters_for_hits(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_optimize_filters_for_hits(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_delete_obsolete_files_period_micros(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_delete_obsolete_files_period_micros(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_prepare_for_bulk_load(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_vector_rep(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_prefix_bloom_size_ratio(rocksdb_options_t* param0, double param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_memtable_prefix_bloom_size_ratio(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_compaction_bytes(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_max_compaction_bytes(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_hash_skip_list_rep(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1, [NativeTypeName("int32_t")] int param2, [NativeTypeName("int32_t")] int param3);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_hash_link_list_rep(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_plain_table_factory(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1, int param2, double param3, [NativeTypeName("size_t")] nuint param4, [NativeTypeName("size_t")] nuint param5, [NativeTypeName("char")] sbyte param6, [NativeTypeName("unsigned char")] byte param7, [NativeTypeName("unsigned char")] byte param8);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_write_dbid_to_manifest(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_dbid_to_manifest(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_write_identity_file(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_identity_file(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_track_and_verify_wals_in_manifest(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_track_and_verify_wals_in_manifest(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_level_to_compress(rocksdb_options_t* opt, int level);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_huge_page_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_memtable_huge_page_size(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_successive_merges(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_max_successive_merges(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bloom_locality(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_bloom_locality(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_inplace_update_support(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_inplace_update_support(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_inplace_update_num_locks(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_inplace_update_num_locks(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_report_bg_io_stats(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_report_bg_io_stats(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_avoid_unnecessary_blocking_io(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_avoid_unnecessary_blocking_io(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_experimental_mempurge_threshold(rocksdb_options_t* param0, double param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_experimental_mempurge_threshold(rocksdb_options_t* param0);

    public const uint rocksdb_tolerate_corrupted_tail_records_recovery = 0;
    public const uint rocksdb_absolute_consistency_recovery = 1;
    public const uint rocksdb_point_in_time_recovery = 2;
    public const uint rocksdb_skip_any_corrupted_records_recovery = 3;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_recovery_mode(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_wal_recovery_mode(rocksdb_options_t* param0);

    public const uint rocksdb_no_compression = 0;
    public const uint rocksdb_snappy_compression = 1;
    public const uint rocksdb_zlib_compression = 2;
    public const uint rocksdb_bz2_compression = 3;
    public const uint rocksdb_lz4_compression = 4;
    public const uint rocksdb_lz4hc_compression = 5;
    public const uint rocksdb_xpress_compression = 6;
    public const uint rocksdb_zstd_compression = 7;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compression(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_bottommost_compression(rocksdb_options_t* param0);

    public const uint rocksdb_level_compaction = 0;
    public const uint rocksdb_universal_compaction = 1;
    public const uint rocksdb_fifo_compaction = 2;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_style(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compaction_style(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_universal_compaction_options(rocksdb_options_t* param0, rocksdb_universal_compaction_options_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_fifo_compaction_options(rocksdb_options_t* opt, rocksdb_fifo_compaction_options_t* fifo);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_ratelimiter(rocksdb_options_t* opt, rocksdb_ratelimiter_t* limiter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_atomic_flush(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_atomic_flush(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_row_cache(rocksdb_options_t* opt, rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_compact_on_deletion_collector_factory(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint window_size, [NativeTypeName("size_t")] nuint num_dels_trigger);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_compact_on_deletion_collector_factory_del_ratio(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint window_size, [NativeTypeName("size_t")] nuint num_dels_trigger, double deletion_ratio);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_compact_on_deletion_collector_factory_min_file_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint window_size, [NativeTypeName("size_t")] nuint num_dels_trigger, double deletion_ratio, [NativeTypeName("uint64_t")] ulong min_file_size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_manual_wal_flush(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_manual_wal_flush(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_compression(rocksdb_options_t* opt, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_wal_compression(rocksdb_options_t* opt);

    public const uint rocksdb_k_by_compensated_size_compaction_pri = 0;
    public const uint rocksdb_k_oldest_largest_seq_first_compaction_pri = 1;
    public const uint rocksdb_k_oldest_smallest_seq_first_compaction_pri = 2;
    public const uint rocksdb_k_min_overlapping_ratio_compaction_pri = 3;
    public const uint rocksdb_k_round_robin_compaction_pri = 4;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_pri(rocksdb_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compaction_pri(rocksdb_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ratelimiter_t* rocksdb_ratelimiter_create([NativeTypeName("int64_t")] long rate_bytes_per_sec, [NativeTypeName("int64_t")] long refill_period_us, [NativeTypeName("int32_t")] int fairness);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ratelimiter_t* rocksdb_ratelimiter_create_auto_tuned([NativeTypeName("int64_t")] long rate_bytes_per_sec, [NativeTypeName("int64_t")] long refill_period_us, [NativeTypeName("int32_t")] int fairness);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ratelimiter_t* rocksdb_ratelimiter_create_with_mode([NativeTypeName("int64_t")] long rate_bytes_per_sec, [NativeTypeName("int64_t")] long refill_period_us, [NativeTypeName("int32_t")] int fairness, int mode, bool auto_tuned);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ratelimiter_destroy(rocksdb_ratelimiter_t* param0);

    public const uint rocksdb_uninitialized = 0;
    public const uint rocksdb_disable = 1;
    public const uint rocksdb_enable_count = 2;
    public const uint rocksdb_enable_time_except_for_mutex = 3;
    public const uint rocksdb_enable_time = 4;
    public const uint rocksdb_out_of_bounds = 5;

    public const uint rocksdb_user_key_comparison_count = 0;
    public const uint rocksdb_block_cache_hit_count = 1;
    public const uint rocksdb_block_read_count = 2;
    public const uint rocksdb_block_read_byte = 3;
    public const uint rocksdb_block_read_time = 4;
    public const uint rocksdb_block_checksum_time = 5;
    public const uint rocksdb_block_decompress_time = 6;
    public const uint rocksdb_get_read_bytes = 7;
    public const uint rocksdb_multiget_read_bytes = 8;
    public const uint rocksdb_iter_read_bytes = 9;
    public const uint rocksdb_internal_key_skipped_count = 10;
    public const uint rocksdb_internal_delete_skipped_count = 11;
    public const uint rocksdb_internal_recent_skipped_count = 12;
    public const uint rocksdb_internal_merge_count = 13;
    public const uint rocksdb_get_snapshot_time = 14;
    public const uint rocksdb_get_from_memtable_time = 15;
    public const uint rocksdb_get_from_memtable_count = 16;
    public const uint rocksdb_get_post_process_time = 17;
    public const uint rocksdb_get_from_output_files_time = 18;
    public const uint rocksdb_seek_on_memtable_time = 19;
    public const uint rocksdb_seek_on_memtable_count = 20;
    public const uint rocksdb_next_on_memtable_count = 21;
    public const uint rocksdb_prev_on_memtable_count = 22;
    public const uint rocksdb_seek_child_seek_time = 23;
    public const uint rocksdb_seek_child_seek_count = 24;
    public const uint rocksdb_seek_min_heap_time = 25;
    public const uint rocksdb_seek_max_heap_time = 26;
    public const uint rocksdb_seek_internal_seek_time = 27;
    public const uint rocksdb_find_next_user_entry_time = 28;
    public const uint rocksdb_write_wal_time = 29;
    public const uint rocksdb_write_memtable_time = 30;
    public const uint rocksdb_write_delay_time = 31;
    public const uint rocksdb_write_pre_and_post_process_time = 32;
    public const uint rocksdb_db_mutex_lock_nanos = 33;
    public const uint rocksdb_db_condition_wait_nanos = 34;
    public const uint rocksdb_merge_operator_time_nanos = 35;
    public const uint rocksdb_read_index_block_nanos = 36;
    public const uint rocksdb_read_filter_block_nanos = 37;
    public const uint rocksdb_new_table_block_iter_nanos = 38;
    public const uint rocksdb_new_table_iterator_nanos = 39;
    public const uint rocksdb_block_seek_nanos = 40;
    public const uint rocksdb_find_table_nanos = 41;
    public const uint rocksdb_bloom_memtable_hit_count = 42;
    public const uint rocksdb_bloom_memtable_miss_count = 43;
    public const uint rocksdb_bloom_sst_hit_count = 44;
    public const uint rocksdb_bloom_sst_miss_count = 45;
    public const uint rocksdb_key_lock_wait_time = 46;
    public const uint rocksdb_key_lock_wait_count = 47;
    public const uint rocksdb_env_new_sequential_file_nanos = 48;
    public const uint rocksdb_env_new_random_access_file_nanos = 49;
    public const uint rocksdb_env_new_writable_file_nanos = 50;
    public const uint rocksdb_env_reuse_writable_file_nanos = 51;
    public const uint rocksdb_env_new_random_rw_file_nanos = 52;
    public const uint rocksdb_env_new_directory_nanos = 53;
    public const uint rocksdb_env_file_exists_nanos = 54;
    public const uint rocksdb_env_get_children_nanos = 55;
    public const uint rocksdb_env_get_children_file_attributes_nanos = 56;
    public const uint rocksdb_env_delete_file_nanos = 57;
    public const uint rocksdb_env_create_dir_nanos = 58;
    public const uint rocksdb_env_create_dir_if_missing_nanos = 59;
    public const uint rocksdb_env_delete_dir_nanos = 60;
    public const uint rocksdb_env_get_file_size_nanos = 61;
    public const uint rocksdb_env_get_file_modification_time_nanos = 62;
    public const uint rocksdb_env_rename_file_nanos = 63;
    public const uint rocksdb_env_link_file_nanos = 64;
    public const uint rocksdb_env_lock_file_nanos = 65;
    public const uint rocksdb_env_unlock_file_nanos = 66;
    public const uint rocksdb_env_new_logger_nanos = 67;
    public const uint rocksdb_number_async_seek = 68;
    public const uint rocksdb_blob_cache_hit_count = 69;
    public const uint rocksdb_blob_read_count = 70;
    public const uint rocksdb_blob_read_byte = 71;
    public const uint rocksdb_blob_read_time = 72;
    public const uint rocksdb_blob_checksum_time = 73;
    public const uint rocksdb_blob_decompress_time = 74;
    public const uint rocksdb_internal_range_del_reseek_count = 75;
    public const uint rocksdb_block_read_cpu_time = 76;
    public const uint rocksdb_internal_merge_point_lookup_count = 77;
    public const uint rocksdb_data_block_read_byte = 78;
    public const uint rocksdb_index_block_read_byte = 79;
    public const uint rocksdb_filter_block_read_byte = 80;
    public const uint rocksdb_compression_dict_block_read_byte = 81;
    public const uint rocksdb_metadata_block_read_byte = 82;
    public const uint rocksdb_blob_cache_read_byte = 83;
    public const uint rocksdb_total_metric_count = 86;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_perf_level(int param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_perfcontext_t* rocksdb_perfcontext_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_perfcontext_reset(rocksdb_perfcontext_t* context);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_perfcontext_report(rocksdb_perfcontext_t* context, [NativeTypeName("unsigned char")] byte exclude_zero_counters);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_perfcontext_metric(rocksdb_perfcontext_t* context, int metric);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_perfcontext_destroy(rocksdb_perfcontext_t* context);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionfilter_t* rocksdb_compactionfilter_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("unsigned char (*)(void *, int, const char *, size_t, const char *, size_t, char **, size_t *, unsigned char *)")] delegate* unmanaged[Cdecl]<void*, int, sbyte*, nuint, sbyte*, nuint, sbyte**, nuint*, byte*, byte> filter, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionfilter_set_ignore_snapshots(rocksdb_compactionfilter_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionfilter_destroy(rocksdb_compactionfilter_t* param0);

    public const uint rocksdb_wal_filter_continue_processing = 0;
    public const uint rocksdb_wal_filter_ignore_current_record = 1;
    public const uint rocksdb_wal_filter_stop_replay = 2;
    public const uint rocksdb_wal_filter_corrupted_record = 3;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_walfilter_t* rocksdb_walfilter_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("rocksdb_walfilter_column_family_log_number_map_cb")] delegate* unmanaged[Cdecl]<void*, uint*, ulong*, nuint, sbyte**, nuint*, uint*, nuint, void> column_family_log_number_map, [NativeTypeName("rocksdb_walfilter_log_record_found_cb")] delegate* unmanaged[Cdecl]<void*, ulong, sbyte*, nuint, rocksdb_writebatch_t*, rocksdb_writebatch_t*, byte*, int> log_record_found, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_walfilter_destroy(rocksdb_walfilter_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionfiltercontext_is_full_compaction(rocksdb_compactionfiltercontext_t* context);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionfiltercontext_is_manual_compaction(rocksdb_compactionfiltercontext_t* context);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionfilterfactory_t* rocksdb_compactionfilterfactory_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("rocksdb_compactionfilter_t *(*)(void *, rocksdb_compactionfiltercontext_t *)")] delegate* unmanaged[Cdecl]<void*, rocksdb_compactionfiltercontext_t*, rocksdb_compactionfilter_t*> create_compaction_filter, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionfilterfactory_destroy(rocksdb_compactionfilterfactory_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_comparator_t* rocksdb_comparator_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("int (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, int> compare, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_comparator_destroy(rocksdb_comparator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_comparator_t* rocksdb_comparator_with_ts_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("int (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, int> compare, [NativeTypeName("int (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, int> compare_ts, [NativeTypeName("int (*)(void *, const char *, size_t, unsigned char, const char *, size_t, unsigned char)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, byte, sbyte*, nuint, byte, int> compare_without_ts, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name, [NativeTypeName("size_t")] nuint timestamp_size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_filterpolicy_destroy(rocksdb_filterpolicy_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_bloom(double bits_per_key);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_bloom_full(double bits_per_key);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_ribbon(double bloom_equivalent_bits_per_key);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_ribbon_hybrid(double bloom_equivalent_bits_per_key, int bloom_before_level);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_mergeoperator_t* rocksdb_mergeoperator_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("char *(*)(void *, const char *, size_t, const char *, size_t, const char *const *, const size_t *, int, unsigned char *, size_t *)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, sbyte**, nuint*, int, byte*, nuint*, sbyte*> full_merge, [NativeTypeName("char *(*)(void *, const char *, size_t, const char *const *, const size_t *, int, unsigned char *, size_t *)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte**, nuint*, int, byte*, nuint*, sbyte*> partial_merge, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> delete_value, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_mergeoperator_destroy(rocksdb_mergeoperator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_readoptions_t* rocksdb_readoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_destroy(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_deadline(rocksdb_readoptions_t* opt, [NativeTypeName("uint64_t")] ulong microseconds);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_readoptions_get_deadline(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_io_timeout(rocksdb_readoptions_t* opt, [NativeTypeName("uint64_t")] ulong microseconds);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_readoptions_get_io_timeout(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_read_tier(rocksdb_readoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_readoptions_get_read_tier(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_rate_limiter_priority(rocksdb_readoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_readoptions_get_rate_limiter_priority(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_value_size_soft_limit(rocksdb_readoptions_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_readoptions_get_value_size_soft_limit(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_verify_checksums(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_verify_checksums(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_fill_cache(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_fill_cache(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_ignore_range_deletions(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_ignore_range_deletions(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_async_io(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_async_io(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_optimize_multiget_for_io(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_optimize_multiget_for_io(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_readahead_size(rocksdb_readoptions_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_readoptions_get_readahead_size(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_max_skippable_internal_keys(rocksdb_readoptions_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_readoptions_get_max_skippable_internal_keys(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_tailing(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_tailing(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_total_order_seek(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_total_order_seek(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_auto_prefix_mode(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_auto_prefix_mode(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_prefix_same_as_start(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_prefix_same_as_start(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_pin_data(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_pin_data(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_adaptive_readahead(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_adaptive_readahead(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_background_purge_on_iterator_cleanup(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_background_purge_on_iterator_cleanup(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_auto_readahead_size(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_auto_readahead_size(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_allow_unprepared_value(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_allow_unprepared_value(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_auto_refresh_iterator_with_snapshot(rocksdb_readoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_auto_refresh_iterator_with_snapshot(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_io_activity(rocksdb_readoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_readoptions_get_io_activity(rocksdb_readoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_snapshot(rocksdb_readoptions_t* param0, [NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_iterate_upper_bound(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_iterate_lower_bound(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_timestamp(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_iter_start_ts(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_merge_operand_count_threshold(rocksdb_readoptions_t* param0, [NativeTypeName("size_t")] nuint threshold);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_clear_merge_operand_count_threshold(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_has_merge_operand_count_threshold(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_readoptions_get_merge_operand_count_threshold(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_request_id(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* request_id, [NativeTypeName("size_t")] nuint request_id_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_clear_request_id(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_readoptions_get_request_id(rocksdb_readoptions_t* param0, [NativeTypeName("size_t *")] nuint* request_id_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_table_filter(rocksdb_readoptions_t* param0, void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("rocksdb_readoptions_table_filter_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_table_properties_t*, byte> table_filter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_clear_table_filter(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_has_table_filter([NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_table_index_factory_from_string(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* value, [NativeTypeName("size_t")] nuint value_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_clear_table_index_factory(rocksdb_readoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_readoptions_get_table_index_factory_name([NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* param0, [NativeTypeName("size_t *")] nuint* name_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writeoptions_t* rocksdb_writeoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_destroy(rocksdb_writeoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compaction_options_t* rocksdb_compaction_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_destroy(rocksdb_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char *")]
    public static extern byte* rocksdb_compaction_options_canceled_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_canceled_destroy([NativeTypeName("unsigned char *")] byte* canceled);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_canceled_set([NativeTypeName("unsigned char *")] byte* canceled, [NativeTypeName("unsigned char")] byte value);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_set_canceled(rocksdb_compaction_options_t* options, [NativeTypeName("unsigned char *")] byte* canceled);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactoptions_t* rocksdb_compactoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_destroy(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_exclusive_manual_compaction(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_exclusive_manual_compaction(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_bottommost_level_compaction(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_bottommost_level_compaction(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_change_level(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_change_level(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_target_level(rocksdb_compactoptions_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_target_level(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_target_path_id(rocksdb_compactoptions_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_target_path_id(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_allow_write_stall(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_allow_write_stall(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_max_subcompactions(rocksdb_compactoptions_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_max_subcompactions(rocksdb_compactoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_full_history_ts_low(rocksdb_compactoptions_t* param0, [NativeTypeName("char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_flushoptions_t* rocksdb_flushoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_destroy(rocksdb_flushoptions_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_flushwaloptions_t* rocksdb_flushwaloptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushwaloptions_destroy(rocksdb_flushwaloptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_wal_with_options(rocksdb_t* db, [NativeTypeName("const rocksdb_flushwaloptions_t *")] rocksdb_flushwaloptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_memory_allocator_t* rocksdb_jemalloc_nodump_allocator_create([NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_allocator_destroy(rocksdb_memory_allocator_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_lru_cache_options_t* rocksdb_lru_cache_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_destroy(rocksdb_lru_cache_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_set_capacity(rocksdb_lru_cache_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_set_num_shard_bits(rocksdb_lru_cache_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_set_memory_allocator(rocksdb_lru_cache_options_t* param0, rocksdb_memory_allocator_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_lru([NativeTypeName("size_t")] nuint capacity);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_lru_with_strict_capacity_limit([NativeTypeName("size_t")] nuint capacity);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_lru_opts([NativeTypeName("const rocksdb_lru_cache_options_t *")] rocksdb_lru_cache_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cache_destroy(rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cache_disown_data(rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cache_set_capacity(rocksdb_cache_t* cache, [NativeTypeName("size_t")] nuint capacity);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_capacity([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_usage([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_pinned_usage([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_table_address_count([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_occupancy_count([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_write_buffer_manager_t* rocksdb_write_buffer_manager_create([NativeTypeName("size_t")] nuint buffer_size, bool allow_stall);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_write_buffer_manager_t* rocksdb_write_buffer_manager_create_with_cache([NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache, bool allow_stall);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_buffer_manager_destroy(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern bool rocksdb_write_buffer_manager_enabled(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern bool rocksdb_write_buffer_manager_cost_to_cache(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_memory_usage(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_mutable_memtable_memory_usage(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_dummy_entries_in_cache_usage(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_buffer_size(rocksdb_write_buffer_manager_t* wbm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_buffer_manager_set_buffer_size(rocksdb_write_buffer_manager_t* wbm, [NativeTypeName("size_t")] nuint new_size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_buffer_manager_set_allow_stall(rocksdb_write_buffer_manager_t* wbm, bool new_allow_stall);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sst_file_manager_t* rocksdb_sst_file_manager_create(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_destroy(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_max_allowed_space_usage(rocksdb_sst_file_manager_t* sfm, [NativeTypeName("uint64_t")] ulong max_allowed_space);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_compaction_buffer_size(rocksdb_sst_file_manager_t* sfm, [NativeTypeName("uint64_t")] ulong compaction_buffer_size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern bool rocksdb_sst_file_manager_is_max_allowed_space_reached(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern bool rocksdb_sst_file_manager_is_max_allowed_space_reached_including_compactions(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_sst_file_manager_get_total_size(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_sst_file_manager_get_delete_rate_bytes_per_second(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_delete_rate_bytes_per_second(rocksdb_sst_file_manager_t* sfm, [NativeTypeName("int64_t")] long delete_rate);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_sst_file_manager_get_max_trash_db_ratio(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_max_trash_db_ratio(rocksdb_sst_file_manager_t* sfm, double ratio);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_sst_file_manager_get_total_trash_size(rocksdb_sst_file_manager_t* sfm);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_hyper_clock_cache_options_t* rocksdb_hyper_clock_cache_options_create([NativeTypeName("size_t")] nuint capacity, [NativeTypeName("size_t")] nuint estimated_entry_charge);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_destroy(rocksdb_hyper_clock_cache_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_capacity(rocksdb_hyper_clock_cache_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_estimated_entry_charge(rocksdb_hyper_clock_cache_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_num_shard_bits(rocksdb_hyper_clock_cache_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_memory_allocator(rocksdb_hyper_clock_cache_options_t* param0, rocksdb_memory_allocator_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_hyper_clock([NativeTypeName("size_t")] nuint capacity, [NativeTypeName("size_t")] nuint estimated_entry_charge);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_hyper_clock_opts([NativeTypeName("const rocksdb_hyper_clock_cache_options_t *")] rocksdb_hyper_clock_cache_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_dbpath_t* rocksdb_dbpath_create([NativeTypeName("const char *")] sbyte* path, [NativeTypeName("uint64_t")] ulong target_size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_dbpath_destroy(rocksdb_dbpath_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_env_t* rocksdb_create_default_env();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_env_t* rocksdb_create_mem_env();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_background_threads(rocksdb_env_t* env, int n);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_background_threads(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_high_priority_background_threads(rocksdb_env_t* env, int n);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_high_priority_background_threads(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_low_priority_background_threads(rocksdb_env_t* env, int n);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_low_priority_background_threads(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_bottom_priority_background_threads(rocksdb_env_t* env, int n);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_bottom_priority_background_threads(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_join_all_threads(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_thread_pool_io_priority(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_high_priority_thread_pool_io_priority(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_thread_pool_cpu_priority(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_high_priority_thread_pool_cpu_priority(rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_destroy(rocksdb_env_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_envoptions_t* rocksdb_envoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_destroy(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_rate_limiter(rocksdb_envoptions_t* opt, rocksdb_ratelimiter_t* rate_limiter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_dir_if_missing(rocksdb_env_t* env, [NativeTypeName("const char *")] sbyte* path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_trace_options_t* rocksdb_trace_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_options_destroy(rocksdb_trace_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_trace_reader_t* rocksdb_trace_reader_create(rocksdb_env_t* env, [NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env_options, [NativeTypeName("const char *")] sbyte* trace_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_trace_reader_read(rocksdb_trace_reader_t* reader, [NativeTypeName("size_t *")] nuint* size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_reader_reset(rocksdb_trace_reader_t* reader, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_reader_close(rocksdb_trace_reader_t* reader, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_reader_destroy(rocksdb_trace_reader_t* reader);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_replay_options_t* rocksdb_replay_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_replay_options_destroy(rocksdb_replay_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_replay_options_set_num_threads(rocksdb_replay_options_t* options, [NativeTypeName("uint32_t")] uint num_threads);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_replay_options_get_num_threads(rocksdb_replay_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_replay_options_set_fast_forward(rocksdb_replay_options_t* options, double fast_forward);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_replay_options_get_fast_forward(rocksdb_replay_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_replayer_t* rocksdb_new_default_replayer(rocksdb_t* db, rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_column_families, rocksdb_env_t* env, [NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env_options, [NativeTypeName("const char *")] sbyte* trace_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_replayer_prepare(rocksdb_replayer_t* replayer, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_replayer_get_header_timestamp([NativeTypeName("const rocksdb_replayer_t *")] rocksdb_replayer_t* replayer);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_replayer_replay(rocksdb_replayer_t* replayer, [NativeTypeName("const rocksdb_replay_options_t *")] rocksdb_replay_options_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_replayer_destroy(rocksdb_replayer_t* replayer);

    public const uint rocksdb_trace_filter_none = 0x0;
    public const uint rocksdb_trace_filter_get = 0x1 << 0;
    public const uint rocksdb_trace_filter_write = 0x1 << 1;
    public const uint rocksdb_trace_filter_iterator_seek = 0x1 << 2;
    public const uint rocksdb_trace_filter_iterator_seek_for_prev = 0x1 << 3;
    public const uint rocksdb_trace_filter_multi_get = 0x1 << 4;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_block_cache_trace_options_t* rocksdb_block_cache_trace_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_cache_trace_options_destroy(rocksdb_block_cache_trace_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_block_cache_trace_writer_options_t* rocksdb_block_cache_trace_writer_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_cache_trace_writer_options_destroy(rocksdb_block_cache_trace_writer_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_start_trace(rocksdb_t* db, rocksdb_env_t* env, [NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env_options, [NativeTypeName("const rocksdb_trace_options_t *")] rocksdb_trace_options_t* options, [NativeTypeName("const char *")] sbyte* trace_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_end_trace(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_start_io_trace(rocksdb_t* db, rocksdb_env_t* env, [NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env_options, [NativeTypeName("const rocksdb_trace_options_t *")] rocksdb_trace_options_t* options, [NativeTypeName("const char *")] sbyte* trace_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_end_io_trace(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_start_block_cache_trace(rocksdb_t* db, rocksdb_env_t* env, [NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env_options, [NativeTypeName("const rocksdb_trace_options_t *")] rocksdb_trace_options_t* options, [NativeTypeName("const char *")] sbyte* trace_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_start_block_cache_trace_with_options(rocksdb_t* db, rocksdb_env_t* env, [NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env_options, [NativeTypeName("const rocksdb_block_cache_trace_options_t *")] rocksdb_block_cache_trace_options_t* options, [NativeTypeName("const rocksdb_block_cache_trace_writer_options_t *")] rocksdb_block_cache_trace_writer_options_t* writer_options, [NativeTypeName("const char *")] sbyte* trace_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_end_block_cache_trace(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sstfilewriter_t* rocksdb_sstfilewriter_create([NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* io_options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sstfilewriter_t* rocksdb_sstfilewriter_create_with_comparator([NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* io_options, [NativeTypeName("const rocksdb_comparator_t *")] rocksdb_comparator_t* comparator);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_open(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_add(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_put(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_put_with_ts(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_merge(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_delete(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_delete_with_ts(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_delete_range(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* begin_key, [NativeTypeName("size_t")] nuint begin_keylen, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_finish(rocksdb_sstfilewriter_t* writer, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_file_size(rocksdb_sstfilewriter_t* writer, [NativeTypeName("uint64_t *")] ulong* file_size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_destroy(rocksdb_sstfilewriter_t* writer);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ingestexternalfileoptions_t* rocksdb_ingestexternalfileoptions_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_move_files(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte move_files);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_snapshot_consistency(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte snapshot_consistency);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_allow_global_seqno(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte allow_global_seqno);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_allow_blocking_flush(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte allow_blocking_flush);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_ingest_behind(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte ingest_behind);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_fail_if_not_bottommost_level(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte fail_if_not_bottommost_level);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_destroy(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingest_external_file(rocksdb_t* db, [NativeTypeName("const char *const *")] sbyte** file_list, [NativeTypeName("const size_t")] nuint list_len, [NativeTypeName("const rocksdb_ingestexternalfileoptions_t *")] rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingest_external_file_cf(rocksdb_t* db, rocksdb_column_family_handle_t* handle, [NativeTypeName("const char *const *")] sbyte** file_list, [NativeTypeName("const size_t")] nuint list_len, [NativeTypeName("const rocksdb_ingestexternalfileoptions_t *")] rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_try_catch_up_with_primary(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slicetransform_t* rocksdb_slicetransform_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("char *(*)(void *, const char *, size_t, size_t *)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, nuint*, sbyte*> transform, [NativeTypeName("unsigned char (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, byte> in_domain, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slicetransform_t* rocksdb_slicetransform_create_fixed_prefix([NativeTypeName("size_t")] nuint param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slicetransform_t* rocksdb_slicetransform_create_noop();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_slicetransform_destroy(rocksdb_slicetransform_t* param0);

    public const uint rocksdb_similar_size_compaction_stop_style = 0;
    public const uint rocksdb_total_size_compaction_stop_style = 1;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_universal_compaction_options_t* rocksdb_universal_compaction_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_size_ratio(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_size_ratio(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_min_merge_width(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_min_merge_width(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_max_merge_width(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_max_merge_width(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_max_size_amplification_percent(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_max_size_amplification_percent(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_compression_size_percent(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_compression_size_percent(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_stop_style(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_stop_style(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_destroy(rocksdb_universal_compaction_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_fifo_compaction_options_t* rocksdb_fifo_compaction_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_allow_compaction(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("unsigned char")] byte allow_compaction);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_fifo_compaction_options_get_allow_compaction(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_max_table_files_size(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("uint64_t")] ulong size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_fifo_compaction_options_get_max_table_files_size(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_max_data_files_size(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("uint64_t")] ulong size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_fifo_compaction_options_get_max_data_files_size(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_use_kv_ratio_compaction(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("unsigned char")] byte use_kv_ratio_compaction);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_fifo_compaction_options_get_use_kv_ratio_compaction(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_destroy(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefiles_t* rocksdb_livefiles_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_livefiles_count([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_column_family_name([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_name([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_directory([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_livefiles_level([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_livefiles_size([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_smallestkey([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_largestkey([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_smallest_seqno([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_largest_seqno([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_entries([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_deletions([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_destroy([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefiles_storage_info_options_t* rocksdb_livefiles_storage_info_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_storage_info_options_destroy(rocksdb_livefiles_storage_info_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefiles_storage_info_t* rocksdb_get_livefiles_storage_info(rocksdb_t* db, [NativeTypeName("const rocksdb_livefiles_storage_info_options_t *")] rocksdb_livefiles_storage_info_options_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_livefiles_storage_info_count([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_storage_info_relative_filename([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_storage_info_directory([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_storage_info_file_number([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_livefiles_storage_info_file_type([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_storage_info_size([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_livefiles_storage_info_temperature([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_storage_info_file_checksum([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_storage_info_file_checksum_func_name([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_storage_info_replacement_contents([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_livefiles_storage_info_trim_to_size([NativeTypeName("const rocksdb_livefiles_storage_info_t *")] rocksdb_livefiles_storage_info_t* info, [NativeTypeName("size_t")] nuint index);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_storage_info_destroy(rocksdb_livefiles_storage_info_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefile_t* rocksdb_livefile_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_column_family_name(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_level(rocksdb_livefile_t* param0, int param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_name(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_directory(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_size(rocksdb_livefile_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_smallest_key(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1, [NativeTypeName("size_t")] nuint param2);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_largest_key(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1, [NativeTypeName("size_t")] nuint param2);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_smallest_seqno(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_largest_seqno(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_num_entries(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_num_deletions(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] ulong param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_destroy(rocksdb_livefile_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_add(rocksdb_livefiles_t* param0, rocksdb_livefile_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_get_options_from_string([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* base_options, [NativeTypeName("const char *")] sbyte* opts_str, rocksdb_options_t* new_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_file_in_range(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_file_in_range_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_t* rocksdb_get_column_family_metadata(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_t* rocksdb_get_column_family_metadata_with_options(rocksdb_t* db, [NativeTypeName("const rocksdb_column_family_metadata_options_t *")] rocksdb_column_family_metadata_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_options_t* rocksdb_column_family_metadata_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_metadata_options_destroy(rocksdb_column_family_metadata_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_metadata_options_set_start_key(rocksdb_column_family_metadata_options_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_column_family_metadata_options_get_start_key([NativeTypeName("const rocksdb_column_family_metadata_options_t *")] rocksdb_column_family_metadata_options_t* options, [NativeTypeName("size_t *")] nuint* key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_metadata_options_set_end_key(rocksdb_column_family_metadata_options_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_column_family_metadata_options_get_end_key([NativeTypeName("const rocksdb_column_family_metadata_options_t *")] rocksdb_column_family_metadata_options_t* options, [NativeTypeName("size_t *")] nuint* key_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_import_column_family_options_t* rocksdb_import_column_family_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_import_column_family_options_set_move_files(rocksdb_import_column_family_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_import_column_family_options_destroy(rocksdb_import_column_family_options_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_export_import_files_metadata_t* rocksdb_export_import_files_metadata_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_export_import_files_metadata_get_db_comparator_name(rocksdb_export_import_files_metadata_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_export_import_files_metadata_set_db_comparator_name(rocksdb_export_import_files_metadata_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefiles_t* rocksdb_export_import_files_metadata_get_files(rocksdb_export_import_files_metadata_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_export_import_files_metadata_set_files(rocksdb_export_import_files_metadata_t* param0, rocksdb_livefiles_t* param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_export_import_files_metadata_destroy(rocksdb_export_import_files_metadata_t* param0);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_t* rocksdb_get_column_family_metadata_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_t* rocksdb_get_column_family_metadata_cf_with_options(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const rocksdb_column_family_metadata_options_t *")] rocksdb_column_family_metadata_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_metadata_destroy(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_column_family_metadata_get_size(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_column_family_metadata_get_file_count(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_column_family_metadata_get_name(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_column_family_metadata_get_level_count(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_level_metadata_t* rocksdb_column_family_metadata_get_level_metadata(rocksdb_column_family_metadata_t* cf_meta, [NativeTypeName("size_t")] nuint i);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_level_metadata_destroy(rocksdb_level_metadata_t* level_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_level_metadata_get_level(rocksdb_level_metadata_t* level_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_level_metadata_get_size(rocksdb_level_metadata_t* level_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_level_metadata_get_file_count(rocksdb_level_metadata_t* level_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sst_file_metadata_t* rocksdb_level_metadata_get_sst_file_metadata(rocksdb_level_metadata_t* level_meta, [NativeTypeName("size_t")] nuint i);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_metadata_destroy(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_relative_filename(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_directory(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_sst_file_metadata_get_size(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_smallestkey(rocksdb_sst_file_metadata_t* file_meta, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_largestkey(rocksdb_sst_file_metadata_t* file_meta, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_transactiondb_create_column_family(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transactiondb_t* rocksdb_transactiondb_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const rocksdb_transactiondb_options_t *")] rocksdb_transactiondb_options_t* txn_db_options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transactiondb_t* rocksdb_transactiondb_open_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const rocksdb_transactiondb_options_t *")] rocksdb_transactiondb_options_t* txn_db_options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_snapshot_t *")]
    public static extern rocksdb_snapshot_t* rocksdb_transactiondb_create_snapshot(rocksdb_transactiondb_t* txn_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_release_snapshot(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* snapshot);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transactiondb_property_value(rocksdb_transactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_transactiondb_property_int(rocksdb_transactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] ulong* out_val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_transactiondb_get_base_db(rocksdb_transactiondb_t* txn_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_close_base_db(rocksdb_t* base_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_t* rocksdb_transaction_begin(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* write_options, [NativeTypeName("const rocksdb_transaction_options_t *")] rocksdb_transaction_options_t* txn_options, rocksdb_transaction_t* old_txn);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_t** rocksdb_transactiondb_get_prepared_transactions(rocksdb_transactiondb_t* txn_db, [NativeTypeName("size_t *")] nuint* cnt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_name(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("size_t")] nuint name_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_name(rocksdb_transaction_t* txn, [NativeTypeName("size_t *")] nuint* name_len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_prepare(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_put(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_put_cf(rocksdb_transaction_t* txn, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_merge(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_merge_cf(rocksdb_transaction_t* txn, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_delete(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_delete_cf(rocksdb_transaction_t* txn, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_commit(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rollback(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_put_log_data(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* blob, [NativeTypeName("size_t")] nuint len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_savepoint(rocksdb_transaction_t* txn);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rollback_to_savepoint(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_destroy(rocksdb_transaction_t* txn);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_transaction_get_writebatch_wi(rocksdb_transaction_t* txn);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rebuild_from_writebatch(rocksdb_transaction_t* txn, rocksdb_writebatch_t* writebatch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rebuild_from_writebatch_wi(rocksdb_transaction_t* txn, rocksdb_writebatch_wi_t* wi, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_commit_timestamp(rocksdb_transaction_t* txn, [NativeTypeName("uint64_t")] ulong commit_timestamp);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_read_timestamp_for_validation(rocksdb_transaction_t* txn, [NativeTypeName("uint64_t")] ulong read_timestamp);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_snapshot_t *")]
    public static extern rocksdb_snapshot_t* rocksdb_transaction_get_snapshot(rocksdb_transaction_t* txn);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_for_update(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned_for_update(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_for_update_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned_for_update_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get_for_update(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get_for_update_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transactiondb_get(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transactiondb_get_pinned(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transactiondb_get_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transactiondb_get_pinned_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_multi_get(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_multi_get_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_put(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_put_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_write(rocksdb_transactiondb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_t* batch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_merge(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_merge_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_delete(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_delete_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_wal(rocksdb_transactiondb_t* txn_db, [NativeTypeName("unsigned char")] byte sync, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transaction_create_iterator(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transaction_create_iterator_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transactiondb_create_iterator(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transactiondb_create_iterator_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_close(rocksdb_transactiondb_t* txn_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_cfs(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t** column_families, int num_column_families, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_wal_with_options(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushwaloptions_t *")] rocksdb_flushwaloptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_checkpoint_t* rocksdb_transactiondb_checkpoint_object_create(rocksdb_transactiondb_t* txn_db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_t* rocksdb_optimistictransactiondb_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_t* rocksdb_optimistictransactiondb_open_with_otxn_db_options([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const rocksdb_optimistictransactiondb_options_t *")] rocksdb_optimistictransactiondb_options_t* otxn_db_options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_t* rocksdb_optimistictransactiondb_open_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_t* rocksdb_optimistictransactiondb_open_column_families_with_otxn_db_options([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const rocksdb_optimistictransactiondb_options_t *")] rocksdb_optimistictransactiondb_options_t* otxn_db_options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_optimistictransactiondb_get_base_db(rocksdb_optimistictransactiondb_t* otxn_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_close_base_db(rocksdb_t* base_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_t* rocksdb_optimistictransaction_begin(rocksdb_optimistictransactiondb_t* otxn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* write_options, [NativeTypeName("const rocksdb_optimistictransaction_options_t *")] rocksdb_optimistictransaction_options_t* otxn_options, rocksdb_transaction_t* old_txn);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_write(rocksdb_optimistictransactiondb_t* otxn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_t* batch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_close(rocksdb_optimistictransactiondb_t* otxn_db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_checkpoint_t* rocksdb_optimistictransactiondb_checkpoint_object_create(rocksdb_optimistictransactiondb_t* otxn_db, [NativeTypeName("char **")] sbyte** errptr);

    public const uint rocksdb_txndb_write_policy_write_committed = 0;
    public const uint rocksdb_txndb_write_policy_write_prepared = 1;
    public const uint rocksdb_txndb_write_policy_write_unprepared = 2;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transactiondb_options_t* rocksdb_transactiondb_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_destroy(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_options_t* rocksdb_transaction_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_destroy(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransaction_options_t* rocksdb_optimistictransaction_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_lock_buckets_t* rocksdb_optimistictransactiondb_lock_buckets_create([NativeTypeName("size_t")] nuint bucket_count, [NativeTypeName("unsigned char")] byte cache_aligned);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_optimistictransactiondb_lock_buckets_approximate_memory_usage(rocksdb_optimistictransactiondb_lock_buckets_t* lock_buckets);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_lock_buckets_destroy(rocksdb_optimistictransactiondb_lock_buckets_t* lock_buckets);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_options_t* rocksdb_optimistictransactiondb_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_options_destroy(rocksdb_optimistictransactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_options_set_shared_lock_buckets(rocksdb_optimistictransactiondb_options_t* opt, rocksdb_optimistictransactiondb_lock_buckets_t* lock_buckets);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransaction_options_destroy(rocksdb_optimistictransaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_optimistictransactiondb_property_value(rocksdb_optimistictransactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_optimistictransactiondb_property_int(rocksdb_optimistictransactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] ulong* out_val);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_free(void* ptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_get_pinned(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_get_pinned_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_pinnableslice_destroy(rocksdb_pinnableslice_t* v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_pinnableslice_value([NativeTypeName("const rocksdb_pinnableslice_t *")] rocksdb_pinnableslice_t* t, [NativeTypeName("size_t *")] nuint* vlen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_memory_consumers_t* rocksdb_memory_consumers_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_consumers_add_db(rocksdb_memory_consumers_t* consumers, rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_consumers_add_cache(rocksdb_memory_consumers_t* consumers, rocksdb_cache_t* cache);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_consumers_destroy(rocksdb_memory_consumers_t* consumers);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_memory_usage_t* rocksdb_approximate_memory_usage_create(rocksdb_memory_consumers_t* consumers, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_memory_usage_destroy(rocksdb_memory_usage_t* usage);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_approximate_memory_usage_get_mem_table_total(rocksdb_memory_usage_t* memory_usage);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_approximate_memory_usage_get_mem_table_unflushed(rocksdb_memory_usage_t* memory_usage);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_approximate_memory_usage_get_mem_table_readers_total(rocksdb_memory_usage_t* memory_usage);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_approximate_memory_usage_get_cache_total(rocksdb_memory_usage_t* memory_usage);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_dump_malloc_stats(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_whole_key_filtering(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cancel_all_background_work(rocksdb_t* db, [NativeTypeName("unsigned char")] byte wait);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_disable_manual_compaction(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_enable_manual_compaction(rocksdb_t* db);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_statistics_histogram_data_t* rocksdb_statistics_histogram_data_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_statistics_histogram_data_destroy(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_median(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_p95(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_p99(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_average(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_std_dev(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_max(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_statistics_histogram_data_get_count(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_statistics_histogram_data_get_sum(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_min(rocksdb_statistics_histogram_data_t* data);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact(rocksdb_t* db, rocksdb_wait_for_compact_options_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wait_for_compact_options_t* rocksdb_wait_for_compact_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_destroy(rocksdb_wait_for_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_abort_on_pause(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_abort_on_pause(rocksdb_wait_for_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_flush(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_flush(rocksdb_wait_for_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_close_db(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_close_db(rocksdb_wait_for_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_timeout(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("uint64_t")] ulong microseconds);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_wait_for_compact_options_get_timeout(rocksdb_wait_for_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnable_handle_t* rocksdb_get_pinned_v2(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnable_handle_t* rocksdb_get_pinned_cf_v2(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_pinnable_handle_get_value([NativeTypeName("const rocksdb_pinnable_handle_t *")] rocksdb_pinnable_handle_t* handle, [NativeTypeName("size_t *")] nuint* vallen);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_pinnable_handle_destroy(rocksdb_pinnable_handle_t* handle);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_get_into_buffer(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("unsigned char *")] byte* found, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_get_into_buffer_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("unsigned char *")] byte* found, [NativeTypeName("char **")] sbyte** errptr);

    public const uint rocksdb_compactionservice_jobstatus_success = 0;
    public const uint rocksdb_compactionservice_jobstatus_failure = 1;
    public const uint rocksdb_compactionservice_jobstatus_aborted = 2;
    public const uint rocksdb_compactionservice_jobstatus_use_local = 3;

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionservice_scheduleresponse_t* rocksdb_compactionservice_scheduleresponse_create([NativeTypeName("const char *")] sbyte* scheduled_job_id, int status, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionservice_scheduleresponse_t* rocksdb_compactionservice_scheduleresponse_create_with_status(int status, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_scheduleresponse_getstatus([NativeTypeName("const rocksdb_compactionservice_scheduleresponse_t *")] rocksdb_compactionservice_scheduleresponse_t* response);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_scheduleresponse_get_scheduled_job_id([NativeTypeName("const rocksdb_compactionservice_scheduleresponse_t *")] rocksdb_compactionservice_scheduleresponse_t* response, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionservice_scheduleresponse_t_destroy(rocksdb_compactionservice_scheduleresponse_t* response);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_db_name([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_db_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_db_session_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_cf_name([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionservice_jobinfo_t_get_cf_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compactionservice_jobinfo_t_get_job_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_priority([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_compaction_reason([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_base_input_level([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_output_level([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionservice_jobinfo_t_is_full_compaction([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionservice_jobinfo_t_is_manual_compaction([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionservice_jobinfo_t_is_bottommost_level([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionservice_t* rocksdb_compactionservice_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("rocksdb_compaction_service_schedule_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_compactionservice_jobinfo_t*, sbyte*, nuint, rocksdb_compactionservice_scheduleresponse_t*> schedule, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("rocksdb_compaction_service_wait_cb")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte**, nuint*, int> wait, [NativeTypeName("rocksdb_compaction_service_cancel_awaiting_jobs_cb")] delegate* unmanaged[Cdecl]<void*, void> cancel_awaiting_jobs, [NativeTypeName("rocksdb_compaction_service_on_installation_cb")] delegate* unmanaged[Cdecl]<void*, sbyte*, int, void> on_installation);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_service(rocksdb_options_t* options, rocksdb_compactionservice_t* service);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compaction_service_options_override_t* rocksdb_compaction_service_options_override_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compaction_service_options_override_t* rocksdb_compaction_service_options_override_create_from_options(rocksdb_options_t* option);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_destroy(rocksdb_compaction_service_options_override_t* override_options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_env(rocksdb_compaction_service_options_override_t* override_options, rocksdb_env_t* env);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_comparator(rocksdb_compaction_service_options_override_t* override_options, rocksdb_comparator_t* comparator);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_merge_operator(rocksdb_compaction_service_options_override_t* override_options, rocksdb_mergeoperator_t* merge_operator);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_compaction_filter(rocksdb_compaction_service_options_override_t* override_options, rocksdb_compactionfilter_t* compaction_filter);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_compaction_filter_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_compactionfilterfactory_t* compaction_filter_factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_prefix_extractor(rocksdb_compaction_service_options_override_t* override_options, rocksdb_slicetransform_t* prefix_extractor);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_block_based_table_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_block_based_table_options_t* table_options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_cuckoo_table_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_cuckoo_table_options_t* table_options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_add_event_listener(rocksdb_compaction_service_options_override_t* override_options, rocksdb_eventlistener_t* event_listener);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_statistics(rocksdb_compaction_service_options_override_t* override_options, rocksdb_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_info_log(rocksdb_compaction_service_options_override_t* override_options, rocksdb_logger_t* logger);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_option(rocksdb_compaction_service_options_override_t* override_options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char *")] sbyte* value);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_file_checksum_gen_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_file_checksum_gen_factory_t* factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_sst_partitioner_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_sst_partitioner_factory_t* factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_add_table_properties_collector_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_table_properties_collector_factory_t* factory);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char *")]
    public static extern byte* rocksdb_open_and_compact_canceled_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_canceled_destroy([NativeTypeName("unsigned char *")] byte* canceled);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_canceled_set([NativeTypeName("unsigned char *")] byte* canceled, [NativeTypeName("unsigned char")] byte value);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_open_and_compact_options_t* rocksdb_open_and_compact_options_create();

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_options_destroy(rocksdb_open_and_compact_options_t* options);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_options_set_canceled(rocksdb_open_and_compact_options_t* options, [NativeTypeName("unsigned char *")] byte* canceled);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_options_set_allow_resumption(rocksdb_open_and_compact_options_t* options, [NativeTypeName("unsigned char")] byte allow_resumption);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_open_and_compact([NativeTypeName("const char *")] sbyte* db_path, [NativeTypeName("const char *")] sbyte* output_directory, [NativeTypeName("const char *")] sbyte* input, [NativeTypeName("size_t")] nuint input_len, [NativeTypeName("size_t *")] nuint* output_len, [NativeTypeName("const rocksdb_compaction_service_options_override_t *")] rocksdb_compaction_service_options_override_t* override_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_open_and_compact_with_options([NativeTypeName("const rocksdb_open_and_compact_options_t *")] rocksdb_open_and_compact_options_t* options, [NativeTypeName("const char *")] sbyte* db_path, [NativeTypeName("const char *")] sbyte* output_directory, [NativeTypeName("const char *")] sbyte* input, [NativeTypeName("size_t")] nuint input_len, [NativeTypeName("size_t *")] nuint* output_len, [NativeTypeName("const rocksdb_compaction_service_options_override_t *")] rocksdb_compaction_service_options_override_t* override_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_flush_verify_memtable_count(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_flush_verify_memtable_count(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_verify_record_count(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_compaction_verify_record_count(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_track_and_verify_wals(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_track_and_verify_wals(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_verify_sst_unique_id_in_manifest(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_verify_sst_unique_id_in_manifest(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_read_io_executor_threads(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_read_io_executor_threads(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_options_get_db_log_dir(rocksdb_options_t* opt, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_options_get_wal_dir(rocksdb_options_t* opt, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_async_wal_precreate(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_async_wal_precreate(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_verify_manifest_content_on_close(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_verify_manifest_content_on_close(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_optimize_manifest_for_recovery(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_optimize_manifest_for_recovery(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_reuse_manifest_on_open(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_reuse_manifest_on_open(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_manifest_space_amp_pct(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_manifest_space_amp_pct(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_direct_io_for_compaction_reads(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_direct_io_for_compaction_reads(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_fallocate(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_fallocate(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_persist_stats_to_disk(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_persist_stats_to_disk(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_stats_history_buffer_size(rocksdb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_stats_history_buffer_size(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_strict_bytes_per_sync(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_strict_bytes_per_sync(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_thread_tracking(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_thread_tracking(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_delayed_write_rate(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_delayed_write_rate(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_write_batch_group_size_bytes(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_max_write_batch_group_size_bytes(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_thread_max_yield_usec(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_write_thread_max_yield_usec(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_thread_slow_yield_usec(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_write_thread_slow_yield_usec(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_2pc(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_2pc(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_dump_malloc_stats(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_avoid_flush_during_recovery(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_avoid_flush_during_recovery(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enforce_write_buffer_manager_during_recovery(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enforce_write_buffer_manager_during_recovery(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_avoid_flush_during_shutdown(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_avoid_flush_during_shutdown(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_two_write_queues(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_two_write_queues(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_background_close_inactive_wals(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_background_close_inactive_wals(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_prefix_seek_opt_in_only(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_prefix_seek_opt_in_only(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_log_readahead_size(rocksdb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_log_readahead_size(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_best_efforts_recovery(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_best_efforts_recovery(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bgerror_resume_count(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_bgerror_resume_count(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bgerror_resume_retry_interval(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_bgerror_resume_retry_interval(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_data_in_errors(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_data_in_errors(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_host_id(rocksdb_options_t* opt, [NativeTypeName("const char *")] sbyte* v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_options_get_db_host_id(rocksdb_options_t* opt, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_lowest_used_cache_tier(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_lowest_used_cache_tier(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enforce_single_del_contracts(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enforce_single_del_contracts(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_daily_offpeak_time_utc(rocksdb_options_t* opt, [NativeTypeName("const char *")] sbyte* v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_options_get_daily_offpeak_time_utc(rocksdb_options_t* opt, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_follower_refresh_catchup_period_ms(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_follower_refresh_catchup_period_ms(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_follower_catchup_retry_count(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_follower_catchup_retry_count(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_follower_catchup_retry_wait_ms(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_follower_catchup_retry_wait_ms(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_metadata_write_temperature(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_metadata_write_temperature(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_write_temperature(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_wal_write_temperature(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_fast_sst_open(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_fast_sst_open(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_memtable_whole_key_filtering(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_target_file_size_is_upper_bound(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_target_file_size_is_upper_bound(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_strict_max_successive_merges(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_strict_max_successive_merges(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_paranoid_file_checks(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_paranoid_file_checks(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_verify_output_flags(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_verify_output_flags(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_force_consistency_checks(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_force_consistency_checks(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_disallow_memtable_writes(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_disallow_memtable_writes(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_sample_for_compression(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_sample_for_compression(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_last_level_temperature(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_last_level_temperature(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_default_write_temperature(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_default_write_temperature(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_default_temperature(rocksdb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_default_temperature(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_preclude_last_level_data_seconds(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_preclude_last_level_data_seconds(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_preserve_internal_time_seconds(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_options_get_preserve_internal_time_seconds(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_blob_direct_write(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_blob_direct_write(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_direct_write_partitions(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_blob_direct_write_partitions(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_protection_bytes_per_key(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_memtable_protection_bytes_per_key(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_persist_user_defined_timestamps(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_persist_user_defined_timestamps(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_block_protection_bytes_per_key(rocksdb_options_t* opt, [NativeTypeName("uint8_t")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint8_t")]
    public static extern byte rocksdb_options_get_block_protection_bytes_per_key(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_file_compaction_delay(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_bottommost_file_compaction_delay(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_paranoid_memory_checks(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_paranoid_memory_checks(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_verify_per_key_checksum_on_seek(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_memtable_verify_per_key_checksum_on_seek(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_cf_allow_ingest_behind(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_cf_allow_ingest_behind(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_batch_lookup_optimization(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_memtable_batch_lookup_optimization(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_max_range_deletions(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_memtable_max_range_deletions(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_uncache_aggressiveness(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_uncache_aggressiveness(rocksdb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_sync(rocksdb_writeoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_sync(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_disable_WAL(rocksdb_writeoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_disable_WAL(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_ignore_missing_column_families(rocksdb_writeoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_ignore_missing_column_families(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_no_slowdown(rocksdb_writeoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_no_slowdown(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_low_pri(rocksdb_writeoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_low_pri(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_memtable_insert_hint_per_batch(rocksdb_writeoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_memtable_insert_hint_per_batch(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_rate_limiter_priority(rocksdb_writeoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_writeoptions_get_rate_limiter_priority(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_protection_bytes_per_key(rocksdb_writeoptions_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_writeoptions_get_protection_bytes_per_key(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_io_activity(rocksdb_writeoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_writeoptions_get_io_activity(rocksdb_writeoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_set_wait(rocksdb_flushoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushoptions_get_wait(rocksdb_flushoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_set_allow_write_stall(rocksdb_flushoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushoptions_get_allow_write_stall(rocksdb_flushoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_set_force_atomic_flush(rocksdb_flushoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushoptions_get_force_atomic_flush(rocksdb_flushoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_set_listener_wait(rocksdb_flushoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushoptions_get_listener_wait(rocksdb_flushoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushwaloptions_set_sync(rocksdb_flushwaloptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushwaloptions_get_sync(rocksdb_flushwaloptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushwaloptions_set_rate_limiter_priority(rocksdb_flushwaloptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_flushwaloptions_get_rate_limiter_priority(rocksdb_flushwaloptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_use_mmap_reads(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_use_mmap_reads(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_use_mmap_writes(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_use_mmap_writes(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_use_direct_reads(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_use_direct_reads(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_use_direct_writes(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_use_direct_writes(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_allow_fallocate(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_allow_fallocate(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_fd_cloexec(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_fd_cloexec(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_bytes_per_sync(rocksdb_envoptions_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_envoptions_get_bytes_per_sync(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_strict_bytes_per_sync(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_strict_bytes_per_sync(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_fallocate_with_keep_size(rocksdb_envoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_envoptions_get_fallocate_with_keep_size(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_compaction_readahead_size(rocksdb_envoptions_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_envoptions_get_compaction_readahead_size(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_set_writable_file_max_buffer_size(rocksdb_envoptions_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_envoptions_get_writable_file_max_buffer_size(rocksdb_envoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_options_set_max_trace_file_size(rocksdb_trace_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_trace_options_get_max_trace_file_size(rocksdb_trace_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_options_set_sampling_frequency(rocksdb_trace_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_trace_options_get_sampling_frequency(rocksdb_trace_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_options_set_filter(rocksdb_trace_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_trace_options_get_filter(rocksdb_trace_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_trace_options_set_preserve_write_order(rocksdb_trace_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_trace_options_get_preserve_write_order(rocksdb_trace_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_cache_trace_options_set_sampling_frequency(rocksdb_block_cache_trace_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_block_cache_trace_options_get_sampling_frequency(rocksdb_block_cache_trace_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_cache_trace_writer_options_set_max_trace_file_size(rocksdb_block_cache_trace_writer_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_block_cache_trace_writer_options_get_max_trace_file_size(rocksdb_block_cache_trace_writer_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_set_compression(rocksdb_compaction_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compaction_options_get_compression(rocksdb_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_set_output_file_size_limit(rocksdb_compaction_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_compaction_options_get_output_file_size_limit(rocksdb_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_set_max_subcompactions(rocksdb_compaction_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compaction_options_get_max_subcompactions(rocksdb_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_set_output_temperature_override(rocksdb_compaction_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compaction_options_get_output_temperature_override(rocksdb_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_options_set_allow_trivial_move(rocksdb_compaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compaction_options_get_allow_trivial_move(rocksdb_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_blob_garbage_collection_policy(rocksdb_compactoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_blob_garbage_collection_policy(rocksdb_compactoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_blob_garbage_collection_age_cutoff(rocksdb_compactoptions_t* opt, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_compactoptions_get_blob_garbage_collection_age_cutoff(rocksdb_compactoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_move_files(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_link_files(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_link_files(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_failed_move_fall_back_to_copy(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_failed_move_fall_back_to_copy(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_snapshot_consistency(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_allow_global_seqno(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_allow_blocking_flush(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_ingest_behind(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_write_global_seqno(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_write_global_seqno(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_verify_checksums_before_ingest(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_verify_checksums_before_ingest(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_verify_checksums_readahead_size(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_ingestexternalfileoptions_get_verify_checksums_readahead_size(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_verify_file_checksum(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_verify_file_checksum(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_fail_if_not_bottommost_level(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_allow_db_generated_files(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_allow_db_generated_files(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_fill_cache(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_fill_cache(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_prefetch_lmax_index_and_filter_blocks(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_ingestexternalfileoptions_get_prefetch_lmax_index_and_filter_blocks(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_file_opening_threads(rocksdb_ingestexternalfileoptions_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_ingestexternalfileoptions_get_file_opening_threads(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_open_and_compact_options_get_allow_resumption(rocksdb_open_and_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_import_column_family_options_get_move_files(rocksdb_import_column_family_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_size_approximation_options_set_include_memtables(rocksdb_size_approximation_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_size_approximation_options_get_include_memtables(rocksdb_size_approximation_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_size_approximation_options_set_include_files(rocksdb_size_approximation_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_size_approximation_options_get_include_files(rocksdb_size_approximation_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_size_approximation_options_set_include_blob_files(rocksdb_size_approximation_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_size_approximation_options_get_include_blob_files(rocksdb_size_approximation_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_size_approximation_options_set_files_size_error_margin(rocksdb_size_approximation_options_t* opt, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_size_approximation_options_get_files_size_error_margin(rocksdb_size_approximation_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_storage_info_options_set_include_checksum_info(rocksdb_livefiles_storage_info_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_livefiles_storage_info_options_get_include_checksum_info(rocksdb_livefiles_storage_info_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_storage_info_options_set_wal_size_for_flush(rocksdb_livefiles_storage_info_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_livefiles_storage_info_options_get_wal_size_for_flush(rocksdb_livefiles_storage_info_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_storage_info_options_set_atomic_flush(rocksdb_livefiles_storage_info_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_livefiles_storage_info_options_get_atomic_flush(rocksdb_livefiles_storage_info_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_metadata_options_set_level(rocksdb_column_family_metadata_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_column_family_metadata_options_get_level(rocksdb_column_family_metadata_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_wait_for_purge(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_wait_for_purge(rocksdb_wait_for_compact_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_cache_index_and_filter_blocks(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_cache_index_and_filter_blocks(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_cache_index_and_filter_blocks_with_high_priority(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_cache_index_and_filter_blocks_with_high_priority(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_pin_l0_filter_and_index_blocks_in_cache(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_pin_l0_filter_and_index_blocks_in_cache(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_pin_top_level_index_and_filter(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_pin_top_level_index_and_filter(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_type(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_index_type(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_block_search_type(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_index_block_search_type(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_data_block_index_type(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_data_block_index_type(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_data_block_hash_table_util_ratio(rocksdb_block_based_table_options_t* opt, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_block_based_options_get_data_block_hash_table_util_ratio(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_checksum(rocksdb_block_based_table_options_t* opt, [NativeTypeName("char")] sbyte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_checksum(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_no_block_cache(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_no_block_cache(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_size(rocksdb_block_based_table_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_block_based_options_get_block_size(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_size_deviation(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_block_size_deviation(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_restart_interval(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_block_restart_interval(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_block_restart_interval(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_index_block_restart_interval(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_metadata_block_size(rocksdb_block_based_table_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_block_based_options_get_metadata_block_size(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_partition_filters(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_partition_filters(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_decouple_partitioned_filters(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_decouple_partitioned_filters(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_optimize_filters_for_memory(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_optimize_filters_for_memory(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_use_delta_encoding(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_use_delta_encoding(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_use_udi_as_primary_index(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_use_udi_as_primary_index(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_fail_if_no_udi_on_open(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_fail_if_no_udi_on_open(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_whole_key_filtering(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_whole_key_filtering(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_detect_filter_construct_corruption(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_detect_filter_construct_corruption(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_verify_compression(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_verify_compression(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_read_amp_bytes_per_bit(rocksdb_block_based_table_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_block_based_options_get_read_amp_bytes_per_bit(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_format_version(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_block_based_options_get_format_version(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_separate_key_value_in_data_block(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_separate_key_value_in_data_block(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_uniform_cv_threshold(rocksdb_block_based_table_options_t* opt, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_block_based_options_get_uniform_cv_threshold(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_enable_index_compression(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_enable_index_compression(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_align(rocksdb_block_based_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_block_based_options_get_block_align(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_super_block_alignment_size(rocksdb_block_based_table_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_block_based_options_get_super_block_alignment_size(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_super_block_alignment_space_overhead_ratio(rocksdb_block_based_table_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_block_based_options_get_super_block_alignment_space_overhead_ratio(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_shortening(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_index_shortening(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_max_auto_readahead_size(rocksdb_block_based_table_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_block_based_options_get_max_auto_readahead_size(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_prepopulate_block_cache(rocksdb_block_based_table_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_block_based_options_get_prepopulate_block_cache(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_initial_auto_readahead_size(rocksdb_block_based_table_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_block_based_options_get_initial_auto_readahead_size(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_num_file_reads_for_auto_readahead(rocksdb_block_based_table_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_block_based_options_get_num_file_reads_for_auto_readahead(rocksdb_block_based_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_hash_table_ratio(rocksdb_cuckoo_table_options_t* opt, double v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_cuckoo_options_get_hash_table_ratio(rocksdb_cuckoo_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_max_search_depth(rocksdb_cuckoo_table_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_cuckoo_options_get_max_search_depth(rocksdb_cuckoo_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_cuckoo_block_size(rocksdb_cuckoo_table_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_cuckoo_options_get_cuckoo_block_size(rocksdb_cuckoo_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_identity_as_first_hash(rocksdb_cuckoo_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_cuckoo_options_get_identity_as_first_hash(rocksdb_cuckoo_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_use_module_hash(rocksdb_cuckoo_table_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_cuckoo_options_get_use_module_hash(rocksdb_cuckoo_table_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_max_read_amp(rocksdb_universal_compaction_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_max_read_amp(rocksdb_universal_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_allow_trivial_move(rocksdb_universal_compaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_universal_compaction_options_get_allow_trivial_move(rocksdb_universal_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_incremental(rocksdb_universal_compaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_universal_compaction_options_get_incremental(rocksdb_universal_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_reduce_file_locking(rocksdb_universal_compaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_universal_compaction_options_get_reduce_file_locking(rocksdb_universal_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_age_for_warm(rocksdb_fifo_compaction_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_fifo_compaction_options_get_age_for_warm(rocksdb_fifo_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_allow_trivial_copy_when_change_temperature(rocksdb_fifo_compaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_fifo_compaction_options_get_allow_trivial_copy_when_change_temperature(rocksdb_fifo_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_trivial_copy_buffer_size(rocksdb_fifo_compaction_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_fifo_compaction_options_get_trivial_copy_buffer_size(rocksdb_fifo_compaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_io_buffer_size(rocksdb_backup_engine_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_backup_engine_options_get_io_buffer_size(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_rate_limit(rocksdb_backup_engine_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_backup_engine_options_get_backup_rate_limit(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_max_background_operations(rocksdb_backup_engine_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_max_background_operations(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_dir(rocksdb_backup_engine_options_t* opt, [NativeTypeName("const char *")] sbyte* v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_backup_engine_options_get_backup_dir(rocksdb_backup_engine_options_t* opt, [NativeTypeName("size_t *")] nuint* size);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_share_table_files(rocksdb_backup_engine_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_share_table_files(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_sync(rocksdb_backup_engine_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_sync(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_destroy_old_data(rocksdb_backup_engine_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_destroy_old_data(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_log_files(rocksdb_backup_engine_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_backup_log_files(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_restore_rate_limit(rocksdb_backup_engine_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_backup_engine_options_get_restore_rate_limit(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_share_files_with_checksum(rocksdb_backup_engine_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_share_files_with_checksum(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_callback_trigger_interval_size(rocksdb_backup_engine_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_backup_engine_options_get_callback_trigger_interval_size(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_max_valid_backups_to_open(rocksdb_backup_engine_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_max_valid_backups_to_open(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_share_files_with_checksum_naming(rocksdb_backup_engine_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_share_files_with_checksum_naming(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_schema_version(rocksdb_backup_engine_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_schema_version(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_current_temperatures_override_manifest(rocksdb_backup_engine_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_current_temperatures_override_manifest(rocksdb_backup_engine_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_set_decrease_background_thread_cpu_priority(rocksdb_create_backup_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_create_backup_options_get_decrease_background_thread_cpu_priority(rocksdb_create_backup_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_set_background_thread_cpu_priority(rocksdb_create_backup_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_create_backup_options_get_background_thread_cpu_priority(rocksdb_create_backup_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_set_flush_before_backup(rocksdb_create_backup_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_create_backup_options_get_flush_before_backup(rocksdb_create_backup_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_backup_options_set_atomic_flush(rocksdb_create_backup_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_create_backup_options_get_atomic_flush(rocksdb_create_backup_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_restore_options_set_keep_log_files(rocksdb_restore_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_restore_options_get_keep_log_files(rocksdb_restore_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_restore_options_set_mode(rocksdb_restore_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_restore_options_get_mode(rocksdb_restore_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_max_num_locks(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transactiondb_options_get_max_num_locks(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_max_num_deadlocks(rocksdb_transactiondb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_transactiondb_options_get_max_num_deadlocks(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_num_stripes(rocksdb_transactiondb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_transactiondb_options_get_num_stripes(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_transaction_lock_timeout(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transactiondb_options_get_transaction_lock_timeout(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_default_lock_timeout(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transactiondb_options_get_default_lock_timeout(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_write_policy(rocksdb_transactiondb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_transactiondb_options_get_write_policy(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_rollback_merge_operands(rocksdb_transactiondb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transactiondb_options_get_rollback_merge_operands(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_use_per_key_point_lock_mgr(rocksdb_transactiondb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transactiondb_options_get_use_per_key_point_lock_mgr(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_skip_concurrency_control(rocksdb_transactiondb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transactiondb_options_get_skip_concurrency_control(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_default_write_batch_flush_threshold(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transactiondb_options_get_default_write_batch_flush_threshold(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_enable_udt_validation(rocksdb_transactiondb_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transactiondb_options_get_enable_udt_validation(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_txn_commit_bypass_memtable_threshold(rocksdb_transactiondb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_transactiondb_options_get_txn_commit_bypass_memtable_threshold(rocksdb_transactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_set_snapshot(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_set_snapshot(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_deadlock_detect(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_deadlock_detect(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_use_only_the_last_commit_time_batch_for_recovery(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_use_only_the_last_commit_time_batch_for_recovery(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_lock_timeout(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transaction_options_get_lock_timeout(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_deadlock_timeout_us(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transaction_options_get_deadlock_timeout_us(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_expiration(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transaction_options_get_expiration(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_deadlock_detect_depth(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transaction_options_get_deadlock_detect_depth(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_max_write_batch_size(rocksdb_transaction_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_transaction_options_get_max_write_batch_size(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_skip_concurrency_control(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_skip_concurrency_control(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_skip_prepare(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_skip_prepare(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_write_batch_flush_threshold(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] long v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern long rocksdb_transaction_options_get_write_batch_flush_threshold(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_write_batch_track_timestamp_size(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_write_batch_track_timestamp_size(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_commit_bypass_memtable(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_transaction_options_get_commit_bypass_memtable(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_large_txn_commit_optimize_threshold(rocksdb_transaction_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_transaction_options_get_large_txn_commit_optimize_threshold(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_large_txn_commit_optimize_byte_threshold(rocksdb_transaction_options_t* opt, [NativeTypeName("uint64_t")] ulong v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern ulong rocksdb_transaction_options_get_large_txn_commit_optimize_byte_threshold(rocksdb_transaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransaction_options_set_set_snapshot(rocksdb_optimistictransaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_optimistictransaction_options_get_set_snapshot(rocksdb_optimistictransaction_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_options_set_validate_policy(rocksdb_optimistictransactiondb_options_t* opt, int v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_optimistictransactiondb_options_get_validate_policy(rocksdb_optimistictransactiondb_options_t* opt);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_options_set_occ_lock_buckets(rocksdb_optimistictransactiondb_options_t* opt, [NativeTypeName("uint32_t")] uint v);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_optimistictransactiondb_options_get_occ_lock_buckets(rocksdb_optimistictransactiondb_options_t* opt);
}

/// <summary>Defines the type of a member as it was used in the native signature.</summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
[Conditional("DEBUG")]
internal sealed partial class NativeTypeNameAttribute : Attribute
{
    private readonly string _name;

    /// <summary>Initializes a new instance of the <see cref="NativeTypeNameAttribute" /> class.</summary>
    /// <param name="name">The name of the type that was used in the native signature.</param>
    public NativeTypeNameAttribute(string name) => _name = name;

    /// <summary>Gets the name of the type that was used in the native signature.</summary>
    public string Name => _name;
}

/// <summary>Defines the annotation found in a native declaration.</summary>
[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
[Conditional("DEBUG")]
internal sealed partial class NativeAnnotationAttribute : Attribute
{
    private readonly string _annotation;

    /// <summary>Initializes a new instance of the <see cref="NativeAnnotationAttribute" /> class.</summary>
    /// <param name="annotation">The annotation that was used in the native declaration.</param>
    public NativeAnnotationAttribute(string annotation) => _annotation = annotation;

    /// <summary>Gets the annotation that was used in the native declaration.</summary>
    public string Annotation => _annotation;
}
