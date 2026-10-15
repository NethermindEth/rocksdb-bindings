using System.Buffers;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Nethermind.RocksDbBindings;
using Perfolizer.Horology;
using Perfolizer.Mathematics.OutlierDetection;

ManualConfig config = ManualConfig.Create(DefaultConfig.Instance);
config.BuildTimeout = TimeSpan.FromMinutes(10);
config.AddJob(Job.Default.WithRuntime(CoreRuntime.Core10_0).WithLaunchCount(2)
    .WithWarmupCount(3).WithIterationCount(6).WithIterationTime(TimeInterval.FromMilliseconds(150))
    .WithOutlierMode(OutlierMode.DontRemove)
    .WithEnvironmentVariable("DOTNET_TieredCompilation", "0"));
BenchmarkSwitcher.FromAssembly(typeof(MultiGetBenchmarks).Assembly).Run(args, config);

[MemoryDiagnoser]
public class MultiGetBenchmarks
{
    private const int Records = 4096;
    private const int Queries = 1024;
    private const int KeyLength = 32;
    private RocksDb _db = null!;
    private ReadOptions _readOptions = null!;
    private IColumnFamilyHandle? _family;
    private IColumnFamilyHandle[]? _families;
    private byte[][][] _jagged = null!;
    private byte[][] _contiguous = null!;
    private long[][] _expected = null!;
    private long[] _output = null!;
    private string _root = null!;
    private readonly NumberDeserializer _decoder = new();
    private readonly NumberValueDeserializer _valueDecoder = new();

    public IEnumerable<ReadCase> Cases =>
    [
        new(1, 128, 100, true, true, false),
        new(32, 128, 100, true, true, false),
        new(256, 128, 100, true, true, false),
        new(512, 128, 100, true, true, false),
        new(32, 128, 50, true, true, false),
        new(32, 8192, 100, true, true, false),
        new(32, 128, 100, false, true, false),
        new(32, 128, 100, true, false, false),
        new(512, 128, 100, true, true, true),
    ];

    [ParamsSource(nameof(Cases))]
    public ReadCase Case { get; set; } = null!;

    [GlobalSetup]
    public void Setup()
    {
        _root = Path.Combine(Environment.GetEnvironmentVariable("BENCH_DATA")!, Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
        using Cache cache = Cache.CreateLru(128UL << 20);
        BlockBasedTableOptions table = new BlockBasedTableOptions().SetBlockCache(cache);
        using DbOptions options = new DbOptions().SetCreateIfMissing().SetCreateMissingColumnFamilies()
            .SetDisableAutoCompactions(1).SetBlockBasedTableFactory(table);
        using ColumnFamilyOptions defaultOptions = new ColumnFamilyOptions().SetBlockBasedTableFactory(table).SetDisableAutoCompactions(1);
        using ColumnFamilyOptions familyOptions = new ColumnFamilyOptions().SetBlockBasedTableFactory(table).SetDisableAutoCompactions(1);
        ColumnFamilies families = new(defaultOptions);
        if (Case.Named) families.Add("bench", familyOptions);
        _db = RocksDb.Open(options, Path.Combine(_root, "data"), families);
        _family = Case.Named ? _db.GetColumnFamily("bench") : null;
        _readOptions = new ReadOptions().SetFillCache(true);
        if (_family is not null)
        {
            _families = new IColumnFamilyHandle[Case.Batch];
            Array.Fill(_families, _family);
        }
        using (WriteBatch batch = new())
        {
            for (int i = 0; i < Records; i++) batch.Put(Key(i), Value(i), _family);
            _db.Write(batch);
        }
        if (Case.Flushed)
        {
            using FlushOptions flush = new FlushOptions().SetWaitForFlush(true);
            if (_family is null) _db.Flush(flush);
            else _db.Flush(flush, _family);
        }

        int batches = Queries / Case.Batch;
        _jagged = new byte[batches][][];
        _contiguous = new byte[batches][];
        _expected = new long[batches][];
        _output = new long[Case.Batch];
        Random random = new(1701);
        for (int batch = 0; batch < batches; batch++)
        {
            int[] ids = new int[Case.Batch];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = ((batch * Case.Batch + i) % 97 == 0 ? 0 : random.Next(Records))
                    + (Case.Hits == 100 || ((batch * Case.Batch + i) & 1) == 0 ? 0 : Records);
            if (Case.Sorted) Array.Sort(ids);
            _jagged[batch] = new byte[Case.Batch][];
            _contiguous[batch] = new byte[Case.Batch * KeyLength];
            _expected[batch] = new long[Case.Batch];
            for (int i = 0; i < ids.Length; i++)
            {
                _jagged[batch][i] = Key(ids[i]);
                _jagged[batch][i].CopyTo(_contiguous[batch], i * KeyLength);
                _expected[batch][i] = ids[i] >= Records ? 0 : ids[i] == 0 ? 1 : ids[i] + 2;
            }
        }
        Verify();
    }

    private void Verify()
    {
        CopyDeserializer copy = new();
        for (int batch = 0; batch < _jagged.Length; batch++)
        {
            KeyValuePair<byte[], byte[]?>[] legacy = _db.MultiGet(_jagged[batch], _families, _readOptions);
            byte[][] reference = new byte[Case.Batch][];
            byte[][] proposed = new byte[Case.Batch][];
            _db.MultiGet(_contiguous[batch], KeyLength, reference.AsSpan(), copy, _family, _readOptions);
            _db.MultiGetDecoded(_contiguous[batch], KeyLength, proposed.AsSpan(), copy, _family, _readOptions, Case.Sorted);
            for (int i = 0; i < Case.Batch; i++)
            {
                int id = BinaryPrimitives.ReadInt32BigEndian(_jagged[batch][i]);
                byte[]? expected = id < Records ? Value(id) : null;
                RequireBytes(legacy[i].Value, expected);
                RequireBytes(reference[i], expected);
                RequireBytes(proposed[i], expected);
            }
            _db.MultiGet(_contiguous[batch], KeyLength, _output.AsSpan(), _decoder, _family, _readOptions);
            RequireNumbers(batch);
            _db.MultiGetDecoded(_contiguous[batch], KeyLength, _output.AsSpan(), _valueDecoder, _family, _readOptions, Case.Sorted);
            RequireNumbers(batch);
        }
        long expectedSum = _expected.Sum(batch => batch.Sum());
        if (LegacyMultiGet() != expectedSum || LegacyControl() != expectedSum || Reference17() != expectedSum
            || Proposed() != expectedSum || Reference17FromJagged() != expectedSum || ProposedFromJagged() != expectedSum
            || Scalar() != expectedSum)
            throw new InvalidOperationException("Incorrect benchmark checksum");
        Console.WriteLine($"Verified {Queries} full values and decoded results for {Case}");
    }

    private static void RequireBytes(byte[]? actual, byte[]? expected)
    {
        if ((actual is null) != (expected is null) || (actual is not null && !actual.AsSpan().SequenceEqual(expected)))
            throw new InvalidOperationException("Incorrect complete value");
    }

    private void RequireNumbers(int batch)
    {
        if (!_output.AsSpan().SequenceEqual(_expected[batch])) throw new InvalidOperationException("Incorrect decoded result");
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = Queries)]
    public long LegacyMultiGet() => ReadLegacy();

    [Benchmark(OperationsPerInvoke = Queries)]
    public long LegacyControl() => ReadLegacy();

    private long ReadLegacy()
    {
        long total = 0;
        for (int batch = 0; batch < _jagged.Length; batch++)
        {
            KeyValuePair<byte[], byte[]?>[] values = _db.MultiGet(_jagged[batch], _families, _readOptions);
            for (int i = 0; i < values.Length; i++)
            {
                _output[i] = values[i].Value is byte[] value ? _decoder.Deserialize(value) : 0;
                total += _output[i];
            }
        }
        return total;
    }

    [Benchmark(OperationsPerInvoke = Queries)]
    public long Reference17()
    {
        long total = 0;
        for (int batch = 0; batch < _contiguous.Length; batch++)
        {
            _db.MultiGet(_contiguous[batch], KeyLength, _output.AsSpan(), _decoder, _family, _readOptions);
            total += SumOutput();
        }
        return total;
    }

    [Benchmark(OperationsPerInvoke = Queries)]
    public long Proposed()
    {
        long total = 0;
        for (int batch = 0; batch < _contiguous.Length; batch++)
        {
            _db.MultiGetDecoded(_contiguous[batch], KeyLength, _output.AsSpan(), _valueDecoder, _family, _readOptions, Case.Sorted);
            total += SumOutput();
        }
        return total;
    }

    [Benchmark(OperationsPerInvoke = Queries)]
    public long Reference17FromJagged() => ReadPacked(false);

    [Benchmark(OperationsPerInvoke = Queries)]
    public long ProposedFromJagged() => ReadPacked(true);

    private long ReadPacked(bool proposed)
    {
        long total = 0;
        for (int batch = 0; batch < _jagged.Length; batch++)
        {
            PackAndRead(_jagged[batch], proposed);
            total += SumOutput();
        }
        return total;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void PackAndRead(byte[][] keys, bool proposed)
    {
        byte[]? rented = null;
        Span<byte> packed = keys.Length <= 256
            ? stackalloc byte[keys.Length * KeyLength]
            : (rented = ArrayPool<byte>.Shared.Rent(keys.Length * KeyLength)).AsSpan(0, keys.Length * KeyLength);
        try
        {
            for (int i = 0; i < keys.Length; i++) keys[i].CopyTo(packed.Slice(i * KeyLength, KeyLength));
            if (proposed) _db.MultiGetDecoded(packed, KeyLength, _output.AsSpan(), _valueDecoder, _family, _readOptions, Case.Sorted);
            else _db.MultiGet(packed, KeyLength, _output.AsSpan(), _decoder, _family, _readOptions);
        }
        finally
        {
            if (rented is not null) ArrayPool<byte>.Shared.Return(rented);
        }
    }

    [Benchmark(OperationsPerInvoke = Queries)]
    public long Scalar()
    {
        long total = 0;
        for (int batch = 0; batch < _jagged.Length; batch++)
        {
            for (int i = 0; i < Case.Batch; i++)
            {
                _output[i] = _db.Get(_jagged[batch][i], _decoder, _family, _readOptions);
                total += _output[i];
            }
        }
        return total;
    }

    private long SumOutput()
    {
        long total = 0;
        foreach (long value in _output) total += value;
        return total;
    }

    private static byte[] Key(int id)
    {
        byte[] key = new byte[KeyLength];
        BinaryPrimitives.WriteInt32BigEndian(key, id);
        return key;
    }

    private byte[] Value(int id)
    {
        if (id == 0) return [];
        byte[] value = new byte[Case.ValueSize];
        for (int i = 0; i < value.Length; i++) value[i] = (byte)((id + i) % 251);
        BinaryPrimitives.WriteInt64LittleEndian(value, id + 2);
        return value;
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _readOptions.Dispose();
        _db.Dispose();
        Directory.Delete(_root, recursive: true);
    }

    private sealed class NumberDeserializer : ISpanDeserializer<long>
    {
        public long Deserialize(ReadOnlySpan<byte> buffer) => buffer.IsEmpty ? 1 : BinaryPrimitives.ReadInt64LittleEndian(buffer);
    }

    private readonly struct NumberValueDeserializer : ISpanDeserializer<long>
    {
        public long Deserialize(ReadOnlySpan<byte> buffer) => buffer.IsEmpty ? 1 : BinaryPrimitives.ReadInt64LittleEndian(buffer);
    }

    private sealed class CopyDeserializer : ISpanDeserializer<byte[]>
    {
        public byte[] Deserialize(ReadOnlySpan<byte> buffer) => buffer.ToArray();
    }
}

public sealed record ReadCase(int Batch, int ValueSize, int Hits, bool Flushed, bool Named, bool Sorted)
{
    public override string ToString() => $"B{Batch}_V{ValueSize}_H{Hits}_{(Flushed ? "SST" : "MEM")}_{(Named ? "CF" : "DEF")}_{(Sorted ? "SORT" : "RAND")}";
}
