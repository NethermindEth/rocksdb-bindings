# AGENTS instructions

C# bindings for RocksDB. See [global.json](./global.json) and [src](./src/) directory for the project requirements and configuration.

## Project structure

- [src](./src/): The main codebase. The C# API was originally derived from [rocksdb-sharp](https://github.com/curiosity-ai/rocksdb-sharp), and has since been substantially rewritten.
- [generation](./generation/): RocksDB bindings generation with ClangSharp.
- [native](./native/): Async and decoded-batch C API extensions, included in upstream `db/c.cc` during the native build. Async declarations generate `RocksDbNative.Async.g.cs`. Batch bindings are declared in `RocksDb.MultiGet.Native.cs`. `rocksdb_prefetch.{h,inc}` reuse the async request for reads whose values are discarded; their bindings are declared next to `PrefetchAsync`.
- [build-rocksdb.yml](./.github/workflows/build-rocksdb.yml): Builds RocksDB and generates bindings for the specified version.
- [test-publish.yml](./.github/workflows/test-publish.yml): Runs the tests and optionally publishes on NuGet.

## Coding guidelines

- Follow [.editorconfig](./.editorconfig).
- Do not assume; measure, research, ask if unsure.
- Keep comments short and to the point.
- Add tests for new code and bug fixes.
- Use conventional commits; keep scoped and imperative.
- Do not edit `RocksDbNative*.g.cs`; regenerate it if needed using the [Dockerfile](./generation/Dockerfile). Do not install ClangSharp locally.
- Prefer the latest versions of GitHub Actions and runners.
- Keep native binaries within the [CPU requirements](./README.md#cpu-requirements).
- Update [THIRD-PARTY-NOTICES](./THIRD-PARTY-NOTICES) when introducing a dependency if needed.
- Keep [AGENTS.md](./AGENTS.md) in sync with the ongoing development.
