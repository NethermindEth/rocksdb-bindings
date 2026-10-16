import json
import math
import sys
from pathlib import Path

if sys.argv[1] == "sources":
    candidate, reference, native = map(Path, sys.argv[2:])
    for path in ("src/Nethermind.RocksDbBindings/RocksDb.cs",
                 "src/Nethermind.RocksDbBindings/RocksDb.MultiGet.cs",
                 "src/Nethermind.RocksDbBindings/RocksDbNative.g.cs",
                 "src/Nethermind.RocksDbBindings/RocksDbNative.Async.g.cs",
                 "native/CMakeLists.txt", "native/rocksdb_async.inc", "native/rocksdb_async.h",
                 ".github/workflows/build-rocksdb.yml", "src/Directory.Build.props"):
        if (candidate / path).read_bytes() != (reference / path).read_bytes():
            raise SystemExit(f"Existing production source differs: {path}")
    import subprocess
    original = subprocess.check_output(["git", "-C", str(native), "show", "HEAD:db/c.cc"])
    expected = original + b'\n#include "rocksdb_async.inc"\n\n#include "rocksdb_batch.inc"\n'
    if (native / "db/c.cc").read_bytes() != expected:
        raise SystemExit("Upstream C API changed outside additive extension includes")
    for name in ("rocksdb_async.inc", "rocksdb_async.h", "rocksdb_batch.inc", "rocksdb_batch.h"):
        if (candidate / "native" / name).read_bytes() != (native / "db" / name).read_bytes():
            raise SystemExit(f"Built extension differs: {name}")
    print("Verified unchanged #24 implementation, native build settings and additive upstream C API extension")
    raise SystemExit(0)

reports = list((Path(sys.argv[1]) / "results").glob("*-report-full-compressed.json"))
if len(reports) != 1:
    raise SystemExit(f"Expected one report, found {len(reports)}")
entries = json.loads(reports[0].read_text())["Benchmarks"]
cases = ('B1_V128_H100_SST_CF_RAND', 'B2_V128_H100_SST_CF_RAND', 'B8_V128_H100_SST_CF_RAND', 'B32_V128_H100_SST_CF_RAND', 'B256_V128_H100_SST_CF_RAND', 'B512_V128_H100_SST_CF_RAND', 'B32_V128_H0_SST_CF_RAND', 'B32_V128_H50_SST_CF_RAND', 'B32_V8_H100_SST_CF_RAND', 'B32_V8192_H100_SST_CF_RAND', 'B32_V128_H100_MEM_CF_RAND', 'B32_V128_H100_SST_DEF_RAND', 'B512_V128_H100_SST_CF_SORT')
methods = ("Reference24", "ReferenceControl", "ReferenceClass", "Candidate", "CandidateClass", "ReferencePacked", "CandidatePacked")
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
