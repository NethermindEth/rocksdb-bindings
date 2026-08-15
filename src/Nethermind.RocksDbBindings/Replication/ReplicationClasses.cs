// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public class ReplicationFile : IDisposable
{
    public required string FileName { get; set; }
    public ulong FileSize { get; set; }
    public required Stream FileStream { get; set; }

    public void Dispose() => FileStream?.Dispose();
}

public class ReplicationBatch
{
    public ulong SequenceNumber { get; set; }
    public required byte[] Data { get; set; }
}

public class PooledReplicationBatch
{
    public ulong SequenceNumber { get; set; }
    public required byte[] PooledData { get; set; }
    public int Length { get; set; }
}

public class ReplicationSession(string tempPath) : IDisposable
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
