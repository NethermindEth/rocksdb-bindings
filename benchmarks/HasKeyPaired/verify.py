import json
import math
import statistics
import sys
from pathlib import Path

root = Path(sys.argv[1])
expected = {(size, hits, flushed) for size in (0, 128, 4096) for hits in (0, 50, 100) for flushed in (False, True)}
grouped = {key: {"control": [], "candidate": []} for key in expected}
for repeat in (1, 2, 3):
    cases = json.loads((root / f"paired-{repeat}.json").read_text())
    keys = {(case["ValueSize"], case["HitPercent"], case["Flushed"]) for case in cases}
    if len(cases) != len(expected) or keys != expected:
        raise SystemExit(f"Incomplete cases in repeat {repeat}")
    for case in cases:
        key = (case["ValueSize"], case["HitPercent"], case["Flushed"])
        for source, target in (("Control", "control"), ("Candidate", "candidate")):
            rounds = case[source]
            if len(rounds) != 12:
                raise SystemExit(f"Incomplete rounds for {key}")
            deltas = []
            for row in rounds:
                for window in row.values():
                    ns = window["Nanoseconds"]
                    if not math.isfinite(ns) or ns <= 0:
                        raise SystemExit(f"Invalid measurement for {key}")
                a = row["A1"]["Nanoseconds"] + row["A2"]["Nanoseconds"]
                b = row["B1"]["Nanoseconds"] + row["B2"]["Nanoseconds"]
                deltas.append((b / a - 1) * 100)
            grouped[key][target].append(statistics.median(deltas))

summary = []
for (size, hits, flushed), values in sorted(grouped.items()):
    summary.append({"ValueSize": size, "HitPercent": hits, "Flushed": flushed,
                    "ControlDeltaPercent": values["control"],
                    "CandidateDeltaPercent": values["candidate"],
                    "MedianDeltaPercent": statistics.median(values["candidate"])})
(root / "summary.json").write_text(json.dumps(summary, indent=2) + "\n")
print("| Value bytes | Hits % | Flushed | A/A deltas % | Candidate deltas % |")
print("| ---: | ---: | --- | --- | --- |")
for row in summary:
    control = ", ".join(f"{x:+.2f}" for x in row["ControlDeltaPercent"])
    candidate = ", ".join(f"{x:+.2f}" for x in row["CandidateDeltaPercent"])
    print(f"| {row['ValueSize']} | {row['HitPercent']} | {row['Flushed']} | {control} | {candidate} |")
