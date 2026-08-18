// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

public sealed unsafe class IngestExternalFileOptions
{
    public nint Handle { get; }

    public IngestExternalFileOptions() => Handle = (nint)rocksdb_ingestexternalfileoptions_create();

    // No Dispose and so no second actor: the finalizer runs at most once, and nothing can read
    // the handle afterwards.
    ~IngestExternalFileOptions() => rocksdb_ingestexternalfileoptions_destroy(RocksDbInterop.IngestExternalFileOptions(Handle));

    public IngestExternalFileOptions SetMoveFiles(bool moveFiles)
    {
        rocksdb_ingestexternalfileoptions_set_move_files(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(moveFiles));
        return this;
    }

    public IngestExternalFileOptions SetSnapshotConsistency(bool snapshotConsistency)
    {
        rocksdb_ingestexternalfileoptions_set_snapshot_consistency(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(snapshotConsistency));
        return this;
    }

    public IngestExternalFileOptions SetAllowGlobalSeqno(bool allow)
    {
        rocksdb_ingestexternalfileoptions_set_allow_global_seqno(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(allow));
        return this;
    }

    public IngestExternalFileOptions SetAllowBlockingFlush(bool allow)
    {
        rocksdb_ingestexternalfileoptions_set_allow_blocking_flush(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(allow));
        return this;
    }
}
