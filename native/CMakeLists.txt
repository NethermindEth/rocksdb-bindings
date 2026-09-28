# SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
# SPDX-License-Identifier: MIT

cmake_minimum_required(VERSION 3.22)
project(rocksdb_bindings LANGUAGES C CXX)

# Upstream disallows a shared target with Folly. Link the PIC static target,
# including all C exports, into the same library used by the existing bindings.
set(ROCKSDB_BUILD_SHARED OFF CACHE BOOL "" FORCE)
add_subdirectory("${ROCKSDB_SOURCE}" rocksdb)
# Filesystem sources check Folly feature macros before including its headers.
target_compile_options(rocksdb PRIVATE -UFOLLY_NO_CONFIG -include folly/portability/Config.h)
file(WRITE "${CMAKE_CURRENT_BINARY_DIR}/exports.cc" "")
add_library(rocksdb-shared SHARED "${CMAKE_CURRENT_BINARY_DIR}/exports.cc")
target_link_libraries(rocksdb-shared PRIVATE
  "-Wl,--whole-archive" rocksdb "-Wl,--no-whole-archive")
set_target_properties(rocksdb-shared PROPERTIES OUTPUT_NAME rocksdb)
