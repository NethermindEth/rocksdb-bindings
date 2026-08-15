// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

/// <summary>
/// A freshly created database in its own temporary directory. Disposing closes the database
/// first and only then removes the files, which is the order rocksdb requires on Windows.
/// </summary>
internal sealed class TestDatabase : IDisposable
{
    private readonly TempDirectory _directory;

    private TestDatabase(TempDirectory directory, RocksDb db)
    {
        _directory = directory;
        Db = db;
    }

    public RocksDb Db { get; }

    public string Path => Db.Path;

    public static TestDatabase Create(DbOptions? options = null)
        => Create(directory => RocksDb.Open(options ?? new DbOptions().SetCreateIfMissing(), directory.Reserve("db")));

    public static TestDatabase Create(DbOptions options, ColumnFamilies columnFamilies)
        => Create(directory => RocksDb.Open(options, directory.Reserve("db"), columnFamilies));

    private static TestDatabase Create(Func<TempDirectory, RocksDb> open)
    {
        var directory = new TempDirectory();

        try
        {
            return new TestDatabase(directory, open(directory));
        }
        catch
        {
            directory.Dispose();
            throw;
        }
    }

    public void Dispose()
    {
        Db.Dispose();
        _directory.Dispose();
    }
}
