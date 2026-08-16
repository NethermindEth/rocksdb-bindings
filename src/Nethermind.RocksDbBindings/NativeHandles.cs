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

    protected override bool ReleaseHandle()
    {
        foreach (ColumnFamilyHandleInternal cfh in _ownedColumnFamilies)
        {
            cfh.Dispose();
        }
        _ownedColumnFamilies.Clear();

        rocksdb_close(RocksDbInterop.Db(handle));
        return true;
    }
}

/// <summary>Owns a native cache wrapper; rocksdb keeps its own reference once attached.</summary>
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
    public IteratorHandle(nint handle, RocksDbHandle? dbLease) : base(handle, dbLease) { }

    protected override void Destroy(nint handle) => rocksdb_iter_destroy(RocksDbInterop.Iterator(handle));
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
