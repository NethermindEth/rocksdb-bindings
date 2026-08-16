// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

/// <summary>
/// String conveniences the library no longer ships: the package surface is span-only, but the
/// tests stay readable by round-tripping through UTF-8 here.
/// </summary>
internal static class Utf8TestExtensions
{
    private static byte[] Utf8(string value) => Encoding.UTF8.GetBytes(value);

    public static void Put(this RocksDb db, string key, string value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
        => db.Put(Utf8(key), Utf8(value), cf, writeOptions);

    public static string? Get(this RocksDb db, string key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        var value = db.Get(Utf8(key), cf, readOptions);
        return value is null ? null : Encoding.UTF8.GetString(value);
    }

    public static bool HasKey(this RocksDb db, string key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
        => db.HasKey(Utf8(key), cf, readOptions);

    public static void Remove(this RocksDb db, string key, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
        => db.Remove(Utf8(key), cf, writeOptions);

    public static void Merge(this RocksDb db, string key, string value, IColumnFamilyHandle? cf = null, WriteOptions? writeOptions = null)
        => db.Merge(Utf8(key), Utf8(value), cf, writeOptions);

    public static WriteBatch Put(this WriteBatch batch, string key, string value, IColumnFamilyHandle? cf = null)
        => batch.Put(Utf8(key), Utf8(value), cf);

    public static void CompactRange(this RocksDb db, string? start, string? limit, IColumnFamilyHandle? cf = null)
        => db.CompactRange(start is null ? null : Utf8(start), limit is null ? null : Utf8(limit), cf);

    public static Iterator Seek(this Iterator iterator, string key) => iterator.Seek(Utf8(key));

    public static Iterator SeekForPrev(this Iterator iterator, string key) => iterator.SeekForPrev(Utf8(key));

    public static string StringKey(this Iterator iterator) => Encoding.UTF8.GetString(iterator.GetKeySpan());

    public static string StringValue(this Iterator iterator) => Encoding.UTF8.GetString(iterator.GetValueSpan());

    public static WriteBatchWithIndex Put(this WriteBatchWithIndex batch, string key, string value, IColumnFamilyHandle? cf = null)
        => batch.Put(Utf8(key), Utf8(value), cf);

    public static string? Get(this WriteBatchWithIndex batch, string key, IColumnFamilyHandle? cf = null, DbOptions? options = null)
    {
        var value = batch.Get(Utf8(key), cf, options);
        return value is null ? null : Encoding.UTF8.GetString(value);
    }

    public static string? Get(this WriteBatchWithIndex batch, RocksDb db, string key, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        var value = batch.Get(db, Utf8(key), cf, readOptions);
        return value is null ? null : Encoding.UTF8.GetString(value);
    }
}
