// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;
using System.Text;
using Nethermind.RocksDbBindings.Native;

namespace Nethermind.RocksDbBindings.Tests;

public class RocksDbTests
{
    private static readonly byte[] Key = "key"u8.ToArray();
    private static readonly byte[] Value = "value"u8.ToArray();

    /// <summary>Reads the first four bytes of a value as a little endian integer.</summary>
    private sealed class Int32Deserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => BitConverter.ToInt32(buffer);
    }

    private sealed class FirstByteDeserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => buffer.IsEmpty ? -1 : buffer[0];
    }

    private sealed class ThrowingDeserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => throw new InvalidOperationException("decoder failed");
    }

    private static DbOptions CreatingOptions()
        => new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();

    private static ColumnFamilies Families(params string[] names)
    {
        ColumnFamilies families = new();
        foreach (string name in names)
            families.Add(name, new ColumnFamilyOptions());
        return families;
    }

    private static bool TryGetFixedSize(RocksDb db, byte[] key, byte[] output)
        => db.GetFixedSizeValue(key, output);

    private static bool HasKey(RocksDb db, byte[] key) => db.HasKey(key.AsSpan());

    [Test]
    [Arguments(false, false, 1)]
    [Arguments(false, false, 2)]
    [Arguments(false, false, 32)]
    [Arguments(false, false, 512)]
    [Arguments(false, true, 1)]
    [Arguments(false, true, 2)]
    [Arguments(false, true, 32)]
    [Arguments(false, true, 512)]
    [Arguments(true, false, 1)]
    [Arguments(true, false, 2)]
    [Arguments(true, false, 32)]
    [Arguments(true, false, 512)]
    [Arguments(true, true, 1)]
    [Arguments(true, true, 2)]
    [Arguments(true, true, 32)]
    [Arguments(true, true, 512)]
    public async Task MultiGet_WithMultiByteValues_PreservesCompletePayload(bool genericDecoder, bool namedFamily, int count)
    {
        using TestDatabase database = namedFamily ? TestDatabase.Create(CreatingOptions(), Families("blocks")) : TestDatabase.Create();
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        byte[] payload = new byte[4097];
        for (int i = 0; i < payload.Length; i++) payload[i] = (byte)(i % 251);
        byte[] keys = new byte[count * 4];
        byte[][] values = new byte[count][];
        for (int i = 0; i < count; i++)
        {
            BitConverter.TryWriteBytes(keys.AsSpan(i * 4, 4), i);
            values[i] = [99];
            if (count == 1 || i != count - 1)
                database.Db.Put(keys.AsSpan(i * 4, 4), count > 2 && i == 0 ? [] : payload, family);
        }

        if (genericDecoder)
            database.Db.MultiGet<byte[], BytesDeserializer>(keys, 4, values.AsSpan(), new BytesDeserializer(), family);
        else
            database.Db.MultiGet<byte[]>(keys, 4, values.AsSpan(), new BytesDeserializer(), family);

        for (int i = 0; i < count; i++)
        {
            if (count > 1 && i == count - 1) await Assert.That(values[i]).IsNull();
            else await Assert.That(values[i]).IsEquivalentTo(count > 2 && i == 0 ? [] : payload, CollectionOrdering.Matching);
        }
    }

    [Test]
    [Arguments(1, false)]
    [Arguments(2, false)]
    [Arguments(3, false)]
    [Arguments(257, false)]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(3, true)]
    [Arguments(257, true)]
    public async Task MultiGet_WhenReplacingReferenceValues_KeepsPreviousValueUntilDecoded(int count, bool cacheOnly)
    {
        using TestDatabase database = TestDatabase.Create();
        using ReadOptions options = new ReadOptions().SetReadTier(cacheOnly ? 1 : 0);
        byte[] keys = new byte[count];
        byte[] previous = new byte[131072];
        byte[][] values = Enumerable.Repeat(previous, count).ToArray();
        database.Db.Put([0], [1, 2, 3]);
        for (int i = 0; i < count; i++) keys[i] = (byte)(i == 0 ? 0 : 1);

        database.Db.MultiGet(keys, 1, values.AsSpan(), new ObservingReplacementDeserializer(values, previous), readOptions: options);

        await Assert.That(values[0]).IsEquivalentTo(new byte[] { 1, 2, 3 }, CollectionOrdering.Matching);
        for (int i = 1; i < count; i++) await Assert.That(values[i]).IsNull();
    }

    [Test]
    public async Task Open_WithoutCreateIfMissing_FailsOnAnEmptyDirectory()
    {
        using var directory = new TempDirectory();

        var exception = await Assert.That(() => RocksDb.Open(new DbOptions(), directory.Reserve("db")))
            .Throws<RocksDbException>();

        await Assert.That(exception!.Message).Contains("does not exist");
    }

    [Test]
    public async Task Open_NullPath_ThrowsInsteadOfPassingANullPointerToTheNativeCall()
    {
        using var options = new DbOptions();

        await Assert.That(() => RocksDb.Open(options, null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Open_RecordsThePathItWasGiven()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.Path).IsEqualTo(database.Path);
    }

    [Test]
    public async Task Open_ReopensAnExistingDatabaseWithItsData()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), path))
            db.Put(Key, Value);

        using var reopened = RocksDb.Open(new DbOptions(), path);

        await Assert.That(reopened.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task OpenReadOnly_SeesTheDataButRefusesWrites()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using (var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), path))
            db.Put(Key, Value);

        using var readOnly = RocksDb.OpenReadOnly(new DbOptions(), path, errorIfLogFileExists: false);

        using (Assert.Multiple())
        {
            await Assert.That(readOnly.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(() => readOnly.Put(Key, Value)).Throws<RocksDbException>();
        }
    }

    [Test]
    public async Task OpenWithTtl_BehavesLikeANormalDatabaseWhileTheDataIsFresh()
    {
        using var directory = new TempDirectory();

        using var db = RocksDb.OpenWithTtl(new DbOptions().SetCreateIfMissing(), directory.Reserve("db"), ttlSeconds: 3600);
        db.Put(Key, Value);

        await Assert.That(db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task OpenAsSecondary_CatchesUpWithThePrimary()
    {
        using var directory = new TempDirectory();
        var primaryPath = directory.Reserve("primary");

        using var primary = RocksDb.Open(new DbOptions().SetCreateIfMissing(), primaryPath);
        primary.Put(Key, Value);
        primary.Flush(new FlushOptions().SetWaitForFlush(true));

        using var secondary = RocksDb.OpenAsSecondary(new DbOptions(), primaryPath, directory.Reserve("secondary"));
        secondary.TryCatchUpWithPrimary();

        await Assert.That(secondary.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Get_MissingKey_ReturnsNull()
    {
        using var database = TestDatabase.Create();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(Key)).IsNull();
            await Assert.That(database.Db.Get("missing")).IsNull();
        }
    }

    [Test]
    public async Task Put_AndGet_RoundTripBytes()
    {
        using var database = TestDatabase.Create();

        database.Db.Put(Key, Value);

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_AndGet_RoundTripSpans()
    {
        using var database = TestDatabase.Create();

        database.Db.Put("k"u8, "v"u8);

        await Assert.That(database.Db.Get("k"u8)).IsEquivalentTo("v"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_AndGet_RoundTripStrings()
    {
        using var database = TestDatabase.Create();

        database.Db.Put("key", "value");

        await Assert.That(database.Db.Get("key")).IsEqualTo("value");
    }

    [Test]
    public async Task Put_WithSlicedSpans_StoresOnlyThoseBytes()
    {
        using var database = TestDatabase.Create();

        database.Db.Put([1, 2, 3], value: ((byte[])[4, 5, 6]).AsSpan(0, 1));

        await Assert.That(database.Db.Get(new byte[] { 1, 2, 3 })).IsEquivalentTo(new byte[] { 4 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_AsAString_IsStoredAsUtf8()
    {
        using var database = TestDatabase.Create();

        database.Db.Put("kü", "vé");

        await Assert.That(database.Db.Get(Encoding.UTF8.GetBytes("kü"))).IsEquivalentTo(Encoding.UTF8.GetBytes("vé"), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_OverwritesAnExistingValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        database.Db.Put(Key, "second"u8.ToArray());

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo("second"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task Put_StoresAnEmptyValue()
    {
        using var database = TestDatabase.Create();

        database.Db.Put(Key, Array.Empty<byte>());

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Array.Empty<byte>(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task HasKey_DistinguishesPresentFromMissing()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        using (Assert.Multiple())
        {
            await Assert.That(HasKey(database.Db, Key)).IsTrue();
            await Assert.That(HasKey(database.Db, "absent"u8.ToArray())).IsFalse();
        }
    }

    /// <remarks>
    /// An empty value is still a value, so presence has to be decided by the returned pointer
    /// rather than by the length RocksDB reports.
    /// </remarks>
    [Test]
    public async Task HasKey_IsTrueForAKeyWithAnEmptyValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Array.Empty<byte>());

        await Assert.That(HasKey(database.Db, Key)).IsTrue();
    }

    [Test]
    public async Task HasKey_ForAString_FindsTheKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("key", "value");

        await Assert.That(database.Db.HasKey("key")).IsTrue();
    }

    [Test]
    public async Task Remove_DeletesTheKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        database.Db.Remove(Key);

        await Assert.That(database.Db.Get(Key)).IsNull();
    }

    [Test]
    public async Task Remove_OfAMissingKey_IsSilent()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.Remove(Key)).ThrowsNothing();
    }

    [Test]
    public async Task Remove_ByString_DeletesTheKey()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("key", "value");

        database.Db.Remove("key");

        await Assert.That(database.Db.Get("key")).IsNull();
    }

    [Test]
    public async Task GetFixedSizeValue_FillsTheBufferWhenTheLengthMatches()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, new byte[] { 1, 2, 3, 4 });
        var output = new byte[4];

        var found = TryGetFixedSize(database.Db, Key, output);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(output).IsEquivalentTo(new byte[] { 1, 2, 3, 4 }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task GetFixedSizeValue_RejectsAValueOfADifferentLength()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, new byte[] { 1, 2, 3 });

        await Assert.That(TryGetFixedSize(database.Db, Key, new byte[4])).IsFalse();
    }

    [Test]
    public async Task GetFixedSizeValue_ReportsAMissingKey()
    {
        using var database = TestDatabase.Create();

        await Assert.That(TryGetFixedSize(database.Db, Key, new byte[4])).IsFalse();
    }

    [Test]
    public async Task Get_IntoABuffer_CopiesTheValueAndLeavesTheRestOfTheDestinationAlone()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, new byte[] { 1, 2, 3 });
        var output = new byte[] { 9, 9, 9, 9 };

        var length = database.Db.Get(Key, output);

        using (Assert.Multiple())
        {
            await Assert.That(length).IsEqualTo(3);
            await Assert.That(output).IsEquivalentTo(new byte[] { 1, 2, 3, 9 }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task Get_IntoABuffer_ReturnsMinusOneForAMissingKey()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.Get(Key, new byte[4])).IsEqualTo(-1);
    }

    [Test]
    public async Task Get_IntoABuffer_ReadsAnEmptyValueIntoAnEmptyDestination()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, []);

        await Assert.That(database.Db.Get(Key, [])).IsEqualTo(0);
    }

    [Test]
    public async Task Get_IntoABuffer_RejectsADestinationTooSmallForTheValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(() => database.Db.Get(Key, new byte[Value.Length - 1])).Throws<ArgumentException>();
    }

    [Test]
    public async Task Get_IntoABuffer_LeavesTheDestinationUntouchedWhenTheValueDoesNotFit()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, new byte[] { 1, 2, 3 });
        var output = new byte[] { 9, 9 };

        await Assert.That(() => database.Db.Get(Key, output)).Throws<ArgumentException>();
        await Assert.That(output).IsEquivalentTo(new byte[] { 9, 9 }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task Get_WithASpanDeserializer_DecodesTheValue()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, BitConverter.GetBytes(1234));

        await Assert.That(database.Db.Get(Key, new Int32Deserializer())).IsEqualTo(1234);
    }

    [Test]
    public async Task Get_WithASpanDeserializer_ReturnsTheDefaultForAMissingKey()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.Get(Key, new Int32Deserializer())).IsEqualTo(0);
    }

    private static void ReadFixed<T>(bool genericDecoder, RocksDb db, ReadOnlySpan<byte> keys, int keyLength,
        T[] values, ISpanDeserializer<T> deserializer, IColumnFamilyHandle? cf = null, ReadOptions? readOptions = null)
    {
        if (genericDecoder && deserializer is not null)
            db.MultiGet<T, ForwardingValueDeserializer<T>>(keys, keyLength, values.AsSpan(), new(deserializer), cf, readOptions);
        else
            db.MultiGet<T>(keys, keyLength, values.AsSpan(), deserializer!, cf, readOptions);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGet_FixedKeysReadsMixedEmptyMissingOrderAndDuplicates(bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put([1, 1], [10], blocks);
        database.Db.Put([1, 2], [], blocks);

        byte[] keys = [1, 2, 1, 3, 1, 1, 1, 2, 1, 1];
        int[] values = [99, 99, 99, 99, 99];
        ReadFixed(genericDecoder, database.Db, keys, 2, values, new FirstByteDeserializer(), blocks);

        await Assert.That(values).IsEquivalentTo([-1, 0, 10, -1, 10], CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(256, false)]
    [Arguments(256, true)]
    [Arguments(512, false)]
    [Arguments(512, true)]
    public async Task MultiGet_FixedKeysHandlesStackAndPooledBatches(int count, bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        const int keyLength = 4;
        byte[] keys = new byte[count * keyLength];
        int[] values = new int[count];

        for (int i = 0; i < count; i++)
        {
            int offset = i * keyLength;
            keys[offset] = (byte)(i >> 24);
            keys[offset + 1] = (byte)(i >> 16);
            keys[offset + 2] = (byte)(i >> 8);
            keys[offset + 3] = (byte)i;
            database.Db.Put(keys.AsSpan(offset, keyLength), [(byte)(i & 0xff)], blocks);
        }

        ReadFixed(genericDecoder, database.Db, keys, keyLength, values, new FirstByteDeserializer(), blocks);

        for (int i = 0; i < count; i++)
            await Assert.That(values[i]).IsEqualTo(i & 0xff);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGet_UsesSnapshotWithCacheFillDisabled(bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put([1, 1], [10], blocks);

        using Snapshot snapshot = database.Db.CreateSnapshot();
        using ReadOptions readOptions = new ReadOptions().SetSnapshot(snapshot).SetFillCache(false);
        database.Db.Put([1, 1], [20], blocks);

        int[] values = [99, 99, 99];
        ReadFixed(genericDecoder, database.Db, [1, 1, 1, 3, 1, 1], 2, values, new FirstByteDeserializer(), blocks, readOptions);

        await Assert.That(values).IsEquivalentTo([10, 0, 10], CollectionOrdering.Matching);

        int[] pair = [99, 99];
        ReadFixed(genericDecoder, database.Db, [1, 1, 1, 3], 2, pair, new FirstByteDeserializer(), blocks, readOptions);
        await Assert.That(pair).IsEquivalentTo([10, 0], CollectionOrdering.Matching);

        int[] single = [99];
        ReadFixed(genericDecoder, database.Db, [1, 1], 2, single, new FirstByteDeserializer(), blocks, readOptions);
        await Assert.That(single).IsEquivalentTo([10], CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGet_ValidatesShapeAndOverflowBeforeClearingOutputs(bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        int[] values = [123];

        using (Assert.Multiple())
        {
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 1, values, null!, blocks))
                .Throws<ArgumentNullException>();
            await Assert.That(values).IsEquivalentTo([123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 0, values, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentOutOfRangeException>();
            await Assert.That(values).IsEquivalentTo([123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 1, values, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentException>();
            await Assert.That(values).IsEquivalentTo([123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 1 << 30, new int[2], new FirstByteDeserializer(), blocks))
                .Throws<ArgumentException>();
            int[] overflowValues = [123, 123, 123, 123];
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 1 << 30, overflowValues, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentException>();
            await Assert.That(overflowValues).IsEquivalentTo([123, 123, 123, 123], CollectionOrdering.Matching);
            int[] pairValues = [123, 123];
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [1, 1, 1, 2], 2, pairValues, null!, blocks))
                .Throws<ArgumentNullException>();
            await Assert.That(pairValues).IsEquivalentTo([123, 123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 0, pairValues, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentOutOfRangeException>();
            await Assert.That(pairValues).IsEquivalentTo([123, 123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 2, pairValues, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentException>();
            await Assert.That(pairValues).IsEquivalentTo([123, 123], CollectionOrdering.Matching);
            int[] multipleValues = [123, 123, 123];
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [1, 1, 1, 2, 1, 3], 2, multipleValues, null!, blocks))
                .Throws<ArgumentNullException>();
            await Assert.That(multipleValues).IsEquivalentTo([123, 123, 123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 0, multipleValues, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentOutOfRangeException>();
            await Assert.That(multipleValues).IsEquivalentTo([123, 123, 123], CollectionOrdering.Matching);
            await Assert.That(() => ReadFixed(genericDecoder, database.Db, [], 2, multipleValues, new FirstByteDeserializer(), blocks))
                .Throws<ArgumentException>();
            await Assert.That(multipleValues).IsEquivalentTo([123, 123, 123], CollectionOrdering.Matching);
        }
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGet_CleansUpPinnableValuesWhenDeserializerThrows(bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle blocks = database.Db.GetColumnFamily("blocks");
        database.Db.Put([1, 1], [10], blocks);
        database.Db.Put([1, 2], [20], blocks);

        await Assert.That(() => ReadFixed(genericDecoder, database.Db, [1, 1, 1, 2], 2, new int[2], new ThrowingDeserializer(), blocks))
            .Throws<InvalidOperationException>();

        await Assert.That(database.Db.Get([1, 1], blocks)).IsEquivalentTo(new byte[] { 10 }, CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGet_WithoutColumnFamily_ReadsDefaultFamily(bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put([1, 1], [10]);

        int[] values = [99, 99];
        ReadFixed(genericDecoder, database.Db, [1, 1, 1, 3], 2, values, new FirstByteDeserializer());

        await Assert.That(values).IsEquivalentTo([10, 0], CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(2, false, false)]
    [Arguments(2, false, true)]
    [Arguments(2, true, false)]
    [Arguments(2, true, true)]
    [Arguments(3, false, false)]
    [Arguments(3, false, true)]
    [Arguments(3, true, false)]
    [Arguments(3, true, true)]
    [Arguments(32, false, false)]
    [Arguments(32, false, true)]
    [Arguments(32, true, false)]
    [Arguments(32, true, true)]
    [Arguments(512, false, false)]
    [Arguments(512, false, true)]
    [Arguments(512, true, false)]
    [Arguments(512, true, true)]
    public async Task MultiGet_WithOnlyMissingOrEmptyValues_PreservesTheirDistinction(int count, bool namedFamily, bool present)
    {
        using TestDatabase database = namedFamily ? TestDatabase.Create(CreatingOptions(), Families("blocks")) : TestDatabase.Create();
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        byte[] keys = new byte[count * 4];
        int[] values = new int[count];
        Array.Fill(values, 99);
        for (int i = 0; i < count; i++)
        {
            BitConverter.TryWriteBytes(keys.AsSpan(i * 4, 4), i);
            if (present) database.Db.Put(keys.AsSpan(i * 4, 4), [], family);
        }

        if (present)
            database.Db.MultiGet(keys, 4, values.AsSpan(), new FirstByteValueDeserializer(), family);
        else
            database.Db.MultiGet(keys, 4, values.AsSpan(), new ThrowingDeserializer(), family);

        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(present ? -1 : 0);
        database.Db.Put(keys.AsSpan(0, 4), [10], family);
        database.Db.MultiGet(keys, 4, values.AsSpan(), new FirstByteValueDeserializer(), family);
        await Assert.That(values[0]).IsEqualTo(10);
        for (int i = 1; i < count; i++) await Assert.That(values[i]).IsEqualTo(present ? -1 : 0);
    }

    [Test]
    [Arguments(false, false)]
    [Arguments(false, true)]
    [Arguments(true, false)]
    [Arguments(true, true)]
    public async Task MultiGet_WithDecoder_HandlesSortedDuplicatesEmptyAndMissing(bool sorted, bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle family = database.Db.GetColumnFamily("blocks");
        database.Db.Put([1, 1], [10], family);
        database.Db.Put([1, 2], [], family);
        int[] values = [99, 99, 99, 99];

        if (genericDecoder)
            database.Db.MultiGet([1, 1, 1, 1, 1, 2, 1, 3], 2, values.AsSpan(), new FirstByteValueDeserializer(), family, sortedInput: sorted);
        else
            database.Db.MultiGet<int>([1, 1, 1, 1, 1, 2, 1, 3], 2, values.AsSpan(), new FirstByteDeserializer(), family, sortedInput: sorted);

        await Assert.That(values).IsEquivalentTo([10, 10, -1, 0], CollectionOrdering.Matching);
    }

    [Test]
    [Arguments(1, 0, false)]
    [Arguments(2, 1, false)]
    [Arguments(4, 2, false)]
    [Arguments(512, 255, false)]
    [Arguments(1, 0, true)]
    [Arguments(2, 1, true)]
    [Arguments(4, 2, true)]
    [Arguments(512, 255, true)]
    public async Task MultiGet_WhenDecoderThrows_PreservesPrefixAndClearsTail(int count, int throwAt, bool defaultFamily)
    {
        using TestDatabase database = defaultFamily ? TestDatabase.Create() : TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle? family = defaultFamily ? null : database.Db.GetColumnFamily("blocks");
        byte[] keys = new byte[count * 4];
        int[] values = new int[count];
        Array.Fill(values, 99);
        for (int i = 0; i < count; i++)
        {
            BitConverter.TryWriteBytes(keys.AsSpan(i * 4, 4), i);
            database.Db.Put(keys.AsSpan(i * 4, 4), [(byte)(i + 1)], family);
        }

        await Assert.That(() => database.Db.MultiGet(keys, 4, values.AsSpan(), new ThrowAfterDeserializer(throwAt), family))
            .Throws<InvalidOperationException>();

        for (int i = 0; i < count; i++)
            await Assert.That(values[i]).IsEqualTo(i < throwAt ? (i + 1) & 255 : 0);
        database.Db.MultiGet(keys, 4, values.AsSpan(), new FirstByteValueDeserializer(), family);
        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo((i + 1) & 255);
    }

    [Test]
    [Arguments(false, false, 1)]
    [Arguments(false, true, 1)]
    [Arguments(true, false, 1)]
    [Arguments(true, true, 1)]
    [Arguments(false, false, 2)]
    [Arguments(false, true, 2)]
    [Arguments(true, false, 2)]
    [Arguments(true, true, 2)]
    [Arguments(false, false, 512)]
    [Arguments(false, true, 512)]
    [Arguments(true, false, 512)]
    [Arguments(true, true, 512)]
    public async Task MultiGet_WhenNativeReadsFail_ClearsOutputsAndRemainsUsable(bool genericDecoder, bool namedFamily, int count)
    {
        using TestDatabase database = namedFamily ? TestDatabase.Create(CreatingOptions(), Families("blocks")) : TestDatabase.Create();
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        database.Db.Put([1, 1], [10], family);
        database.Db.Put([1, 2], [20], family);
        byte[] keys = new byte[count * 2];
        int[] values = new int[count];
        Array.Fill(values, 99);
        for (int i = 0; i < count; i++)
        {
            keys[i * 2] = 1;
            keys[i * 2 + 1] = (byte)(i % 2 + 1);
        }

        await Assert.That(() => ReadWithUnsupportedTimestamp(genericDecoder, database.Db, keys, values, family))
            .Throws<RocksDbNativeException>();

        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(0);
        ReadFixed(genericDecoder, database.Db, keys, 2, values, new FirstByteDeserializer(), family);
        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(i % 2 == 0 ? 10 : 20);
    }

    private static unsafe void ReadWithUnsupportedTimestamp(bool genericDecoder, RocksDb db, byte[] keys, int[] values, IColumnFamilyHandle? family)
    {
        using ReadOptions options = new();
        using HandleLease lease = options.Lease(out nint handle);
        byte timestamp = 1;
        RocksDbNative.rocksdb_readoptions_set_timestamp((rocksdb_readoptions_t*)handle, (sbyte*)&timestamp, 1);
        ReadFixed(genericDecoder, db, keys, 2, values, new FirstByteDeserializer(), family, options);
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task MultiGet_WhenLaterNativeReadsFail_ThrowsBeforeDecoding(bool genericDecoder)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle family = database.Db.GetColumnFamily("blocks");
        const int count = 512;
        byte[] keys = new byte[count * 2];
        int[] values = new int[count];
        Array.Fill(values, 99);
        for (int i = 0; i < count; i++)
        {
            keys[i * 2] = (byte)(i >> 8);
            keys[i * 2 + 1] = (byte)i;
            database.Db.Put(keys.AsSpan(i * 2, 2), [10], family);
        }

        await Assert.That(() => ReadWithZeroValueLimit(genericDecoder, database.Db, keys, values, family))
            .Throws<RocksDbNativeException>();

        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(0);
        ReadFixed(genericDecoder, database.Db, keys, 2, values, new FirstByteDeserializer(), family);
        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(10);
    }

    private static unsafe void ReadWithZeroValueLimit(bool genericDecoder, RocksDb db, byte[] keys, int[] values, IColumnFamilyHandle family)
    {
        using ReadOptions options = new();
        using HandleLease lease = options.Lease(out nint handle);
        RocksDbNative.rocksdb_readoptions_set_value_size_soft_limit((rocksdb_readoptions_t*)handle, 0);
        ReadFixed(genericDecoder, db, keys, 2, values, new ThrowingDeserializer(), family, options);
    }

    [Test]
    [Arguments(false, 0, 1)]
    [Arguments(false, 0, 2)]
    [Arguments(false, 0, 3)]
    [Arguments(false, 0, 512)]
    [Arguments(false, 1, 1)]
    [Arguments(false, 1, 2)]
    [Arguments(false, 1, 3)]
    [Arguments(false, 1, 512)]
    [Arguments(false, 2, 1)]
    [Arguments(false, 2, 2)]
    [Arguments(false, 2, 3)]
    [Arguments(false, 2, 512)]
    [Arguments(true, 0, 1)]
    [Arguments(true, 0, 2)]
    [Arguments(true, 0, 3)]
    [Arguments(true, 0, 512)]
    [Arguments(true, 1, 1)]
    [Arguments(true, 1, 2)]
    [Arguments(true, 1, 3)]
    [Arguments(true, 1, 512)]
    [Arguments(true, 2, 1)]
    [Arguments(true, 2, 2)]
    [Arguments(true, 2, 3)]
    [Arguments(true, 2, 512)]
    public async Task MultiGet_WithCacheOnlyReads_HonorsReadTierForEveryFamily(bool genericDecoder, int familyKind, int count)
    {
        using TestDatabase database = CreateUncachedFamilyDatabase();
        IColumnFamilyHandle? family = GetReadFamily(database.Db, familyKind);
        database.Db.Put([1, 1], [10], family);
        using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
        database.Db.Flush(flush, family ?? database.Db.GetDefaultColumnFamily());
        using ReadOptions readOptions = new ReadOptions().SetReadTier(1);
        byte[] keys = new byte[count * 2];
        Array.Fill(keys, (byte)1);
        int[] values = new int[count];
        Array.Fill(values, 99);

        await Assert.That(() => database.Db.Get([1, 1], family, readOptions)).Throws<RocksDbNativeException>();
        await Assert.That(() => ReadFixed(genericDecoder, database.Db, keys, 2, values, new FirstByteDeserializer(), family, readOptions))
            .Throws<RocksDbNativeException>();

        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(0);
        ReadFixed(genericDecoder, database.Db, keys, 2, values, new FirstByteDeserializer(), family);
        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(10);
    }

    [Test]
    [Arguments(false, 0, 2)]
    [Arguments(false, 0, 3)]
    [Arguments(false, 1, 2)]
    [Arguments(false, 1, 3)]
    [Arguments(false, 2, 2)]
    [Arguments(false, 2, 3)]
    [Arguments(true, 0, 2)]
    [Arguments(true, 0, 3)]
    [Arguments(true, 1, 2)]
    [Arguments(true, 1, 3)]
    [Arguments(true, 2, 2)]
    [Arguments(true, 2, 3)]
    public async Task MultiGet_WhenCacheOnlyReadFails_RetainsDecodedPrefix(bool genericDecoder, int familyKind, int count)
    {
        using TestDatabase database = CreateUncachedFamilyDatabase();
        IColumnFamilyHandle? family = GetReadFamily(database.Db, familyKind);
        database.Db.Put([1, 2], [20], family);
        using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
        database.Db.Flush(flush, family ?? database.Db.GetDefaultColumnFamily());
        database.Db.Put([1, 1], [10], family);
        using ReadOptions readOptions = new ReadOptions().SetReadTier(1);
        byte[] keys = new byte[count * 2];
        for (int i = 0; i < count; i++)
        {
            keys[i * 2] = 1;
            keys[i * 2 + 1] = (byte)(i == 0 ? 1 : 2);
        }
        int[] values = new int[count];
        Array.Fill(values, 99);

        await Assert.That(database.Db.Get([1, 1], family, readOptions)).IsEquivalentTo(new byte[] { 10 }, CollectionOrdering.Matching);
        await Assert.That(() => database.Db.Get([1, 2], family, readOptions)).Throws<RocksDbNativeException>();
        await Assert.That(() => ReadFixed(genericDecoder, database.Db, keys, 2, values, new FirstByteDeserializer(), family, readOptions))
            .Throws<RocksDbNativeException>();

        await Assert.That(values[0]).IsEqualTo(10);
        for (int i = 1; i < count; i++) await Assert.That(values[i]).IsEqualTo(0);
    }

    [Test]
    [Arguments(false, false, 2)]
    [Arguments(false, false, 3)]
    [Arguments(false, true, 2)]
    [Arguments(false, true, 3)]
    [Arguments(true, false, 2)]
    [Arguments(true, false, 3)]
    [Arguments(true, true, 2)]
    [Arguments(true, true, 3)]
    public async Task MultiGet_WhenScalarDecoderClosesFamily_PreservesPrefixAndRejectsFurtherReads(bool genericDecoder, bool namedFamily, int count)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle family = namedFamily ? database.Db.GetColumnFamily("blocks") : database.Db.GetDefaultColumnFamily();
        database.Db.Put([1, 1], [10], family);
        byte[] keys = new byte[count * 2];
        Array.Fill(keys, (byte)1);
        int[] values = new int[count];
        Array.Fill(values, 99);
        using ReadOptions options = new ReadOptions().SetReadTier(1);

        await Assert.That(() => ReadFixed(genericDecoder, database.Db, keys, 2, values, new ClosingFamilyDeserializer(family), family, options))
            .Throws<ObjectDisposedException>();

        await Assert.That(family.Handle).IsEqualTo(nint.Zero);
        await Assert.That(values[0]).IsEqualTo(10);
        for (int i = 1; i < count; i++) await Assert.That(values[i]).IsEqualTo(0);
    }

    [Test]
    [Arguments(0)]
    [Arguments(1)]
    [Arguments(2)]
    public async Task TryGet_AtTheBlockCacheTier_ReportsAFlushedValueAsIncompleteWithoutThrowing(int familyKind)
    {
        using TestDatabase database = CreateUncachedFamilyDatabase();
        IColumnFamilyHandle? family = GetReadFamily(database.Db, familyKind);
        database.Db.Put([1, 1], [10, 11], family);
        using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
        database.Db.Flush(flush, family ?? database.Db.GetDefaultColumnFamily());
        database.Db.Put([2, 2], [20], family);
        using ReadOptions cacheOnly = new ReadOptions().SetReadTier(1);
        byte[] destination = new byte[4];

        GetResult flushed = database.Db.TryGet([1, 1], destination, out long flushedLength, family, cacheOnly);
        GetResult inMemtable = database.Db.TryGet([2, 2], destination, out long memtableLength, family, cacheOnly);
        GetResult fromDisk = database.Db.TryGet([1, 1], destination, out long diskLength, family);

        using (Assert.Multiple())
        {
            await Assert.That(flushed).IsEqualTo(GetResult.Incomplete);
            await Assert.That(flushedLength).IsEqualTo(-1L);
            await Assert.That(inMemtable).IsEqualTo(GetResult.Found);
            await Assert.That(memtableLength).IsEqualTo(1L);
            await Assert.That(fromDisk).IsEqualTo(GetResult.Found);
            await Assert.That(diskLength).IsEqualTo(2L);
            await Assert.That(destination[..2]).IsEquivalentTo(new byte[] { 10, 11 }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task TryGet_ReportsAMissingKeyAsNotFound()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put([1], [10]);

        GetResult result = database.Db.TryGet([2], new byte[4], out long length);

        using (Assert.Multiple())
        {
            await Assert.That(result).IsEqualTo(GetResult.NotFound);
            await Assert.That(length).IsEqualTo(-1L);
        }
    }

    [Test]
    [Arguments(0)]
    [Arguments(2)]
    public async Task TryGet_ReportsTheLengthOfAValueLongerThanTheDestinationAndCopiesNothing(int destinationLength)
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put([1], [10, 11, 12]);
        byte[] destination = new byte[destinationLength];
        Array.Fill(destination, (byte)99);

        GetResult result = database.Db.TryGet([1], destination, out long length);

        using (Assert.Multiple())
        {
            await Assert.That(result).IsEqualTo(GetResult.Found);
            await Assert.That(length).IsEqualTo(3L);
            await Assert.That(destination.All(static b => b == 99)).IsTrue();
        }
    }

    [Test]
    public async Task TryGet_ReportsAnEmptyValueAsFound()
    {
        using TestDatabase database = TestDatabase.Create();
        database.Db.Put([1], []);

        GetResult result = database.Db.TryGet([1], [], out long length);

        using (Assert.Multiple())
        {
            await Assert.That(result).IsEqualTo(GetResult.Found);
            await Assert.That(length).IsEqualTo(0L);
        }
    }

    [Test]
    public async Task TryGet_ReportsACorruptBlockAsFailedWithoutThrowing()
    {
        using TempDirectory directory = new();
        string path = directory.Reserve("db");
        BlockBasedTableOptions table = new BlockBasedTableOptions().SetNoBlockCache(true);
        using DbOptions options = CreatingOptions().SetBlockBasedTableFactory(table);
        // Random values do not compress, so each one fills its own data blocks and can be found in the file.
        Random random = new(7);
        byte[][] values = new byte[64][];
        using (RocksDb db = RocksDb.Open(options, path))
        {
            for (byte i = 0; i < values.Length; i++)
            {
                random.NextBytes(values[i] = new byte[8192]);
                db.Put([i], values[i]);
            }

            using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
            db.Flush(flush);
        }

        // Damage only the data of one key in the middle, which opening the database does not read.
        const byte damagedKey = 32;
        string sst = Directory.GetFiles(path, "*.sst").Single();
        byte[] file = File.ReadAllBytes(sst);
        int offset = file.AsSpan().IndexOf(values[damagedKey].AsSpan(0, 64));
        await Assert.That(offset).IsGreaterThan(0);
        for (int i = offset; i < offset + 64; i++) file[i] ^= 0xFF;
        File.WriteAllBytes(sst, file);

        using RocksDb reopened = RocksDb.Open(options, path);
        GetResult damaged = reopened.TryGet([damagedKey], new byte[8], out long damagedLength);
        GetResult intact = reopened.TryGet([1], [], out long intactLength);

        using (Assert.Multiple())
        {
            await Assert.That(damaged).IsEqualTo(GetResult.Failed);
            await Assert.That(damagedLength).IsEqualTo(-1L);
            await Assert.That(intact).IsEqualTo(GetResult.Found);
            await Assert.That(intactLength).IsEqualTo(8192L);
            await Assert.That(() => reopened.Get([damagedKey], new byte[8192].AsSpan())).Throws<RocksDbException>();
        }
    }

    [Test]
    public async Task TryGet_OnADisposedDatabase_Throws()
    {
        TestDatabase database = TestDatabase.Create();
        RocksDb db = database.Db;
        database.Dispose();

        await Assert.That(() => db.TryGet([1], [], out _)).Throws<ObjectDisposedException>();
    }

    private static TestDatabase CreateUncachedFamilyDatabase()
    {
        BlockBasedTableOptions table = new BlockBasedTableOptions().SetNoBlockCache(true);
        using ColumnFamilyOptions defaultOptions = new ColumnFamilyOptions().SetBlockBasedTableFactory(table);
        using ColumnFamilyOptions namedOptions = new ColumnFamilyOptions().SetBlockBasedTableFactory(table);
        ColumnFamilies families = new(defaultOptions);
        families.Add("blocks", namedOptions);
        using DbOptions options = CreatingOptions();
        return TestDatabase.Create(options, families);
    }

    private static IColumnFamilyHandle? GetReadFamily(RocksDb db, int familyKind)
        => familyKind == 0 ? null : familyKind == 1 ? db.GetDefaultColumnFamily() : db.GetColumnFamily("blocks");

    [Test]
    [Arguments(false, false, 0)]
    [Arguments(false, false, 1)]
    [Arguments(false, false, 2)]
    [Arguments(false, false, 3)]
    [Arguments(false, false, 512)]
    [Arguments(false, true, 0)]
    [Arguments(false, true, 1)]
    [Arguments(false, true, 2)]
    [Arguments(false, true, 3)]
    [Arguments(false, true, 512)]
    [Arguments(true, false, 0)]
    [Arguments(true, false, 1)]
    [Arguments(true, false, 2)]
    [Arguments(true, false, 3)]
    [Arguments(true, false, 512)]
    [Arguments(true, true, 0)]
    [Arguments(true, true, 1)]
    [Arguments(true, true, 2)]
    [Arguments(true, true, 3)]
    [Arguments(true, true, 512)]
    public async Task MultiGet_WithDisposedFamily_RejectsBeforeChangingOutputs(bool genericDecoder, bool namedFamily, int count)
    {
        using TestDatabase database = TestDatabase.Create(CreatingOptions(), Families("blocks"));
        IColumnFamilyHandle family = namedFamily ? database.Db.GetColumnFamily("blocks") : database.Db.GetDefaultColumnFamily();
        ((IDisposable)family).Dispose();
        int[] values = new int[count];
        Array.Fill(values, 99);
        byte[] keys = new byte[count];

        await Assert.That(family.Handle).IsEqualTo(nint.Zero);
        await Assert.That(() => ReadFixed(genericDecoder, database.Db, keys, 1, values, new FirstByteDeserializer(), family))
            .Throws<ObjectDisposedException>();

        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo(99);
    }

    [Test]
    [Arguments(false, -1)]
    [Arguments(false, 0)]
    [Arguments(false, 1)]
    [Arguments(true, -1)]
    [Arguments(true, 0)]
    [Arguments(true, 1)]
    public async Task MultiGet_WithOneKey_DecodesMissingEmptyAndPresentValues(bool namedFamily, int valueKind)
    {
        using TestDatabase database = namedFamily ? TestDatabase.Create(CreatingOptions(), Families("blocks")) : TestDatabase.Create();
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        if (valueKind >= 0) database.Db.Put([1, 1], valueKind == 0 ? [] : [10], family);
        int expected = valueKind < 0 ? 0 : valueKind == 0 ? -1 : 10;
        int[] values = [99];

        database.Db.MultiGet([1, 1], 2, values.AsSpan(), new FirstByteValueDeserializer(), family);
        await Assert.That(values[0]).IsEqualTo(expected);

        values[0] = 99;
        database.Db.MultiGet([1, 1], 2, values.AsSpan(), new FirstByteDeserializer(), family);
        await Assert.That(values[0]).IsEqualTo(expected);
    }

    [Test]
    [Arguments(1, false)]
    [Arguments(2, false)]
    [Arguments(32, false)]
    [Arguments(512, false)]
    [Arguments(1, true)]
    [Arguments(2, true)]
    [Arguments(32, true)]
    [Arguments(512, true)]
    public async Task MultiGet_WhenDecoderClosesDatabase_KeepsTheValuesAlive(int count, bool namedFamily)
    {
        using TestDatabase database = namedFamily ? TestDatabase.Create(CreatingOptions(), Families("blocks")) : TestDatabase.Create();
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        database.Db.Put([1, 1], [10], family);
        byte[] keys = new byte[count * 2];
        int[] values = new int[count];
        int[] expected = new int[count];
        Array.Fill(values, 99);
        Array.Fill(expected, 10);
        Array.Fill(keys, (byte)1);

        database.Db.MultiGet(keys, 2, values.AsSpan(), new ClosingDeserializer(database.Db), family);

        await Assert.That(values).IsEquivalentTo(expected, CollectionOrdering.Matching);
        await Assert.That(database.Db.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    [Arguments(1, false, 0)]
    [Arguments(1, false, 1)]
    [Arguments(1, true, 0)]
    [Arguments(1, true, 1)]
    [Arguments(2, false, 0)]
    [Arguments(2, false, 1)]
    [Arguments(2, true, 0)]
    [Arguments(2, true, 1)]
    [Arguments(3, false, 0)]
    [Arguments(3, false, 1)]
    [Arguments(3, true, 0)]
    [Arguments(3, true, 1)]
    [Arguments(512, false, 0)]
    [Arguments(512, false, 1)]
    [Arguments(512, true, 0)]
    [Arguments(512, true, 1)]
    public async Task MultiGet_WithStatefulStructDecoder_PreservesStateAcrossValues(int count, bool namedFamily, int readTier)
    {
        using TestDatabase database = namedFamily ? TestDatabase.Create(CreatingOptions(), Families("blocks")) : TestDatabase.Create();
        IColumnFamilyHandle? family = namedFamily ? database.Db.GetColumnFamily("blocks") : null;
        database.Db.Put([1, 1], [10], family);
        database.Db.Put([1, 2], [20], family);
        using ReadOptions options = new ReadOptions().SetReadTier(readTier);
        byte[] keys = new byte[count * 2];
        int[] values = new int[count];
        for (int i = 0; i < count; i++)
        {
            keys[i * 2] = 1;
            keys[i * 2 + 1] = (byte)(i % 2 + 1);
            values[i] = 99;
        }

        database.Db.MultiGet(keys, 2, values.AsSpan(), new CountingValueDeserializer(), family, options);

        for (int i = 0; i < count; i++) await Assert.That(values[i]).IsEqualTo((i % 2 == 0 ? 10 : 20) + i + 1);
    }

    private readonly struct ForwardingValueDeserializer<T>(ISpanDeserializer<T> deserializer) : ISpanDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> buffer) => deserializer.Deserialize(buffer);
    }

    private struct CountingValueDeserializer : ISpanDeserializer<int>
    {
        private int _calls;
        public int Deserialize(ReadOnlySpan<byte> buffer) => buffer[0] + ++_calls;
    }

    private readonly struct ObservingReplacementDeserializer(byte[][] values, byte[] previous) : ISpanDeserializer<byte[]>
    {
        public byte[] Deserialize(ReadOnlySpan<byte> buffer)
        {
            if (!ReferenceEquals(values[0], previous))
                throw new InvalidOperationException("The previous result must stay in place until decoding completes.");
            return buffer.ToArray();
        }
    }

    private readonly struct BytesDeserializer : ISpanDeserializer<byte[]>
    {
        public byte[] Deserialize(ReadOnlySpan<byte> buffer) => buffer.ToArray();
    }

    private sealed class ClosingFamilyDeserializer(IColumnFamilyHandle family) : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer)
        {
            ((IDisposable)family).Dispose();
            return buffer[0];
        }
    }

    private sealed class ClosingDeserializer(RocksDb db) : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer)
        {
            db.Dispose();
            return buffer[0];
        }
    }

    private readonly struct FirstByteValueDeserializer : ISpanDeserializer<int>
    {
        public int Deserialize(ReadOnlySpan<byte> buffer) => buffer.IsEmpty ? -1 : buffer[0];
    }

    private sealed class ThrowAfterDeserializer(int throwAt) : ISpanDeserializer<int>
    {
        private int _calls;
        public int Deserialize(ReadOnlySpan<byte> buffer) => _calls++ == throwAt
            ? throw new InvalidOperationException("decoder failed")
            : buffer[0];
    }

    [Test]
    public async Task MultiGet_ReturnsOneResultPerKeyInOrder()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a"u8.ToArray(), "1"u8.ToArray());
        database.Db.Put("b"u8.ToArray(), "2"u8.ToArray());

        var results = database.Db.MultiGet([[.. "b"u8], [.. "a"u8]]);

        using (Assert.Multiple())
        {
            await Assert.That(results.Select(result => Encoding.UTF8.GetString(result.Key))).IsEquivalentTo(new[] { "b", "a" }, CollectionOrdering.Matching);
            await Assert.That(results.Select(result => Encoding.UTF8.GetString(result.Value!))).IsEquivalentTo(new[] { "2", "1" }, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task MultiGet_ReportsMissingKeysAsNull()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a"u8.ToArray(), "1"u8.ToArray());

        var results = database.Db.MultiGet([[.. "a"u8], [.. "absent"u8]]);

        using (Assert.Multiple())
        {
            await Assert.That(results[0].Value).IsNotNull();
            await Assert.That(results[1].Value).IsNull();
        }
    }

    [Test]
    public async Task MultiGet_WithNoKeys_ReturnsNothing()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.MultiGet(Array.Empty<byte[]>())).IsEmpty();
    }

    [Test]
    public async Task MultiGet_NullKeyArray_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.MultiGet((byte[][])null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task MultiGet_NullKey_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.MultiGet(new byte[][] { null! })).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task MultiGet_WithFewerColumnFamiliesThanKeys_Throws()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.MultiGet([[.. "a"u8]], []))
            .ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Write_AppliesEveryOperationInTheBatch()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("stale"u8.ToArray(), Value);

        using var batch = new WriteBatch();
        batch.Put("fresh"u8.ToArray(), Value).Delete("stale"u8.ToArray());
        database.Db.Write(batch);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get("fresh"u8.ToArray())).IsEquivalentTo(Value, CollectionOrdering.Matching);
            await Assert.That(database.Db.Get("stale"u8.ToArray())).IsNull();
        }
    }

    [Test]
    public async Task Write_WithSyncWriteOptions_Succeeds()
    {
        using var database = TestDatabase.Create();

        using var batch = new WriteBatch();
        batch.Put(Key, Value);
        database.Db.Write(batch, new WriteOptions().SetSync(true));

        await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task SetOptions_AppliesAKnownOption()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.SetOptions([new("disable_auto_compactions", "true")])).ThrowsNothing();
    }

    [Test]
    public async Task SetOptions_RejectsAnUnknownOption()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.SetOptions([new("not_an_option", "1")])).Throws<RocksDbException>();
    }

    [Test]
    public async Task GetProperty_ReadsAKnownProperty()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(database.Db.GetProperty("rocksdb.num-entries-active-mem-table")).IsEqualTo("1");
    }

    [Test]
    public async Task GetProperty_ReturnsNullForAnUnknownProperty()
    {
        using var database = TestDatabase.Create();

        await Assert.That(database.Db.GetProperty("rocksdb.not-a-property")).IsNull();
    }

    [Test]
    public async Task GetLatestSequenceNumber_AdvancesWithEveryWrite()
    {
        using var database = TestDatabase.Create();
        var before = database.Db.GetLatestSequenceNumber();

        database.Db.Put(Key, Value);

        await Assert.That(database.Db.GetLatestSequenceNumber()).IsGreaterThan(before);
    }

    [Test]
    public async Task Flush_MovesTheMemtableToDisk()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        // Both of these only hold once the flush has actually run to completion.
        using (Assert.Multiple())
        {
            await Assert.That(database.Db.GetProperty("rocksdb.num-entries-active-mem-table")).IsEqualTo("0");
            await Assert.That(database.Db.GetProperty("rocksdb.num-files-at-level0")).IsNotEqualTo("0");
        }
    }

    [Test]
    public async Task GetLiveFileNames_ListsTheFlushedTableFiles()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        await Assert.That(database.Db.GetLiveFileNames()).IsNotEmpty();
    }

    [Test]
    public async Task GetLiveFilesMetadata_DescribesTheKeyRangeOfEachFile()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("aaa"u8.ToArray(), Value);
        database.Db.Put("zzz"u8.ToArray(), Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        var metadata = database.Db.GetLiveFilesMetadata()!.Single();

        using (Assert.Multiple())
        {
            await Assert.That(metadata.FileMetadata.FileName).IsNotEmpty();
            await Assert.That(metadata.FileMetadata.FileSize).IsGreaterThan(0ul);
            await Assert.That(metadata.FileDataMetadata!.SmallestKeyInFile).IsEqualTo("aaa");
            await Assert.That(metadata.FileDataMetadata.LargestKeyInFile).IsEqualTo("zzz");
            await Assert.That(metadata.FileDataMetadata.NumEntriesInFile).IsEqualTo(2ul);
        }
    }

    [Test]
    public async Task GetLiveFilesMetadata_CanSkipTheKeyRange()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        var metadata = database.Db.GetLiveFilesMetadata(populateFileMetadataOnly: true)!.Single();

        using (Assert.Multiple())
        {
            await Assert.That(metadata.FileMetadata.FileName).IsNotEmpty();
            await Assert.That(metadata.FileDataMetadata).IsNull();
        }
    }

    [Test]
    public async Task GetLiveFileNames_OnAnUnflushedDatabase_IsEmpty()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(database.Db.GetLiveFileNames()).IsEmpty();
    }

    [Test]
    public async Task CompactRange_LeavesTheDataReadable()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("a"u8.ToArray(), Value);
        database.Db.Put("z"u8.ToArray(), Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        database.Db.CompactRange("a", "z");

        await Assert.That(database.Db.Get("a"u8.ToArray())).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    [Test]
    public async Task CompactRange_AcceptsAnUnboundedRange()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(() => database.Db.CompactRange((byte[]?)null, null)).ThrowsNothing();
    }

    [Test]
    public async Task CompactRange_CanForceTheBottommostLevel()
    {
        using var database = TestDatabase.Create();
        database.Db.Put("aaa"u8.ToArray(), Value);
        database.Db.Put("zzz"u8.ToArray(), Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));

        database.Db.CompactRange(null, null, forceBottommost: true);
        FileMetadata before = database.Db.GetLiveFilesMetadata(populateFileMetadataOnly: true)!.Single().FileMetadata;

        database.Db.CompactRange(null, null, forceBottommost: true);
        FileMetadata after = database.Db.GetLiveFilesMetadata(populateFileMetadataOnly: true)!.Single().FileMetadata;

        using (Assert.Multiple())
        {
            await Assert.That(after.FileLevel).IsEqualTo(before.FileLevel);
            await Assert.That(after.FileName).IsNotEqualTo(before.FileName);
        }
    }

    [Test]
    public async Task ManualCompactionControls_RejectADisposedDatabase()
    {
        using var database = TestDatabase.Create();
        database.Db.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.DisableManualCompaction).Throws<ObjectDisposedException>();
            await Assert.That(database.Db.EnableManualCompaction).Throws<ObjectDisposedException>();
        }
    }

    [Test]
    public async Task ManualCompaction_CanBeDisabledAndReEnabled()
    {
        using var database = TestDatabase.Create();

        database.Db.DisableManualCompaction();

        await Assert.That(database.Db.EnableManualCompaction).ThrowsNothing();
    }

    [Test]
    public async Task SuggestCompactRange_AcceptsAValidRange()
    {
        using var database = TestDatabase.Create();

        await Assert.That(() => database.Db.SuggestCompactRange(Key, "z"u8)).ThrowsNothing();
    }

    [Test]
    public async Task RangeFileOperations_RejectADisposedDatabase()
    {
        using var database = TestDatabase.Create();
        database.Db.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(() => database.Db.DeleteFilesInRange(Key, "z"u8)).Throws<ObjectDisposedException>();
            await Assert.That(() => database.Db.SuggestCompactRange(Key, "z"u8)).Throws<ObjectDisposedException>();
        }
    }

    [Test]
    public async Task RangeFileOperations_RejectEmptyBounds()
    {
        using var database = TestDatabase.Create();

        using (Assert.Multiple())
        {
            await Assert.That(() => database.Db.DeleteFilesInRange([], Key)).ThrowsExactly<ArgumentException>();
            await Assert.That(() => database.Db.DeleteFilesInRange(Key, [])).ThrowsExactly<ArgumentException>();
            await Assert.That(() => database.Db.SuggestCompactRange([], Key)).ThrowsExactly<ArgumentException>();
            await Assert.That(() => database.Db.SuggestCompactRange(Key, [])).ThrowsExactly<ArgumentException>();
        }
    }

    [Test]
    public async Task DeleteFilesInRange_RemovesSettledFilesInsideTheRange()
    {
        using var database = TestDatabase.Create();
        byte[] firstKey = "bbb"u8.ToArray();
        byte[] secondKey = "ccc"u8.ToArray();
        database.Db.Put(firstKey, Value);
        database.Db.Put(secondKey, Value);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));
        database.Db.CompactRange(null, null, forceBottommost: true);

        await Assert.That(database.Db.GetLiveFileNames()).IsNotEmpty();

        database.Db.DeleteFilesInRange("aaa"u8, "zzz"u8);

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.Get(firstKey)).IsNull();
            await Assert.That(database.Db.Get(secondKey)).IsNull();
            await Assert.That(database.Db.GetLiveFileNames()).IsEmpty();
        }
    }

    [Test]
    public async Task DeleteFilesInRange_OnlyRemovesFilesFromTheSelectedColumnFamily()
    {
        using var options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies();
        using var defaultFamilyOptions = new ColumnFamilyOptions();
        using var selectedFamilyOptions = new ColumnFamilyOptions();
        var families = new ColumnFamilies(defaultFamilyOptions);
        families.Add("selected", selectedFamilyOptions);
        using var database = TestDatabase.Create(options, families);
        IColumnFamilyHandle selected = database.Db.GetColumnFamily("selected");
        database.Db.Put(Key, Value);
        database.Db.Put(Key, Value, selected);
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true));
        database.Db.Flush(new FlushOptions().SetWaitForFlush(true), selected);
        database.Db.CompactRange(null, null, forceBottommost: true);
        database.Db.CompactRange(null, null, forceBottommost: true, selected);
        int filesBefore = database.Db.GetLiveFileNames().Count;

        database.Db.DeleteFilesInRange("aaa"u8, "zzz"u8, selected);
        int filesAfter = database.Db.GetLiveFileNames().Count;

        using (Assert.Multiple())
        {
            await Assert.That(filesBefore).IsEqualTo(2);
            await Assert.That(filesAfter).IsEqualTo(1);
            await Assert.That(database.Db.Get(Key, selected)).IsNull();
            await Assert.That(database.Db.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
        }
    }

    [Test]
    public async Task FileDeletions_CanBeDisabledAndReEnabled()
    {
        using var database = TestDatabase.Create();

        database.Db.DisableFileDeletions();

        await Assert.That(database.Db.EnableFileDeletions).ThrowsNothing();
    }

    [Test]
    public async Task Dispose_ClearsTheHandle()
    {
        using var directory = new TempDirectory();
        var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), directory.Reserve("db"));

        db.Dispose();

        await Assert.That(db.Handle).IsEqualTo(nint.Zero);
    }

    [Test]
    public async Task Dispose_IsIdempotent()
    {
        using var directory = new TempDirectory();
        var db = RocksDb.Open(new DbOptions().SetCreateIfMissing(), directory.Reserve("db"));
        db.Dispose();

        await Assert.That(db.Dispose).ThrowsNothing();
    }

    [Test]
    public async Task Dispose_MakesLaterCallsThrowInsteadOfReachingNativeCode()
    {
        using var directory = new TempDirectory();
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var db = RocksDb.Open(creatingOptions, directory.Reserve("db"));
        db.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(() => db.Get(Key)).Throws<ObjectDisposedException>();
            await Assert.That(() => db.Put(Key, Value)).Throws<ObjectDisposedException>();
            await Assert.That(() => db.FlushWal(sync: false)).Throws<ObjectDisposedException>();
        }
    }

    /// <remarks>
    /// The LOCK file is the observable: RocksDB releases it only on the real native close, so a
    /// reopen of the same path fails while any child still holds a lease.
    /// </remarks>
    [Test]
    public async Task Dispose_DefersTheNativeCloseUntilTheLastIteratorIsDisposed()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var db = RocksDb.Open(creatingOptions, path);
        db.Put(Key, Value);
        using var iterator = db.NewIterator();

        db.Dispose();

        using var reopenOptions = new DbOptions();
        using (Assert.Multiple())
        {
            // The database is still open natively: the iterator reads and the LOCK is held.
            await Assert.That(iterator.SeekToFirst().Valid()).IsTrue();
            await Assert.That(iterator.GetKeySpan().ToArray()).IsEquivalentTo(Key, CollectionOrdering.Matching);
            await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).Throws<RocksDbException>();
            // Logical disposal is immediate even while the child defers the physical close.
            await Assert.That(() => db.Get(Key)).Throws<ObjectDisposedException>();
            await Assert.That(db.Handle).IsEqualTo(nint.Zero);
        }

        iterator.Dispose();

        await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).ThrowsNothing();
    }

    [Test]
    public async Task Dispose_DefersTheNativeCloseUntilTheLastSnapshotIsDisposed()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var db = RocksDb.Open(creatingOptions, path);
        using var snapshot = db.CreateSnapshot();

        db.Dispose();

        using var reopenOptions = new DbOptions();
        await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).Throws<RocksDbException>();

        snapshot.Dispose();

        await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).ThrowsNothing();
    }

    /// <remarks>
    /// An abandoned child must not pin the database forever: its critical finalizer destroys the
    /// native object and releases the database lease, letting the deferred close finally run.
    /// </remarks>
    [Test]
    public async Task AbandonedIterator_IsReclaimedByFinalizationAndReleasesTheDatabase()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");
        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        var db = RocksDb.Open(creatingOptions, path);
        db.Put(Key, Value);

        CreateAbandonedIterator(db);
        db.Dispose();

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        using var reopenOptions = new DbOptions();
        await Assert.That(() => RocksDb.Open(reopenOptions, path).Dispose()).ThrowsNothing();

        // Not inlined so the caller never roots the abandoned iterator.
        [MethodImpl(MethodImplOptions.NoInlining)]
        static void CreateAbandonedIterator(RocksDb db) => db.NewIterator();
    }

    [Test]
    public async Task FlushWal_SucceedsAfterWrites()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        await Assert.That(() => database.Db.FlushWal(sync: true)).ThrowsNothing();
    }

    [Test]
    public async Task TryGetIntProperty_ReadsAnIntegerProperty()
    {
        using var database = TestDatabase.Create();
        database.Db.Put(Key, Value);

        var found = database.Db.TryGetIntProperty("rocksdb.estimate-num-keys", out var keys);

        using (Assert.Multiple())
        {
            await Assert.That(found).IsTrue();
            await Assert.That(keys).IsEqualTo(1ul);
        }
    }

    [Test]
    public async Task TryGetIntProperty_ReturnsFalseForAnUnknownProperty()
    {
        using var database = TestDatabase.Create();

        using (Assert.Multiple())
        {
            await Assert.That(database.Db.TryGetIntProperty("rocksdb.no-such-property", out var value)).IsFalse();
            await Assert.That(value).IsEqualTo(0ul);
        }
    }

    [Test]
    public async Task Repair_LeavesAFlushedDatabaseReopenableWithItsData()
    {
        using var directory = new TempDirectory();
        var path = directory.Reserve("db");

        using var creatingOptions = new DbOptions().SetCreateIfMissing();
        using (var db = RocksDb.Open(creatingOptions, path))
        {
            db.Put(Key, Value);
            using var flushOptions = new FlushOptions().SetWaitForFlush(true);
            db.Flush(flushOptions);
        }

        using var repairOptions = new DbOptions();
        RocksDb.Repair(repairOptions, path);

        using var reopenOptions = new DbOptions();
        using var reopened = RocksDb.Open(reopenOptions, path);
        await Assert.That(reopened.Get(Key)).IsEquivalentTo(Value, CollectionOrdering.Matching);
    }

    /// <summary>Counts its enumerations, like a non-replayable LINQ source would misbehave past one.</summary>
    private sealed class SingleEnumerationOptions : IEnumerable<KeyValuePair<string, string>>
    {
        public int EnumerationCount { get; private set; }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            EnumerationCount++;
            yield return new("write_buffer_size", "1048576");
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <remarks>
    /// The native call indexes the keys and values arrays through one count, so they must come
    /// from a single enumeration; a second pass over an unstable source could desynchronize them.
    /// </remarks>
    [Test]
    public async Task SetOptions_EnumeratesTheOptionsExactlyOnce()
    {
        using var database = TestDatabase.Create();
        var options = new SingleEnumerationOptions();

        database.Db.SetOptions(options);

        await Assert.That(options.EnumerationCount).IsEqualTo(1);
    }
}
