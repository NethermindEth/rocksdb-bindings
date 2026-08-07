using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings.Native;

public partial struct rocksdb_t
{
}

public partial struct rocksdb_status_ptr_t
{
}

public partial struct rocksdb_backup_engine_t
{
}

public partial struct rocksdb_backup_engine_info_t
{
}

public partial struct rocksdb_backup_engine_options_t
{
}

public partial struct rocksdb_restore_options_t
{
}

public partial struct rocksdb_memory_allocator_t
{
}

public partial struct rocksdb_lru_cache_options_t
{
}

public partial struct rocksdb_hyper_clock_cache_options_t
{
}

public partial struct rocksdb_cache_t
{
}

public partial struct rocksdb_write_buffer_manager_t
{
}

public partial struct rocksdb_sst_file_manager_t
{
}

public partial struct rocksdb_compactionfilter_t
{
}

public partial struct rocksdb_compactionfiltercontext_t
{
}

public partial struct rocksdb_compactionfilterfactory_t
{
}

public partial struct rocksdb_file_checksum_gen_factory_t
{
}

public partial struct rocksdb_sst_partitioner_factory_t
{
}

public partial struct rocksdb_table_properties_collector_factory_t
{
}

public partial struct rocksdb_comparator_t
{
}

public partial struct rocksdb_dbpath_t
{
}

public partial struct rocksdb_env_t
{
}

public partial struct rocksdb_fifo_compaction_options_t
{
}

public partial struct rocksdb_filelock_t
{
}

public partial struct rocksdb_filterpolicy_t
{
}

public partial struct rocksdb_flushoptions_t
{
}

public partial struct rocksdb_iterator_t
{
}

public partial struct rocksdb_logger_t
{
}

public partial struct rocksdb_mergeoperator_t
{
}

public partial struct rocksdb_options_t
{
}

public partial struct rocksdb_compactoptions_t
{
}

public partial struct rocksdb_block_based_table_options_t
{
}

public partial struct rocksdb_cuckoo_table_options_t
{
}

public partial struct rocksdb_randomfile_t
{
}

public partial struct rocksdb_readoptions_t
{
}

public partial struct rocksdb_seqfile_t
{
}

public partial struct rocksdb_slicetransform_t
{
}

public partial struct rocksdb_snapshot_t
{
}

public partial struct rocksdb_writablefile_t
{
}

public partial struct rocksdb_writebatch_t
{
}

public partial struct rocksdb_writebatch_wi_t
{
}

public partial struct rocksdb_writeoptions_t
{
}

public partial struct rocksdb_universal_compaction_options_t
{
}

public partial struct rocksdb_livefile_t
{
}

public partial struct rocksdb_livefiles_t
{
}

public partial struct rocksdb_column_family_handle_t
{
}

public partial struct rocksdb_column_family_metadata_t
{
}

public partial struct rocksdb_import_column_family_options_t
{
}

public partial struct rocksdb_export_import_files_metadata_t
{
}

public partial struct rocksdb_level_metadata_t
{
}

public partial struct rocksdb_sst_file_metadata_t
{
}

public partial struct rocksdb_envoptions_t
{
}

public partial struct rocksdb_ingestexternalfileoptions_t
{
}

public partial struct rocksdb_sstfilewriter_t
{
}

public partial struct rocksdb_ratelimiter_t
{
}

public partial struct rocksdb_perfcontext_t
{
}

public partial struct rocksdb_pinnableslice_t
{
}

public partial struct rocksdb_transactiondb_options_t
{
}

public partial struct rocksdb_transactiondb_t
{
}

public partial struct rocksdb_transaction_options_t
{
}

public partial struct rocksdb_optimistictransactiondb_t
{
}

public partial struct rocksdb_optimistictransaction_options_t
{
}

public partial struct rocksdb_transaction_t
{
}

public partial struct rocksdb_checkpoint_t
{
}

public partial struct rocksdb_wal_iterator_t
{
}

public partial struct rocksdb_wal_readoptions_t
{
}

public partial struct rocksdb_memory_consumers_t
{
}

public partial struct rocksdb_memory_usage_t
{
}

public partial struct rocksdb_statistics_histogram_data_t
{
}

public partial struct rocksdb_wait_for_compact_options_t
{
}

public unsafe partial struct rocksdb_slice_t
{
    [NativeTypeName("const char *")]
    public sbyte* data;

    [NativeTypeName("size_t")]
    public nuint size;
}

public partial struct rocksdb_flushjobinfo_t
{
}

public partial struct rocksdb_compactionjobinfo_t
{
}

public partial struct rocksdb_subcompactionjobinfo_t
{
}

public partial struct rocksdb_externalfileingestioninfo_t
{
}

public partial struct rocksdb_eventlistener_t
{
}

public partial struct rocksdb_writestallinfo_t
{
}

public partial struct rocksdb_writestallcondition_t
{
}

public partial struct rocksdb_memtableinfo_t
{
}

public partial struct rocksdb_compactionservice_scheduleresponse_t
{
}

public partial struct rocksdb_compactionservice_jobinfo_t
{
}

public partial struct rocksdb_compactionservice_t
{
}

public partial struct rocksdb_compaction_service_options_override_t
{
}

public partial struct rocksdb_open_and_compact_options_t
{
}

public partial struct rocksdb_pinnable_handle_t
{
}

public static unsafe partial class RocksDbNative
{
    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_with_ttl([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int ttl, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_for_read_only([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("unsigned char")] byte error_if_wal_file_exists, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_as_secondary([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* secondary_path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_backup_engine_t* rocksdb_backup_engine_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_backup_engine_t* rocksdb_backup_engine_open_opts([NativeTypeName("const rocksdb_backup_engine_options_t *")] rocksdb_backup_engine_options_t* options, rocksdb_env_t* env, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_create_new_backup(rocksdb_backup_engine_t* be, rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_create_new_backup_flush(rocksdb_backup_engine_t* be, rocksdb_t* db, [NativeTypeName("unsigned char")] byte flush_before_backup, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_purge_old_backups(rocksdb_backup_engine_t* be, [NativeTypeName("uint32_t")] uint num_backups_to_keep, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_restore_options_t* rocksdb_restore_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_restore_options_destroy(rocksdb_restore_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_restore_options_set_keep_log_files(rocksdb_restore_options_t* opt, int v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_verify_backup(rocksdb_backup_engine_t* be, [NativeTypeName("uint32_t")] uint backup_id, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_restore_db_from_latest_backup(rocksdb_backup_engine_t* be, [NativeTypeName("const char *")] sbyte* db_dir, [NativeTypeName("const char *")] sbyte* wal_dir, [NativeTypeName("const rocksdb_restore_options_t *")] rocksdb_restore_options_t* restore_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_restore_db_from_backup(rocksdb_backup_engine_t* be, [NativeTypeName("const char *")] sbyte* db_dir, [NativeTypeName("const char *")] sbyte* wal_dir, [NativeTypeName("const rocksdb_restore_options_t *")] rocksdb_restore_options_t* restore_options, [NativeTypeName("const uint32_t")] uint backup_id, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_backup_engine_info_t *")]
    public static extern rocksdb_backup_engine_info_t* rocksdb_backup_engine_get_backup_info(rocksdb_backup_engine_t* be);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_info_count([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern nint rocksdb_backup_engine_info_timestamp([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_backup_engine_info_backup_id([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_backup_engine_info_size([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_backup_engine_info_number_files([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_info_destroy([NativeTypeName("const rocksdb_backup_engine_info_t *")] rocksdb_backup_engine_info_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_close(rocksdb_backup_engine_t* be);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_singledelete_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_increase_full_history_ts_low(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* ts_low, [NativeTypeName("size_t")] nuint ts_lowlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_full_history_ts_low(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("size_t *")] nuint* ts_lowlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_backup_engine_options_t* rocksdb_backup_engine_options_create([NativeTypeName("const char *")] sbyte* backup_dir);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_dir(rocksdb_backup_engine_options_t* options, [NativeTypeName("const char *")] sbyte* backup_dir);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_env(rocksdb_backup_engine_options_t* options, rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_share_table_files(rocksdb_backup_engine_options_t* options, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_share_table_files(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_sync(rocksdb_backup_engine_options_t* options, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_sync(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_destroy_old_data(rocksdb_backup_engine_options_t* options, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_destroy_old_data(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_log_files(rocksdb_backup_engine_options_t* options, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_backup_engine_options_get_backup_log_files(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_backup_rate_limit(rocksdb_backup_engine_options_t* options, [NativeTypeName("uint64_t")] nuint limit);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_backup_engine_options_get_backup_rate_limit(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_restore_rate_limit(rocksdb_backup_engine_options_t* options, [NativeTypeName("uint64_t")] nuint limit);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_backup_engine_options_get_restore_rate_limit(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_max_background_operations(rocksdb_backup_engine_options_t* options, int val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_max_background_operations(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_callback_trigger_interval_size(rocksdb_backup_engine_options_t* options, [NativeTypeName("uint64_t")] nuint size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_backup_engine_options_get_callback_trigger_interval_size(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_max_valid_backups_to_open(rocksdb_backup_engine_options_t* options, int val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_max_valid_backups_to_open(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_set_share_files_with_checksum_naming(rocksdb_backup_engine_options_t* options, int val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_backup_engine_options_get_share_files_with_checksum_naming(rocksdb_backup_engine_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_backup_engine_options_destroy(rocksdb_backup_engine_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_checkpoint_t* rocksdb_checkpoint_object_create(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_checkpoint_create(rocksdb_checkpoint_t* checkpoint, [NativeTypeName("const char *")] sbyte* checkpoint_dir, [NativeTypeName("uint64_t")] nuint log_size_for_flush, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_export_import_files_metadata_t* rocksdb_checkpoint_export_column_family(rocksdb_checkpoint_t* checkpoint, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* export_dir, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_checkpoint_object_destroy(rocksdb_checkpoint_t* checkpoint);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_and_trim_history([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char *")] sbyte* trim_ts, [NativeTypeName("size_t")] nuint trim_tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_column_families_with_ttl([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("const int *")] int* ttls, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_for_read_only_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("unsigned char")] byte error_if_wal_file_exists, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_open_as_secondary_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("const char *")] sbyte* secondary_path, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char **")]
    public static extern sbyte** rocksdb_list_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("size_t *")] nuint* lencf, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_list_column_families_destroy([NativeTypeName("char **")] sbyte** list, [NativeTypeName("size_t")] nuint len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_create_column_family(rocksdb_t* db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t** rocksdb_create_column_families(rocksdb_t* db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("size_t *")] nuint* lencfs, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_column_families_destroy(rocksdb_column_family_handle_t** list);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_create_column_family_with_import(rocksdb_t* db, rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, rocksdb_import_column_family_options_t* import_options, rocksdb_export_import_files_metadata_t* metadata, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_create_column_family_with_ttl(rocksdb_t* db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, int ttl, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_drop_column_family(rocksdb_t* db, rocksdb_column_family_handle_t* handle, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_get_default_column_family_handle(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_handle_destroy(rocksdb_column_family_handle_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_column_family_handle_get_id(rocksdb_column_family_handle_t* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_column_family_handle_get_name(rocksdb_column_family_handle_t* handle, [NativeTypeName("size_t *")] nuint* name_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_close(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_put_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_range_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_merge(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_merge_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_t* batch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** ts, [NativeTypeName("size_t *")] nuint* tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** ts, [NativeTypeName("size_t *")] nuint* tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_get_db_identity(rocksdb_t* db, [NativeTypeName("size_t *")] nuint* id_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** timestamp_list, [NativeTypeName("size_t *")] nuint* timestamp_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_multi_get_cf_with_ts(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** timestamps_list, [NativeTypeName("size_t *")] nuint* timestamps_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_batched_multi_get_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, rocksdb_pinnableslice_t** values, [NativeTypeName("char **")] sbyte** errs, [NativeTypeName("const _Bool")] bool sorted_input);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_batched_multi_get_cf_slice(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const rocksdb_slice_t *")] rocksdb_slice_t* keys_list, rocksdb_pinnableslice_t** values, [NativeTypeName("char **")] sbyte** errs, [NativeTypeName("const _Bool")] bool sorted_input);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_key_may_exist(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint key_len, [NativeTypeName("char **")] sbyte** value, [NativeTypeName("size_t *")] nuint* val_len, [NativeTypeName("const char *")] sbyte* timestamp, [NativeTypeName("size_t")] nuint timestamp_len, [NativeTypeName("unsigned char *")] byte* value_found);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_key_may_exist_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint key_len, [NativeTypeName("char **")] sbyte** value, [NativeTypeName("size_t *")] nuint* val_len, [NativeTypeName("const char *")] sbyte* timestamp, [NativeTypeName("size_t")] nuint timestamp_len, [NativeTypeName("unsigned char *")] byte* value_found);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_create_iterator(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wal_iterator_t* rocksdb_get_updates_since(rocksdb_t* db, [NativeTypeName("uint64_t")] nuint seq_number, [NativeTypeName("const rocksdb_wal_readoptions_t *")] rocksdb_wal_readoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_create_iterator_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_iterators(rocksdb_t* db, rocksdb_readoptions_t* opts, rocksdb_column_family_handle_t** column_families, rocksdb_iterator_t** iterators, [NativeTypeName("size_t")] nuint size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_snapshot_t *")]
    public static extern rocksdb_snapshot_t* rocksdb_create_snapshot(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_release_snapshot(rocksdb_t* db, [NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* snapshot);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_snapshot_get_sequence_number([NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* snapshot);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_property_value(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_property_int(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] nuint* out_val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_property_int_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] nuint* out_val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_property_value_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes(rocksdb_t* db, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint64_t *")] nuint* sizes, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint64_t *")] nuint* sizes, [NativeTypeName("char **")] sbyte** errptr);

    public const uint rocksdb_size_approximation_flags_none = 0;
    public const uint rocksdb_size_approximation_flags_include_memtable = 1 << 0;
    public const uint rocksdb_size_approximation_flags_include_files = 1 << 1;
    public const uint rocksdb_size_approximation_flags_include_blob_files = 1 << 2;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_sizes_cf_with_flags(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, int num_ranges, [NativeTypeName("const char *const *")] sbyte** range_start_key, [NativeTypeName("const size_t *")] nuint* range_start_key_len, [NativeTypeName("const char *const *")] sbyte** range_limit_key, [NativeTypeName("const size_t *")] nuint* range_limit_key_len, [NativeTypeName("uint8_t")] byte include_flags, [NativeTypeName("uint64_t *")] nuint* sizes, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_suggest_compact_range(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_suggest_compact_range_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range_opt(rocksdb_t* db, rocksdb_compactoptions_t* opt, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compact_range_cf_opt(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, rocksdb_compactoptions_t* opt, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_livefiles_t *")]
    public static extern rocksdb_livefiles_t* rocksdb_livefiles(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush(rocksdb_t* db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_cfs(rocksdb_t* db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t** column_family, int num_column_families, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flush_wal(rocksdb_t* db, [NativeTypeName("unsigned char")] byte sync, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_disable_file_deletions(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_enable_file_deletions(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_destroy_db([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_repair_db([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_destroy(rocksdb_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_iter_valid([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek_to_first(rocksdb_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek_to_last(rocksdb_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek(rocksdb_iterator_t* param0, [NativeTypeName("const char *")] sbyte* k, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_seek_for_prev(rocksdb_iterator_t* param0, [NativeTypeName("const char *")] sbyte* k, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_next(rocksdb_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_prev(rocksdb_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_iter_key([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("size_t *")] nuint* klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_iter_value([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("size_t *")] nuint* vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_iter_timestamp([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("size_t *")] nuint* tslen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_get_error([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* param0, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slice_t rocksdb_iter_key_slice([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slice_t rocksdb_iter_value_slice([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slice_t rocksdb_iter_timestamp_slice([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_iter_refresh([NativeTypeName("const rocksdb_iterator_t *")] rocksdb_iterator_t* iter, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_iter_next(rocksdb_wal_iterator_t* iter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wal_iter_valid([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_iter_status([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* iter, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_wal_iter_get_batch([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* iter, [NativeTypeName("uint64_t *")] nuint* seq);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_get_latest_sequence_number(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wal_iter_destroy([NativeTypeName("const rocksdb_wal_iterator_t *")] rocksdb_wal_iterator_t* iter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_writebatch_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_writebatch_create_from([NativeTypeName("const char *")] sbyte* rep, [NativeTypeName("size_t")] nuint size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_t* rocksdb_writebatch_create_with_params([NativeTypeName("size_t")] nuint reserved_bytes, [NativeTypeName("size_t")] nuint max_bytes, [NativeTypeName("size_t")] nuint protection_bytes_per_key, [NativeTypeName("size_t")] nuint default_cf_ts_sz);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_destroy(rocksdb_writebatch_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_clear(rocksdb_writebatch_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_writebatch_count(rocksdb_writebatch_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put(rocksdb_writebatch_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put_cf(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put_cf_with_ts(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_putv(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_putv_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_merge(rocksdb_writebatch_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_merge_cf(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_mergev(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_mergev_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete(rocksdb_writebatch_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_singledelete(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_cf(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_cf_with_ts(rocksdb_writebatch_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_singledelete_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_singledelete_cf_with_ts(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_deletev(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_deletev_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_range(rocksdb_writebatch_t* b, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_range_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_rangev(rocksdb_writebatch_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_delete_rangev_cf(rocksdb_writebatch_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_put_log_data(rocksdb_writebatch_t* param0, [NativeTypeName("const char *")] sbyte* blob, [NativeTypeName("size_t")] nuint len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate_ld(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> log_data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate_cf(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> put_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, void> deleted_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> merge_cf);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_iterate_cf_ld(rocksdb_writebatch_t* param0, void* state, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> put_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, void> deleted_cf, [NativeTypeName("void (*)(void *, uint32_t, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, sbyte*, nuint, void> merge_cf, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> log_data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_writebatch_data(rocksdb_writebatch_t* param0, [NativeTypeName("size_t *")] nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_set_save_point(rocksdb_writebatch_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_rollback_to_save_point(rocksdb_writebatch_t* param0, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_pop_save_point(rocksdb_writebatch_t* param0, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_update_timestamps(rocksdb_writebatch_t* wb, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, void* state, [NativeTypeName("size_t (*)(void *, uint32_t)")] delegate* unmanaged[Cdecl]<void*, uint, nuint> get_ts_size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_writebatch_wi_create([NativeTypeName("size_t")] nuint reserved_bytes, [NativeTypeName("unsigned char")] byte overwrite_keys);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_writebatch_wi_create_from([NativeTypeName("const char *")] sbyte* rep, [NativeTypeName("size_t")] nuint size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_writebatch_wi_create_with_params(rocksdb_comparator_t* backup_index_comparator, [NativeTypeName("size_t")] nuint reserved_bytes, [NativeTypeName("unsigned char")] byte overwrite_key, [NativeTypeName("size_t")] nuint max_bytes, [NativeTypeName("size_t")] nuint protection_bytes_per_key);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_destroy(rocksdb_writebatch_wi_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_clear(rocksdb_writebatch_wi_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_writebatch_wi_count(rocksdb_writebatch_wi_t* b);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_put(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_put_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_putv(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_putv_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_merge(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_merge_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_mergev(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_mergev_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, int num_values, [NativeTypeName("const char *const *")] sbyte** values_list, [NativeTypeName("const size_t *")] nuint* values_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_singledelete(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_singledelete_cf(rocksdb_writebatch_wi_t* param0, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_deletev(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_deletev_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_range(rocksdb_writebatch_wi_t* b, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_range_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_key_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_rangev(rocksdb_writebatch_wi_t* b, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_delete_rangev_cf(rocksdb_writebatch_wi_t* b, rocksdb_column_family_handle_t* column_family, int num_keys, [NativeTypeName("const char *const *")] sbyte** start_keys_list, [NativeTypeName("const size_t *")] nuint* start_keys_list_sizes, [NativeTypeName("const char *const *")] sbyte** end_keys_list, [NativeTypeName("const size_t *")] nuint* end_keys_list_sizes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_put_log_data(rocksdb_writebatch_wi_t* param0, [NativeTypeName("const char *")] sbyte* blob, [NativeTypeName("size_t")] nuint len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_iterate(rocksdb_writebatch_wi_t* b, void* state, [NativeTypeName("void (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, void> put, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> deleted);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_writebatch_wi_data(rocksdb_writebatch_wi_t* b, [NativeTypeName("size_t *")] nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_set_save_point(rocksdb_writebatch_wi_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_rollback_to_save_point(rocksdb_writebatch_wi_t* param0, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch(rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch_cf(rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch_and_db(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_writebatch_wi_get_pinned_from_batch_and_db(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_writebatch_wi_get_from_batch_and_db_cf(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_writebatch_wi_get_pinned_from_batch_and_db_cf(rocksdb_writebatch_wi_t* wbwi, rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_writebatch_wi(rocksdb_t* db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base_readopts(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base_cf(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator, rocksdb_column_family_handle_t* cf);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_writebatch_wi_create_iterator_with_base_cf_readopts(rocksdb_writebatch_wi_t* wbwi, rocksdb_iterator_t* base_iterator, rocksdb_column_family_handle_t* cf, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writebatch_wi_update_timestamps(rocksdb_writebatch_wi_t* wbwi, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, void* state, [NativeTypeName("size_t (*)(void *, uint32_t)")] delegate* unmanaged[Cdecl]<void*, uint, nuint> get_ts_size, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_load_latest_options([NativeTypeName("const char *")] sbyte* db_path, rocksdb_env_t* env, [NativeTypeName("_Bool")] bool ignore_unknown_options, rocksdb_cache_t* cache, rocksdb_options_t** db_options, [NativeTypeName("size_t *")] nuint* num_column_families, [NativeTypeName("char ***")] sbyte*** column_family_names, rocksdb_options_t*** column_family_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_load_latest_options_destroy(rocksdb_options_t* db_options, [NativeTypeName("char **")] sbyte** list_column_family_names, rocksdb_options_t** list_column_family_options, [NativeTypeName("size_t")] nuint len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_block_based_table_options_t* rocksdb_block_based_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_destroy(rocksdb_block_based_table_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_checksum(rocksdb_block_based_table_options_t* param0, [NativeTypeName("char")] sbyte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_size(rocksdb_block_based_table_options_t* options, [NativeTypeName("size_t")] nuint block_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_size_deviation(rocksdb_block_based_table_options_t* options, int block_size_deviation);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_restart_interval(rocksdb_block_based_table_options_t* options, int block_restart_interval);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_block_restart_interval(rocksdb_block_based_table_options_t* options, int index_block_restart_interval);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_metadata_block_size(rocksdb_block_based_table_options_t* options, [NativeTypeName("uint64_t")] nuint metadata_block_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_partition_filters(rocksdb_block_based_table_options_t* options, [NativeTypeName("unsigned char")] byte partition_filters);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_optimize_filters_for_memory(rocksdb_block_based_table_options_t* options, [NativeTypeName("unsigned char")] byte optimize_filters_for_memory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_use_delta_encoding(rocksdb_block_based_table_options_t* options, [NativeTypeName("unsigned char")] byte use_delta_encoding);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_filter_policy(rocksdb_block_based_table_options_t* options, rocksdb_filterpolicy_t* filter_policy);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_no_block_cache(rocksdb_block_based_table_options_t* options, [NativeTypeName("unsigned char")] byte no_block_cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_cache(rocksdb_block_based_table_options_t* options, rocksdb_cache_t* block_cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_whole_key_filtering(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_format_version(rocksdb_block_based_table_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_separate_key_value_in_data_block(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    public const uint rocksdb_block_based_table_index_type_binary_search = 0;
    public const uint rocksdb_block_based_table_index_type_hash_search = 1;
    public const uint rocksdb_block_based_table_index_type_two_level_index_search = 2;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_type(rocksdb_block_based_table_options_t* param0, int param1);

    public const uint rocksdb_block_based_table_data_block_index_type_binary_search = 0;
    public const uint rocksdb_block_based_table_data_block_index_type_binary_search_and_hash = 1;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_data_block_index_type(rocksdb_block_based_table_options_t* param0, int param1);

    public const uint rocksdb_block_based_table_index_block_search_type_binary = 0;
    public const uint rocksdb_block_based_table_index_block_search_type_interpolation = 1;
    public const uint rocksdb_block_based_table_index_block_search_type_auto = 2;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_index_block_search_type(rocksdb_block_based_table_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_uniform_cv_threshold(rocksdb_block_based_table_options_t* param0, double param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_data_block_hash_ratio(rocksdb_block_based_table_options_t* options, double v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_cache_index_and_filter_blocks(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_cache_index_and_filter_blocks_with_high_priority(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_pin_l0_filter_and_index_blocks_in_cache(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_pin_top_level_index_and_filter(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_block_based_table_factory(rocksdb_options_t* opt, rocksdb_block_based_table_options_t* table_options);

    public const uint rocksdb_block_based_k_fallback_pinning_tier = 0;
    public const uint rocksdb_block_based_k_none_pinning_tier = 1;
    public const uint rocksdb_block_based_k_flush_and_similar_pinning_tier = 2;
    public const uint rocksdb_block_based_k_all_pinning_tier = 3;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_top_level_index_pinning_tier(rocksdb_block_based_table_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_partition_pinning_tier(rocksdb_block_based_table_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_unpartitioned_pinning_tier(rocksdb_block_based_table_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_block_based_options_set_block_align(rocksdb_block_based_table_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_buffer_manager(rocksdb_options_t* opt, rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_sst_file_manager(rocksdb_options_t* opt, rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_flushjobinfo_cf_name([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_flushjobinfo_file_path([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushjobinfo_triggered_writes_slowdown([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushjobinfo_triggered_writes_stop([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_flushjobinfo_largest_seqno([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_flushjobinfo_smallest_seqno([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_reset_status(rocksdb_status_ptr_t* status_ptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_flushjobinfo_flush_reason([NativeTypeName("const rocksdb_flushjobinfo_t *")] rocksdb_flushjobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_status_ptr_get_error(rocksdb_status_ptr_t* status, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionjobinfo_status([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_cf_name([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_input_files_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_input_file_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_output_files_count([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionjobinfo_output_file_at([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0, [NativeTypeName("size_t")] nuint pos, [NativeTypeName("size_t *")] nuint* param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionjobinfo_elapsed_micros([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionjobinfo_num_corrupt_keys([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionjobinfo_base_input_level([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionjobinfo_output_level([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionjobinfo_input_records([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionjobinfo_output_records([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionjobinfo_total_input_bytes([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionjobinfo_total_output_bytes([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionjobinfo_compaction_reason([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_num_input_files([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_compactionjobinfo_num_input_files_at_output_level([NativeTypeName("const rocksdb_compactionjobinfo_t *")] rocksdb_compactionjobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_subcompactionjobinfo_status([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* param0, [NativeTypeName("char **")] sbyte** param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_subcompactionjobinfo_cf_name([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_subcompactionjobinfo_thread_id([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_subcompactionjobinfo_base_input_level([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_subcompactionjobinfo_output_level([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_subcompactionjobinfo_compaction_reason([NativeTypeName("const rocksdb_subcompactionjobinfo_t *")] rocksdb_subcompactionjobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_externalfileingestioninfo_cf_name([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_externalfileingestioninfo_internal_file_path([NativeTypeName("const rocksdb_externalfileingestioninfo_t *")] rocksdb_externalfileingestioninfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_writestallinfo_cf_name([NativeTypeName("const rocksdb_writestallinfo_t *")] rocksdb_writestallinfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_writestallcondition_t *")]
    public static extern rocksdb_writestallcondition_t* rocksdb_writestallinfo_cur([NativeTypeName("const rocksdb_writestallinfo_t *")] rocksdb_writestallinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_writestallcondition_t *")]
    public static extern rocksdb_writestallcondition_t* rocksdb_writestallinfo_prev([NativeTypeName("const rocksdb_writestallinfo_t *")] rocksdb_writestallinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_memtableinfo_cf_name([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* param0, [NativeTypeName("size_t *")] nuint* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_memtableinfo_first_seqno([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_memtableinfo_earliest_seqno([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_memtableinfo_num_entries([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_memtableinfo_num_deletes([NativeTypeName("const rocksdb_memtableinfo_t *")] rocksdb_memtableinfo_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_eventlistener_t* rocksdb_eventlistener_create(void* state_, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor_, [NativeTypeName("on_flush_begin_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_flushjobinfo_t*, void> on_flush_begin, [NativeTypeName("on_flush_completed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_flushjobinfo_t*, void> on_flush_completed, [NativeTypeName("on_compaction_begin_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_compactionjobinfo_t*, void> on_compaction_begin, [NativeTypeName("on_compaction_completed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_compactionjobinfo_t*, void> on_compaction_completed, [NativeTypeName("on_subcompaction_begin_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_subcompactionjobinfo_t*, void> on_subcompaction_begin, [NativeTypeName("on_subcompaction_completed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_subcompactionjobinfo_t*, void> on_subcompaction_completed, [NativeTypeName("on_external_file_ingested_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_t*, rocksdb_externalfileingestioninfo_t*, void> on_external_file_ingested, [NativeTypeName("on_background_error_cb")] delegate* unmanaged[Cdecl]<void*, uint, rocksdb_status_ptr_t*, void> on_background_error, [NativeTypeName("on_stall_conditions_changed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_writestallinfo_t*, void> on_stall_conditions_changed, [NativeTypeName("on_memtable_sealed_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_memtableinfo_t*, void> on_memtable_sealed);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_eventlistener_destroy(rocksdb_eventlistener_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_eventlistener(rocksdb_options_t* param0, rocksdb_eventlistener_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cuckoo_table_options_t* rocksdb_cuckoo_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_destroy(rocksdb_cuckoo_table_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_hash_ratio(rocksdb_cuckoo_table_options_t* options, double v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_max_search_depth(rocksdb_cuckoo_table_options_t* options, [NativeTypeName("uint32_t")] uint v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_cuckoo_block_size(rocksdb_cuckoo_table_options_t* options, [NativeTypeName("uint32_t")] uint v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_identity_as_first_hash(rocksdb_cuckoo_table_options_t* options, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cuckoo_options_set_use_module_hash(rocksdb_cuckoo_table_options_t* options, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_cuckoo_table_factory(rocksdb_options_t* opt, rocksdb_cuckoo_table_options_t* table_options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_options(rocksdb_t* db, int count, [NativeTypeName("const char *const[]")] sbyte** keys, [NativeTypeName("const char *const[]")] sbyte** values, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_options_cf(rocksdb_t* db, rocksdb_column_family_handle_t* handle, int count, [NativeTypeName("const char *const[]")] sbyte** keys, [NativeTypeName("const char *const[]")] sbyte** values, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_options_t* rocksdb_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_destroy(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_options_t* rocksdb_options_create_copy(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_increase_parallelism(rocksdb_options_t* opt, int total_threads);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_optimize_for_point_lookup(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint block_cache_size_mb);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_optimize_level_style_compaction(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint memtable_memory_budget);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_optimize_universal_style_compaction(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint memtable_memory_budget);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_ingest_behind(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_ingest_behind(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_filter(rocksdb_options_t* param0, rocksdb_compactionfilter_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_filter_factory(rocksdb_options_t* param0, rocksdb_compactionfilterfactory_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_compaction_readahead_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_compaction_readahead_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_comparator(rocksdb_options_t* param0, rocksdb_comparator_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_merge_operator(rocksdb_options_t* param0, rocksdb_mergeoperator_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_uint64add_merge_operator(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_per_level(rocksdb_options_t* opt, [NativeTypeName("const int *")] int* level_values, [NativeTypeName("size_t")] nuint num_levels);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_create_if_missing(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_create_if_missing(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_create_missing_column_families(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_create_missing_column_families(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_error_if_exists(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_error_if_exists(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_paranoid_checks(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_paranoid_checks(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_open_files_async(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_open_files_async(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_paths(rocksdb_options_t* param0, [NativeTypeName("const rocksdb_dbpath_t **")] rocksdb_dbpath_t** path_values, [NativeTypeName("size_t")] nuint num_paths);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_cf_paths(rocksdb_options_t* param0, [NativeTypeName("const rocksdb_dbpath_t **")] rocksdb_dbpath_t** path_values, [NativeTypeName("size_t")] nuint num_paths);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_env(rocksdb_options_t* param0, rocksdb_env_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_info_log(rocksdb_options_t* param0, rocksdb_logger_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_logger_t* rocksdb_options_get_info_log(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_info_log_level(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_info_log_level(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_logger_t* rocksdb_logger_create_stderr_logger(int log_level, [NativeTypeName("const char *")] sbyte* prefix);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_logger_t* rocksdb_logger_create_callback_logger(int log_level, [NativeTypeName("void (*)(void *, unsigned int, char *, size_t)")] delegate* unmanaged[Cdecl]<void*, uint, sbyte*, nuint, void> param1, void* priv);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_logger_destroy(rocksdb_logger_t* logger);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_file_checksum_gen_factory_t* rocksdb_file_checksum_gen_crc32c_factory_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_file_checksum_gen_factory_destroy(rocksdb_file_checksum_gen_factory_t* factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_file_checksum_gen_factory(rocksdb_options_t* param0, rocksdb_file_checksum_gen_factory_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sst_partitioner_factory_t* rocksdb_sst_partitioner_fixed_prefix_factory_create([NativeTypeName("size_t")] nuint prefix_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_partitioner_factory_destroy(rocksdb_sst_partitioner_factory_t* factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_sst_partitioner_factory(rocksdb_options_t* param0, rocksdb_sst_partitioner_factory_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_table_properties_collector_factory_destroy(rocksdb_table_properties_collector_factory_t* factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_table_properties_collector_factory(rocksdb_options_t* param0, rocksdb_table_properties_collector_factory_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_buffer_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_write_buffer_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_write_buffer_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_db_write_buffer_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_open_files(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_open_files(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_file_opening_threads(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_file_opening_threads(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_total_wal_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint n);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_max_total_wal_size(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options(rocksdb_options_t* param0, int param1, int param2, int param3, int param4);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_zstd_max_train_bytes(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compression_options_zstd_max_train_bytes(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_use_zstd_dict_trainer(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_compression_options_use_zstd_dict_trainer(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_parallel_threads(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compression_options_parallel_threads(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression_options_max_dict_buffer_bytes(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_compression_options_max_dict_buffer_bytes(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options(rocksdb_options_t* param0, int param1, int param2, int param3, int param4, [NativeTypeName("unsigned char")] byte param5);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options_zstd_max_train_bytes(rocksdb_options_t* param0, int param1, [NativeTypeName("unsigned char")] byte param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options_use_zstd_dict_trainer(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1, [NativeTypeName("unsigned char")] byte param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_bottommost_compression_options_use_zstd_dict_trainer(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression_options_max_dict_buffer_bytes(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1, [NativeTypeName("unsigned char")] byte param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_prefix_extractor(rocksdb_options_t* param0, rocksdb_slicetransform_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_num_levels(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_num_levels(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level0_file_num_compaction_trigger(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_level0_file_num_compaction_trigger(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level0_slowdown_writes_trigger(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_level0_slowdown_writes_trigger(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level0_stop_writes_trigger(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_level0_stop_writes_trigger(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_target_file_size_base(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_target_file_size_base(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_target_file_size_multiplier(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_target_file_size_multiplier(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bytes_for_level_base(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_max_bytes_for_level_base(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_level_compaction_dynamic_level_bytes(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_level_compaction_dynamic_level_bytes(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bytes_for_level_multiplier(rocksdb_options_t* param0, double param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_max_bytes_for_level_multiplier(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_bytes_for_level_multiplier_additional(rocksdb_options_t* param0, int* level_values, [NativeTypeName("size_t")] nuint num_levels);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_enable_statistics(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_ttl(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_ttl(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_periodic_compaction_seconds(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_periodic_compaction_seconds(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_op_scan_flush_trigger(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_memtable_op_scan_flush_trigger(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_avg_op_scan_flush_trigger(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_memtable_avg_op_scan_flush_trigger(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_tombstones_for_range_conversion(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_min_tombstones_for_range_conversion(rocksdb_options_t* param0);

    public const uint rocksdb_statistics_level_disable_all = 0;
    public const uint rocksdb_statistics_level_except_tickers = rocksdb_statistics_level_disable_all;
    public const uint rocksdb_statistics_level_except_histogram_or_timers = 1;
    public const uint rocksdb_statistics_level_except_timers = 2;
    public const uint rocksdb_statistics_level_except_detailed_timers = 3;
    public const uint rocksdb_statistics_level_except_time_for_mutex = 4;
    public const uint rocksdb_statistics_level_all = 5;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_statistics_level(rocksdb_options_t* param0, int level);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_statistics_level(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_skip_stats_update_on_db_open(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_skip_stats_update_on_db_open(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_blob_files(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_blob_files(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_blob_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_min_blob_size(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_file_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_blob_file_size(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_compression_type(rocksdb_options_t* opt, int val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_blob_compression_type(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_blob_gc(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_blob_gc(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_gc_age_cutoff(rocksdb_options_t* opt, double val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_blob_gc_age_cutoff(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_gc_force_threshold(rocksdb_options_t* opt, double val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_blob_gc_force_threshold(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_read_triggered_compaction_threshold(rocksdb_options_t* opt, double val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_read_triggered_compaction_threshold(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_compaction_trigger_wakeup_seconds(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_max_compaction_trigger_wakeup_seconds(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_compaction_readahead_size(rocksdb_options_t* opt, [NativeTypeName("uint64_t")] nuint val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_blob_compaction_readahead_size(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_file_starting_level(rocksdb_options_t* opt, int val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_blob_file_starting_level(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_blob_cache(rocksdb_options_t* opt, rocksdb_cache_t* blob_cache);

    public const uint rocksdb_prepopulate_blob_disable = 0;
    public const uint rocksdb_prepopulate_blob_flush_only = 1;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_prepopulate_blob_cache(rocksdb_options_t* opt, int val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_prepopulate_blob_cache(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_options_statistics_get_string(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_statistics_get_ticker_count(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint ticker_type);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_statistics_get_histogram_data(rocksdb_options_t* opt, [NativeTypeName("uint32_t")] uint histogram_type, rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_write_buffer_number(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_write_buffer_number(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_write_buffer_number_to_merge(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_min_write_buffer_number_to_merge(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_write_buffer_size_to_maintain(rocksdb_options_t* param0, [NativeTypeName("int64_t")] nint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern nint rocksdb_options_get_max_write_buffer_size_to_maintain(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_pipelined_write(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_pipelined_write(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_unordered_write(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_unordered_write(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_subcompactions(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_max_subcompactions(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_background_jobs(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_background_jobs(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_background_compactions(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_background_compactions(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_background_flushes(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_max_background_flushes(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_log_file_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_max_log_file_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_log_file_time_to_roll(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_log_file_time_to_roll(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_keep_log_file_num(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_keep_log_file_num(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_recycle_log_file_num(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_recycle_log_file_num(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_async_wal_precreate(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_async_wal_precreate(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_soft_pending_compaction_bytes_limit(rocksdb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_soft_pending_compaction_bytes_limit(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_hard_pending_compaction_bytes_limit(rocksdb_options_t* opt, [NativeTypeName("size_t")] nuint v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_hard_pending_compaction_bytes_limit(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_manifest_file_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_max_manifest_file_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_table_cache_numshardbits(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_table_cache_numshardbits(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_arena_block_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_arena_block_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_fsync(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_use_fsync(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_db_log_dir(rocksdb_options_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_dir(rocksdb_options_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_WAL_ttl_seconds(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_WAL_ttl_seconds(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_WAL_size_limit_MB(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_WAL_size_limit_MB(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_manifest_preallocation_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_manifest_preallocation_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_mmap_reads(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_mmap_reads(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_mmap_writes(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_mmap_writes(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_direct_reads(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_direct_reads(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_direct_io_for_flush_and_compaction(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_direct_io_for_flush_and_compaction(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_is_fd_close_on_exec(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_is_fd_close_on_exec(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_stats_dump_period_sec(rocksdb_options_t* param0, [NativeTypeName("unsigned int")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint rocksdb_options_get_stats_dump_period_sec(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_stats_persist_period_sec(rocksdb_options_t* param0, [NativeTypeName("unsigned int")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned int")]
    public static extern uint rocksdb_options_get_stats_persist_period_sec(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_advise_random_on_open(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_advise_random_on_open(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_use_adaptive_mutex(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_use_adaptive_mutex(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bytes_per_sync(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_bytes_per_sync(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_bytes_per_sync(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_wal_bytes_per_sync(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_writable_file_max_buffer_size(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_writable_file_max_buffer_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_allow_concurrent_memtable_write(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_allow_concurrent_memtable_write(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_enable_write_thread_adaptive_yield(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_enable_write_thread_adaptive_yield(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_sequential_skip_in_iterations(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_max_sequential_skip_in_iterations(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_disable_auto_compactions(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_disable_auto_compactions(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_optimize_filters_for_hits(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_optimize_filters_for_hits(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_delete_obsolete_files_period_micros(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_delete_obsolete_files_period_micros(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_prepare_for_bulk_load(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_vector_rep(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_prefix_bloom_size_ratio(rocksdb_options_t* param0, double param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_memtable_prefix_bloom_size_ratio(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_compaction_bytes(rocksdb_options_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_options_get_max_compaction_bytes(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_hash_skip_list_rep(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1, [NativeTypeName("int32_t")] int param2, [NativeTypeName("int32_t")] int param3);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_hash_link_list_rep(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_plain_table_factory(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1, int param2, double param3, [NativeTypeName("size_t")] nuint param4, [NativeTypeName("size_t")] nuint param5, [NativeTypeName("char")] sbyte param6, [NativeTypeName("unsigned char")] byte param7, [NativeTypeName("unsigned char")] byte param8);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_write_dbid_to_manifest(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_dbid_to_manifest(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_write_identity_file(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_write_identity_file(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_track_and_verify_wals_in_manifest(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_track_and_verify_wals_in_manifest(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_min_level_to_compress(rocksdb_options_t* opt, int level);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_huge_page_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_memtable_huge_page_size(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_batch_lookup_optimization(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_memtable_batch_lookup_optimization(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_max_successive_merges(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_max_successive_merges(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bloom_locality(rocksdb_options_t* param0, [NativeTypeName("uint32_t")] uint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_options_get_bloom_locality(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_inplace_update_support(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_inplace_update_support(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_inplace_update_num_locks(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_options_get_inplace_update_num_locks(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_report_bg_io_stats(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_report_bg_io_stats(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_avoid_unnecessary_blocking_io(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_avoid_unnecessary_blocking_io(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_experimental_mempurge_threshold(rocksdb_options_t* param0, double param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_options_get_experimental_mempurge_threshold(rocksdb_options_t* param0);

    public const uint rocksdb_tolerate_corrupted_tail_records_recovery = 0;
    public const uint rocksdb_absolute_consistency_recovery = 1;
    public const uint rocksdb_point_in_time_recovery = 2;
    public const uint rocksdb_skip_any_corrupted_records_recovery = 3;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_recovery_mode(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_wal_recovery_mode(rocksdb_options_t* param0);

    public const uint rocksdb_no_compression = 0;
    public const uint rocksdb_snappy_compression = 1;
    public const uint rocksdb_zlib_compression = 2;
    public const uint rocksdb_bz2_compression = 3;
    public const uint rocksdb_lz4_compression = 4;
    public const uint rocksdb_lz4hc_compression = 5;
    public const uint rocksdb_xpress_compression = 6;
    public const uint rocksdb_zstd_compression = 7;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compression(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compression(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_bottommost_compression(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_bottommost_compression(rocksdb_options_t* param0);

    public const uint rocksdb_level_compaction = 0;
    public const uint rocksdb_universal_compaction = 1;
    public const uint rocksdb_fifo_compaction = 2;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_style(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compaction_style(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_universal_compaction_options(rocksdb_options_t* param0, rocksdb_universal_compaction_options_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_fifo_compaction_options(rocksdb_options_t* opt, rocksdb_fifo_compaction_options_t* fifo);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_ratelimiter(rocksdb_options_t* opt, rocksdb_ratelimiter_t* limiter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_atomic_flush(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_atomic_flush(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_row_cache(rocksdb_options_t* opt, rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_compact_on_deletion_collector_factory(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint window_size, [NativeTypeName("size_t")] nuint num_dels_trigger);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_compact_on_deletion_collector_factory_del_ratio(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint window_size, [NativeTypeName("size_t")] nuint num_dels_trigger, double deletion_ratio);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_add_compact_on_deletion_collector_factory_min_file_size(rocksdb_options_t* param0, [NativeTypeName("size_t")] nuint window_size, [NativeTypeName("size_t")] nuint num_dels_trigger, double deletion_ratio, [NativeTypeName("uint64_t")] nuint min_file_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_manual_wal_flush(rocksdb_options_t* opt, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_options_get_manual_wal_flush(rocksdb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_wal_compression(rocksdb_options_t* opt, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_wal_compression(rocksdb_options_t* opt);

    public const uint rocksdb_k_by_compensated_size_compaction_pri = 0;
    public const uint rocksdb_k_oldest_largest_seq_first_compaction_pri = 1;
    public const uint rocksdb_k_oldest_smallest_seq_first_compaction_pri = 2;
    public const uint rocksdb_k_min_overlapping_ratio_compaction_pri = 3;
    public const uint rocksdb_k_round_robin_compaction_pri = 4;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_pri(rocksdb_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_options_get_compaction_pri(rocksdb_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ratelimiter_t* rocksdb_ratelimiter_create([NativeTypeName("int64_t")] nint rate_bytes_per_sec, [NativeTypeName("int64_t")] nint refill_period_us, [NativeTypeName("int32_t")] int fairness);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ratelimiter_t* rocksdb_ratelimiter_create_auto_tuned([NativeTypeName("int64_t")] nint rate_bytes_per_sec, [NativeTypeName("int64_t")] nint refill_period_us, [NativeTypeName("int32_t")] int fairness);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ratelimiter_t* rocksdb_ratelimiter_create_with_mode([NativeTypeName("int64_t")] nint rate_bytes_per_sec, [NativeTypeName("int64_t")] nint refill_period_us, [NativeTypeName("int32_t")] int fairness, int mode, [NativeTypeName("_Bool")] bool auto_tuned);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
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

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_set_perf_level(int param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_perfcontext_t* rocksdb_perfcontext_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_perfcontext_reset(rocksdb_perfcontext_t* context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_perfcontext_report(rocksdb_perfcontext_t* context, [NativeTypeName("unsigned char")] byte exclude_zero_counters);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_perfcontext_metric(rocksdb_perfcontext_t* context, int metric);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_perfcontext_destroy(rocksdb_perfcontext_t* context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionfilter_t* rocksdb_compactionfilter_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("unsigned char (*)(void *, int, const char *, size_t, const char *, size_t, char **, size_t *, unsigned char *)")] delegate* unmanaged[Cdecl]<void*, int, sbyte*, nuint, sbyte*, nuint, sbyte**, nuint*, byte*, byte> filter, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionfilter_set_ignore_snapshots(rocksdb_compactionfilter_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionfilter_destroy(rocksdb_compactionfilter_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionfiltercontext_is_full_compaction(rocksdb_compactionfiltercontext_t* context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionfiltercontext_is_manual_compaction(rocksdb_compactionfiltercontext_t* context);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionfilterfactory_t* rocksdb_compactionfilterfactory_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("rocksdb_compactionfilter_t *(*)(void *, rocksdb_compactionfiltercontext_t *)")] delegate* unmanaged[Cdecl]<void*, rocksdb_compactionfiltercontext_t*, rocksdb_compactionfilter_t*> create_compaction_filter, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionfilterfactory_destroy(rocksdb_compactionfilterfactory_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_comparator_t* rocksdb_comparator_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("int (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, int> compare, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_comparator_destroy(rocksdb_comparator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_comparator_t* rocksdb_comparator_with_ts_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("int (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, int> compare, [NativeTypeName("int (*)(void *, const char *, size_t, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, int> compare_ts, [NativeTypeName("int (*)(void *, const char *, size_t, unsigned char, const char *, size_t, unsigned char)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, byte, sbyte*, nuint, byte, int> compare_without_ts, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name, [NativeTypeName("size_t")] nuint timestamp_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_filterpolicy_destroy(rocksdb_filterpolicy_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_bloom(double bits_per_key);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_bloom_full(double bits_per_key);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_ribbon(double bloom_equivalent_bits_per_key);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_filterpolicy_t* rocksdb_filterpolicy_create_ribbon_hybrid(double bloom_equivalent_bits_per_key, int bloom_before_level);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_mergeoperator_t* rocksdb_mergeoperator_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("char *(*)(void *, const char *, size_t, const char *, size_t, const char *const *, const size_t *, int, unsigned char *, size_t *)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte*, nuint, sbyte**, nuint*, int, byte*, nuint*, sbyte*> full_merge, [NativeTypeName("char *(*)(void *, const char *, size_t, const char *const *, const size_t *, int, unsigned char *, size_t *)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, sbyte**, nuint*, int, byte*, nuint*, sbyte*> partial_merge, [NativeTypeName("void (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, void> delete_value, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_mergeoperator_destroy(rocksdb_mergeoperator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_readoptions_t* rocksdb_readoptions_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_destroy(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_verify_checksums(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_verify_checksums(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_fill_cache(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_fill_cache(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_snapshot(rocksdb_readoptions_t* param0, [NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_iterate_upper_bound(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_iterate_lower_bound(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_read_tier(rocksdb_readoptions_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_readoptions_get_read_tier(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_tailing(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_tailing(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_readahead_size(rocksdb_readoptions_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_readoptions_get_readahead_size(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_prefix_same_as_start(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_prefix_same_as_start(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_pin_data(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_pin_data(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_total_order_seek(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_total_order_seek(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_max_skippable_internal_keys(rocksdb_readoptions_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_readoptions_get_max_skippable_internal_keys(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_background_purge_on_iterator_cleanup(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_background_purge_on_iterator_cleanup(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_ignore_range_deletions(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_ignore_range_deletions(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_deadline(rocksdb_readoptions_t* param0, [NativeTypeName("uint64_t")] nuint microseconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_readoptions_get_deadline(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_io_timeout(rocksdb_readoptions_t* param0, [NativeTypeName("uint64_t")] nuint microseconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_readoptions_get_io_timeout(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_async_io(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_async_io(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_optimize_multiget_for_io(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_readoptions_get_optimize_multiget_for_io(rocksdb_readoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_timestamp(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_iter_start_ts(rocksdb_readoptions_t* param0, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_readoptions_set_auto_readahead_size(rocksdb_readoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writeoptions_t* rocksdb_writeoptions_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_destroy(rocksdb_writeoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_sync(rocksdb_writeoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_sync(rocksdb_writeoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_disable_WAL(rocksdb_writeoptions_t* opt, int disable);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_disable_WAL(rocksdb_writeoptions_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_ignore_missing_column_families(rocksdb_writeoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_ignore_missing_column_families(rocksdb_writeoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_no_slowdown(rocksdb_writeoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_no_slowdown(rocksdb_writeoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_low_pri(rocksdb_writeoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_low_pri(rocksdb_writeoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_writeoptions_set_memtable_insert_hint_per_batch(rocksdb_writeoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_writeoptions_get_memtable_insert_hint_per_batch(rocksdb_writeoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactoptions_t* rocksdb_compactoptions_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_destroy(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_exclusive_manual_compaction(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_exclusive_manual_compaction(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_bottommost_level_compaction(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_bottommost_level_compaction(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_change_level(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_change_level(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_target_level(rocksdb_compactoptions_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_target_level(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_target_path_id(rocksdb_compactoptions_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_target_path_id(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_allow_write_stall(rocksdb_compactoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactoptions_get_allow_write_stall(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_max_subcompactions(rocksdb_compactoptions_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactoptions_get_max_subcompactions(rocksdb_compactoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactoptions_set_full_history_ts_low(rocksdb_compactoptions_t* param0, [NativeTypeName("char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_flushoptions_t* rocksdb_flushoptions_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_destroy(rocksdb_flushoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_flushoptions_set_wait(rocksdb_flushoptions_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_flushoptions_get_wait(rocksdb_flushoptions_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_memory_allocator_t* rocksdb_jemalloc_nodump_allocator_create([NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_allocator_destroy(rocksdb_memory_allocator_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_lru_cache_options_t* rocksdb_lru_cache_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_destroy(rocksdb_lru_cache_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_set_capacity(rocksdb_lru_cache_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_set_num_shard_bits(rocksdb_lru_cache_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_lru_cache_options_set_memory_allocator(rocksdb_lru_cache_options_t* param0, rocksdb_memory_allocator_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_lru([NativeTypeName("size_t")] nuint capacity);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_lru_with_strict_capacity_limit([NativeTypeName("size_t")] nuint capacity);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_lru_opts([NativeTypeName("const rocksdb_lru_cache_options_t *")] rocksdb_lru_cache_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cache_destroy(rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cache_disown_data(rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cache_set_capacity(rocksdb_cache_t* cache, [NativeTypeName("size_t")] nuint capacity);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_capacity([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_usage([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_pinned_usage([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_table_address_count([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_cache_get_occupancy_count([NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_write_buffer_manager_t* rocksdb_write_buffer_manager_create([NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("_Bool")] bool allow_stall);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_write_buffer_manager_t* rocksdb_write_buffer_manager_create_with_cache([NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("const rocksdb_cache_t *")] rocksdb_cache_t* cache, [NativeTypeName("_Bool")] bool allow_stall);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_buffer_manager_destroy(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern bool rocksdb_write_buffer_manager_enabled(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern bool rocksdb_write_buffer_manager_cost_to_cache(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_memory_usage(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_mutable_memtable_memory_usage(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_dummy_entries_in_cache_usage(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_write_buffer_manager_buffer_size(rocksdb_write_buffer_manager_t* wbm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_buffer_manager_set_buffer_size(rocksdb_write_buffer_manager_t* wbm, [NativeTypeName("size_t")] nuint new_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_write_buffer_manager_set_allow_stall(rocksdb_write_buffer_manager_t* wbm, [NativeTypeName("_Bool")] bool new_allow_stall);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sst_file_manager_t* rocksdb_sst_file_manager_create(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_destroy(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_max_allowed_space_usage(rocksdb_sst_file_manager_t* sfm, [NativeTypeName("uint64_t")] nuint max_allowed_space);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_compaction_buffer_size(rocksdb_sst_file_manager_t* sfm, [NativeTypeName("uint64_t")] nuint compaction_buffer_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern bool rocksdb_sst_file_manager_is_max_allowed_space_reached(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("_Bool")]
    public static extern bool rocksdb_sst_file_manager_is_max_allowed_space_reached_including_compactions(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_sst_file_manager_get_total_size(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("int64_t")]
    public static extern nint rocksdb_sst_file_manager_get_delete_rate_bytes_per_second(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_delete_rate_bytes_per_second(rocksdb_sst_file_manager_t* sfm, [NativeTypeName("int64_t")] nint delete_rate);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_sst_file_manager_get_max_trash_db_ratio(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_manager_set_max_trash_db_ratio(rocksdb_sst_file_manager_t* sfm, double ratio);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_sst_file_manager_get_total_trash_size(rocksdb_sst_file_manager_t* sfm);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_hyper_clock_cache_options_t* rocksdb_hyper_clock_cache_options_create([NativeTypeName("size_t")] nuint capacity, [NativeTypeName("size_t")] nuint estimated_entry_charge);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_destroy(rocksdb_hyper_clock_cache_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_capacity(rocksdb_hyper_clock_cache_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_estimated_entry_charge(rocksdb_hyper_clock_cache_options_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_num_shard_bits(rocksdb_hyper_clock_cache_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_hyper_clock_cache_options_set_memory_allocator(rocksdb_hyper_clock_cache_options_t* param0, rocksdb_memory_allocator_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_hyper_clock([NativeTypeName("size_t")] nuint capacity, [NativeTypeName("size_t")] nuint estimated_entry_charge);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_cache_t* rocksdb_cache_create_hyper_clock_opts([NativeTypeName("const rocksdb_hyper_clock_cache_options_t *")] rocksdb_hyper_clock_cache_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_dbpath_t* rocksdb_dbpath_create([NativeTypeName("const char *")] sbyte* path, [NativeTypeName("uint64_t")] nuint target_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_dbpath_destroy(rocksdb_dbpath_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_env_t* rocksdb_create_default_env();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_env_t* rocksdb_create_mem_env();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_background_threads(rocksdb_env_t* env, int n);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_background_threads(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_high_priority_background_threads(rocksdb_env_t* env, int n);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_high_priority_background_threads(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_low_priority_background_threads(rocksdb_env_t* env, int n);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_low_priority_background_threads(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_set_bottom_priority_background_threads(rocksdb_env_t* env, int n);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_env_get_bottom_priority_background_threads(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_join_all_threads(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_thread_pool_io_priority(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_high_priority_thread_pool_io_priority(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_thread_pool_cpu_priority(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_lower_high_priority_thread_pool_cpu_priority(rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_env_destroy(rocksdb_env_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_envoptions_t* rocksdb_envoptions_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_envoptions_destroy(rocksdb_envoptions_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_create_dir_if_missing(rocksdb_env_t* env, [NativeTypeName("const char *")] sbyte* path, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sstfilewriter_t* rocksdb_sstfilewriter_create([NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* io_options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sstfilewriter_t* rocksdb_sstfilewriter_create_with_comparator([NativeTypeName("const rocksdb_envoptions_t *")] rocksdb_envoptions_t* env, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* io_options, [NativeTypeName("const rocksdb_comparator_t *")] rocksdb_comparator_t* comparator);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_open(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_add(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_put(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_put_with_ts(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_merge(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_delete(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_delete_with_ts(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* ts, [NativeTypeName("size_t")] nuint tslen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_delete_range(rocksdb_sstfilewriter_t* writer, [NativeTypeName("const char *")] sbyte* begin_key, [NativeTypeName("size_t")] nuint begin_keylen, [NativeTypeName("const char *")] sbyte* end_key, [NativeTypeName("size_t")] nuint end_keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_finish(rocksdb_sstfilewriter_t* writer, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_file_size(rocksdb_sstfilewriter_t* writer, [NativeTypeName("uint64_t *")] nuint* file_size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sstfilewriter_destroy(rocksdb_sstfilewriter_t* writer);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_ingestexternalfileoptions_t* rocksdb_ingestexternalfileoptions_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_move_files(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte move_files);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_snapshot_consistency(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte snapshot_consistency);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_allow_global_seqno(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte allow_global_seqno);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_allow_blocking_flush(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte allow_blocking_flush);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_ingest_behind(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte ingest_behind);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_set_fail_if_not_bottommost_level(rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("unsigned char")] byte fail_if_not_bottommost_level);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingestexternalfileoptions_destroy(rocksdb_ingestexternalfileoptions_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingest_external_file(rocksdb_t* db, [NativeTypeName("const char *const *")] sbyte** file_list, [NativeTypeName("const size_t")] nuint list_len, [NativeTypeName("const rocksdb_ingestexternalfileoptions_t *")] rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_ingest_external_file_cf(rocksdb_t* db, rocksdb_column_family_handle_t* handle, [NativeTypeName("const char *const *")] sbyte** file_list, [NativeTypeName("const size_t")] nuint list_len, [NativeTypeName("const rocksdb_ingestexternalfileoptions_t *")] rocksdb_ingestexternalfileoptions_t* opt, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_try_catch_up_with_primary(rocksdb_t* db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slicetransform_t* rocksdb_slicetransform_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("char *(*)(void *, const char *, size_t, size_t *)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, nuint*, sbyte*> transform, [NativeTypeName("unsigned char (*)(void *, const char *, size_t)")] delegate* unmanaged[Cdecl]<void*, sbyte*, nuint, byte> in_domain, [NativeTypeName("const char *(*)(void *)")] delegate* unmanaged[Cdecl]<void*, sbyte*> name);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slicetransform_t* rocksdb_slicetransform_create_fixed_prefix([NativeTypeName("size_t")] nuint param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_slicetransform_t* rocksdb_slicetransform_create_noop();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_slicetransform_destroy(rocksdb_slicetransform_t* param0);

    public const uint rocksdb_similar_size_compaction_stop_style = 0;
    public const uint rocksdb_total_size_compaction_stop_style = 1;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_universal_compaction_options_t* rocksdb_universal_compaction_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_size_ratio(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_size_ratio(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_min_merge_width(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_min_merge_width(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_max_merge_width(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_max_merge_width(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_max_size_amplification_percent(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_max_size_amplification_percent(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_compression_size_percent(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_compression_size_percent(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_set_stop_style(rocksdb_universal_compaction_options_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_universal_compaction_options_get_stop_style(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_universal_compaction_options_destroy(rocksdb_universal_compaction_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_fifo_compaction_options_t* rocksdb_fifo_compaction_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_allow_compaction(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("unsigned char")] byte allow_compaction);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_fifo_compaction_options_get_allow_compaction(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_max_table_files_size(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("uint64_t")] nuint size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_fifo_compaction_options_get_max_table_files_size(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_max_data_files_size(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("uint64_t")] nuint size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_fifo_compaction_options_get_max_data_files_size(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_set_use_kv_ratio_compaction(rocksdb_fifo_compaction_options_t* fifo_opts, [NativeTypeName("unsigned char")] byte use_kv_ratio_compaction);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_fifo_compaction_options_get_use_kv_ratio_compaction(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_fifo_compaction_options_destroy(rocksdb_fifo_compaction_options_t* fifo_opts);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefiles_t* rocksdb_livefiles_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_livefiles_count([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_column_family_name([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_name([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_directory([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_livefiles_level([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_livefiles_size([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_smallestkey([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index, [NativeTypeName("size_t *")] nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_livefiles_largestkey([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index, [NativeTypeName("size_t *")] nuint* size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_livefiles_smallest_seqno([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_livefiles_largest_seqno([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_livefiles_entries([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_livefiles_deletions([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0, int index);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_destroy([NativeTypeName("const rocksdb_livefiles_t *")] rocksdb_livefiles_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefile_t* rocksdb_livefile_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_column_family_name(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_level(rocksdb_livefile_t* param0, int param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_name(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_directory(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_size(rocksdb_livefile_t* param0, [NativeTypeName("size_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_smallest_key(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1, [NativeTypeName("size_t")] nuint param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_largest_key(rocksdb_livefile_t* param0, [NativeTypeName("const char *")] sbyte* param1, [NativeTypeName("size_t")] nuint param2);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_smallest_seqno(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_largest_seqno(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_num_entries(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_set_num_deletions(rocksdb_livefile_t* param0, [NativeTypeName("uint64_t")] nuint param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefile_destroy(rocksdb_livefile_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_livefiles_add(rocksdb_livefiles_t* param0, rocksdb_livefile_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_get_options_from_string([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* base_options, [NativeTypeName("const char *")] sbyte* opts_str, rocksdb_options_t* new_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_file_in_range(rocksdb_t* db, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_delete_file_in_range_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* start_key, [NativeTypeName("size_t")] nuint start_key_len, [NativeTypeName("const char *")] sbyte* limit_key, [NativeTypeName("size_t")] nuint limit_key_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_t* rocksdb_get_column_family_metadata(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_import_column_family_options_t* rocksdb_import_column_family_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_import_column_family_options_set_move_files(rocksdb_import_column_family_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_import_column_family_options_destroy(rocksdb_import_column_family_options_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_export_import_files_metadata_t* rocksdb_export_import_files_metadata_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_export_import_files_metadata_get_db_comparator_name(rocksdb_export_import_files_metadata_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_export_import_files_metadata_set_db_comparator_name(rocksdb_export_import_files_metadata_t* param0, [NativeTypeName("const char *")] sbyte* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_livefiles_t* rocksdb_export_import_files_metadata_get_files(rocksdb_export_import_files_metadata_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_export_import_files_metadata_set_files(rocksdb_export_import_files_metadata_t* param0, rocksdb_livefiles_t* param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_export_import_files_metadata_destroy(rocksdb_export_import_files_metadata_t* param0);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_metadata_t* rocksdb_get_column_family_metadata_cf(rocksdb_t* db, rocksdb_column_family_handle_t* column_family);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_column_family_metadata_destroy(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_column_family_metadata_get_size(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_column_family_metadata_get_file_count(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_column_family_metadata_get_name(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_column_family_metadata_get_level_count(rocksdb_column_family_metadata_t* cf_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_level_metadata_t* rocksdb_column_family_metadata_get_level_metadata(rocksdb_column_family_metadata_t* cf_meta, [NativeTypeName("size_t")] nuint i);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_level_metadata_destroy(rocksdb_level_metadata_t* level_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_level_metadata_get_level(rocksdb_level_metadata_t* level_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_level_metadata_get_size(rocksdb_level_metadata_t* level_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("size_t")]
    public static extern nuint rocksdb_level_metadata_get_file_count(rocksdb_level_metadata_t* level_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_sst_file_metadata_t* rocksdb_level_metadata_get_sst_file_metadata(rocksdb_level_metadata_t* level_meta, [NativeTypeName("size_t")] nuint i);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_sst_file_metadata_destroy(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_relative_filename(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_directory(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_sst_file_metadata_get_size(rocksdb_sst_file_metadata_t* file_meta);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_smallestkey(rocksdb_sst_file_metadata_t* file_meta, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_sst_file_metadata_get_largestkey(rocksdb_sst_file_metadata_t* file_meta, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_column_family_handle_t* rocksdb_transactiondb_create_column_family(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* column_family_options, [NativeTypeName("const char *")] sbyte* column_family_name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transactiondb_t* rocksdb_transactiondb_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const rocksdb_transactiondb_options_t *")] rocksdb_transactiondb_options_t* txn_db_options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transactiondb_t* rocksdb_transactiondb_open_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const rocksdb_transactiondb_options_t *")] rocksdb_transactiondb_options_t* txn_db_options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_snapshot_t *")]
    public static extern rocksdb_snapshot_t* rocksdb_transactiondb_create_snapshot(rocksdb_transactiondb_t* txn_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_release_snapshot(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_snapshot_t *")] rocksdb_snapshot_t* snapshot);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transactiondb_property_value(rocksdb_transactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_transactiondb_property_int(rocksdb_transactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] nuint* out_val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_transactiondb_get_base_db(rocksdb_transactiondb_t* txn_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_close_base_db(rocksdb_t* base_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_t* rocksdb_transaction_begin(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* write_options, [NativeTypeName("const rocksdb_transaction_options_t *")] rocksdb_transaction_options_t* txn_options, rocksdb_transaction_t* old_txn);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_t** rocksdb_transactiondb_get_prepared_transactions(rocksdb_transactiondb_t* txn_db, [NativeTypeName("size_t *")] nuint* cnt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_name(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("size_t")] nuint name_len, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_name(rocksdb_transaction_t* txn, [NativeTypeName("size_t *")] nuint* name_len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_prepare(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_commit(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rollback(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_savepoint(rocksdb_transaction_t* txn);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rollback_to_savepoint(rocksdb_transaction_t* txn, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_destroy(rocksdb_transaction_t* txn);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_writebatch_wi_t* rocksdb_transaction_get_writebatch_wi(rocksdb_transaction_t* txn);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rebuild_from_writebatch(rocksdb_transaction_t* txn, rocksdb_writebatch_t* writebatch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_rebuild_from_writebatch_wi(rocksdb_transaction_t* txn, rocksdb_writebatch_wi_t* wi, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_commit_timestamp(rocksdb_transaction_t* txn, [NativeTypeName("uint64_t")] nuint commit_timestamp);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_set_read_timestamp_for_validation(rocksdb_transaction_t* txn, [NativeTypeName("uint64_t")] nuint read_timestamp);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const rocksdb_snapshot_t *")]
    public static extern rocksdb_snapshot_t* rocksdb_transaction_get_snapshot(rocksdb_transaction_t* txn);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_for_update(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned_for_update(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transaction_get_for_update_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transaction_get_pinned_for_update_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("unsigned char")] byte exclusive, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get_for_update(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_multi_get_for_update_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transactiondb_get(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("size_t *")] nuint* vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transactiondb_get_pinned(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_transactiondb_get_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_transactiondb_get_pinned_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_multi_get(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_multi_get_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("size_t")] nuint num_keys, [NativeTypeName("const char *const *")] sbyte** keys_list, [NativeTypeName("const size_t *")] nuint* keys_list_sizes, [NativeTypeName("char **")] sbyte** values_list, [NativeTypeName("size_t *")] nuint* values_list_sizes, [NativeTypeName("char **")] sbyte** errs);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_put(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_put_cf(rocksdb_transaction_t* txn, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_put(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_put_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vallen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_write(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_t* batch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_merge(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_merge_cf(rocksdb_transaction_t* txn, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_merge(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_merge_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("const char *")] sbyte* val, [NativeTypeName("size_t")] nuint vlen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_delete(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_delete_cf(rocksdb_transaction_t* txn, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_put_log_data(rocksdb_transaction_t* txn, [NativeTypeName("const char *")] sbyte* blob, [NativeTypeName("size_t")] nuint len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_delete(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint klen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_delete_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transaction_create_iterator(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transaction_create_iterator_cf(rocksdb_transaction_t* txn, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transactiondb_create_iterator(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_iterator_t* rocksdb_transactiondb_create_iterator_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_close(rocksdb_transactiondb_t* txn_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_cf(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_cfs(rocksdb_transactiondb_t* txn_db, [NativeTypeName("const rocksdb_flushoptions_t *")] rocksdb_flushoptions_t* options, rocksdb_column_family_handle_t** column_families, int num_column_families, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_flush_wal(rocksdb_transactiondb_t* txn_db, [NativeTypeName("unsigned char")] byte sync, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_checkpoint_t* rocksdb_transactiondb_checkpoint_object_create(rocksdb_transactiondb_t* txn_db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_t* rocksdb_optimistictransactiondb_open([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransactiondb_t* rocksdb_optimistictransactiondb_open_column_families([NativeTypeName("const rocksdb_options_t *")] rocksdb_options_t* options, [NativeTypeName("const char *")] sbyte* name, int num_column_families, [NativeTypeName("const char *const *")] sbyte** column_family_names, [NativeTypeName("const rocksdb_options_t *const *")] rocksdb_options_t** column_family_options, rocksdb_column_family_handle_t** column_family_handles, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_t* rocksdb_optimistictransactiondb_get_base_db(rocksdb_optimistictransactiondb_t* otxn_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_close_base_db(rocksdb_t* base_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_t* rocksdb_optimistictransaction_begin(rocksdb_optimistictransactiondb_t* otxn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* write_options, [NativeTypeName("const rocksdb_optimistictransaction_options_t *")] rocksdb_optimistictransaction_options_t* otxn_options, rocksdb_transaction_t* old_txn);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_write(rocksdb_optimistictransactiondb_t* otxn_db, [NativeTypeName("const rocksdb_writeoptions_t *")] rocksdb_writeoptions_t* options, rocksdb_writebatch_t* batch, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransactiondb_close(rocksdb_optimistictransactiondb_t* otxn_db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_checkpoint_t* rocksdb_optimistictransactiondb_checkpoint_object_create(rocksdb_optimistictransactiondb_t* otxn_db, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transactiondb_options_t* rocksdb_transactiondb_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_destroy(rocksdb_transactiondb_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_max_num_locks(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] nint max_num_locks);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_num_stripes(rocksdb_transactiondb_options_t* opt, [NativeTypeName("size_t")] nuint num_stripes);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_transaction_lock_timeout(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] nint txn_lock_timeout);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transactiondb_options_set_default_lock_timeout(rocksdb_transactiondb_options_t* opt, [NativeTypeName("int64_t")] nint default_lock_timeout);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_transaction_options_t* rocksdb_transaction_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_destroy(rocksdb_transaction_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_set_snapshot(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_deadlock_detect(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_lock_timeout(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] nint lock_timeout);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_expiration(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] nint expiration);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_deadlock_detect_depth(rocksdb_transaction_options_t* opt, [NativeTypeName("int64_t")] nint depth);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_max_write_batch_size(rocksdb_transaction_options_t* opt, [NativeTypeName("size_t")] nuint size);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_transaction_options_set_skip_prepare(rocksdb_transaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_optimistictransaction_options_t* rocksdb_optimistictransaction_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransaction_options_destroy(rocksdb_optimistictransaction_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_optimistictransaction_options_set_set_snapshot(rocksdb_optimistictransaction_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_optimistictransactiondb_property_value(rocksdb_optimistictransactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_optimistictransactiondb_property_int(rocksdb_optimistictransactiondb_t* db, [NativeTypeName("const char *")] sbyte* propname, [NativeTypeName("uint64_t *")] nuint* out_val);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_free(void* ptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_get_pinned(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnableslice_t* rocksdb_get_pinned_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_pinnableslice_destroy(rocksdb_pinnableslice_t* v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_pinnableslice_value([NativeTypeName("const rocksdb_pinnableslice_t *")] rocksdb_pinnableslice_t* t, [NativeTypeName("size_t *")] nuint* vlen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_memory_consumers_t* rocksdb_memory_consumers_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_consumers_add_db(rocksdb_memory_consumers_t* consumers, rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_consumers_add_cache(rocksdb_memory_consumers_t* consumers, rocksdb_cache_t* cache);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_memory_consumers_destroy(rocksdb_memory_consumers_t* consumers);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_memory_usage_t* rocksdb_approximate_memory_usage_create(rocksdb_memory_consumers_t* consumers, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_approximate_memory_usage_destroy(rocksdb_memory_usage_t* usage);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_approximate_memory_usage_get_mem_table_total(rocksdb_memory_usage_t* memory_usage);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_approximate_memory_usage_get_mem_table_unflushed(rocksdb_memory_usage_t* memory_usage);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_approximate_memory_usage_get_mem_table_readers_total(rocksdb_memory_usage_t* memory_usage);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_approximate_memory_usage_get_cache_total(rocksdb_memory_usage_t* memory_usage);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_dump_malloc_stats(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_memtable_whole_key_filtering(rocksdb_options_t* param0, [NativeTypeName("unsigned char")] byte param1);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_cancel_all_background_work(rocksdb_t* db, [NativeTypeName("unsigned char")] byte wait);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_disable_manual_compaction(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_enable_manual_compaction(rocksdb_t* db);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_statistics_histogram_data_t* rocksdb_statistics_histogram_data_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_statistics_histogram_data_destroy(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_median(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_p95(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_p99(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_average(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_std_dev(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_max(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_statistics_histogram_data_get_count(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_statistics_histogram_data_get_sum(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern double rocksdb_statistics_histogram_data_get_min(rocksdb_statistics_histogram_data_t* data);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact(rocksdb_t* db, rocksdb_wait_for_compact_options_t* options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_wait_for_compact_options_t* rocksdb_wait_for_compact_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_destroy(rocksdb_wait_for_compact_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_abort_on_pause(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_abort_on_pause(rocksdb_wait_for_compact_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_flush(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_flush(rocksdb_wait_for_compact_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_close_db(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("unsigned char")] byte v);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_wait_for_compact_options_get_close_db(rocksdb_wait_for_compact_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_wait_for_compact_options_set_timeout(rocksdb_wait_for_compact_options_t* opt, [NativeTypeName("uint64_t")] nuint microseconds);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_wait_for_compact_options_get_timeout(rocksdb_wait_for_compact_options_t* opt);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnable_handle_t* rocksdb_get_pinned_v2(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_pinnable_handle_t* rocksdb_get_pinned_cf_v2(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_pinnable_handle_get_value([NativeTypeName("const rocksdb_pinnable_handle_t *")] rocksdb_pinnable_handle_t* handle, [NativeTypeName("size_t *")] nuint* vallen);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_pinnable_handle_destroy(rocksdb_pinnable_handle_t* handle);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_get_into_buffer(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("unsigned char *")] byte* found, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_get_into_buffer_cf(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, rocksdb_column_family_handle_t* column_family, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("size_t")] nuint keylen, [NativeTypeName("char *")] sbyte* buffer, [NativeTypeName("size_t")] nuint buffer_size, [NativeTypeName("size_t *")] nuint* vallen, [NativeTypeName("unsigned char *")] byte* found, [NativeTypeName("char **")] sbyte** errptr);

    public const uint rocksdb_compactionservice_jobstatus_success = 0;
    public const uint rocksdb_compactionservice_jobstatus_failure = 1;
    public const uint rocksdb_compactionservice_jobstatus_aborted = 2;
    public const uint rocksdb_compactionservice_jobstatus_use_local = 3;

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionservice_scheduleresponse_t* rocksdb_compactionservice_scheduleresponse_create([NativeTypeName("const char *")] sbyte* scheduled_job_id, int status, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionservice_scheduleresponse_t* rocksdb_compactionservice_scheduleresponse_create_with_status(int status, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_scheduleresponse_getstatus([NativeTypeName("const rocksdb_compactionservice_scheduleresponse_t *")] rocksdb_compactionservice_scheduleresponse_t* response);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_scheduleresponse_get_scheduled_job_id([NativeTypeName("const rocksdb_compactionservice_scheduleresponse_t *")] rocksdb_compactionservice_scheduleresponse_t* response, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compactionservice_scheduleresponse_t_destroy(rocksdb_compactionservice_scheduleresponse_t* response);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_db_name([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_db_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_db_session_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_compactionservice_jobinfo_t_get_cf_name([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info, [NativeTypeName("size_t *")] nuint* len);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint32_t")]
    public static extern uint rocksdb_compactionservice_jobinfo_t_get_cf_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("uint64_t")]
    public static extern nuint rocksdb_compactionservice_jobinfo_t_get_job_id([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_priority([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_compaction_reason([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_base_input_level([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern int rocksdb_compactionservice_jobinfo_t_get_output_level([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionservice_jobinfo_t_is_full_compaction([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionservice_jobinfo_t_is_manual_compaction([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char")]
    public static extern byte rocksdb_compactionservice_jobinfo_t_is_bottommost_level([NativeTypeName("const rocksdb_compactionservice_jobinfo_t *")] rocksdb_compactionservice_jobinfo_t* info);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compactionservice_t* rocksdb_compactionservice_create(void* state, [NativeTypeName("void (*)(void *)")] delegate* unmanaged[Cdecl]<void*, void> destructor, [NativeTypeName("rocksdb_compaction_service_schedule_cb")] delegate* unmanaged[Cdecl]<void*, rocksdb_compactionservice_jobinfo_t*, sbyte*, nuint, rocksdb_compactionservice_scheduleresponse_t*> schedule, [NativeTypeName("const char *")] sbyte* name, [NativeTypeName("rocksdb_compaction_service_wait_cb")] delegate* unmanaged[Cdecl]<void*, sbyte*, sbyte**, nuint*, int> wait, [NativeTypeName("rocksdb_compaction_service_cancel_awaiting_jobs_cb")] delegate* unmanaged[Cdecl]<void*, void> cancel_awaiting_jobs, [NativeTypeName("rocksdb_compaction_service_on_installation_cb")] delegate* unmanaged[Cdecl]<void*, sbyte*, int, void> on_installation);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_options_set_compaction_service(rocksdb_options_t* options, rocksdb_compactionservice_t* service);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compaction_service_options_override_t* rocksdb_compaction_service_options_override_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_compaction_service_options_override_t* rocksdb_compaction_service_options_override_create_from_options(rocksdb_options_t* option);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_destroy(rocksdb_compaction_service_options_override_t* override_options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_env(rocksdb_compaction_service_options_override_t* override_options, rocksdb_env_t* env);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_comparator(rocksdb_compaction_service_options_override_t* override_options, rocksdb_comparator_t* comparator);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_merge_operator(rocksdb_compaction_service_options_override_t* override_options, rocksdb_mergeoperator_t* merge_operator);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_compaction_filter(rocksdb_compaction_service_options_override_t* override_options, rocksdb_compactionfilter_t* compaction_filter);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_compaction_filter_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_compactionfilterfactory_t* compaction_filter_factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_prefix_extractor(rocksdb_compaction_service_options_override_t* override_options, rocksdb_slicetransform_t* prefix_extractor);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_block_based_table_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_block_based_table_options_t* table_options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_cuckoo_table_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_cuckoo_table_options_t* table_options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_add_event_listener(rocksdb_compaction_service_options_override_t* override_options, rocksdb_eventlistener_t* event_listener);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_statistics(rocksdb_compaction_service_options_override_t* override_options, rocksdb_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_info_log(rocksdb_compaction_service_options_override_t* override_options, rocksdb_logger_t* logger);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_option(rocksdb_compaction_service_options_override_t* override_options, [NativeTypeName("const char *")] sbyte* key, [NativeTypeName("const char *")] sbyte* value);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_file_checksum_gen_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_file_checksum_gen_factory_t* factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_set_sst_partitioner_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_sst_partitioner_factory_t* factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_compaction_service_options_override_add_table_properties_collector_factory(rocksdb_compaction_service_options_override_t* override_options, rocksdb_table_properties_collector_factory_t* factory);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("unsigned char *")]
    public static extern byte* rocksdb_open_and_compact_canceled_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_canceled_destroy([NativeTypeName("unsigned char *")] byte* canceled);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_canceled_set([NativeTypeName("unsigned char *")] byte* canceled, [NativeTypeName("unsigned char")] byte value);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_open_and_compact_options_t* rocksdb_open_and_compact_options_create();

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_options_destroy(rocksdb_open_and_compact_options_t* options);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_options_set_canceled(rocksdb_open_and_compact_options_t* options, [NativeTypeName("unsigned char *")] byte* canceled);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_open_and_compact_options_set_allow_resumption(rocksdb_open_and_compact_options_t* options, [NativeTypeName("unsigned char")] byte allow_resumption);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_open_and_compact([NativeTypeName("const char *")] sbyte* db_path, [NativeTypeName("const char *")] sbyte* output_directory, [NativeTypeName("const char *")] sbyte* input, [NativeTypeName("size_t")] nuint input_len, [NativeTypeName("size_t *")] nuint* output_len, [NativeTypeName("const rocksdb_compaction_service_options_override_t *")] rocksdb_compaction_service_options_override_t* override_options, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("char *")]
    public static extern sbyte* rocksdb_open_and_compact_with_options([NativeTypeName("const rocksdb_open_and_compact_options_t *")] rocksdb_open_and_compact_options_t* options, [NativeTypeName("const char *")] sbyte* db_path, [NativeTypeName("const char *")] sbyte* output_directory, [NativeTypeName("const char *")] sbyte* input, [NativeTypeName("size_t")] nuint input_len, [NativeTypeName("size_t *")] nuint* output_len, [NativeTypeName("const rocksdb_compaction_service_options_override_t *")] rocksdb_compaction_service_options_override_t* override_options, [NativeTypeName("char **")] sbyte** errptr);
}
