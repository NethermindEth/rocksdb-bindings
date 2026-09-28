// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

// Included by db/c.cc so the upstream opaque wrapper layouts stay authoritative.
#include "rocksdb_async.h"

#include <mutex>

struct rocksdb_net_read_t final : DB::AsyncCallback {
  DB* db;
  ReadOptions options;
  std::vector<std::string> key_storage;
  std::vector<Slice> keys;
  std::vector<ColumnFamilyHandle*> column_families;
  std::vector<PinnableSlice> values;
  std::vector<Status> statuses;
  std::recursive_mutex mutex;
  void* context = nullptr;
  rocksdb_net_read_callback callback = nullptr;

  rocksdb_net_read_t(rocksdb_t* database, const rocksdb_readoptions_t* opts,
                    size_t count)
      : db(database->rep), options(opts->rep), key_storage(count), keys(count),
        column_families(count), values(count), statuses(count) {}

  void Complete() {
    std::lock_guard<std::recursive_mutex> lock(mutex);
    callback(context);
  }

  // The callback signature changed after 11.8; keep both source-compatible.
  void OnComplete() { Complete(); }
  void OnComplete(const ROCKSDB_NAMESPACE::PerfContext*,
                  const ROCKSDB_NAMESPACE::IOStatsContext*) { Complete(); }
};

extern "C" {

rocksdb_net_read_t* rocksdb_net_read_create(
    rocksdb_t* db, const rocksdb_readoptions_t* options, size_t count,
    rocksdb_column_family_handle_t* const* column_families,
    const char* const* keys, const size_t* key_lengths, char** errptr) {
  try {
    auto request = std::make_unique<rocksdb_net_read_t>(db, options, count);
    for (size_t i = 0; i < count; ++i) {
      if (key_lengths[i] != 0) {
        request->key_storage[i].assign(keys[i], key_lengths[i]);
      }
      request->keys[i] = Slice(request->key_storage[i]);
      request->column_families[i] = column_families == nullptr
          ? db->rep->DefaultColumnFamily() : column_families[i]->rep;
    }
    return request.release();
  } catch (const std::exception& error) {
    SaveError(errptr, Status::Aborted(error.what()));
    return nullptr;
  }
}

static void StartRead(rocksdb_net_read_t* request, void* context,
                      rocksdb_net_read_callback callback, bool multi) {
  std::lock_guard<std::recursive_mutex> lock(request->mutex);
  request->context = context;
  request->callback = callback;
  try {
    if (multi) {
      request->db->MultiGetAsync(request->options, request->keys.size(),
          request->column_families.data(), request->keys.data(),
          request->values.data(), request->statuses.data(), *request);
    } else {
      request->db->GetAsync(request->options, request->column_families[0],
          request->keys[0], &request->values[0], request->statuses[0], *request);
    }
  } catch (const std::exception& error) {
    for (auto& status : request->statuses) {
      status = Status::Aborted(error.what());
    }
    request->Complete();
  }
}

void rocksdb_net_get_async(rocksdb_net_read_t* request, void* context,
                           rocksdb_net_read_callback callback) {
  StartRead(request, context, callback, false);
}

void rocksdb_net_multi_get_async(rocksdb_net_read_t* request, void* context,
                                 rocksdb_net_read_callback callback) {
  StartRead(request, context, callback, true);
}

const char* rocksdb_net_read_value(rocksdb_net_read_t* request, size_t index,
                                  size_t* length, unsigned char* found,
                                  char** errptr) {
  std::lock_guard<std::recursive_mutex> lock(request->mutex);
  const auto& status = request->statuses[index];
  *found = status.ok();
  *length = status.ok() ? request->values[index].size() : 0;
  if (!status.ok()) {
    if (!status.IsNotFound()) {
      SaveError(errptr, status);
    }
    return nullptr;
  }
  return request->values[index].data();
}

void rocksdb_net_read_destroy(rocksdb_net_read_t* request) {
  if (request != nullptr) {
    {
      std::lock_guard<std::recursive_mutex> lock(request->mutex);
    }
    delete request;
  }
}

}
