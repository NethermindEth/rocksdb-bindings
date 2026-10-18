// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

// Included by db/c.cc. The POSIX file system (env/fs_posix.cc) declares this symbol weak and
// uses io_uring only when the host binary defines it and it returns true. db_bench defines it;
// a library does not, so without this definition MultiRead and the async_io read path fall back
// to one synchronous pread per block. ROCKSDB_NET_IO_URING=0 turns it off at process start.

#include <cstdlib>
#include <cstring>

extern "C" bool RocksDbIOUringEnable() {
  static const bool enabled = [] {
    const char* value = std::getenv("ROCKSDB_NET_IO_URING");
    return value == nullptr || std::strcmp(value, "0") != 0;
  }();
  return enabled;
}
