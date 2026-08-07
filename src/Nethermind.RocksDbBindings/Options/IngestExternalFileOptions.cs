// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.Text;

namespace Nethermind.RocksDbBindings;

public unsafe class IngestExternalFileOptions
{
    public IntPtr Handle { get; protected set; }

    public IngestExternalFileOptions()
    {
        Handle = (IntPtr)RocksDbNative.rocksdb_ingestexternalfileoptions_create();
    }

    ~IngestExternalFileOptions()
    {
        if (Handle != IntPtr.Zero)
        {
#if !NODESTROY
            RocksDbNative.rocksdb_ingestexternalfileoptions_destroy(RocksDbInterop.IngestExternalFileOptions(Handle));
#endif
            Handle = IntPtr.Zero;
        }
    }

    public IngestExternalFileOptions SetMoveFiles(bool moveFiles)
    {
        RocksDbNative.rocksdb_ingestexternalfileoptions_set_move_files(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(moveFiles));
        return this;
    }

    public IngestExternalFileOptions SetSnapshotConsistency(bool snapshotConsistency)
    {
        RocksDbNative.rocksdb_ingestexternalfileoptions_set_snapshot_consistency(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(snapshotConsistency));
        return this;
    }

    public IngestExternalFileOptions SetAllowGlobalSeqno(bool allow)
    {
        RocksDbNative.rocksdb_ingestexternalfileoptions_set_allow_global_seqno(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(allow));
        return this;
    }

    public IngestExternalFileOptions SetAllowBlockingFlush(bool allow)
    {
        RocksDbNative.rocksdb_ingestexternalfileoptions_set_allow_blocking_flush(RocksDbInterop.IngestExternalFileOptions(Handle), RocksDbInterop.Bool(allow));
        return this;
    }
}
