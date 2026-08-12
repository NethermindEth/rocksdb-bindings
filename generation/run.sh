#!/usr/bin/env bash
# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

# Usage: run.sh [rocksdb-ref]

set -euo pipefail

root=$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)
ref=${1:-}

if [ -z "$ref" ]; then
  version=$(sed -n 's|.*<VersionPrefix>\(.*\)</VersionPrefix>.*|\1|p' \
    "$root/src/Directory.Build.props")
  ref=v$version
fi

docker build -t rocksdb-bindings-generator "$root/generation"
docker run --rm --user "$(id -u):$(id -g)" -v "$root:/app" \
  rocksdb-bindings-generator bash generation/generate.sh "$ref"
