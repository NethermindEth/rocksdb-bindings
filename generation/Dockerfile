# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

FROM mcr.microsoft.com/dotnet/sdk:10.0-resolute

RUN apt-get update && \
  apt-get install -y --no-install-recommends libclang-common-21-dev && \
  rm -rf /var/lib/apt/lists/*

# The ClangSharp major version must match the libclang-common package above.
RUN dotnet tool install ClangSharpPInvokeGenerator --version 21.1.8.4 --tool-path /opt/clangsharp

RUN set -eux; \
  find /opt/clangsharp -type f \( -name 'libclang.so' -o -name 'libClangSharp.so' \) \
  -exec sh -c 'for f do ln -sf "$f" "/opt/clangsharp/$(basename "$f")"; done' sh {} +; \
  test -f /opt/clangsharp/libclang.so; \
  test -f /opt/clangsharp/libClangSharp.so; \
  chmod -R a+rX /opt/clangsharp

ENV LD_LIBRARY_PATH=/opt/clangsharp
ENV PATH=/opt/clangsharp:$PATH

WORKDIR /app
