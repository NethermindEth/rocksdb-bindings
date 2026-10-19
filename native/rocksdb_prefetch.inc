// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

// Included by db/c.cc after rocksdb_async.inc, whose request it reuses.
#include "rocksdb_prefetch.h"

extern "C" {

rocksdb_net_read_t* rocksdb_net_prefetch_create(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, size_t count, const char* keys,
    size_t key_length, char** errptr) {
  try {
    auto request = std::make_unique<rocksdb_net_read_t>(db, options, count);
    request->key_storage.assign(keys, count * key_length);
    ColumnFamilyHandle* column_family = family == nullptr
        ? db->rep->DefaultColumnFamily() : family->rep;
    const char* key = request->key_storage.data();
    for (size_t i = 0; i < count; ++i) {
      request->keys[i] = Slice(key + i * key_length, key_length);
      request->column_families[i] = column_family;
    }
    return request.release();
  } catch (const std::exception& error) {
    SaveError(errptr, Status::Aborted(error.what()));
    return nullptr;
  }
}

unsigned char rocksdb_net_read_failed(rocksdb_net_read_t* request,
                                      char** errptr) {
  std::lock_guard<std::recursive_mutex> lock(request->mutex);
  for (const auto& status : request->statuses) {
    if (!status.ok() && !status.IsNotFound()) {
      SaveError(errptr, status);
      return 1;
    }
  }
  return 0;
}

}
