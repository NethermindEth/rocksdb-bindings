# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

file(READ "${ROCKSDB_SOURCE}/db/c.cc" source)
if(source MATCHES "rocksdb_async.inc")
  message(FATAL_ERROR "Async extension already applied")
endif()
file(APPEND "${ROCKSDB_SOURCE}/db/c.cc" "\n#include \"rocksdb_async.inc\"\n")
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_async.inc"
               "${ROCKSDB_SOURCE}/db/rocksdb_async.inc" COPYONLY)
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_async.h"
               "${ROCKSDB_SOURCE}/db/rocksdb_async.h" COPYONLY)
