# Same-database presence diagnostic

Both probes use the same open database, read options and key sequence. The original
probe reproduces the previous HasKey body through an UnsafeAccessor to the unchanged
private GetPinned method. It keeps the database/options leases and native destruction.

Each timed window contains 65,536 lookups after warmup. Twelve rounds alternate
ABBA and BAAB. An A/A comparison runs the original probe on both sides. JSON keeps
all window timings, managed allocations, gen0 counts and GC pause durations.
CI disables tiered compilation and repeats the full run in three fresh processes.

This diagnostic uses Stopwatch for paired windows. It does not replace the original
BenchmarkDotNet results or establish a client/RPC speedup. Check the A/A variation
and all three repeat deltas before interpreting a candidate delta.
