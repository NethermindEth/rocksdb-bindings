// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

#include "rocksdb_batch.h"

extern "C" int je_mallctl(const char*, void*, size_t*, void*, size_t);

struct rocksdb_net_batch_t {
  std::vector<PinnableSlice> values;

  explicit rocksdb_net_batch_t(size_t count) : values(count) {}
};

extern "C" {

rocksdb_net_batch_t* rocksdb_net_batch_read(
    rocksdb_t* db, const rocksdb_readoptions_t* options,
    rocksdb_column_family_handle_t* family, size_t count,
    const rocksdb_slice_t* keys, rocksdb_slice_t* values,
    unsigned char sorted, char** errptr) {
  try {
    auto batch = std::make_unique<rocksdb_net_batch_t>(count);
    std::vector<Status> statuses(count);
    static_assert(sizeof(rocksdb_slice_t) == sizeof(Slice));
    static_assert(alignof(rocksdb_slice_t) == alignof(Slice));
    ColumnFamilyHandle* column_family = family == nullptr
        ? db->rep->DefaultColumnFamily() : family->rep;
    db->rep->MultiGet(options->rep, column_family, count,
        reinterpret_cast<const Slice*>(keys), batch->values.data(),
        statuses.data(), sorted != 0);

    for (size_t i = 0; i < count; ++i) {
      if (!statuses[i].ok() && !statuses[i].IsNotFound()) {
        SaveError(errptr, statuses[i]);
        return nullptr;
      }
    }
    for (size_t i = 0; i < count; ++i) {
      values[i].data = statuses[i].ok() ? batch->values[i].data() : nullptr;
      values[i].size = statuses[i].ok()
          ? batch->values[i].size() : static_cast<size_t>(-1);
    }
    return batch.release();
  } catch (const std::exception& error) {
    SaveError(errptr, Status::Aborted(error.what()));
    return nullptr;
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
