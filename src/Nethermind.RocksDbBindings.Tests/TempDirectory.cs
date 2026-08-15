// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

/// <summary>
/// A scratch directory unique to one test, removed when the test finishes. Every test that
/// touches the file system gets its own, so the suite stays safe to run in parallel.
/// </summary>
/// <remarks>
/// The names are kept short on purpose. RocksDB derives its LOG file name from the whole
/// absolute database path with the separators replaced, so a chatty temporary directory pushes
/// that derived name past the Windows path limit.
/// </remarks>
internal sealed class TempDirectory : IDisposable
{
    public TempDirectory()
    {
        Path = System.IO.Path.Combine(
            System.IO.Path.GetTempPath(),
            "rocksdb-tests",
            Guid.NewGuid().ToString("N")[..12]);

        Directory.CreateDirectory(Path);
    }

    public string Path { get; }

    /// <summary>
    /// A path inside this directory that has deliberately not been created, for the APIs that
    /// insist on creating it themselves.
    /// </summary>
    public string Reserve(string name) => System.IO.Path.Combine(Path, name);

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(Path))
                Directory.Delete(Path, recursive: true);
        }
        catch (IOException)
        {
            // A directory left behind by a failing test is not itself a test failure.
        }
    }
}
