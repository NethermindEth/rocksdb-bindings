// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#pragma once

#include "rocksdb/c.h"

#ifdef __cplusplus
extern "C" {
#endif

typedef struct rocksdb_net_read_t rocksdb_net_read_t;
typedef void (*rocksdb_net_read_callback)(void* context);

// Copies keys and options. The DB, column families and resources referenced by
// ReadOptions must remain alive until destroy. Do not mutate those resources.
extern ROCKSDB_LIBRARY_API rocksdb_net_read_t* rocksdb_net_read_create(
    rocksdb_t* db, const rocksdb_readoptions_t* options, size_t count,
    rocksdb_column_family_handle_t* const* column_families,
    const char* const* keys, const size_t* key_lengths, char** errptr);

// Start exactly once. Completion may run inline. The callback must not throw,
// start another read, or destroy the request. Consume/destroy from another
// thread after callback notification; these operations synchronize with it.
extern ROCKSDB_LIBRARY_API void rocksdb_net_get_async(
    rocksdb_net_read_t* request, void* context, rocksdb_net_read_callback callback);
extern ROCKSDB_LIBRARY_API void rocksdb_net_multi_get_async(
    rocksdb_net_read_t* request, void* context, rocksdb_net_read_callback callback);

// Returns borrowed bytes valid until destroy. found distinguishes empty values
// from missing keys; errors use rocksdb_free, as in the upstream C API.
extern ROCKSDB_LIBRARY_API const char* rocksdb_net_read_value(
    rocksdb_net_read_t* request, size_t index, size_t* length,
    unsigned char* found, char** errptr);
extern ROCKSDB_LIBRARY_API void rocksdb_net_read_destroy(
    rocksdb_net_read_t* request);

#ifdef __cplusplus
}
#endif
