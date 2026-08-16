// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Nethermind.RocksDbBindings.Tests;

public class WriteBatchTests
{
    /// <summary>
    /// One replayed batch operation. The bytes are held as hex so that entries compare by
    /// content and a mismatch reads as the two byte strings rather than two array references.
    /// </summary>
    private sealed record Entry(string Key, string? Value)
    {
        public static Entry Written(ReadOnlySpan<byte> key, ReadOnlySpan<byte> value)
            => new(Convert.ToHexString(key), Convert.ToHexString(value));

        public static Entry Deleted(ReadOnlySpan<byte> key) => new(Convert.ToHexString(key), null);
    }

    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Value = "value"u8.ToArray();

    /// <summary>
    /// Replays a batch through the native iterator, which is the only way to see what a batch
    /// actually recorded without writing it to a database.
    /// </summary>
    private static unsafe List<Entry> Replay(WriteBatch batch)
    {
        var entries = new List<Entry>();
        var state = GCHandle.Alloc(entries);

        try
        {
            batch.Iterate((void*)GCHandle.ToIntPtr(state), &OnPut, &OnDelete);
        }
        finally
        {
            state.Free();
        }

        return entries;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void OnPut(void* state, sbyte* key, nuint keyLength, sbyte* value, nuint valueLength)
        => Collect(state, Entry.Written(
            new ReadOnlySpan<byte>(key, (int)keyLength),
            new ReadOnlySpan<byte>(value, (int)valueLength)));

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void OnDelete(void* state, sbyte* key, nuint keyLength)
        => Collect(state, Entry.Deleted(new ReadOnlySpan<byte>(key, (int)keyLength)));

    private static unsafe void Collect(void* state, Entry entry)
    {
        // Nothing may escape into the rocksdb frames that called this, so failures are reported
        // by the entries the test inspects afterwards rather than by an exception.
        try
        {
            ((List<Entry>)GCHandle.FromIntPtr((nint)state).Target!).Add(entry);
        }
        catch (Exception exception)
        {
            Environment.FailFast("A write batch callback threw.", exception);
        }
    }

    [Test]
    public async Task NewBatch_IsEmpty()
    {
        using var batch = new WriteBatch();

        await Assert.That(batch.Count()).IsEqualTo(0);
    }

    [Test]
    public async Task Put_IsCounted()
    {
        using var batch = new WriteBatch();

        batch.Put(Key, Value);

        await Assert.That(batch.Count()).IsEqualTo(1);
    }

    [Test]
    public async Task Delete_IsCounted()
    {
        using var batch = new WriteBatch();

        batch.Put(Key, Value).Delete(Key);

        await Assert.That(batch.Count()).IsEqualTo(2);
    }

    [Test]
    public async Task Clear_DropsEverythingRecordedSoFar()
    {
        using var batch = new WriteBatch();
        batch.Put(Key, Value);

        batch.Clear();

        await Assert.That(batch.Count()).IsEqualTo(0);
    }

    [Test]
    public async Task DeleteRange_RemovesEveryKeyInTheRangeWhenWritten()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a", "1");
        database.Db.Put("b", "2");
        database.Db.Put("c", "3");
        database.Db.Put("d", "4");

        using var batch = new WriteBatch();
        batch.DeleteRange("b"u8, "d"u8);
        database.Db.Write(batch);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("a")).IsEqualTo("1");
            await Assert.That(database.Db.Get("b")).IsNull();
            await Assert.That(database.Db.Get("c")).IsNull();
            await Assert.That(database.Db.Get("d")).IsEqualTo("4");
        }
    }

    [Test]
    public async Task DeleteRange_CanBeScopedToAFamily()
    {
        using var options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using var familyOptions = new ColumnFamilyOptions();
        var families = new ColumnFamilies { { "blocks", familyOptions } };
        using var database = TestDatabase.Create(options, families);
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put("b"u8, "family"u8, blocks);
        database.Db.Put("b", "default");

        using var batch = new WriteBatch();
        batch.DeleteRange("a"u8, "c"u8, blocks);
        database.Db.Write(batch);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("b"u8.ToArray(), blocks)).IsNull();
            await Assert.That(database.Db.Get("b")).IsEqualTo("default");
        }
    }

    [Test]
    public async Task DataSize_GrowsAsOperationsAreRecorded()
    {
        using var batch = new WriteBatch();
        var emptySize = batch.DataSize;

        batch.Put(Key, Value);

        await Assert.That(batch.DataSize).IsGreaterThan(emptySize);
    }

    [Test]
    public async Task Put_RecordsTheKeyAndValue()
    {
        using var batch = new WriteBatch();

        batch.Put(Key, Value);

        await Assert.That(Replay(batch)).IsEquivalentTo(new[] { Entry.Written(Key, Value) }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_FromSpans_RecordsTheKeyAndValue()
    {
        using var batch = new WriteBatch();

        batch.Put("k"u8, "v"u8);

        await Assert.That(Replay(batch)).IsEquivalentTo(new[] { Entry.Written("k"u8, "v"u8) }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_FromStrings_EncodesAsUtf8ByDefault()
    {
        using var batch = new WriteBatch();

        batch.Put("kü", "vé");

        await Assert.That(Replay(batch)).IsEquivalentTo(new[]
        {
            Entry.Written(Encoding.UTF8.GetBytes("kü"), Encoding.UTF8.GetBytes("vé")),
        }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_WithSlicedSpans_RecordsOnlyThoseBytes()
    {
        using var batch = new WriteBatch();

        batch.Put(((byte[])[1, 2, 3]).AsSpan(0, 2), ((byte[])[4, 5, 6]).AsSpan(0, 1));

        await Assert.That(Replay(batch)).IsEquivalentTo(new[] { Entry.Written([1, 2], [4]) }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Delete_RecordsTheKeyWithoutAValue()
    {
        using var batch = new WriteBatch();

        batch.Delete(Key);

        await Assert.That(Replay(batch)).IsEquivalentTo(new[] { Entry.Deleted(Key) }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Entries_AreReplayedInTheOrderTheyWereRecorded()
    {
        using var batch = new WriteBatch();

        batch.Put([1], [10]).Delete([2]).Put([3], [30]);

        await Assert.That(Replay(batch)).IsEquivalentTo(new[]
        {
            Entry.Written([1], [10]),
            Entry.Deleted([2]),
            Entry.Written([3], [30]),
        }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task ToBytes_RoundTripsThroughFromSpan()
    {
        using var source = new WriteBatch();
        source.Put([1], [10]).Delete([2]);

        using var restored = WriteBatch.FromSpan(source.ToBytes());

        using (Assert.Multiple())
        {
            await Assert.That(restored.Count()).IsEqualTo(2);
            await Assert.That(Replay(restored)).IsEquivalentTo(Replay(source), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task FromSpan_RoundTripsTheRepresentation()
    {
        using var source = new WriteBatch();
        source.Put(Key, Value);

        using var restored = WriteBatch.FromSpan(source.ToBytes());

        await Assert.That(Replay(restored)).IsEquivalentTo(Replay(source), CollectionOrdering.Matching);
    }

    [Test]
    public async Task ToBytesPooled_MatchesToBytesForTheReportedLength()
    {
        using var batch = new WriteBatch();
        batch.Put(Key, Value);

        var pooled = batch.ToBytesPooled(out var size);

        try
        {
            await Assert.That(pooled.AsSpan(0, size).ToArray()).IsEquivalentTo(batch.ToBytes(), CollectionOrdering.Matching);
        }
        finally
        {
            WriteBatch.ReturnPooledBytes(pooled);
        }
    }

    [Test]
    public async Task RollbackToSavePoint_DiscardsWhatCameAfterIt()
    {
        using var batch = new WriteBatch();
        batch.Put([1], [10]);
        batch.SetSavePoint();
        batch.Put([2], [20]);

        batch.RollbackToSavePoint();

        await Assert.That(Replay(batch)).IsEquivalentTo(new[] { Entry.Written([1], [10]) }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task RollbackToSavePoint_WithoutASavePoint_Throws()
    {
        using var batch = new WriteBatch();

        await Assert.That(batch.RollbackToSavePoint).Throws<RocksDbException>();
    }

    [Test]
    public async Task PopSavePoint_KeepsTheWritesButForgetsTheMarker()
    {
        using var batch = new WriteBatch();
        batch.SetSavePoint();
        batch.Put([1], [10]);

        batch.PopSavePoint();

        using (Assert.Multiple())
        {
            await Assert.That(Replay(batch)).IsEquivalentTo(new[] { Entry.Written([1], [10]) }, CollectionOrdering.Matching);
            await Assert.That(batch.RollbackToSavePoint).Throws<RocksDbException>();
        }
    }

    [Test]
    public async Task PopSavePoint_WithoutASavePoint_Throws()
    {
        using var batch = new WriteBatch();

        await Assert.That(batch.PopSavePoint).Throws<RocksDbException>();
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        var batch = new WriteBatch();

        batch.Dispose();

        await Assert.That(batch.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        var batch = new WriteBatch();
        batch.Dispose();

        await Assert.That(batch.Dispose).ThrowsNothing();
    }

}
