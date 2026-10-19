// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#pragma once

#include "rocksdb/c.h"

#ifdef __cplusplus
extern "C" {
#endif

// Result codes of rocksdb_net_get_into_buffer_status.
#define ROCKSDB_NET_GET_FOUND 1
#define ROCKSDB_NET_GET_NOT_FOUND 2
#define ROCKSDB_NET_GET_INCOMPLETE 3
#define ROCKSDB_NET_GET_FAILED 4

// Reads one value and returns how the read ended as a code, with no error string and no
// exception. Incomplete is the result of a read at the block-cache tier that would need file IO.
// Failed is any other error, such as corruption or an IO error. On Found, *vallen is the value
// length and the value is copied only when it fits in buffer.
extern ROCKSDB_LIBRARY_API unsigned char rocksdb_net_get_into_buffer_status(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, const char* key, size_t keylen,
    char* buffer, size_t buffer_size, size_t* vallen);

#ifdef __cplusplus
}
#endif
