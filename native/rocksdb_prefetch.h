// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#pragma once

#include "rocksdb_async.h"

#ifdef __cplusplus
extern "C" {
#endif

// Copies count keys of key_length bytes each, back to back in keys, for rocksdb_net_multi_get_async. A null family
// reads the default column family. The values are never handed out: the read only brings their blocks into the cache.
extern ROCKSDB_LIBRARY_API rocksdb_net_read_t* rocksdb_net_prefetch_create(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, size_t count, const char* keys,
    size_t key_length, char** errptr);

// After completion: saves the first error other than NotFound and returns 1, or returns 0 when every read succeeded.
extern ROCKSDB_LIBRARY_API unsigned char rocksdb_net_read_failed(
    rocksdb_net_read_t* request, char** errptr);

#ifdef __cplusplus
}
#endif
