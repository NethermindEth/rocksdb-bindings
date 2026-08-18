// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

/// <summary>The file system and thread pools a database runs its work through.</summary>
/// <remarks>
/// An environment must outlive every database opened with it, because RocksDB stores a bare
/// pointer to it and never takes ownership. <see cref="DbOptions.SetEnv"/> holds the wrapper on
/// the options, and an opening database takes its own reference that lasts until the native
/// close, which an iterator or a snapshot can defer well past the database wrapper itself.
/// Disposing an environment by hand while it is still in use is not covered by either reference:
/// for an in-memory one that frees the storage the database is reading.
/// </remarks>
public sealed unsafe class Env : IDisposable
{
    private readonly EnvHandle _handle;

    public nint Handle => _handle.IsClosed ? nint.Zero : _handle.DangerousGetHandle();

    private Env(nint handle)
    {
        _handle = new EnvHandle(handle);
    }

    /// <summary>
    /// The environment a database uses when none is set: the real file system, with thread pools
    /// shared across the process.
    /// </summary>
    /// <remarks>
    /// Every call wraps the same underlying environment, so a thread pool sized through one of
    /// them is sized for all of them. Disposing frees the wrapper and leaves that environment be.
    /// </remarks>
    public static Env CreateDefault() => new((nint)rocksdb_create_default_env());

    /// <summary>
    /// An environment holding its files in memory, for tests and short-lived databases that
    /// should never reach the disk.
    /// </summary>
    /// <remarks>Unlike <see cref="CreateDefault"/>, disposing frees the environment itself.</remarks>
    public static Env CreateInMemory() => new((nint)rocksdb_create_mem_env());

    /// <summary>
    /// Destroys the wrapper, and with it the environment itself unless this is the default one;
    /// the SafeHandle's critical finalizer is the backstop.
    /// </summary>
    /// <remarks>
    /// Dispose only once no database is running on this environment and no options still carry
    /// it: a database opened later from those options would be handed a freed pointer.
    /// </remarks>
    public void Dispose() => _handle.Dispose();

    /// <summary>Points the given <c>rocksdb_options_t</c> at this environment.</summary>
    /// <remarks>
    /// Leased, so a concurrent dispose cannot free the wrapper out from under the native call,
    /// and so attaching an already disposed environment throws rather than storing null.
    /// </remarks>
    internal void AttachTo(nint options)
    {
        using var lease = new HandleLease(_handle);
        rocksdb_options_set_env(RocksDbInterop.Options(options), RocksDbInterop.Env(_handle.DangerousGetHandle()));
    }

    /// <summary>Sizes the low priority thread pool, the one compactions run on.</summary>
    public Env SetBackgroundThreads(int value)
    {
        using var lease = new HandleLease(_handle);
        rocksdb_env_set_background_threads(RocksDbInterop.Env(_handle.DangerousGetHandle()), value);
        return this;
    }

    /// <summary>Reports the size of the pool <see cref="SetBackgroundThreads"/> sizes.</summary>
    public int GetBackgroundThreads()
    {
        using var lease = new HandleLease(_handle);
        return rocksdb_env_get_background_threads(RocksDbInterop.Env(_handle.DangerousGetHandle()));
    }

    /// <summary>Sizes the high priority thread pool, the one memtable flushes run on.</summary>
    public Env SetHighPriorityBackgroundThreads(int value)
    {
        using var lease = new HandleLease(_handle);
        rocksdb_env_set_high_priority_background_threads(RocksDbInterop.Env(_handle.DangerousGetHandle()), value);
        return this;
    }

    /// <summary>Reports the size of the pool <see cref="SetHighPriorityBackgroundThreads"/> sizes.</summary>
    public int GetHighPriorityBackgroundThreads()
    {
        using var lease = new HandleLease(_handle);
        return rocksdb_env_get_high_priority_background_threads(RocksDbInterop.Env(_handle.DangerousGetHandle()));
    }

    /// <summary>Waits for the threads started through this environment to terminate.</summary>
    /// <remarks>
    /// RocksDB joins only what it started with <c>StartThread</c>; the background pools are not
    /// among them, so this is not a way to wait for compactions or flushes to finish.
    /// </remarks>
    public void JoinAllThreads()
    {
        using var lease = new HandleLease(_handle);
        rocksdb_env_join_all_threads(RocksDbInterop.Env(_handle.DangerousGetHandle()));
    }
}
