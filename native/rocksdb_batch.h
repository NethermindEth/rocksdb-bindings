// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#pragma once

#include <stdint.h>

#include "rocksdb/c.h"

#ifdef __cplusplus
extern "C" {
#endif

typedef struct rocksdb_net_batch_t rocksdb_net_batch_t;

extern ROCKSDB_LIBRARY_API unsigned char rocksdb_net_batch_read(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, size_t count,
    const rocksdb_slice_t* keys, rocksdb_slice_t* values,
    unsigned char sorted, rocksdb_net_batch_t** batch, char** errptr);
extern ROCKSDB_LIBRARY_API void rocksdb_net_batch_destroy(
    rocksdb_net_batch_t* batch);
extern ROCKSDB_LIBRARY_API int rocksdb_net_thread_allocated(uint64_t* allocated);

#ifdef __cplusplus
}
#endif
