import json
import math
import statistics
import sys
from pathlib import Path

cases = ('B1_V128_H100_SST_CF_RAND', 'B2_V128_H100_SST_CF_RAND', 'B8_V128_H100_SST_CF_RAND', 'B32_V128_H100_SST_CF_RAND', 'B256_V128_H100_SST_CF_RAND', 'B512_V128_H100_SST_CF_RAND', 'B32_V128_H0_SST_CF_RAND', 'B32_V128_H50_SST_CF_RAND', 'B32_V8_H100_SST_CF_RAND', 'B32_V8192_H100_SST_CF_RAND', 'B32_V128_H100_MEM_CF_RAND', 'B32_V128_H100_SST_DEF_RAND', 'B512_V128_H100_SST_CF_SORT')
comparisons = ("MainControl", "Reference17Control", "StructControl", "ClassControl", "Main", "Reference17", "Struct", "Class")
root = Path(sys.argv[1])
summary = {case: {name: [] for name in comparisons} for case in cases}
intervals = {case: {name: [] for name in comparisons} for case in cases}
for process in (1, 2, 3):
    report = json.loads((root / f"paired-{process}.json").read_text())
    if report["Process"] != process or len(report["Cases"]) != len(cases):
        raise SystemExit(f"Invalid process or case count: {process}")
    if {case["Case"] for case in report["Cases"]} != set(cases):
        raise SystemExit(f"Incomplete cases: {process}")
    for case in report["Cases"]:
        repetitions = case["Repetitions"]
        if not isinstance(repetitions, int) or not 1 <= repetitions <= 32768 or repetitions & (repetitions - 1):
            raise SystemExit("Invalid calibration")
        expected = {(name, index) for name in comparisons for index in range(12)}
        actual = {(entry["Comparison"], entry["Round"]) for entry in case["Rounds"]}
        if len(case["Rounds"]) != len(expected) or actual != expected:
            raise SystemExit(f"Incomplete paired rounds: {process}, {case['Case']}")
        deltas = {name: [] for name in comparisons}
        for entry in case["Rounds"]:
            order = "ABBA" if (entry["Round"] + process) % 2 == 0 else "BAAB"
            if entry["Order"] != order or len(entry["Samples"]) != 4:
                raise SystemExit("Invalid measurement order")
            means = {"A": [], "B": []}
            for arm, sample in zip(order, entry["Samples"]):
                elapsed = sample["Nanoseconds"]
                if sample["Arm"] != arm or not math.isfinite(elapsed) or elapsed <= 0:
                    raise SystemExit("Invalid paired measurement")
                if sample["Operations"] != repetitions * 1024:
                    raise SystemExit("Incorrect operation count")
                if any(not isinstance(sample[key], int) or sample[key] < 0
                       for key in ("AllocatedBytes", "Gen0", "Gen1", "Gen2")):
                    raise SystemExit("Invalid allocation or collection count")
                means[arm].append(elapsed / sample["Operations"])
            delta = (statistics.mean(means["B"]) / statistics.mean(means["A"]) - 1) * 100
            deltas[entry["Comparison"]].append(delta)
        for name in comparisons:
            summary[case["Case"]][name].append(statistics.median(deltas[name]))
            ordered = sorted(deltas[name])
            intervals[case["Case"]][name].append([ordered[2], ordered[9]])

assessments = {}
regressions = []
for case in cases:
    assessments[case] = {}
    for name in ("Main", "Reference17", "Struct", "Class"):
        controls = ({"Main": "MainControl", "Reference17": "Reference17Control", "Struct": "StructControl", "Class": "ClassControl"}[name],)
        process_verdicts = []
        for process in range(3):
            control_low = min(0, *(intervals[case][control][process][0] for control in controls))
            control_high = max(0, *(intervals[case][control][process][1] for control in controls))
            low, high = intervals[case][name][process]
            verdict = "lower" if high < control_low else "higher" if low > control_high else "inconclusive"
            process_verdicts.append(verdict)
            if verdict == "higher":
                regressions.append(f"{case}, {name}, process {process + 1}: {low:+.2f}% to {high:+.2f}% above control {control_high:+.2f}%")
        assessments[case][name] = process_verdicts

lines = ["# Same-database paired measurements", "",
         "Time change B / A; negative is better. Each value is the median of 12 ABBA/BAAB rounds in one process.", "",
         "The three processes run on the same pinned CPU. All methods in a case share one database, cache, keys, and output buffer.", "",
         "A/A controls measure variation. Small A/B differences require controls smaller than the claimed effect.", "",
         "Classification compares the third-to-tenth ordered round deltas out of twelve with the same band from A/A controls and with zero. These bands describe observed variation, not a guarantee for other workloads or machines."]
for name in comparisons:
    lines += ["", f"## {name}", "", "| Case | Process 1 | Process 2 | Process 3 |", "|---|---:|---:|---:|"]
    for case in cases:
        lines.append("| " + case + " | " + " | ".join(f"{value:+.2f}%" for value in summary[case][name]) + " |")
    if not name.endswith("Control"):
        lines += ["", "| Case | Control comparison: processes 1 / 2 / 3 |", "|---|---|"]
        for case in cases:
            lines.append("| " + case + " | " + " / ".join(assessments[case][name]) + " |")
(root / "paired-summary.json").write_text(json.dumps(summary, indent=2) + "\n")
(root / "paired-controls.json").write_text(json.dumps({"intervals": intervals, "assessments": assessments, "regressions": regressions}, indent=2) + "\n")
print("Verified three processes, thirteen cases, eight comparisons, twelve balanced rounds and four arms per round")
for regression in regressions:
    print("Higher time beyond controls: " + regression)
(root / "paired-github.md").write_text("\n".join(lines) + "\n")
