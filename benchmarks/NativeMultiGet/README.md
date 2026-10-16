# Decoded MultiGet benchmarks

The benchmark compares the current public API with the unchanged allocating API from main, the fixed-key interface API from #17, and the previous managed implementation from #24.
The workflow pins and checks each reference source. It also checks the native build source and compares the packaged Linux binary byte for byte with its verified build artifact.

Frozen reference partials compile only when `IncludeMultiGetBenchmarkReferences=true`.
The benchmark project builds them with the current source into `Nethermind.RocksDbBindings.BenchmarkReferences`; normal builds and packages exclude them.
Both compared paths share one database, cache, native binary, key set and output buffer in each paired case.
Main returns newly allocated value arrays; the decoded APIs use caller-owned output spans. Packing measurements include jagged-to-contiguous conversion.

The matrix has 13 cases and 9 BenchmarkDotNet methods, two launches and six measured iterations per launch.
The paired diagnostic uses three fresh processes on one pinned CPU, twelve balanced ABBA/BAAB rounds and eight comparisons with matched A/A controls.
Negative time change means less time. Inconclusive results overlap the control bands in at least one process.
The workflow checks complete reports and retains higher-time findings in the artifacts.

`NativeAllocatedBytes` records calling-thread jemalloc allocation traffic outside the timed interval.
It excludes libc and other threads and does not measure retained or peak memory.
The native batch owner retains pinned values until decoding completes; older reads release each pin during decoding.
Warm numeric-decoder results do not establish behavior with slow decoders or cache pressure.

The final combined measurements are recorded in PR #24 after its packaged native binaries pass all six platform tests.
