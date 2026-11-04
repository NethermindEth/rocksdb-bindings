// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Security.Cryptography;
using System.Text;

namespace Nethermind.RocksDbBindings.CompatibilityTests;

/// <summary>
/// Writes a deterministic workload and tracks the expected contents, so another build can verify
/// them by replaying the workload without a database.
/// </summary>
internal sealed class Workload
{
    private const int IngestedKeyCount = 500;

    private readonly DatabaseSpec _spec;
    private readonly Random _random;
    private readonly byte[][] _keys;

    public Workload(DatabaseSpec spec)
    {
        _spec = spec;
        _random = new Random(BitConverter.ToInt32(SHA256.HashData(Encoding.UTF8.GetBytes(spec.Name))) & int.MaxValue);
        _keys = [.. Enumerable.Range(0, spec.KeyCount).Select(Key)];
        Expected = spec.AllFamilies.ToDictionary(family => family, _ => new SortedDictionary<string, byte[]>(StringComparer.Ordinal));
    }

    /// <summary>Expected contents per column family, keyed by lowercase hex, which sorts like the bytes.</summary>
    public Dictionary<string, SortedDictionary<string, byte[]>> Expected { get; }

    public byte[] Key(int index) => SHA256.HashData(Encoding.UTF8.GetBytes($"{_spec.Name}:{index}"));

    /// <summary>Applies the workload to <paramref name="db"/>, or only to <see cref="Expected"/> when it is null.</summary>
    public void Run(RocksDb? db, string sstDirectory)
    {
        using FlushOptions flush = new();

        foreach (string family in _spec.AllFamilies)
        {
            IColumnFamilyHandle? cf = db is null || family == ColumnFamilies.DefaultName ? null : db.GetColumnFamily(family);

            // Bulk load compacted into bottommost files.
            Write(db, batch =>
            {
                foreach (byte[] key in _keys)
                    Put(batch, family, cf, key, Value());
            });
            if (db is not null)
            {
                db.Flush(flush, cf ?? db.GetDefaultColumnFamily());
                db.CompactRange(null, null, cf);
            }

            // Overwrites, deletes and a range deletion flushed to L0.
            Write(db, batch =>
            {
                for (int i = 0; i < _keys.Length; i += 7)
                    Put(batch, family, cf, _keys[i], Value());
                for (int i = 3; i < _keys.Length; i += 11)
                    Delete(batch, family, cf, _keys[i]);
                DeleteRange(batch, family, cf, [0x40], [0x48]);
            });
            db?.Flush(flush, cf ?? db.GetDefaultColumnFamily());

            // An SST written by this build and ingested.
            (byte[] Key, byte[] Value)[] ingested = [.. Enumerable.Range(0, IngestedKeyCount).Select(i => (Encoding.Latin1.GetBytes($"\xff-ingested-{i:D5}"), Value()))];
            foreach ((byte[] key, byte[] value) in ingested)
                Expected[family][Convert.ToHexStringLower(key)] = value;
            if (db is not null)
            {
                string sst = Path.Combine(sstDirectory, $"{_spec.Name}-{family}.sst");
                using (ColumnFamilyOptions options = new ColumnFamilyOptions().ApplyFromString(_spec.Options))
                using (SstFileWriter writer = new(null, options))
                {
                    writer.Open(sst);
                    foreach ((byte[] key, byte[] value) in ingested)
                        writer.Put(key, value);
                    writer.Finish();
                }
                db.IngestExternalFiles([sst], new IngestExternalFileOptions().SetMoveFiles(true), cf);
            }

            // Writes left only in the WAL when the database closes.
            Write(db, batch =>
            {
                for (int i = 1; i < _keys.Length; i += 13)
                    Put(batch, family, cf, _keys[i], Value());
                for (int i = 5; i < _keys.Length; i += 17)
                    Delete(batch, family, cf, _keys[i]);
            });
        }
    }

    private static void Write(RocksDb? db, Action<WriteBatch?> fill)
    {
        using WriteBatch? batch = db is null ? null : new WriteBatch();
        fill(batch);
        if (batch is not null)
            db!.Write(batch);
    }

    private void Put(WriteBatch? batch, string family, IColumnFamilyHandle? cf, byte[] key, byte[] value)
    {
        Expected[family][Convert.ToHexStringLower(key)] = value;
        batch?.Put(key, value, cf);
    }

    private void Delete(WriteBatch? batch, string family, IColumnFamilyHandle? cf, byte[] key)
    {
        Expected[family].Remove(Convert.ToHexStringLower(key));
        batch?.Delete(key, cf);
    }

    private void DeleteRange(WriteBatch? batch, string family, IColumnFamilyHandle? cf, byte[] start, byte[] end)
    {
        string first = Convert.ToHexStringLower(start), last = Convert.ToHexStringLower(end);
        foreach (string key in Expected[family].Keys.Where(k => string.CompareOrdinal(k, first) >= 0 && string.CompareOrdinal(k, last) < 0).ToList())
            Expected[family].Remove(key);
        batch?.DeleteRange(start, end, cf);
    }

    private byte[] Value()
    {
        byte[] value = new byte[_random.Next(_spec.MinValueSize, _spec.MaxValueSize)];
        // Half random and half zeros, so compression has work to do.
        _random.NextBytes(value.AsSpan(0, value.Length / 2));
        return value;
    }
}
