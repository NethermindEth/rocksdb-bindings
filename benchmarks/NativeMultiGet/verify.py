import json
import math
import sys
from pathlib import Path

if sys.argv[1] == "sources":
    candidate, baseline, reference, previous = map(Path, sys.argv[2:])
    legacy = "src/Nethermind.RocksDbBindings/RocksDb.cs"
    current = (candidate / legacy).read_text()
    wrapper_start = current.index('    /// <summary>Reads fixed-length contiguous keys with an interface decoder.</summary>')
    wrapper_end = current.index('    public KeyValuePair<byte[], byte[]?>[] MultiGet(', wrapper_start)
    if current[:wrapper_start] + current[wrapper_end:] != (baseline / legacy).read_text():
        raise SystemExit("Legacy source changed outside the new forwarding overload")
    original = (reference / legacy).read_text()
    start = original.index("    public void MultiGet<T>(")
    end = original.index("    public KeyValuePair<byte[], byte[]?>[] MultiGet(", start)
    imported = (candidate / "benchmarks/NativeMultiGet/References/Reference17.cs").read_text()
    if original[start:end].strip().replace("MultiGet<T>(", "MultiGetReference17<T>(").replace("StackMultiGetCapacity", "Reference17Capacity") not in imported:
        raise SystemExit("Imported #17 method differs")
    frozen = (previous / "src/Nethermind.RocksDbBindings/RocksDb.MultiGet.cs").read_text()
    for old, new in (("MultiGet<T, TDeserializer>", "MultiGetReference24<T, TDeserializer>"),
                     ("StackReadCapacity", "Reference24Capacity"),
                     ("ReadSingleDecodedValue", "ReadReference24Single"),
                     ("ReadDecodedValue", "ReadReference24Scalar")):
        frozen = frozen.replace(old, new)
    if frozen != (candidate / "benchmarks/NativeMultiGet/References/Reference24.cs").read_text():
        raise SystemExit("Frozen previous implementation differs")
    print("Verified unchanged main allocating API and frozen #17/previous implementations")
    raise SystemExit(0)

reports = list((Path(sys.argv[1]) / "results").glob("*-report-full-compressed.json"))
if len(reports) != 1:
    raise SystemExit(f"Expected one report, found {len(reports)}")
entries = json.loads(reports[0].read_text())["Benchmarks"]
cases = ('B1_V128_H100_SST_CF_RAND', 'B2_V128_H100_SST_CF_RAND', 'B8_V128_H100_SST_CF_RAND', 'B32_V128_H100_SST_CF_RAND', 'B256_V128_H100_SST_CF_RAND', 'B512_V128_H100_SST_CF_RAND', 'B32_V128_H0_SST_CF_RAND', 'B32_V128_H50_SST_CF_RAND', 'B32_V8_H100_SST_CF_RAND', 'B32_V8192_H100_SST_CF_RAND', 'B32_V128_H100_MEM_CF_RAND', 'B32_V128_H100_SST_DEF_RAND', 'B512_V128_H100_SST_CF_SORT')
methods = ("Main", "Reference17", "Reference24", "ReferenceControl", "ReferenceClass", "Candidate", "CandidateClass", "ReferencePacked", "CandidatePacked")
expected = {(method, f"NativeMultiGetBenchmarks.{method}(Case: {case})") for method in methods for case in cases}
actual = {(entry["Method"], entry["FullName"]) for entry in entries}
if len(entries) != len(expected) or actual != expected:
    raise SystemExit(f"Incomplete results: missing={expected - actual}, extra={actual - expected}")
for entry in entries:
    stats = entry.get("Statistics")
    if not stats or stats["N"] < 12 or not math.isfinite(stats["Mean"]) or stats["Mean"] <= 0:
        raise SystemExit(f"Invalid measurements for {entry['Method']}, {entry['Parameters']}")
    actual_measurements = [measurement for measurement in entry["Measurements"]
                           if measurement["IterationMode"] == "Workload" and measurement["IterationStage"] == "Actual"]
    launches = {measurement["LaunchIndex"] for measurement in actual_measurements}
    if not {1, 2}.issubset(launches) or any(sum(m["LaunchIndex"] == launch for m in actual_measurements) < 6 for launch in (1, 2)):
        raise SystemExit(f"Missing independent launches for {entry['Method']}, {entry['Parameters']}")
print(f"Verified all {len(expected)} benchmark results with two launches")
