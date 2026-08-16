// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public sealed class ReplicationSession(string tempPath) : IDisposable
{
    private readonly string _tempPath = tempPath;

    public IEnumerable<ReplicationFile> Files
    {
        get
        {
            foreach (var filePath in Directory.GetFiles(_tempPath))
            {
                var fileName = Path.GetFileName(filePath);
                var fileInfo = new FileInfo(filePath);
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                yield return new ReplicationFile
                {
                    FileName = fileName,
                    FileSize = (ulong)fileInfo.Length,
                    FileStream = stream
                };
            }
        }
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempPath))
        {
            Directory.Delete(_tempPath, true);
        }
    }
}
