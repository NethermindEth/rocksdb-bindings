# Runs the arms of one case in rotation, with the page cache dropped before every process, and prints medians.
# Usage: python3 run.py <case> <reps> <out.csv> <db> <extra args> <label=dll[:ENV=value]>...
# BENCH_TEMPERATURE=page skips the drop and reads every database file once first, so reads start in the page cache.
import os, statistics, subprocess, sys, collections
case, reps, out, db, extra = sys.argv[1], int(sys.argv[2]), sys.argv[3], sys.argv[4], sys.argv[5].split()
arms = []
for spec in sys.argv[6:]:
    label, rest = spec.split('=', 1)
    dll, _, env = rest.partition(':')
    arms.append((label, dll, dict([env.split('=', 1)]) if env else {}))
page = os.environ.get('BENCH_TEMPERATURE') == 'page'
if page:
    # Once per invocation: the page cache then holds every file for all processes, while each process starts with
    # an empty block cache.
    subprocess.run(['sh', '-c', f'cat {db}/* > /dev/null'], check=True)
with open(out, 'a') as f:
    for rep in range(reps):
        for label, dll, env in (arms if rep % 2 == 0 else arms[::-1]):
            if not page:
                subprocess.run(['sudo', 'sh', '-c', 'sync; echo 3 > /proc/sys/vm/drop_caches'], check=True)
            # The same seed for every arm of a rep: each arm reads the same keys, each from a cold cache.
            args = ['dotnet', dll, case, db, label] + extra + [str(rep + 1)]
            result = subprocess.run(args, check=True, capture_output=True, text=True, env={**os.environ, **env})
            f.write(result.stdout)
            f.flush()
rows = collections.defaultdict(list)
for line in open(out):
    label, kind, arm, _, us, allocated = line.strip().split(',')
    rows[(kind, arm, label)].append((float(us), int(allocated)))
labels = [a[0] for a in arms]
print('| case | read | ' + ' | '.join(f'{l} us' for l in labels) + ' | ' + ' | '.join(f'{l} vs {labels[0]}' for l in labels[1:]) + ' | bytes per batch |')
print('|' + '---|' * (3 + 2 * len(labels) - 1))
for kind, arm in sorted({(k[0], k[1]) for k in rows}):
    present = [l for l in labels if rows.get((kind, arm, l))]
    med = {l: statistics.median(u for u, _ in rows[(kind, arm, l)]) for l in present}
    alloc = ' / '.join(f'{l} {statistics.median(a for _, a in rows[(kind, arm, l)]):.0f}' for l in present)
    cells = ' | '.join(f'{med[l]:.0f}' if l in med else '-' for l in labels)
    base = med.get(labels[0])
    deltas = ' | '.join(f'{(med[l] / base - 1) * 100:+.1f}%' if base and l in med else '-' for l in labels[1:])
    print(f'| {kind} | {arm} | {cells} | {deltas} | {alloc} |')
