// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Buffers.Binary;

using ZstdSharp;

namespace Nethermind.RocksDbBindings.Tests;

/// <summary>
/// The inspector parses rocksdb's write-ahead log format by hand, so these tests feed it
/// hand-built logs rather than logs produced by rocksdb: that is the only way to cover the
/// record types, fragment boundaries and malformed inputs deterministically.
/// </summary>
public class RocksDbWalInspectorTests
{
    private const int BlockSize = 32768;

    private const byte ZeroType = 0;
    private const byte FullType = 1;
    private const byte FirstType = 2;
    private const byte MiddleType = 3;
    private const byte LastType = 4;
    private const byte RecyclableFullType = 5;
    private const byte SetCompressionType = 9;
    private const byte UserDefinedTimestampSizeType = 10;

    /// <summary>
    /// A log record: a 4 byte checksum the reader does not verify, a little endian payload
    /// length, the record type, and — for the recyclable types only — the log number.
    /// </summary>
    private static byte[] Record(byte type, ReadOnlySpan<byte> payload, uint? logNumber = null)
    {
        var headerSize = logNumber is null ? 7 : 11;
        var record = new byte[headerSize + payload.Length];

        BinaryPrimitives.WriteUInt16LittleEndian(record.AsSpan(4, 2), (ushort)payload.Length);
        record[6] = type;

        if (logNumber is not null)
            BinaryPrimitives.WriteUInt32LittleEndian(record.AsSpan(7, 4), logNumber.Value);

        payload.CopyTo(record.AsSpan(headerSize));
        return record;
    }

    /// <summary>The 12 byte write batch header the sequence number is read from, plus content.</summary>
    private static byte[] WriteBatch(ulong sequenceNumber, int entryCount = 1)
    {
        var batch = new byte[12];
        BinaryPrimitives.WriteUInt64LittleEndian(batch.AsSpan(0, 8), sequenceNumber);
        BinaryPrimitives.WriteInt32LittleEndian(batch.AsSpan(8, 4), entryCount);
        return batch;
    }

    private static string WriteLog(TempDirectory directory, string fileName, params byte[][] parts)
    {
        var path = Path.Combine(directory.Path, fileName);
        File.WriteAllBytes(path, parts.SelectMany(part => part).ToArray());
        return path;
    }

    private static ulong FirstSequenceNumber(TempDirectory directory, params byte[][] parts)
    {
        WriteLog(directory, "000001.log", parts);
        return RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path)["000001.log"];
    }

    [Test]
    public async Task GetFirstSequenceNumbers_NullFolder_Throws()
        => await Assert.That(() => RocksDbWalInspector.GetFirstSequenceNumbers(null!))
            .ThrowsExactly<ArgumentNullException>();

    [Test]
    public async Task GetFirstSequenceNumbers_MissingFolder_Throws()
    {
        using var directory = new TempDirectory();
        var missing = directory.Reserve("absent");

        await Assert.That(() => RocksDbWalInspector.GetFirstSequenceNumbers(missing))
            .ThrowsExactly<DirectoryNotFoundException>();
    }

    [Test]
    public async Task GetFirstSequenceNumbers_EmptyFolder_ReturnsNothing()
    {
        using var directory = new TempDirectory();

        await Assert.That(RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path)).IsEmpty();
    }

    [Test]
    public async Task GetFirstSequenceNumbers_IgnoresFilesThatAreNotLogs()
    {
        using var directory = new TempDirectory();
        WriteLog(directory, "000001.log", Record(FullType, WriteBatch(5)));
        File.WriteAllText(Path.Combine(directory.Path, "CURRENT"), "MANIFEST-000001");
        File.WriteAllText(Path.Combine(directory.Path, "000002.sst"), "not a log");

        var sequenceNumbers = RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path);

        await Assert.That(sequenceNumbers.Keys).IsEquivalentTo(new[] { "000001.log" }, CollectionOrdering.Matching);
    }

    [Test]
    public async Task GetFirstSequenceNumbers_ReadsEveryLogSeparately()
    {
        using var directory = new TempDirectory();
        WriteLog(directory, "000001.log", Record(FullType, WriteBatch(11)));
        WriteLog(directory, "000002.log", Record(FullType, WriteBatch(22)));

        var sequenceNumbers = RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path);

        using (Assert.Multiple())
        {
            await Assert.That(sequenceNumbers["000001.log"]).IsEqualTo(11ul);
            await Assert.That(sequenceNumbers["000002.log"]).IsEqualTo(22ul);
        }
    }

    [Test]
    public async Task GetFirstSequenceNumbers_LooksUpFileNamesCaseInsensitively()
    {
        using var directory = new TempDirectory();
        WriteLog(directory, "000001.log", Record(FullType, WriteBatch(7)));

        var sequenceNumbers = RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path);

        await Assert.That(sequenceNumbers["000001.LOG"]).IsEqualTo(7ul);
    }

    [Test]
    public async Task EmptyLog_HasNoSequenceNumber()
    {
        using var directory = new TempDirectory();

        await Assert.That(FirstSequenceNumber(directory)).IsEqualTo(0ul);
    }

    [Test]
    public async Task FullRecord_YieldsItsSequenceNumber()
    {
        using var directory = new TempDirectory();

        await Assert.That(FirstSequenceNumber(directory, Record(FullType, WriteBatch(1234)))).IsEqualTo(1234ul);
    }

    [Test]
    public async Task FirstMatchWins_LaterRecordsAreNotRead()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(FullType, WriteBatch(100)),
            Record(FullType, WriteBatch(200)));

        await Assert.That(sequenceNumber).IsEqualTo(100ul);
    }

    [Test]
    public async Task RecordShorterThanAWriteBatchHeader_IsSkipped()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(FullType, new byte[8]),
            Record(FullType, WriteBatch(77)));

        await Assert.That(sequenceNumber).IsEqualTo(77ul);
    }

    [Test]
    public async Task RecordWithAZeroSequenceNumber_IsSkipped()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(FullType, WriteBatch(0)),
            Record(FullType, WriteBatch(9)));

        await Assert.That(sequenceNumber).IsEqualTo(9ul);
    }

    [Test]
    public async Task MetadataRecords_AreSkipped()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(UserDefinedTimestampSizeType, [1, 2, 3, 4]),
            Record(FullType, WriteBatch(64)));

        await Assert.That(sequenceNumber).IsEqualTo(64ul);
    }

    [Test]
    public async Task ZeroLengthPadding_IsSkipped()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(ZeroType, []),
            Record(ZeroType, []),
            Record(FullType, WriteBatch(31)));

        await Assert.That(sequenceNumber).IsEqualTo(31ul);
    }

    [Test]
    public async Task ZeroTypeCarryingData_StopsTheScan()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(ZeroType, [1]),
            Record(FullType, WriteBatch(31)));

        await Assert.That(sequenceNumber).IsEqualTo(0ul);
    }

    [Test]
    public async Task UnknownRecordType_StopsTheScan()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(200, [1]),
            Record(FullType, WriteBatch(31)));

        await Assert.That(sequenceNumber).IsEqualTo(0ul);
    }

    [Test]
    public async Task RecordRunningPastTheEndOfTheData_StopsTheScan()
    {
        using var directory = new TempDirectory();
        var truncated = Record(FullType, WriteBatch(55))[..10];

        await Assert.That(FirstSequenceNumber(directory, truncated)).IsEqualTo(0ul);
    }

    [Test]
    public async Task FragmentedRecord_IsReassembledBeforeTheHeaderIsRead()
    {
        using var directory = new TempDirectory();
        var batch = WriteBatch(4242);

        // No fragment on its own holds the whole 12 byte write batch header, so a reader that
        // did not stitch them back together could not recover the sequence number.
        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(FirstType, batch.AsSpan(0, 5)),
            Record(MiddleType, batch.AsSpan(5, 4)),
            Record(LastType, batch.AsSpan(9)));

        await Assert.That(sequenceNumber).IsEqualTo(4242ul);
    }

    [Test]
    public async Task FragmentsWithoutTheirFirstRecord_AreDiscarded()
    {
        using var directory = new TempDirectory();
        var batch = WriteBatch(4242);

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(MiddleType, batch.AsSpan(0, 6)),
            Record(LastType, batch.AsSpan(6)),
            Record(FullType, WriteBatch(8)));

        await Assert.That(sequenceNumber).IsEqualTo(8ul);
    }

    [Test]
    public async Task ASecondFirstRecord_RestartsTheReassembly()
    {
        using var directory = new TempDirectory();
        var abandoned = WriteBatch(1111);
        var batch = WriteBatch(2222);

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(FirstType, abandoned.AsSpan(0, 5)),
            Record(FirstType, batch.AsSpan(0, 5)),
            Record(LastType, batch.AsSpan(5)));

        await Assert.That(sequenceNumber).IsEqualTo(2222ul);
    }

    [Test]
    public async Task RecyclableRecord_IsReadPastItsLongerHeader()
    {
        using var directory = new TempDirectory();

        // A reader that assumed the 7 byte header here would decode the sequence number from
        // four bytes of log number plus the first four bytes of the batch.
        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(RecyclableFullType, WriteBatch(0xAABBCCDD), logNumber: 0xFFFFFFFF));

        await Assert.That(sequenceNumber).IsEqualTo(0xAABBCCDDul);
    }

    [Test]
    public async Task RecordsAreReadAcrossBlockBoundaries()
    {
        using var directory = new TempDirectory();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            new byte[BlockSize],
            Record(FullType, WriteBatch(999)));

        await Assert.That(sequenceNumber).IsEqualTo(999ul);
    }

    [Test]
    public async Task SetCompressionRecord_ShorterThanItsCompressionType_Throws()
    {
        using var directory = new TempDirectory();
        WriteLog(directory, "000001.log", Record(SetCompressionType, [1, 2]));

        await Assert.That(() => RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path))
            .ThrowsExactly<InvalidDataException>();
    }

    [Test]
    public async Task CompressionOtherThanZstd_IsRejected()
    {
        using var directory = new TempDirectory();
        // 1 is snappy.
        WriteLog(directory, "000001.log", Record(SetCompressionType, [1, 0, 0, 0]));

        await Assert.That(() => RocksDbWalInspector.GetFirstSequenceNumbers(directory.Path))
            .ThrowsExactly<NotSupportedException>();
    }

    [Test]
    public async Task ZstdCompressedRecord_IsDecompressed()
    {
        using var directory = new TempDirectory();
        using var compressor = new Compressor();
        var compressed = compressor.Wrap(WriteBatch(31337)).ToArray();

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(SetCompressionType, [7, 0, 0, 0]),
            Record(FullType, compressed));

        await Assert.That(sequenceNumber).IsEqualTo(31337ul);
    }

    [Test]
    public async Task ZstdCompressedFragments_AreDecompressedThenReassembled()
    {
        using var directory = new TempDirectory();
        using var compressor = new Compressor();
        var batch = WriteBatch(2026);

        var sequenceNumber = FirstSequenceNumber(
            directory,
            Record(SetCompressionType, [7, 0, 0, 0]),
            Record(FirstType, compressor.Wrap(batch.AsSpan(0, 5)).ToArray()),
            Record(LastType, compressor.Wrap(batch.AsSpan(5)).ToArray()));

        await Assert.That(sequenceNumber).IsEqualTo(2026ul);
    }
}
