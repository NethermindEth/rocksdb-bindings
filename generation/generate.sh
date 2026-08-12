#!/usr/bin/env bash
# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

set -euo pipefail

ref=${1:-}

if [ -z "$ref" ]; then
  echo "Usage: generate.sh <rocksdb-ref>" >&2
  exit 1
fi

root=$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)
work=$(mktemp -d)
trap 'rm -rf "$work"' EXIT

curl -fsSL -o "$work/c.h" \
  "https://raw.githubusercontent.com/facebook/rocksdb/$ref/include/rocksdb/c.h"

ClangSharpPInvokeGenerator @"$root/generation/rocksdb.rsp" \
  --header-file "$root/generation/header.txt" \
  --file "$work/c.h" \
  --output "$work/RocksDbNative.g.cs"

mv "$work/RocksDbNative.g.cs" \
  "$root/src/Nethermind.RocksDbBindings/RocksDbNative.g.cs"
