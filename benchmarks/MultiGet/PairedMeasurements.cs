using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.Json;

internal static class PairedMeasurements
{
    private const int Queries = 1024;
    private const int Rounds = 12;

    public static void Run(string output, int process)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(process, 1);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(process, 3);
        List<CaseResult> cases = [];
        foreach (ReadCase readCase in new MultiGetBenchmarks().Cases)
        {
            MultiGetBenchmarks fixture = new() { Case = readCase };
            fixture.Setup();
            try
            {
                cases.Add(MeasureCase(fixture, process));
            }
            finally
            {
                fixture.Cleanup();
            }
        }
        File.WriteAllText(output, JsonSerializer.Serialize(new PairedResult(process, cases)));
    }

    private static CaseResult MeasureCase(MultiGetBenchmarks fixture, int process)
    {
        Comparison[] comparisons =
        [
            new("LegacyControl", fixture.LegacyMultiGet, fixture.LegacyControl),
            new("ReferenceControl", fixture.Reference17, fixture.Reference17),
            new("Reference17", fixture.Reference17, fixture.Proposed),
            new("Reference17Class", fixture.Reference17, fixture.ProposedClass),
            new("Reference17Interface", fixture.Reference17, fixture.Interface),
            new("Main", fixture.LegacyMultiGet, fixture.Proposed),
        ];
        long expected = fixture.Reference17();
        foreach (Comparison comparison in comparisons)
        {
            for (int i = 0; i < 24; i++)
            {
                if (comparison.A() != expected || comparison.B() != expected)
                    throw new InvalidOperationException("Incorrect paired warmup checksum");
            }
        }

        int repetitions = 1;
        while (Measure(fixture.Reference17, expected, repetitions, "A").Nanoseconds < 25_000_000)
        {
            repetitions = checked(repetitions * 2);
            if (repetitions > 32768) throw new InvalidOperationException("Paired calibration did not converge");
        }

        List<PairedRound> rounds = [];
        for (int round = 0; round < Rounds; round++)
        {
            string order = ((round + process) & 1) == 0 ? "ABBA" : "BAAB";
            for (int offset = 0; offset < comparisons.Length; offset++)
            {
                Comparison comparison = comparisons[(offset + round + process) % comparisons.Length];
                Sample[] samples = new Sample[4];
                for (int arm = 0; arm < samples.Length; arm++)
                {
                    bool a = order[arm] == 'A';
                    samples[arm] = Measure(a ? comparison.A : comparison.B, expected, repetitions, a ? "A" : "B");
                }
                rounds.Add(new PairedRound(comparison.Name, round, order, samples));
            }
        }
        Console.WriteLine($"Paired {fixture.Case}: {Rounds} rounds, {repetitions} repetitions per arm");
        return new CaseResult(fixture.Case.ToString(), repetitions, rounds);
    }

    private static Sample Measure(Func<long> read, long expected, int repetitions, string arm)
    {
        int gen0 = GC.CollectionCount(0);
        int gen1 = GC.CollectionCount(1);
        int gen2 = GC.CollectionCount(2);
        long allocated = GC.GetAllocatedBytesForCurrentThread();
        long started = Stopwatch.GetTimestamp();
        long checksum = ReadRepeated(read, repetitions);
        long elapsed = Stopwatch.GetTimestamp() - started;
        long bytes = GC.GetAllocatedBytesForCurrentThread() - allocated;
        if (checksum != checked(expected * repetitions))
            throw new InvalidOperationException("Incorrect paired measurement checksum");
        return new Sample(arm, elapsed * (1_000_000_000d / Stopwatch.Frequency),
            checked(repetitions * Queries), bytes, GC.CollectionCount(0) - gen0,
            GC.CollectionCount(1) - gen1, GC.CollectionCount(2) - gen2);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static long ReadRepeated(Func<long> read, int repetitions)
    {
        long checksum = 0;
        for (int i = 0; i < repetitions; i++) checksum += read();
        return checksum;
    }

    private sealed record Comparison(string Name, Func<long> A, Func<long> B);
    private sealed record Sample(string Arm, double Nanoseconds, int Operations, long AllocatedBytes, int Gen0, int Gen1, int Gen2);
    private sealed record PairedRound(string Comparison, int Round, string Order, Sample[] Samples);
    private sealed record CaseResult(string Case, int Repetitions, List<PairedRound> Rounds);
    private sealed record PairedResult(int Process, List<CaseResult> Cases);
}
