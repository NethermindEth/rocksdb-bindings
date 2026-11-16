# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

FROM mcr.microsoft.com/dotnet/sdk:10.0-resolute

RUN apt-get update && \
  apt-get install -y --no-install-recommends libclang-common-22-dev && \
  rm -rf /var/lib/apt/lists/*

# ClangSharp only finds the builtin headers of its own Clang major, so pass Ubuntu's newest.
# TODO: use libclang-common-23-dev once Ubuntu ships it.
ENV CLANG_RESOURCE_DIR=/usr/lib/llvm-22/lib/clang/22
RUN test -f "$CLANG_RESOURCE_DIR/include/stdbool.h"

RUN dotnet tool install ClangSharpPInvokeGenerator --version "23.*" --tool-path /opt/clangsharp

RUN set -eux; \
  find /opt/clangsharp -type f \( -name 'libclang.so' -o -name 'libClangSharp.so' \) \
  -exec sh -c 'for f do ln -sf "$f" "/opt/clangsharp/$(basename "$f")"; done' sh {} +; \
  test -f /opt/clangsharp/libclang.so; \
  test -f /opt/clangsharp/libClangSharp.so; \
  chmod -R a+rX /opt/clangsharp

ENV LD_LIBRARY_PATH=/opt/clangsharp
ENV PATH=/opt/clangsharp:$PATH

WORKDIR /app
