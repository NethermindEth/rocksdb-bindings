# RocksDB bindings

[![Tests](https://github.com/nethermindeth/rocksdb-bindings/actions/workflows/test-publish.yml/badge.svg)](https://github.com/nethermindeth/rocksdb-bindings/actions/workflows/test-publish.yml)
[![Nethermind.RocksDbBindings](https://img.shields.io/nuget/v/Nethermind.RocksDbBindings)](https://www.nuget.org/packages/Nethermind.RocksDbBindings)

C# bindings for [RocksDB](https://github.com/facebook/rocksdb).

## CPU requirements

Linux and Windows x64 binaries require x86-64-v2 (SSE4.2 and POPCNT), the .NET 11
baseline. macOS x64 binaries require AVX2, available on all Intel Macs supported by
macOS 15.

## Async reads

`GetAsync` and `MultiGetAsync` follow the results and error handling of `Get` and
`MultiGet`. Keys are copied during submission. Read options must not be mutated
until completion; disposing the database, read options or snapshot defers their
native release until outstanding reads finish.

```csharp
byte[]? value = await db.GetAsync(key);
KeyValuePair<byte[], byte[]?>[] values = await db.MultiGetAsync(keys);
```

`GetAsync<T>` accepts the existing `ISpanDeserializer<T>` and deserializes directly
from native memory without copying the value into a managed byte array. For struct
deserializers, use `GetAsync<T, TDeserializer>` to avoid boxing:

```csharp
int value = await db.GetAsync<int, Int32Deserializer>(key, default);

readonly struct Int32Deserializer : ISpanDeserializer<int>
{
    public int Deserialize(ReadOnlySpan<byte> buffer)
        => System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(buffer);
}
```

Missing keys return `default(T)` without invoking the deserializer. Deserialization
runs on a thread-pool thread; shared deserializers must support concurrent calls.
The span is valid only during `Deserialize`. Single reads use pooled completions:
consume each returned `ValueTask` once, or call `AsTask()` once to share the result.

The API is the same on every platform. Linux builds enable RocksDB's native
coroutine reads using Folly and io_uring. Windows, macOS and filesystems without
a read executor use RocksDB's synchronous fallback, which can block during
submission. `DbOptions.SetReadIoExecutorThreads` configures the shared executor
on supported builds. This API does not cancel submitted reads.

## Reusing iterator bounds

`ReadOptions.UpdateIterateBounds` copies new lower and upper bounds into the
buffers reserved by `SetIterateBounds`. Each new bound must fit its original
buffer. Larger bounds and missing buffers are rejected before either bound
changes. Updating does not allocate or replace the buffers.

Bounds reuse supports non-tailing iterators only. Updates are rejected while
`SetTailing(true)` is set. Once these options have created a tailing iterator,
use fresh non-tailing options for bounds reuse. Setting tailing to false later
does not change an existing iterator's mode.

```csharp
using ReadOptions options = new ReadOptions().SetIterateBounds(firstLower, firstUpper);
using Iterator iterator = db.NewIterator(readOptions: options);

foreach ((byte[] lower, byte[] upper) in ranges)
{
    options.UpdateIterateBounds(lower, upper);
    iterator.Seek(lower);
    while (iterator.Valid())
    {
        Consume(iterator.GetKeySpan(), iterator.GetValueSpan());
        iterator.Next();
    }
    iterator.ThrowIfError();
}
```

All iterators created from these options observe the new bounds. Stop using
them before updating, then seek each iterator again before reading it. Do not
update bounds concurrently with iteration or outstanding asynchronous reads.
The ordinary `SetIterate*` methods replace buffers and must not be called while
an iterator uses them.

Updating bounds does not refresh the iterator's snapshot. Keep reuse local to
a request or other bounded read scope, then dispose the iterator. A long-lived
iterator can retain memtables and SST files after compaction.

## License

This project is licensed under the [MIT](https://github.com/nethermindeth/rocksdb-bindings/blob/main/LICENSE) license.

Parts of the managed API were originally derived from [rocksdb-sharp](https://github.com/curiosity-ai/rocksdb-sharp), used under [BSD-2-Clause](https://github.com/curiosity-ai/rocksdb-sharp/blob/master/LICENSE), and have since been substantially rewritten.

The package also ships prebuilt RocksDB binaries, used under the [Apache-2.0](https://github.com/facebook/rocksdb/blob/main/LICENSE.Apache) option of RocksDB's dual Apache-2.0/[GPL-2.0](https://github.com/facebook/rocksdb/blob/main/COPYING) license. Those binaries statically link LZ4, Snappy, Zstandard, and, on Linux, jemalloc, Folly and its dependencies.
