// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <inheritdoc/>
public sealed class ColumnFamilyOptions : Options<ColumnFamilyOptions> { }

// The callbacks live here rather than on Options<T> because UnmanagedCallersOnly methods
// cannot be declared in a generic type.
internal sealed unsafe class OptionsBase
{
    // The managed instance is reached through a GCHandle stored in the state that RocksDB
    // hands back to every callback, so nothing has to outlive it on the managed side.
    [StructLayout(LayoutKind.Sequential)]
    internal struct ComparatorState
    {
        public nint Instance { get; set; }
        public nint NamePtr { get; set; }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MergeOperatorState
    {
        public nint Instance { get; set; }
        public nint NamePtr { get; set; }
    }

    // An exception cannot unwind through the native frames that called us, so report the
    // culprit instead of letting the runtime tear the process down without one.
    [DoesNotReturn]
    private static void Fail(Exception exception)
        => Environment.FailFast($"A RocksDB callback threw {exception.GetType()}.", exception);

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static int Comparator_Compare(void* state, sbyte* a, nuint alen, sbyte* b, nuint blen)
    {
        try
        {
            var comparator = (IComparator)GCHandle.FromIntPtr(((ComparatorState*)state)->Instance).Target!;
            // The spans live only for this call, which is all the interface promises.
            return comparator.Compare(new ReadOnlySpan<byte>(a, checked((int)alen)), new ReadOnlySpan<byte>(b, checked((int)blen)));
        }
        catch (Exception exception)
        {
            Fail(exception);
            return 0;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void Comparator_Destroy(void* state)
    {
        GCHandle.FromIntPtr(((ComparatorState*)state)->Instance).Free();
        Utf8StringMarshaller.Free((byte*)((ComparatorState*)state)->NamePtr);
        NativeMemory.Free(state);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static sbyte* Comparator_GetNamePtr(void* state)
        => (sbyte*)((ComparatorState*)state)->NamePtr;

    private static IMergeOperator FromState(void* state)
        => (IMergeOperator)GCHandle.FromIntPtr(((MergeOperatorState*)state)->Instance).Target!;

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static sbyte* MergeOperator_PartialMerge(void* state, sbyte* key, nuint keyLength, sbyte** operandsList, nuint* operandsListLength, int numOperands, byte* success, nuint* newValueLength)
    {
        try
        {
            var result = FromState(state).PartialMerge(
                (nint)key, keyLength, (nint)operandsList, (nint)operandsListLength, numOperands, out byte succeeded, out nint length);
            *success = succeeded;
            *newValueLength = (nuint)length;
            return (sbyte*)result;
        }
        catch (Exception exception)
        {
            Fail(exception);
            return null;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static sbyte* MergeOperator_FullMerge(void* state, sbyte* key, nuint keyLength, sbyte* existingValue, nuint existingValueLength, sbyte** operandsList, nuint* operandsListLength, int numOperands, byte* success, nuint* newValueLength)
    {
        try
        {
            var result = FromState(state).FullMerge(
                (nint)key, keyLength, (nint)existingValue, existingValueLength, (nint)operandsList, (nint)operandsListLength, numOperands, out byte succeeded, out nint length);
            *success = succeeded;
            *newValueLength = (nuint)length;
            return (sbyte*)result;
        }
        catch (Exception exception)
        {
            Fail(exception);
            return null;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void MergeOperator_DeleteValue(void* state, sbyte* value, nuint valueLength)
    {
        try
        {
            FromState(state).DeleteValue((nint)value, valueLength);
        }
        catch (Exception exception)
        {
            Fail(exception);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static void MergeOperator_Destroy(void* state)
    {
        GCHandle.FromIntPtr(((MergeOperatorState*)state)->Instance).Free();
        Utf8StringMarshaller.Free((byte*)((MergeOperatorState*)state)->NamePtr);
        NativeMemory.Free(state);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    internal static sbyte* MergeOperator_GetNamePtr(void* state)
        => (sbyte*)((MergeOperatorState*)state)->NamePtr;
}

public unsafe abstract partial class Options<T> : OptionsHandle where T : Options<T>
{

    /// <summary>
    /// Configures the block-based format that SST files are written in and read through.
    /// </summary>
    /// <remarks>
    /// RocksDB builds a table factory from a copy of <paramref name="tableOptions"/>, so the
    /// wrapper has to survive this call but not outlive it.
    /// </remarks>
    public T SetBlockBasedTableFactory(BlockBasedTableOptions tableOptions)
    {
        rocksdb_options_set_block_based_table_factory(RocksDbInterop.Options(Handle), RocksDbInterop.BlockBasedTableOptions(tableOptions.Handle));
        // Without this, the finalizer could destroy the table options mid-copy.
        GC.KeepAlive(tableOptions);
        return (T)this;
    }

    /// <summary>
    /// Tunes the family for point reads, at the expense of range scans, sizing the block cache in MiB.
    /// </summary>
    public T OptimizeForPointLookup(ulong blockCacheSizeMb)
    {
        rocksdb_options_optimize_for_point_lookup(RocksDbInterop.Options(Handle), (nuint)blockCacheSizeMb);
        return (T)this;
    }

    /// <summary>
    /// Applies RocksDB's level-compaction preset, sized to a memtable budget in bytes.
    /// </summary>
    public T OptimizeLevelStyleCompaction(ulong memtableMemoryBudget)
    {
        rocksdb_options_optimize_level_style_compaction(RocksDbInterop.Options(Handle), (nuint)memtableMemoryBudget);
        return (T)this;
    }

    /// <summary>
    /// Applies RocksDB's universal-compaction preset, sized to a memtable budget in bytes.
    /// Universal compaction writes less than level compaction but uses more space.
    /// </summary>
    public T OptimizeUniversalStyleCompaction(ulong memtableMemoryBudget)
    {
        rocksdb_options_optimize_universal_style_compaction(RocksDbInterop.Options(Handle), (nuint)memtableMemoryBudget);
        return (T)this;
    }

    /// <summary>
    /// Installs a filter that drops or rewrites entries as compaction passes over them.
    /// RocksDB never destroys the filter, so it must outlive every database opened with these options.
    /// </summary>
    public T SetCompactionFilter(nint compactionFilter)
    {
        rocksdb_options_set_compaction_filter(RocksDbInterop.Options(Handle), RocksDbInterop.CompactionFilter(compactionFilter));
        return (T)this;
    }

    /// <summary>
    /// Installs a factory that supplies a fresh compaction filter per compaction run.
    /// RocksDB never destroys the factory, so it must outlive every database opened with these options.
    /// </summary>
    public T SetCompactionFilterFactory(nint compactionFilterFactory)
    {
        rocksdb_options_set_compaction_filter_factory(RocksDbInterop.Options(Handle), RocksDbInterop.CompactionFilterFactory(compactionFilterFactory));
        return (T)this;
    }

    /// <summary>
    /// Reads this many bytes ahead during compaction, turning its reads sequential.
    /// </summary>
    public T SetCompactionReadaheadSize(ulong size)
    {
        rocksdb_options_compaction_readahead_size(RocksDbInterop.Options(Handle), (nuint)size);
        return (T)this;
    }

    /// <summary>
    /// Orders the keys of the family. A database records its comparator's name and refuses to
    /// open under one reporting a different name.
    /// </summary>
    public T SetComparator(nint comparator)
    {
        rocksdb_options_set_comparator(RocksDbInterop.Options(Handle), RocksDbInterop.Comparator(comparator));
        return (T)this;
    }

    /// <inheritdoc cref="SetComparator(nint)"/>
    /// <remarks>
    /// RocksDB stores a comparator as a non-owning pointer and never destroys it, and neither does
    /// this method, so the created comparator, its name and the managed instance behind it are held
    /// for the life of the process.
    /// </remarks>
    public T SetComparator(IComparator comparator)
    {
        // Allocate some memory for the name bytes
        var name = comparator.Name ?? comparator.GetType().FullName;
        var namePtr = (nint)Utf8StringMarshaller.ConvertToUnmanaged(name);

        // Allocate the state
        var state = new OptionsBase.ComparatorState
        {
            NamePtr = namePtr,
            Instance = GCHandle.ToIntPtr(GCHandle.Alloc(comparator))
        };
        var statePtr = (nint)NativeMemory.Alloc((nuint)sizeof(OptionsBase.ComparatorState));
        *(OptionsBase.ComparatorState*)statePtr = state;

        // Create the comparator
        nint handle = (nint)rocksdb_comparator_create(
            (void*)statePtr,
            &OptionsBase.Comparator_Destroy,
            &OptionsBase.Comparator_Compare,
            &OptionsBase.Comparator_GetNamePtr);

        return SetComparator(handle);
    }

    /// <summary>
    /// Resolves the operands of Merge writes into a value. Merging without one fails, and a
    /// database records the operator's name and refuses to open under one reporting a different name.
    /// </summary>
    public T SetMergeOperator(IMergeOperator mergeOperator)
    {
        // Allocate some memory for the name bytes
        var name = mergeOperator.Name ?? mergeOperator.GetType().FullName;
        var namePtr = (nint)Utf8StringMarshaller.ConvertToUnmanaged(name);

        // Allocate the state
        var state = new OptionsBase.MergeOperatorState
        {
            NamePtr = namePtr,
            Instance = GCHandle.ToIntPtr(GCHandle.Alloc(mergeOperator))
        };
        var statePtr = (nint)NativeMemory.Alloc((nuint)sizeof(OptionsBase.MergeOperatorState));
        *(OptionsBase.MergeOperatorState*)statePtr = state;

        // Keep delete_value non-null so the allocating IMergeOperator releases its results.
        nint handle = (nint)rocksdb_mergeoperator_create(
            (void*)statePtr,
            &OptionsBase.MergeOperator_Destroy,
            &OptionsBase.MergeOperator_FullMerge,
            &OptionsBase.MergeOperator_PartialMerge,
            &OptionsBase.MergeOperator_DeleteValue,
            &OptionsBase.MergeOperator_GetNamePtr);

        return SetMergeOperator(handle);
    }

    /// <summary>
    /// Resolves the operands of Merge writes into a value. Merging without one fails, and a
    /// database records the operator's name and refuses to open under one reporting a different name.
    /// </summary>
    public T SetMergeOperator(nint mergeOperator)
    {
        rocksdb_options_set_merge_operator(RocksDbInterop.Options(Handle), RocksDbInterop.MergeOperator(mergeOperator));
        return (T)this;
    }

    /// <summary>
    /// Installs RocksDB's built-in operator that reads values as little-endian 64-bit counters
    /// and adds them.
    /// </summary>
    public T SetUint64addMergeOperator()
    {
        rocksdb_options_set_uint64add_merge_operator(RocksDbInterop.Options(Handle));
        return (T)this;
    }

    /// <summary>
    /// Overrides <see cref="SetCompression"/> per level, taking one entry per level starting at
    /// level 0. Passing nothing drops the overrides, leaving every level on
    /// <see cref="SetCompression"/>.
    /// </summary>
    public T SetCompressionPerLevel(params ReadOnlySpan<Compression> levelValues)
    {
        fixed (int* valuesPtr = MemoryMarshal.Cast<Compression, int>(levelValues))
        {
            rocksdb_options_set_compression_per_level(RocksDbInterop.Options(Handle), valuesPtr, (nuint)levelValues.Length);
        }
        return (T)this;
    }

    /// <summary>
    /// Sets how much the database writes to its LOG file.
    /// </summary>
    public T SetInfoLogLevel(InfoLogLevel value)
    {
        rocksdb_options_set_info_log_level(RocksDbInterop.Options(Handle), (int)value);
        return (T)this;
    }

    /// <summary>
    /// Bytes a memtable holds before it is flushed to an SST file. Larger buffers make bulk
    /// writes faster and recovery slower.
    /// </summary>
    public T SetWriteBufferSize(ulong value)
    {
        rocksdb_options_set_write_buffer_size(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Tunes the compression algorithm.
    /// </summary>
    public T SetCompressionOptions(int p1, int p2, int p3, int p4)
    {
        rocksdb_options_set_compression_options(RocksDbInterop.Options(Handle), p1, p2, p3, p4);
        return (T)this;
    }

    /// <summary>
    /// Sets the prefix that bloom filters and hash indexes are built over, which is what makes
    /// prefix seeks fast. It must agree with the comparator: a key must order after its own prefix.
    /// </summary>
    public T SetPrefixExtractor(nint sliceTransform)
    {
        rocksdb_options_set_prefix_extractor(RocksDbInterop.Options(Handle), RocksDbInterop.SliceTransform(sliceTransform));
        return (T)this;
    }

    /// <summary>
    /// Sets the prefix that bloom filters and hash indexes are built over, which is what makes
    /// prefix seeks fast. It must agree with the comparator: a key must order after its own prefix.
    /// </summary>
    public T SetPrefixExtractor(SliceTransform sliceTransform)
    {
        rocksdb_options_set_prefix_extractor(RocksDbInterop.Options(Handle), RocksDbInterop.SliceTransform(sliceTransform.Handle));
        return (T)this;
    }

    /// <summary>
    /// Number of levels in the LSM tree.
    /// </summary>
    public T SetNumLevels(int value)
    {
        rocksdb_options_set_num_levels(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Number of level-0 files that starts a compaction.
    /// </summary>
    public T SetLevel0FileNumCompactionTrigger(int value)
    {
        rocksdb_options_set_level0_file_num_compaction_trigger(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Number of level-0 files at which writes are throttled.
    /// </summary>
    public T SetLevel0SlowdownWritesTrigger(int value)
    {
        rocksdb_options_set_level0_slowdown_writes_trigger(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Number of level-0 files at which writes stop until compaction catches up.
    /// </summary>
    public T SetLevel0StopWritesTrigger(int value)
    {
        rocksdb_options_set_level0_stop_writes_trigger(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Target size of an SST file at level 1; deeper levels scale it by
    /// <see cref="SetTargetFileSizeMultiplier"/>.
    /// </summary>
    public T SetTargetFileSizeBase(ulong value)
    {
        rocksdb_options_set_target_file_size_base(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Scales the target file size from each level to the next.
    /// </summary>
    public T SetTargetFileSizeMultiplier(int value)
    {
        rocksdb_options_set_target_file_size_multiplier(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Target total size of level 1; deeper levels scale it by
    /// <see cref="SetMaxBytesForLevelMultiplier"/>.
    /// </summary>
    public T SetMaxBytesForLevelBase(ulong value)
    {
        rocksdb_options_set_max_bytes_for_level_base(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Sizes the levels from the bottom up as the data grows, which keeps space amplification low.
    /// </summary>
    public T SetLevelCompactionDynamicLevelBytes(bool value)
    {
        rocksdb_options_set_level_compaction_dynamic_level_bytes(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Scales the target total size from each level to the next.
    /// </summary>
    public T SetMaxBytesForLevelMultiplier(double value)
    {
        rocksdb_options_set_max_bytes_for_level_multiplier(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Applies a further multiplier per level, taking one entry per level starting at level 0.
    /// Passing nothing drops the multipliers.
    /// </summary>
    public T SetMaxBytesForLevelMultiplierAdditional(params ReadOnlySpan<int> levelValues)
    {
        fixed (int* valuesPtr = levelValues)
        {
            rocksdb_options_set_max_bytes_for_level_multiplier_additional(RocksDbInterop.Options(Handle), valuesPtr, (nuint)levelValues.Length);
        }
        return (T)this;
    }

    /// <summary>
    /// How many memtables may exist at once before writes stall.
    /// </summary>
    public T SetMaxWriteBufferNumber(int value)
    {
        rocksdb_options_set_max_write_buffer_number(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// How many memtables must fill up before they are merged into one SST file.
    /// </summary>
    public T SetMinWriteBufferNumberToMerge(int value)
    {
        rocksdb_options_set_min_write_buffer_number_to_merge(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Bytes of already-flushed memtables to keep in memory, so recent writes can be read there.
    /// </summary>
    public T SetMaxWriteBufferSizeToMaintain(int value)
    {
        rocksdb_options_set_max_write_buffer_size_to_maintain(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Backlog of pending compaction bytes at which writes are throttled.
    /// </summary>
    public T SetSoftPendingCompactionBytesLimit(ulong value)
    {
        rocksdb_options_set_soft_pending_compaction_bytes_limit(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Backlog of pending compaction bytes at which writes stop until compaction catches up.
    /// </summary>
    public T SetHardPendingCompactionBytesLimit(ulong value)
    {
        rocksdb_options_set_hard_pending_compaction_bytes_limit(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Block size of the arena a memtable allocates from.
    /// </summary>
    public T SetArenaBlockSize(ulong value)
    {
        rocksdb_options_set_arena_block_size(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// How many versions of a key an iterator step walks past before it seeks instead.
    /// </summary>
    public T SetMaxSequentialSkipInIterations(ulong value)
    {
        rocksdb_options_set_max_sequential_skip_in_iterations(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Stops background compaction when non-zero; explicit compactions still run.
    /// </summary>
    public T SetDisableAutoCompactions(int value)
    {
        rocksdb_options_set_disable_auto_compactions(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Drops the bloom filter on the bottom level, where lookups usually hit anyway, to save memory.
    /// </summary>
    public T SetOptimizeFiltersForHits(int value)
    {
        rocksdb_options_set_optimize_filters_for_hits(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Switches the memtable to a vector, which suits bulk loading and not much else.
    /// </summary>
    public T SetMemtableVectorRep()
    {
        rocksdb_options_set_memtable_vector_rep(RocksDbInterop.Options(Handle));
        return (T)this;
    }

    /// <summary>
    /// Sizes the memtable's prefix bloom filter as a fraction of the memtable.
    /// </summary>
    public T SetMemtablePrefixBloomSizeRatio(double ratio)
    {
        rocksdb_options_set_memtable_prefix_bloom_size_ratio(RocksDbInterop.Options(Handle), ratio);
        return (T)this;
    }

    /// <summary>
    /// Caps how many bytes a single compaction may take as input.
    /// </summary>
    public T SetMaxCompactionBytes(ulong bytes)
    {
        rocksdb_options_set_max_compaction_bytes(RocksDbInterop.Options(Handle), (nuint)bytes);
        return (T)this;
    }

    /// <summary>
    /// Switches the memtable to a hash of skip lists, which needs a prefix extractor.
    /// </summary>
    public T SetHashSkipListRep(ulong bucketCount, int skipListHeight, int skipListBranchingFactor)
    {
        rocksdb_options_set_hash_skip_list_rep(RocksDbInterop.Options(Handle), (nuint)bucketCount, skipListHeight, skipListBranchingFactor);
        return (T)this;
    }

    /// <summary>
    /// Switches the memtable to a hash of linked lists, which needs a prefix extractor.
    /// </summary>
    public T SetHashLinkListRep(ulong value)
    {
        rocksdb_options_set_hash_link_list_rep(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Switches SST files to the plain table format, which suits in-memory databases with
    /// fixed-length keys.
    /// </summary>
    public T SetPlainTableFactory(uint userKeyLength,
        int bloomBitsPerKey,
        double hashTableRatio,
        int indexSparseness,
        int hugePageTlbSize,
        char encodingType,
        bool fullScanMode,
        bool storeIndexInFile)
    {
        rocksdb_options_set_plain_table_factory(RocksDbInterop.Options(Handle), userKeyLength, bloomBitsPerKey, hashTableRatio, (nuint)indexSparseness, (nuint)hugePageTlbSize, (sbyte)encodingType, RocksDbInterop.Bool(fullScanMode), RocksDbInterop.Bool(storeIndexInFile));
        return (T)this;
    }

    /// <summary>
    /// Compresses only from the given level upwards, leaving the levels below it uncompressed and
    /// the rest on the type <see cref="SetCompression"/> configures. A negative level is ignored.
    /// </summary>
    /// <remarks>
    /// This writes the per-level values, so it and <see cref="SetCompressionPerLevel"/> are
    /// alternatives rather than complements: whichever is called last wins.
    /// </remarks>
    public T SetMinLevelToCompress(int level)
    {
        rocksdb_options_set_min_level_to_compress(RocksDbInterop.Options(Handle), level);
        return (T)this;
    }

    /// <summary>
    /// How many merge operands may pile up on one key in the memtable before they are
    /// resolved eagerly.
    /// </summary>
    public T SetMaxSuccessiveMerges(ulong value)
    {
        rocksdb_options_set_max_successive_merges(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Packs a key's bloom bits into fewer cache lines, trading a higher false-positive rate for
    /// fewer cache misses.
    /// </summary>
    public T SetBloomLocality(uint value)
    {
        rocksdb_options_set_bloom_locality(RocksDbInterop.Options(Handle), value);
        return (T)this;
    }

    /// <summary>
    /// Overwrites values in the memtable rather than appending a new version of the key.
    /// </summary>
    /// <remarks>
    /// A family with in-place updates enabled cannot be read at a point in time: the overwritten
    /// versions no longer exist, so <see cref="RocksDb.CreateSnapshot"/> and
    /// <see cref="RocksDb.NewIterator"/> may observe writes made after them. Incompatible with
    /// <see cref="SetAllowConcurrentMemtableWrite"/>.
    /// </remarks>
    public T SetInplaceUpdateSupport(bool value)
    {
        rocksdb_options_set_inplace_update_support(RocksDbInterop.Options(Handle), RocksDbInterop.Bool(value));
        return (T)this;
    }

    /// <summary>
    /// Number of locks shared by in-place memtable updates.
    /// </summary>
    public T SetInplaceUpdateNumLocks(ulong value)
    {
        rocksdb_options_set_inplace_update_num_locks(RocksDbInterop.Options(Handle), (nuint)value);
        return (T)this;
    }

    /// <summary>
    /// Collects I/O statistics for flushes and compactions.
    /// </summary>
    public T SetReportBgIoStats(bool value)
    {
        rocksdb_options_set_report_bg_io_stats(RocksDbInterop.Options(Handle), value ? 1 : 0);
        return (T)this;
    }

    /// <summary>
    /// Selects the algorithm SST blocks are compressed with.
    /// </summary>
    public T SetCompression(Compression value)
    {
        rocksdb_options_set_compression(RocksDbInterop.Options(Handle), (int)value);
        return (T)this;
    }

    /// <summary>
    /// Selects how compaction reorganizes the data: level, universal, or FIFO.
    /// </summary>
    public T SetCompactionStyle(Compaction value)
    {
        rocksdb_options_set_compaction_style(RocksDbInterop.Options(Handle), (int)value);
        return (T)this;
    }

    /// <summary>
    /// Settings used when <see cref="SetCompactionStyle"/> selects universal compaction.
    /// </summary>
    public T SetUniversalCompactionOptions(nint universalCompactionOptions)
    {
        rocksdb_options_set_universal_compaction_options(RocksDbInterop.Options(Handle), RocksDbInterop.UniversalCompactionOptions(universalCompactionOptions));
        return (T)this;
    }

    /// <summary>
    /// Settings used when <see cref="SetCompactionStyle"/> selects FIFO compaction.
    /// </summary>
    public T SetFifoCompactionOptions(nint fifoCompactionOptions)
    {
        rocksdb_options_set_fifo_compaction_options(RocksDbInterop.Options(Handle), RocksDbInterop.FifoCompactionOptions(fifoCompactionOptions));
        return (T)this;
    }

    /// <summary>
    /// Allocates the memtable bloom filter from huge pages of this size, which the system must
    /// have reserved; otherwise it falls back to ordinary allocation.
    /// </summary>
    public T SetMemtableHugePageSize(ulong size)
    {
        rocksdb_options_set_memtable_huge_page_size(RocksDbInterop.Options(Handle), (nuint)size);
        return (T)this;
    }

};
