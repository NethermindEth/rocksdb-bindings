// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

/// <summary>
/// <see cref="PinnedSlice"/> is a ref struct, so it cannot live across an await. Each test reads
/// synchronously into locals, releases the slice, and only then asserts.
/// </summary>
public class PinnedSliceTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Value = "value"u8.ToArray();

    /// <summary>Reads the first four bytes of a value as a little endian integer.</summary>
    private sealed class Int32Deserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => BitConverter.ToInt32(buffer);
    }

    private static (bool Found, bool HasValue, byte[] Value) GetPinned(RocksDb db, byte[] key, IColumnFamilyHandle? cf = null)
    {
        var found = db.TryGetPinned(key, out var slice, cf);
        try
        {
            return (found, slice.HasValue, slice.Value.ToArray());
        }
        finally
        {
            slice.Dispose();
        }
    }

    [Test]
    public async Task TryGetPinned_ReturnsTheValueForAnExistingKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        var (found, hasValue, value) = GetPinned(database.Db, Key);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(hasValue).IsTrue();
            await Assert.That(value).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task TryGetPinned_ReturnsFalseForAMissingKey()
    {
        using var database = TestDatabase.Create();

        var (found, hasValue, value) = GetPinned(database.Db, Key);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsFalse();
            await Assert.That(hasValue).IsFalse();
            await Assert.That(value.Length).IsEqualTo(0);
        }
    }

    [Test]
    public async Task TryGetPinned_DistinguishesAnEmptyValueFromAMissingKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, ReadOnlySpan<byte>.Empty);

        var (found, hasValue, value) = GetPinned(database.Db, Key);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(hasValue).IsTrue();
            await Assert.That(value.Length).IsEqualTo(0);
        }
    }

    [Test]
    public async Task TryGetPinned_ReadsFromTheGivenColumnFamily()
    {
        using var options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using var familyOptions = new ColumnFamilyOptions();
        var families = new ColumnFamilies { { "blocks", familyOptions } };
        using var database = TestDatabase.Create(options, families);
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, Value, blocks);

        var inFamily = GetPinned(database.Db, Key, blocks);
        var inDefault = GetPinned(database.Db, Key);

        using (Assert.Multiple())
        {
            await Assert.That(inFamily.Found).IsTrue();
            await Assert.That(inDefault.Found).IsFalse();
        }
    }

    [Test]
    public async Task Dispose_IsIdempotentAndSafeOnADefaultedSlice()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        database.Db.TryGetPinned(Key, out var slice);

        slice.Dispose();
        slice.Dispose();
        default(PinnedSlice).Dispose();

        // The database still works, proving nothing was double-freed.
        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task DangerousDetach_KeepsTheValueAliveUntilDangerousDestroy()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        nint handle = 0;
        try
        {
            database.Db.TryGetPinned(Key, out var slice);
            handle = slice.DangerousDetach();
            slice.Dispose();

            // The bytes must still be readable after the slice itself was disposed.
            var value = slice.Value.ToArray();

            using (Assert.Multiple())
            {
                await Assert.That(handle).IsNotEqualTo(nint.Zero);
                await Assert.That(value).IsEquivalentTo(Value, CollectionOrdering.Matching);
            }
        }
        finally
        {
            PinnedSlice.DangerousDestroy(handle);
        }

        PinnedSlice.DangerousDestroy(nint.Zero);
    }

    [Test]
    public async Task Get_CopiesTheValueIntoTheDestination()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        var destination = new byte[Value.Length + 3];

        var length = database.Db.Get(Key, destination.AsSpan());

        using (Assert.Multiple())
        {
            await Assert.That(length).IsEqualTo(Value.Length);
            await Assert.That(destination[..length]).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Get_ReturnsMinusOneForAMissingKey()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.Get(Key, new byte[8].AsSpan())).IsEqualTo(-1);
    }

    [Test]
    public async Task Get_ThrowsWhenTheDestinationIsTooSmall()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(() =>
        {
            var destination = new byte[Value.Length - 1];
            return database.Db.Get(Key, destination.AsSpan());
        }).Throws<ArgumentException>();
    }

    [Test]
    public async Task Get_WithSpanDeserializer_ReadsWithoutMaterializingAnArray()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, BitConverter.GetBytes(1234));

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key, new Int32Deserializer())).IsEqualTo(1234);
            await Assert.That(database.Db.Get("missing"u8.ToArray().AsSpan(), new Int32Deserializer())).IsEqualTo(0);
        }
    }

    [Test]
    public async Task HasKey_IsTrueForAnEmptyValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, ReadOnlySpan<byte>.Empty);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.HasKey(Key.AsSpan())).IsTrue();
            await Assert.That(database.Db.HasKey("missing"u8.ToArray().AsSpan())).IsFalse();
        }
    }

    [Test]
    public async Task Get_ReturnsAnEmptyArrayForAnEmptyValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, ReadOnlySpan<byte>.Empty);

        var value = database.Db.Get(Key);

        using (Assert.Multiple())
        {
            await Assert.That(value).IsNotNull();
            await Assert.That(value!.Length).IsEqualTo(0);
        }
    }

    private static byte[] GetSpanCopy(RocksDb db, byte[] key, IColumnFamilyHandle? cf = null)
    {
        var span = db.GetSpan(key, cf);
        try
        {
            return span.ToArray();
        }
        finally
        {
            db.DangerousReleaseMemory(span);
        }
    }

    [Test]
    public async Task GetSpan_ReturnsAReleasableCopyOfTheValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(GetSpanCopy(database.Db, Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetSpan_ReadsFromTheGivenColumnFamily()
    {
        using var options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using var familyOptions = new ColumnFamilyOptions();
        var families = new ColumnFamilies { { "blocks", familyOptions } };
        using var database = TestDatabase.Create(options, families);
        var blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put(Key, "in default"u8);
        database.Db.Put(Key, "in blocks"u8, blocks);

        using (Assert.Multiple())
        {
            await Assert.That(GetSpanCopy(database.Db, Key, blocks)).IsEquivalentTo("in blocks"u8.ToArray(), CollectionOrdering.Matching);
            await Assert.That(GetSpanCopy(database.Db, Key)).IsEquivalentTo("in default"u8.ToArray(), CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task GetSpan_IsEmptyForAMissingKeyAndForAnEmptyValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, ReadOnlySpan<byte>.Empty);

        var missing = database.Db.GetSpan("missing"u8).IsEmpty;
        var empty = database.Db.GetSpan(Key).IsEmpty;

        using (Assert.Multiple())
        {
            await Assert.That(missing).IsTrue();
            await Assert.That(empty).IsTrue();
        }
    }

    [Test]
    public async Task DangerousReleaseMemory_IgnoresAnEmptySpan()
    {
        using var database = TestDatabase.Create();

        database.Db.DangerousReleaseMemory(ReadOnlySpan<byte>.Empty);

        // The database still works, proving nothing was freed by mistake.
        database.Db.Put(Key, Value);
        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }
}
