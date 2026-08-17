// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using Nethermind.RocksDbBindings.Native;

namespace Nethermind.RocksDbBindings.Tests;

/// <remarks>
/// Pins the layout and by-value ABI of <c>rocksdb_slice_t</c>, the C API's only non-opaque struct.
/// Timestamp slices return the same struct the same way, and calling one without timestamp support
/// aborts an assertion-enabled RocksDB, so key and value cover the ABI on their own.
/// </remarks>
public class NativeSliceTests
{
    private static unsafe (int Size, nint DataOffset, nint SizeOffset) Layout()
    {
        rocksdb_slice_t slice;
        var address = (nint)(&slice);

        return (sizeof(rocksdb_slice_t), (nint)(&slice.data) - address, (nint)(&slice.size) - address);
    }

    private static unsafe byte[] KeySlice(Iterator iterator)
    {
        var slice = RocksDbNative.rocksdb_iter_key_slice((rocksdb_iterator_t*)iterator.Handle);

        return new ReadOnlySpan<byte>(slice.data, checked((int)slice.size)).ToArray();
    }

    private static unsafe byte[] ValueSlice(Iterator iterator)
    {
        var slice = RocksDbNative.rocksdb_iter_value_slice((rocksdb_iterator_t*)iterator.Handle);

        return new ReadOnlySpan<byte>(slice.data, checked((int)slice.size)).ToArray();
    }

    /// <summary>A database holding a single entry whose key and value differ in length.</summary>
    /// <remarks>
    /// The lengths differ so that a slice read through the wrong offset, or one that picked up the
    /// other field, cannot still produce the expected bytes.
    /// </remarks>
    private static TestDatabase SingleEntry()
    {
        var database = TestDatabase.Create();

        database.Db.Put("alpha", "ALPHA-VALUE");

        return database;
    }

    /// <remarks>
    /// The deterministic half of the guard. The behavioural tests below read a slice the native
    /// side filled in, but they run on one architecture at a time; this states the shape the
    /// generator has to keep producing.
    /// </remarks>
    [Test]
    public async Task Slice_IsTwoPointerSizedFieldsInDeclarationOrder()
    {
        var (size, dataOffset, sizeOffset) = Layout();

        using var _ = Assert.Multiple();

        await Assert.That(size).IsEqualTo(nint.Size * 2);
        await Assert.That(dataOffset).IsEqualTo((nint)0);
        await Assert.That(sizeOffset).IsEqualTo((nint)nint.Size);
    }

    [Test]
    public async Task KeySlice_ReturnedByValue_CarriesTheCurrentKey()
    {
        using var database = SingleEntry();
        using var iterator = database.Db.NewIterator();

        iterator.SeekToFirst();

        await Assert.That(iterator.Valid()).IsTrue();
        await Assert.That(KeySlice(iterator)).IsEquivalentTo("alpha"u8.ToArray(), CollectionOrdering.Matching);
    }

    [Test]
    public async Task ValueSlice_ReturnedByValue_CarriesTheCurrentValue()
    {
        using var database = SingleEntry();
        using var iterator = database.Db.NewIterator();

        iterator.SeekToFirst();

        await Assert.That(iterator.Valid()).IsTrue();
        await Assert.That(ValueSlice(iterator)).IsEquivalentTo("ALPHA-VALUE"u8.ToArray(), CollectionOrdering.Matching);
    }
}
