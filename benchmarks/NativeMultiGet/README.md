# Native MultiGet experiment

This branch compares a batch-owned native result with PR #24 at `f189a514f67d83066126ba57f97fa73b520eff9c`.
It does not publish a package. Existing managed read implementations and native build settings remain unchanged.

The new context owns a fixed-size array of pinned values. Descriptors borrow their data until context destruction.
`size == SIZE_MAX` identifies missing values; size zero identifies present empty values.
Native errors are checked before decoding. Database and option leases outlive context destruction.

The workflow builds Linux x64 and ARM64 binaries with the production dependencies and CPU baseline.
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
