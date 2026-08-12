// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public class LiveFileMetadata
{
    public FileMetadata FileMetadata;
    public FileDataMetadata FileDataMetadata;
}

public class FileMetadata
{
    public string FileName;
    public int FileLevel;
    public ulong FileSize;
}

public class FileDataMetadata
{
    public string SmallestKeyInFile;
    public string LargestKeyInFile;
    public ulong NumEntriesInFile;
    public ulong NumDeletionsInFile;
}
