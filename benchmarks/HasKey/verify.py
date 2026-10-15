import json
import math
import sys
from pathlib import Path

reports = list((Path(sys.argv[1]) / "results").glob("*-report-full-compressed.json"))
if len(reports) != 1:
    raise SystemExit(f"Expected one report, found {len(reports)}")
entries = json.loads(reports[0].read_text())["Benchmarks"]
expected = {
    (method, f"ValueSize={size}&HitPercent={hits}&Flushed={flushed}")
    for method in ("PinnedHandle", "StackSlice")
    for size in (0, 128, 4096)
    for hits in (0, 50, 100)
    for flushed in ("False", "True")
}
actual = {(entry["Method"], entry["Parameters"]) for entry in entries}
if len(entries) != len(expected) or actual != expected:
    raise SystemExit(f"Incomplete measurements: missing={expected - actual}, extra={actual - expected}")
for entry in entries:
    stats = entry.get("Statistics")
    if not stats or stats["N"] < 6 or not math.isfinite(stats["Mean"]) or stats["Mean"] <= 0:
        raise SystemExit(f"Invalid measurements for {entry['Method']}, {entry['Parameters']}")
print(f"Verified {len(entries)} complete benchmark results")
