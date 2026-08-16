# RocksDB bindings

[![Tests](https://github.com/nethermindeth/rocksdb-bindings/actions/workflows/test-publish.yml/badge.svg)](https://github.com/nethermindeth/rocksdb-bindings/actions/workflows/test-publish.yml)
[![Nethermind.RocksDbBindings](https://img.shields.io/nuget/v/Nethermind.RocksDbBindings)](https://www.nuget.org/packages/Nethermind.RocksDbBindings)

C# bindings for [RocksDB](https://github.com/facebook/rocksdb).

## License

This project is licensed under the [MIT](https://github.com/nethermindeth/rocksdb-bindings/blob/main/LICENSE) license.

Parts of the managed API were originally derived from [rocksdb-sharp](https://github.com/curiosity-ai/rocksdb-sharp), used under [BSD-2-Clause](https://github.com/curiosity-ai/rocksdb-sharp/blob/master/LICENSE), and have since been substantially rewritten.

The package also ships prebuilt RocksDB binaries, used under the [Apache-2.0](https://github.com/facebook/rocksdb/blob/main/LICENSE.Apache) option of RocksDB's dual Apache-2.0/[GPL-2.0](https://github.com/facebook/rocksdb/blob/main/COPYING) license. Those binaries statically link LZ4, Snappy, Zstandard, and, on Linux, jemalloc.
