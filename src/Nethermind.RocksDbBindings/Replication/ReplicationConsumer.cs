// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public class ReplicationConsumer(RocksDb db)
{
    private readonly RocksDb _db = db;

    public static void IngestFile(ReplicationFile file, string destinationDbPath)
    {
        Directory.CreateDirectory(destinationDbPath);

        string destPath = Path.Combine(destinationDbPath, file.FileName);

        using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        {
            file.FileStream.CopyTo(fileStream);
        }
    }

    public void IngestBatch(ReplicationBatch batch)
    {
        if (_db == null) throw new InvalidOperationException("DB is not initialized.");

        using (var writeBatch = new WriteBatch(batch.Data))
        {
            _db.Write(writeBatch);
        }
    }

    public void IngestBatch(ulong sequenceNo, ReadOnlySpan<byte> batchData)
    {
        if (_db == null) throw new InvalidOperationException("DB is not initialized.");

        using (var writeBatch = WriteBatch.FromSpan(batchData))
        {
            _db.Write(writeBatch);
        }
    }

}
