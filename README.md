# RocksDB bindings

[![Tests](https://github.com/nethermindeth/rocksdb-bindings/actions/workflows/test-publish.yml/badge.svg)](https://github.com/nethermindeth/rocksdb-bindings/actions/workflows/test-publish.yml)
[![Nethermind.RocksDbBindings](https://img.shields.io/nuget/v/Nethermind.RocksDbBindings)](https://www.nuget.org/packages/Nethermind.RocksDbBindings)

C# bindings for [RocksDB](https://github.com/facebook/rocksdb).

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

## License

This project is licensed under the [MIT](https://github.com/nethermindeth/rocksdb-bindings/blob/main/LICENSE) license.

Parts of the managed API were originally derived from [rocksdb-sharp](https://github.com/curiosity-ai/rocksdb-sharp), used under [BSD-2-Clause](https://github.com/curiosity-ai/rocksdb-sharp/blob/master/LICENSE), and have since been substantially rewritten.

The package also ships prebuilt RocksDB binaries, used under the [Apache-2.0](https://github.com/facebook/rocksdb/blob/main/LICENSE.Apache) option of RocksDB's dual Apache-2.0/[GPL-2.0](https://github.com/facebook/rocksdb/blob/main/COPYING) license. Those binaries statically link LZ4, Snappy, Zstandard, and, on Linux, jemalloc, Folly and its dependencies.
