// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// Owns the native database. The close runs when the last lease is released, so children such
/// as iterators and snapshots defer it past <see cref="RocksDb.Dispose"/> instead of racing it.
/// Column family handles are destroyed here as well, immediately before the close: only then is
/// it certain no leased call is still using them.
/// </summary>
internal sealed unsafe class RocksDbHandle : SafeHandle
{
    public RocksDbHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    // Ownership, not lookup: every handle ever created for this database, dropped ones included,
    // since dropping a column family does not destroy its handle. Appended under RocksDb's
    // column-family lock (or before the first lease, at open), and read in ReleaseHandle only
    // once every lease is released — appenders hold a lease, so no append can be concurrent.
    private readonly List<ColumnFamilyHandleInternal> _ownedColumnFamilies = [];

    internal void RegisterOwnedColumnFamily(ColumnFamilyHandleInternal cfh) => _ownedColumnFamilies.Add(cfh);

    // The environment the database was opened with, if any. RocksDB keeps a bare pointer to it,
    // and a child can defer the close long past the collection of the RocksDb wrapper, so the
    // reference is held here. It is the environment of one open call; the options it came from
    // are free to be pointed at another afterwards.
    internal Env? Env { get; set; }

    protected override bool ReleaseHandle()
    {
        foreach (ColumnFamilyHandleInternal cfh in _ownedColumnFamilies)
        {
            cfh.Dispose();
        }
        _ownedColumnFamilies.Clear();

        rocksdb_close(RocksDbInterop.Db(handle));
        // The environment outlives the close only if something else refers to it; a disposed but
        // still reachable database has no reason to pin one, least of all an in-memory file system.
        Env = null;
        return true;
    }
}

/// <summary>Owns a native cache wrapper; RocksDB keeps its own reference once attached.</summary>
internal sealed unsafe class CacheHandle : SafeHandle
{
    public CacheHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    protected override bool ReleaseHandle()
    {
        rocksdb_cache_destroy(RocksDbInterop.Cache(handle));
        return true;
    }
}

/// <summary>Owns a <c>rocksdb_options_t</c>.</summary>
internal sealed unsafe class OptionsSafeHandle : SafeHandle
{
    public OptionsSafeHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    /// <summary>The environment these options carry a bare pointer to, if any.</summary>
    internal Env? Env { get; set; }

    protected override bool ReleaseHandle()
    {
        rocksdb_options_destroy(RocksDbInterop.Options(handle));

        // Only once RocksDB can no longer read through them.
        Env = null;
        return true;
    }
}

/// <summary>Owns a <c>rocksdb_flushoptions_t</c>.</summary>
internal sealed unsafe class FlushOptionsHandle : SafeHandle
{
    public FlushOptionsHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    protected override bool ReleaseHandle()
    {
        rocksdb_flushoptions_destroy(RocksDbInterop.FlushOptions(handle));
        return true;
    }
}

/// <summary>Owns a <c>rocksdb_writeoptions_t</c>.</summary>
internal sealed unsafe class WriteOptionsHandle : SafeHandle
{
    public WriteOptionsHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    protected override bool ReleaseHandle()
    {
        rocksdb_writeoptions_destroy(RocksDbInterop.WriteOptions(handle));
        return true;
    }
}

/// <summary>
/// Owns a <c>rocksdb_readoptions_t</c> together with everything RocksDB reads through it without
/// owning: the iterate bound buffers, and the snapshot the options point at. All of it is held
/// here rather than on the wrapper, so that a lease keeps it alive even once the wrapper is gone.
/// </summary>
internal sealed unsafe class ReadOptionsHandle : SafeHandle
{
    private nint _lowerBound;
    private nint _upperBound;

    public ReadOptionsHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    /// <summary>The snapshot these options carry a bare pointer to, if any.</summary>
    internal Snapshot? Snapshot { get; set; }

    /// <summary>
    /// Points RocksDB at a new bound buffer and frees the one it was using.
    /// </summary>
    /// <remarks>
    /// The native setter stores the pointer without copying, so the old buffer must stay alive
    /// until the new one is installed: allocate, point RocksDB at it, then free the old one. A
    /// failed allocation thus leaves both the field and RocksDB on the still-valid old buffer.
    /// </remarks>
    internal void InstallLowerBound(nint buffer) => Install(ref _lowerBound, buffer);

    /// <inheritdoc cref="InstallLowerBound"/>
    internal void InstallUpperBound(nint buffer) => Install(ref _upperBound, buffer);

    protected override bool ReleaseHandle()
    {
        rocksdb_readoptions_destroy(RocksDbInterop.ReadOptions(handle));

        // Only once RocksDB can no longer read through them.
        Free(ref _lowerBound);
        Free(ref _upperBound);
        Snapshot = null;
        return true;
    }

    private static void Install(ref nint bound, nint buffer)
    {
        var previous = bound;
        bound = buffer;
        NativeMemory.Free((void*)previous);
    }

    private static void Free(ref nint bound) => NativeMemory.Free((void*)Interlocked.Exchange(ref bound, nint.Zero));
}

/// <summary>
/// Owns a native environment wrapper. Destroying it frees the environment as well, unless it is
/// the process-wide default one, which RocksDB owns and keeps.
/// </summary>
internal sealed unsafe class EnvHandle : SafeHandle
{
    public EnvHandle(nint handle) : base(nint.Zero, ownsHandle: true) => SetHandle(handle);

    public override bool IsInvalid => handle == nint.Zero;

    protected override bool ReleaseHandle()
    {
        rocksdb_env_destroy(RocksDbInterop.Env(handle));
        return true;
    }
}

/// <summary>
/// Owns a native child of the database — an iterator, snapshot, checkpoint, or WAL iterator —
/// together with the lease that keeps the database open under it. The critical finalizer makes
/// an abandoned child recoverable: it destroys the native object and releases the lease, so the
/// database can still close instead of leaking its handle and LOCK forever.
/// </summary>
internal abstract unsafe class ChildHandle : SafeHandle
{
    private RocksDbHandle? _dbLease;

    protected ChildHandle(nint handle, RocksDbHandle? dbLease) : base(nint.Zero, ownsHandle: true)
    {
        SetHandle(handle);
        _dbLease = dbLease;
    }

    public override bool IsInvalid => handle == nint.Zero;

    /// <summary>The database lease; valid inside <see cref="Destroy"/>, released right after it.</summary>
    protected RocksDbHandle? DbLease => _dbLease;

    /// <summary>
    /// Transfers the database lease to the caller, who must release it exactly once. Used when
    /// native ownership of the child moves elsewhere (see <see cref="WriteBatchWithIndex.NewIterator"/>).
    /// </summary>
    internal RocksDbHandle? TakeDbLease()
    {
        var lease = _dbLease;
        _dbLease = null;
        return lease;
    }

    protected sealed override bool ReleaseHandle()
    {
        Destroy(handle);
        TakeDbLease()?.DangerousRelease();
        return true;
    }

    protected abstract void Destroy(nint handle);
}

internal sealed unsafe class IteratorHandle : ChildHandle
{
    // An acquired reference on the read options, not merely the managed wrapper: the native
    // iterator keeps reading the iterate bounds those options own, so disposing them under it has
    // to defer the free rather than take it away.
    private SafeHandle? _readOptions;

    public IteratorHandle(nint handle, RocksDbHandle? dbLease, SafeHandle? readOptions, Snapshot? snapshot)
        : base(handle, dbLease)
    {
        if (readOptions is not null)
        {
            var added = false;
            readOptions.DangerousAddRef(ref added);
            // Only reachable when the add-ref succeeded, so the release matches what was taken.
            _readOptions = readOptions;
        }

        Snapshot = snapshot;
    }

    /// <summary>Transfers the read options reference to the caller, who must release it once.</summary>
    internal SafeHandle? TakeReadOptions()
    {
        var readOptions = _readOptions;
        _readOptions = null;
        return readOptions;
    }

    // The snapshot the read options carried when this iterator was created. RocksDB keeps a bare
    // pointer to it, and those options are free to be pointed at another one afterwards, so the
    // iterator holds the one it actually reads through.
    internal Snapshot? Snapshot { get; private set; }

    /// <summary>Transfers the snapshot to the caller, leaving this handle holding nothing.</summary>
    internal Snapshot? TakeSnapshot()
    {
        var snapshot = Snapshot;
        Snapshot = null;
        return snapshot;
    }

    protected override void Destroy(nint handle)
    {
        rocksdb_iter_destroy(RocksDbInterop.Iterator(handle));
        TakeReadOptions()?.DangerousRelease();
        TakeSnapshot();
    }
}

internal sealed unsafe class SnapshotHandle : ChildHandle
{
    public SnapshotHandle(nint handle, RocksDbHandle dbLease) : base(handle, dbLease) { }

    protected override void Destroy(nint handle) =>
        rocksdb_release_snapshot(RocksDbInterop.Db(DbLease!.DangerousGetHandle()), RocksDbInterop.Snapshot(handle));
}

internal sealed unsafe class CheckpointHandle : ChildHandle
{
    public CheckpointHandle(nint handle, RocksDbHandle dbLease) : base(handle, dbLease) { }

    protected override void Destroy(nint handle) => rocksdb_checkpoint_object_destroy(RocksDbInterop.Checkpoint(handle));
}

internal sealed unsafe class WalIteratorHandle : ChildHandle
{
    public WalIteratorHandle(nint handle, RocksDbHandle dbLease) : base(handle, dbLease) { }

    protected override void Destroy(nint handle) => rocksdb_wal_iter_destroy(RocksDbInterop.WalIterator(handle));
}

/// <summary>
/// A ref-count lease guarding one native call: while held, the handle cannot be released, and
/// acquiring one on a disposed handle throws <see cref="ObjectDisposedException"/>.
/// </summary>
/// <remarks>
/// Copies alias the same acquired reference, so dispose exactly one copy — use it only as a
/// <c>using</c> local. Longer-lived children hold the <see cref="SafeHandle"/> itself instead.
/// </remarks>
internal readonly struct HandleLease : IDisposable
{
    private readonly SafeHandle _handle;

    public HandleLease(SafeHandle handle)
    {
        var added = false;
        handle.DangerousAddRef(ref added);
        // Only reachable when the add-ref succeeded, so Dispose releases exactly what was taken.
        _handle = handle;
    }

    public void Dispose() => _handle?.DangerousRelease();
}
