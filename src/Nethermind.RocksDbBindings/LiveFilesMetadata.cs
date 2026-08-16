// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public class LiveFileMetadata
{
    public required FileMetadata FileMetadata { get; set; }
    public FileDataMetadata? FileDataMetadata { get; set; }
}

public class FileMetadata
{
    public required string FileName { get; set; }
    public int FileLevel { get; set; }
    public ulong FileSize { get; set; }
}

public class FileDataMetadata
{
    public required string SmallestKeyInFile { get; set; }
    public required string LargestKeyInFile { get; set; }
    public ulong NumEntriesInFile { get; set; }
    public ulong NumDeletionsInFile { get; set; }
}
