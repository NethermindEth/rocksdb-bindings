// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings.Native;

public partial struct rocksdb_net_read_t
{
}

public static unsafe partial class RocksDbNative
{
    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern rocksdb_net_read_t* rocksdb_net_read_create(rocksdb_t* db, [NativeTypeName("const rocksdb_readoptions_t *")] rocksdb_readoptions_t* options, [NativeTypeName("size_t")] nuint count, [NativeTypeName("rocksdb_column_family_handle_t *const *")] rocksdb_column_family_handle_t** column_families, [NativeTypeName("const char *const *")] sbyte** keys, [NativeTypeName("const size_t *")] nuint* key_lengths, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_net_get_async(rocksdb_net_read_t* request, void* context, [NativeTypeName("rocksdb_net_read_callback")] delegate* unmanaged[Cdecl]<void*, void> callback);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_net_multi_get_async(rocksdb_net_read_t* request, void* context, [NativeTypeName("rocksdb_net_read_callback")] delegate* unmanaged[Cdecl]<void*, void> callback);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    [return: NativeTypeName("const char *")]
    public static extern sbyte* rocksdb_net_read_value(rocksdb_net_read_t* request, [NativeTypeName("size_t")] nuint index, [NativeTypeName("size_t *")] nuint* length, [NativeTypeName("unsigned char *")] byte* found, [NativeTypeName("char **")] sbyte** errptr);

    [DllImport("rocksdb", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
    public static extern void rocksdb_net_read_destroy(rocksdb_net_read_t* request);
}
