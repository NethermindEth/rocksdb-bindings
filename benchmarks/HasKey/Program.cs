extern alias baseline;

using System.Buffers.Binary;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Current = Nethermind.RocksDbBindings;
using Previous = baseline::Nethermind.RocksDbBindings;

ManualConfig config = ManualConfig.Create(DefaultConfig.Instance);
config.BuildTimeout = TimeSpan.FromMinutes(10);
BenchmarkSwitcher.FromAssembly(typeof(HasKeyBenchmarks).Assembly).Run(args, config);

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net10_0, launchCount: 1, warmupCount: 3, iterationCount: 8)]
public class HasKeyBenchmarks
{
    private const int RecordCount = 1024;
    private const int QueryCount = 8192;
    private Current.RocksDb _current = null!;
    private Previous.RocksDb _previous = null!;
    private byte[][] _keys = null!;
    private string _root = null!;

    [Params(0, 128, 4096)]
    public int ValueSize { get; set; }

    [Params(0, 50, 100)]
    public int HitPercent { get; set; }

    [Params(false, true)]
    public bool Flushed { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _root = Path.Combine(Environment.GetEnvironmentVariable("BENCH_DATA")!, Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_root);
        using Current.DbOptions currentOptions = new Current.DbOptions().SetCreateIfMissing().SetDisableAutoCompactions(1);
        using Previous.DbOptions previousOptions = new Previous.DbOptions().SetCreateIfMissing().SetDisableAutoCompactions(1);
        _current = Current.RocksDb.Open(currentOptions, Path.Combine(_root, "current"));
        _previous = Previous.RocksDb.Open(previousOptions, Path.Combine(_root, "previous"));
        byte[] value = new byte[ValueSize];
        Random random = new(1701);
        random.NextBytes(value);
        using (Current.WriteBatch currentBatch = new())
        using (Previous.WriteBatch previousBatch = new())
        {
            for (int i = 0; i < RecordCount; i++)
            {
                byte[] key = Key(i);
                currentBatch.Put(key, value);
                previousBatch.Put(key, value);
            }
            _current.Write(currentBatch);
            _previous.Write(previousBatch);
        }
        if (Flushed)
        {
            using Current.FlushOptions currentFlush = new Current.FlushOptions().SetWaitForFlush(true);
            using Previous.FlushOptions previousFlush = new Previous.FlushOptions().SetWaitForFlush(true);
            _current.Flush(currentFlush);
            _previous.Flush(previousFlush);
        }
        _keys = new byte[QueryCount][];
        for (int i = 0; i < QueryCount; i++)
        {
            bool present = random.Next(100) < HitPercent;
            _keys[i] = Key(random.Next(RecordCount) + (present ? 0 : RecordCount));
            if (_current.HasKey(_keys[i]) != present || _previous.HasKey(_keys[i]) != present)
                throw new InvalidOperationException($"Incorrect presence for query {i}");
        }
        Console.WriteLine($"Verified {QueryCount * 2} presence checks; value={ValueSize}, hits={HitPercent}, flushed={Flushed}");
    }

    [Benchmark(Baseline = true, OperationsPerInvoke = QueryCount)]
    public int PinnedHandle()
    {
        int found = 0;
        foreach (byte[] key in _keys)
            if (_previous.HasKey(key)) found++;
        return found;
    }

    [Benchmark(OperationsPerInvoke = QueryCount)]
    public int StackSlice()
    {
        int found = 0;
        foreach (byte[] key in _keys)
            if (_current.HasKey(key)) found++;
        return found;
    }

    private static byte[] Key(int index)
    {
        byte[] key = new byte[32];
        BinaryPrimitives.WriteInt32BigEndian(key, index);
        return key;
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _current.Dispose();
        _previous.Dispose();
        Directory.Delete(_root, recursive: true);
    }
}
