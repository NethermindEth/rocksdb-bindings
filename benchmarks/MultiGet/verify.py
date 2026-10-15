import json
import math
import sys
from pathlib import Path

if sys.argv[1] == "sources":
    candidate, baseline, reference = map(Path, sys.argv[2:])
    legacy = "src/Nethermind.RocksDbBindings/RocksDb.cs"
    if (candidate / legacy).read_bytes() != (baseline / legacy).read_bytes():
        raise SystemExit("Legacy binding source changed")
    original = (reference / legacy).read_text()
    start = original.index("    public void MultiGet<T>(")
    end = original.index("    public KeyValuePair<byte[], byte[]?>[] MultiGet(", start)
    imported = (candidate / "src/Nethermind.RocksDbBindings/RocksDb.MultiGet.cs").read_text()
    if original[start:end].strip() not in imported:
        raise SystemExit("Imported PR17 method differs")
    for native in (baseline / "src/Nethermind.RocksDbBindings/runtimes").rglob("*rocksdb*"):
        if native.is_file() and native.read_bytes() != (candidate / native.relative_to(baseline)).read_bytes():
            raise SystemExit(f"Native library changed: {native}")
    print("Verified unchanged legacy source/native libraries and exact PR17 method")
    raise SystemExit(0)

reports = list((Path(sys.argv[1]) / "results").glob("*-report-full-compressed.json"))
if len(reports) != 1:
    raise SystemExit(f"Expected one report, found {len(reports)}")
entries = json.loads(reports[0].read_text())["Benchmarks"]
cases = (
    "B1_V128_H100_SST_CF_RAND", "B32_V128_H100_SST_CF_RAND",
    "B256_V128_H100_SST_CF_RAND", "B512_V128_H100_SST_CF_RAND",
    "B32_V128_H50_SST_CF_RAND", "B32_V8192_H100_SST_CF_RAND",
    "B32_V128_H100_MEM_CF_RAND", "B32_V128_H100_SST_DEF_RAND",
    "B512_V128_H100_SST_CF_SORT",
)
methods = ("LegacyMultiGet", "LegacyControl", "Reference17", "Proposed", "Reference17FromJagged", "ProposedFromJagged", "Scalar")
expected = {(method, f"MultiGetBenchmarks.{method}(Case: {case})") for method in methods for case in cases}
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
