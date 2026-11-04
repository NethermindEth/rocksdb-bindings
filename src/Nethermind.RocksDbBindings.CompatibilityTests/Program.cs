// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

// Checks that databases written by one build can be read by another, in both directions.
// Usage: write <root> | verify <root> [--compact]
//   write   creates one database per DatabaseSpec under <root> and leaves part of the data in the WAL.
//   verify  replays the workload to rebuild the expected contents, then checks every key with a full
//           scan and point reads. With --compact, it also fully compacts each column family and checks
//           again, so this build rewrites every file. It always reopens and checks once more.

using Nethermind.RocksDbBindings;
using Nethermind.RocksDbBindings.CompatibilityTests;

if (args.Length < 2 || args[0] is not ("write" or "verify"))
{
    Console.Error.WriteLine("Usage: write <root> | verify <root> [--compact]");
    return 2;
}

DateTime started = DateTime.UtcNow;
bool write = args[0] == "write";
string root = Path.GetFullPath(args[1]);
bool compact = args.Contains("--compact");
int failures = 0;
Directory.CreateDirectory(root);

foreach (DatabaseSpec spec in DatabaseSpec.All)
{
    string path = Path.Combine(root, spec.Name);
    Workload workload = new(spec);

    if (write)
    {
        using (RocksDb db = Open(spec, path, create: true))
        {
            workload.Run(db, root);
            failures += Verify(spec, workload, db, "written");
        }
    }
    else
    {
        workload.Run(null, root);
        using (RocksDb db = Open(spec, path, create: false))
        {
            failures += Verify(spec, workload, db, "opened");
            if (compact)
            {
                foreach (string family in spec.AllFamilies)
                    db.CompactRange(null, null, family == ColumnFamilies.DefaultName ? null : db.GetColumnFamily(family));
                failures += Verify(spec, workload, db, "compacted");
            }
        }

        // This build has now rewritten the MANIFEST and OPTIONS file.
        using (RocksDb db = Open(spec, path, create: false))
            failures += Verify(spec, workload, db, "reopened");
    }

    failures += ReportLogs(path, started);
}

Console.WriteLine(failures == 0 ? "All checks passed" : $"{failures} checks failed");
return failures == 0 ? 0 : 1;

static RocksDb Open(DatabaseSpec spec, string path, bool create)
{
    DbOptions options = new DbOptions()
        .SetCreateIfMissing(create)
        .SetCreateMissingColumnFamilies(create)
        .ApplyFromString(spec.Options);
    ColumnFamilies families = new(new ColumnFamilyOptions().ApplyFromString(spec.Options));
    foreach (string family in spec.Families)
        families.Add(family, new ColumnFamilyOptions().ApplyFromString(spec.Options));
    return RocksDb.Open(options, path, families);
}

static int Verify(DatabaseSpec spec, Workload workload, RocksDb db, string stage)
{
    int failures = 0;
    foreach (string family in spec.AllFamilies)
    {
        SortedDictionary<string, byte[]> expected = workload.Expected[family];
        IColumnFamilyHandle handle = family == ColumnFamilies.DefaultName ? db.GetDefaultColumnFamily() : db.GetColumnFamily(family);
        IColumnFamilyHandle? cf = family == ColumnFamilies.DefaultName ? null : handle;
        // Hash-indexed tables need a total-order seek to iterate every key.
        using ReadOptions readOptions = new ReadOptions().SetTotalOrderSeek(spec.HasPrefixExtractor).SetVerifyChecksums(true);

        int scanned = 0, scanMismatches = 0;
        using (SortedDictionary<string, byte[]>.Enumerator next = expected.GetEnumerator())
        {
            using (Iterator iterator = db.NewIterator(cf, readOptions))
            {
                for (iterator.SeekToFirst(); iterator.Valid(); iterator.Next())
                {
                    scanned++;
                    if (!next.MoveNext()
                        || next.Current.Key != Convert.ToHexStringLower(iterator.GetKeySpan())
                        || !next.Current.Value.AsSpan().SequenceEqual(iterator.GetValueSpan()))
                        scanMismatches++;
                }
                iterator.ThrowIfError();
            }
            if (next.MoveNext())
                scanMismatches++;
        }

        // Point reads exercise filters and indexes, including keys that were deleted.
        int reads = 0, readMismatches = 0;
        foreach ((string key, byte[] value) in expected.Where((_, i) => i % 3 == 0))
        {
            reads++;
            if (db.Get(Convert.FromHexString(key), cf) is not { } actual || !actual.AsSpan().SequenceEqual(value))
                readMismatches++;
        }
        for (int i = 0; i < spec.KeyCount; i += 5)
        {
            byte[] key = workload.Key(i);
            if (expected.ContainsKey(Convert.ToHexStringLower(key)))
                continue;
            reads++;
            if (db.Get(key, cf) is not null)
                readMismatches++;
        }

        bool ok = scanned == expected.Count && scanMismatches == 0 && readMismatches == 0;
        if (!ok)
            failures++;
        Console.WriteLine(
            $"{(ok ? "ok  " : "FAIL")} {spec.Name}/{family} {stage}: {scanned}/{expected.Count} keys scanned, {scanMismatches} scan mismatches, "
            + $"{reads} reads, {readMismatches} read mismatches, files by level 0/1/6: "
            + $"{db.GetProperty("rocksdb.num-files-at-level0", handle)}/{db.GetProperty("rocksdb.num-files-at-level1", handle)}/"
            + $"{db.GetProperty("rocksdb.num-files-at-level6", handle)}, blob files: {db.GetProperty("rocksdb.num-blob-files", handle)}");
    }
    return failures;
}

// Prints the RocksDB version from the logs this run wrote, which shows which native library ran, and
// fails on corruption. Each open rotates the previous LOG to LOG.old.*, which keeps its write time.
static int ReportLogs(string path, DateTime started)
{
    int failures = 0;
    IEnumerable<string> logs = Directory.EnumerateFiles(path, "LOG*")
        .Where(log => File.GetLastWriteTimeUtc(log) >= started)
        .OrderBy(File.GetLastWriteTimeUtc);
    foreach (string line in logs.SelectMany(File.ReadLines))
    {
        bool corrupt = line.Contains("Corruption", StringComparison.OrdinalIgnoreCase) || line.Contains("malformed", StringComparison.OrdinalIgnoreCase);
        if (corrupt)
            failures++;
        if (corrupt || line.Contains("RocksDB version:", StringComparison.Ordinal) || line.Contains("[ERROR]", StringComparison.Ordinal))
            Console.WriteLine($"     {Path.GetFileName(path)} LOG: {line.Trim()}");
    }
    return failures;
}
