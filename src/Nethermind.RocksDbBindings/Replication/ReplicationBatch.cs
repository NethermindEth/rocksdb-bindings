// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

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
