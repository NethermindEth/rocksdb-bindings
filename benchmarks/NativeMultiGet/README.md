# Native MultiGet experiment

This branch compares a batch-owned native result with PR #24 at `f189a514f67d83066126ba57f97fa73b520eff9c`.
It does not publish a package. Existing managed read implementations and native build settings remain unchanged.

The new context owns a fixed-size array of pinned values. Descriptors borrow their data until context destruction.
`size == SIZE_MAX` identifies missing values; size zero identifies present empty values.
A small inline dispatcher selects the path. A single key uses the existing pinned-read helper without entering the large batch method.
Two keys use the existing MultiGet implementation.
A successful native read with no owner means all keys are missing. Descriptors are not read in that case.
The native owner is allocated only when at least one value is found, including an empty value.
Native errors are checked before decoding. Database and option leases outlive context destruction.

The workflow builds Linux x64 and ARM64 binaries with the production dependencies and CPU baseline.
The measurement workflow can reuse verified native build artifacts. It checks the build steps, source revision, native files and build commands before reuse.
Native source changes on this branch trigger the full build. Both workflows also support `workflow_dispatch`.
Artifact reuse requires an unchanged native ABI and source; the measurement workflow must be dispatched with a matching native build run.
Both compared methods use the same rebuilt binary and database. This does not measure the rebuilt binary against an older shipped binary.
Contract tests run before the measurements, including decoder failure and database disposal during batch decoding.

The matrix has 13 cases and 7 BenchmarkDotNet methods, with two launches and six measured iterations per launch.
The paired diagnostic uses three processes, one pinned CPU, twelve balanced rounds and separate class/struct A/A controls.
Negative time change means less time. Control-band classifications distinguish lower, higher and inconclusive results.
The exploratory workflow validates complete reports but does not fail when it measures a regression; all regressions remain in the report.

`NativeAllocatedBytes` records calling-thread jemalloc allocated bytes outside the timed interval.
It excludes libc allocations and allocations on other threads. It does not measure live or peak memory.
The reference releases pinned values during decoding; the candidate retains them until batch destruction.
Warm numeric decoder results do not establish the effect on slow decoders or cache pressure.

## Measured results

[Run 38877281792](https://github.com/NethermindEth/rocksdb-bindings/actions/runs/38877281792) measured managed revision `6e626f99968d1725b5159ae1825a15b38c2e50bb`.
It reused verified native revision `6a3d9439bd616f437a15de84d158fe25aaf4e452` from [build 38874335797](https://github.com/NethermindEth/rocksdb-bindings/actions/runs/38874335797).
The reference was PR #24 at `f189a514f67d83066126ba57f97fa73b520eff9c`.
The x64 runner reported AMD EPYC 9V74; the ARM64 runner reported Neoverse-N2.

Both architectures passed 461 tests with no failures or skipped tests, all 91 BenchmarkDotNet results with two launches, and the complete paired matrix.
The table shows the median of the three process medians for elapsed time versus the reference.
Negative means less time. `*` means the result overlapped the A/A control band in at least one process; it is not a proved gain or regression.
No comparison was classified as higher beyond its A/A controls in any process.

| Case | x64 struct | ARM64 struct | x64 class | ARM64 class |
|---|---:|---:|---:|---:|
| 1 key | -0.46%* | -0.67% | -0.54% | -1.72% |
| 2 keys | +0.13%* | +0.02%* | +0.06%* | -0.11%* |
| 8 keys | -2.24% | -2.33% | -1.51% | -1.88% |
| 32 keys | -2.91% | -2.74% | -2.67% | -2.43% |
| 256 keys | -4.15% | -4.22% | -4.10% | -4.17% |
| 512 keys | -4.66% | -5.37% | -4.35% | -5.14% |
| 32 keys, all missing | -1.16% | -0.65% | -1.58% | -0.32% |
| 32 keys, half found | -1.77% | -1.98% | -1.89% | -1.54% |
| 32 keys, 8-byte values | -2.70% | -2.73% | -2.63% | -2.14% |
| 32 keys, 8192-byte values | -2.85% | -2.82% | -2.78% | -2.08% |
| 32 keys, memtable | -3.91% | -4.37% | -4.35% | -4.67% |
| 32 keys, default CF | -3.07% | -2.72% | -2.82% | -2.60% |
| 512 keys, sorted | -4.03% | -5.86% | -3.99% | -5.39% |

For 32 keys with all values found, calling-thread jemalloc allocated bytes fell from 971.06 to 856.06 per key (-11.84%) on both architectures.
For 32 missing keys, they fell from 870 to 850 per key (-2.30%).
No paired sample recorded managed allocations or GC collections.

These are warm component lookups, not RPC or block-processing measurements.
The runner CPU differed from earlier x64 runs. Only the same-process reference comparisons above support these claims.
