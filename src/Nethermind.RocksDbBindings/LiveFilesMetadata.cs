// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public class LiveFileMetadata
{
    public required FileMetadata FileMetadata;
    public FileDataMetadata? FileDataMetadata;
}

public class FileMetadata
{
    public required string FileName;
    public int FileLevel;
    public ulong FileSize;
}

public class FileDataMetadata
{
    public required string SmallestKeyInFile;
    public required string LargestKeyInFile;
    public ulong NumEntriesInFile;
    public ulong NumDeletionsInFile;
}
