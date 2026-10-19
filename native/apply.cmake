# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

if(NOT ROCKSDB_SOURCE)
  set(ROCKSDB_SOURCE "${CMAKE_CURRENT_BINARY_DIR}")
endif()
file(READ "${ROCKSDB_SOURCE}/db/c.cc" source)
if(source MATCHES "rocksdb_async.inc")
  message(FATAL_ERROR "Async extension already applied")
endif()
file(APPEND "${ROCKSDB_SOURCE}/db/c.cc" "\n#include \"rocksdb_async.inc\"\n")
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_async.inc"
               "${ROCKSDB_SOURCE}/db/rocksdb_async.inc" COPYONLY)
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_async.h"
               "${ROCKSDB_SOURCE}/db/rocksdb_async.h" COPYONLY)
file(APPEND "${ROCKSDB_SOURCE}/db/c.cc" "\n#include \"rocksdb_batch.inc\"\n")
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_batch.inc"
               "${ROCKSDB_SOURCE}/db/rocksdb_batch.inc" COPYONLY)
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_batch.h"
               "${ROCKSDB_SOURCE}/db/rocksdb_batch.h" COPYONLY)
file(APPEND "${ROCKSDB_SOURCE}/db/c.cc" "\n#include \"rocksdb_read.inc\"\n")
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_read.inc"
               "${ROCKSDB_SOURCE}/db/rocksdb_read.inc" COPYONLY)
configure_file("${CMAKE_CURRENT_LIST_DIR}/rocksdb_read.h"
               "${ROCKSDB_SOURCE}/db/rocksdb_read.h" COPYONLY)
