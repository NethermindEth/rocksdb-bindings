// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#include <cstring>

#include "rocksdb_read.h"

extern "C" {

unsigned char rocksdb_net_get_into_buffer_status(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, const char* key, size_t keylen,
    char* buffer, size_t buffer_size, size_t* vallen) {
  *vallen = 0;
  try {
    ColumnFamilyHandle* column_family = family == nullptr
        ? db->rep->DefaultColumnFamily() : family->rep;
    PinnableSlice value;
    Status status = db->rep->Get(options->rep, column_family, Slice(key, keylen), &value);
    if (status.ok()) {
      *vallen = value.size();
      if (value.size() <= buffer_size && value.size() != 0) {
        memcpy(buffer, value.data(), value.size());
      }
      return ROCKSDB_NET_GET_FOUND;
    }
    if (status.IsNotFound()) {
      return ROCKSDB_NET_GET_NOT_FOUND;
    }
    if (status.IsIncomplete()) {
      return ROCKSDB_NET_GET_INCOMPLETE;
    }
    return ROCKSDB_NET_GET_FAILED;
  } catch (...) {
    return ROCKSDB_NET_GET_FAILED;
  }
}

}
