// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#include "rocksdb_batch.h"

extern "C" int je_mallctl(const char*, void*, size_t*, void*, size_t);

struct rocksdb_net_batch_t {
  std::vector<PinnableSlice> values;

  explicit rocksdb_net_batch_t(std::vector<PinnableSlice>&& pinned_values)
      : values(std::move(pinned_values)) {}
};

extern "C" {

unsigned char rocksdb_net_batch_read(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, size_t count,
    const rocksdb_slice_t* keys, rocksdb_slice_t* values,
    unsigned char sorted, rocksdb_net_batch_t** result, char** errptr) {
  *result = nullptr;
  try {
    std::vector<PinnableSlice> pinned_values(count);
    std::vector<Status> statuses(count);
    static_assert(sizeof(rocksdb_slice_t) == sizeof(Slice));
    static_assert(alignof(rocksdb_slice_t) == alignof(Slice));
    ColumnFamilyHandle* column_family = family == nullptr
        ? db->rep->DefaultColumnFamily() : family->rep;
    db->rep->MultiGet(options->rep, column_family, count,
        reinterpret_cast<const Slice*>(keys), pinned_values.data(),
        statuses.data(), sorted != 0);

    bool any_found = false;
    for (size_t i = 0; i < count; ++i) {
      if (statuses[i].ok()) {
        any_found = true;
      } else if (!statuses[i].IsNotFound()) {
        SaveError(errptr, statuses[i]);
        return 0;
      }
    }
    if (!any_found) {
      return 1;
    }
    auto batch = std::make_unique<rocksdb_net_batch_t>(std::move(pinned_values));
    for (size_t i = 0; i < count; ++i) {
      values[i].data = statuses[i].ok() ? batch->values[i].data() : nullptr;
      values[i].size = statuses[i].ok()
          ? batch->values[i].size() : static_cast<size_t>(-1);
    }
    *result = batch.release();
    return 1;
  } catch (const std::exception& error) {
    SaveError(errptr, Status::Aborted(error.what()));
    return 0;
  }
}

void rocksdb_net_batch_destroy(rocksdb_net_batch_t* batch) {
  delete batch;
}

int rocksdb_net_thread_allocated(uint64_t* allocated) {
  size_t length = sizeof(*allocated);
  return je_mallctl("thread.allocated", allocated, &length, nullptr, 0);
}

}
