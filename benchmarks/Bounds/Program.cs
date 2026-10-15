extern alias baseline;

using System.Buffers.Binary;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Current = Nethermind.RocksDbBindings;
using Previous = baseline::Nethermind.RocksDbBindings;

BenchmarkSwitcher.FromAssembly(typeof(BoundsBenchmarks).Assembly).Run(args);

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0, launchCount: 1, warmupCount: 3, iterationCount: 8)]
public class BoundsBenchmarks
{
    private const int GroupCount = 25000;
    private const int Versions = 8;
    private const int QueryCount = 8192;
    private Current.RocksDb _current = null!;
    private Previous.RocksDb _previous = null!;
    private string _root = null!;
    private byte[][] _seek = null!;
    private byte[][] _upper = null!;
    private byte[][] _values = null!;
    private bool[] _present = null!;

    [Params("active", "deleted95", "compacted95")]
    public string State { get; set; } = "active";

    [Params(24, 256)]
    public int ScopeSize { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _root = Path.Combine(Environment.GetEnvironmentVariable("BENCH_DATA") ?? Path.GetTempPath(), "bounds-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
        _seek = new byte[QueryCount][];
        _upper = new byte[QueryCount][];
        _values = new byte[QueryCount][];
        _present = new bool[QueryCount];
        Random random = new(1701);
        for (int i = 0; i < QueryCount; i++)
        {
            int group = random.Next(GroupCount);
            _seek[i] = Key(group, 4);
            _upper[i] = new byte[41];
            Key(group, ulong.MaxValue).CopyTo(_upper[i], 0);
            _upper[i][40] = byte.MaxValue;
            _values[i] = Value(group, 4);
            _present[i] = State == "active" || group % 20 == 0;
        }
        OpenCurrent();
        OpenPrevious();
        ValidateCurrent();
        ValidatePrevious();
        Console.WriteLine($"Verified {QueryCount * 2} exact results; state={State}, scope={ScopeSize}, native=11.8.1");
    }

    private void OpenCurrent()
    {
        using Current.DbOptions options = new Current.DbOptions().SetCreateIfMissing().SetDisableAutoCompactions(1);
        _current = Current.RocksDb.Open(options, Path.Combine(_root, "Current"));
        using (Current.WriteBatch batch = new())
        {
            for (int group = 0; group < GroupCount; group++)
                for (ulong version = 0; version < Versions; version++)
                    batch.Put(Key(group, version), Value(group, version));
            _current.Write(batch);
        }
        using Current.FlushOptions flush = new Current.FlushOptions().SetWaitForFlush(true);
        _current.Flush(flush);
        if (State != "active")
        {
            using Current.WriteBatch batch = new();
            for (int group = 0; group < GroupCount; group++)
                if (group % 20 != 0)
                    for (ulong version = 0; version < Versions; version++)
                        batch.Delete(Key(group, version));
            _current.Write(batch);
            _current.Flush(flush);
        }
        if (State == "compacted95") _current.CompactRange(null, null);
    }

    private void OpenPrevious()
    {
        using Previous.DbOptions options = new Previous.DbOptions().SetCreateIfMissing().SetDisableAutoCompactions(1);
        _previous = Previous.RocksDb.Open(options, Path.Combine(_root, "Previous"));
        using (Previous.WriteBatch batch = new())
        {
            for (int group = 0; group < GroupCount; group++)
                for (ulong version = 0; version < Versions; version++)
                    batch.Put(Key(group, version), Value(group, version));
            _previous.Write(batch);
        }
        using Previous.FlushOptions flush = new Previous.FlushOptions().SetWaitForFlush(true);
        _previous.Flush(flush);
        if (State != "active")
        {
            using Previous.WriteBatch batch = new();
            for (int group = 0; group < GroupCount; group++)
                if (group % 20 != 0)
                    for (ulong version = 0; version < Versions; version++)
                        batch.Delete(Key(group, version));
            _previous.Write(batch);
            _previous.Flush(flush);
        }
        if (State == "compacted95") _previous.CompactRange(null, null);
    }

    [Benchmark(OperationsPerInvoke = QueryCount)]
    public long ReuseBoundedIterator()
    {
        long checksum = 0;
        for (int start = 0; start < QueryCount; start += ScopeSize)
        {
            int end = Math.Min(start + ScopeSize, QueryCount);
            using Current.ReadOptions options = new Current.ReadOptions().SetIterateBounds(_seek[start], _upper[start]);
            using Current.Iterator iterator = _current.NewIterator(readOptions: options);
            for (int i = start; i < end; i++)
            {
                options.UpdateIterateBounds(_seek[i], _upper[i]);
                iterator.Seek(_seek[i]);
                if (iterator.Valid()) checksum += iterator.GetValueSpan()[3];
                iterator.ThrowIfError();
            }
        }
        return checksum;
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = QueryCount)]
    public long FreshBoundedIterator()
    {
        long checksum = 0;
        for (int start = 0; start < QueryCount; start += ScopeSize)
        {
            int end = Math.Min(start + ScopeSize, QueryCount);
            for (int i = start; i < end; i++)
            {
                using Previous.ReadOptions options = new Previous.ReadOptions().SetIterateBounds(_seek[i], _upper[i]);
                using Previous.Iterator iterator = _previous.NewIterator(readOptions: options);
                iterator.Seek(_seek[i]);
                if (iterator.Valid()) checksum += iterator.GetValueSpan()[3];
                iterator.ThrowIfError();
            }
        }
        return checksum;
    }

    private void ValidateCurrent()
    {
        for (int start = 0; start < QueryCount; start += ScopeSize)
        {
            int end = Math.Min(start + ScopeSize, QueryCount);
            using Current.ReadOptions options = new Current.ReadOptions().SetIterateBounds(_seek[start], _upper[start]);
            using Current.Iterator iterator = _current.NewIterator(readOptions: options);
            for (int i = start; i < end; i++)
            {
                options.UpdateIterateBounds(_seek[i], _upper[i]);
                iterator.Seek(_seek[i]);
                if (iterator.Valid() != _present[i]) throw new InvalidOperationException($"Current presence mismatch at {i}");
                if (iterator.Valid() && (!iterator.GetKeySpan().SequenceEqual(_seek[i]) || !iterator.GetValueSpan().SequenceEqual(_values[i])))
                    throw new InvalidOperationException($"Current key/value mismatch at {i}");
                iterator.ThrowIfError();
            }
        }
    }

    private void ValidatePrevious()
    {
        for (int start = 0; start < QueryCount; start += ScopeSize)
        {
            int end = Math.Min(start + ScopeSize, QueryCount);
            using Previous.ReadOptions options = new Previous.ReadOptions().SetIterateBounds(_seek[start], _upper[start]);
            
            for (int i = start; i < end; i++)
            {
                using Previous.ReadOptions perLookup = new Previous.ReadOptions().SetIterateBounds(_seek[i], _upper[i]);
                using Previous.Iterator iterator = _previous.NewIterator(readOptions: perLookup);
                iterator.Seek(_seek[i]);
                if (iterator.Valid() != _present[i]) throw new InvalidOperationException($"Previous presence mismatch at {i}");
                if (iterator.Valid() && (!iterator.GetKeySpan().SequenceEqual(_seek[i]) || !iterator.GetValueSpan().SequenceEqual(_values[i])))
                    throw new InvalidOperationException($"Previous key/value mismatch at {i}");
                iterator.ThrowIfError();
            }
        }
    }


    private static byte[] Key(int group, ulong version)
    {
        byte[] key = new byte[40];
        BinaryPrimitives.WriteInt32BigEndian(key, group);
        BinaryPrimitives.WriteUInt64BigEndian(key.AsSpan(32), version);
        return key;
    }

    private static byte[] Value(int group, ulong version)
    {
        byte[] value = new byte[64];
        BinaryPrimitives.WriteInt32BigEndian(value, group);
        BinaryPrimitives.WriteUInt64BigEndian(value.AsSpan(32), version);
        return value;
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _current.Dispose();
        _previous.Dispose();
        Directory.Delete(_root, recursive: true);
    }
}
