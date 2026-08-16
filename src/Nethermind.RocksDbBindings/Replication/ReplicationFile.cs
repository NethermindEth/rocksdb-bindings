// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public sealed class ReplicationFile : IDisposable
{
    public required string FileName { get; set; }
    public ulong FileSize { get; set; }
    public required Stream FileStream { get; set; }

    public void Dispose() => FileStream?.Dispose();
}
