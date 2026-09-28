// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Nethermind.RocksDbBindings.Native;

using static Nethermind.RocksDbBindings.Native.RocksDbNative;

namespace Nethermind.RocksDbBindings;

internal sealed unsafe class AsyncReadHandle : SafeHandle
{
    private readonly HandleLease _dbLease;
    private readonly HandleLease _optionsLease;
    private readonly HandleLease _snapshotLease;

    internal AsyncReadHandle(nint request, HandleLease dbLease,
        HandleLease optionsLease, HandleLease snapshotLease) : base(0, ownsHandle: true)
    {
        SetHandle(request);
        _dbLease = dbLease;
        _optionsLease = optionsLease;
        _snapshotLease = snapshotLease;
    }

    public override bool IsInvalid => handle == 0;

    protected override bool ReleaseHandle()
    {
        rocksdb_net_read_destroy((rocksdb_net_read_t*)handle);
        _snapshotLease.Dispose();
        _optionsLease.Dispose();
        _dbLease.Dispose();
        return true;
    }
}

internal abstract unsafe class AsyncReadOperation(AsyncReadHandle handle) : IThreadPoolWorkItem
{
    private readonly AsyncReadHandle _handle = handle;

    internal void Start(bool multi)
    {
        GCHandle context = default;
        try
        {
            context = GCHandle.Alloc(this);
            rocksdb_net_read_t* request = (rocksdb_net_read_t*)_handle.DangerousGetHandle();
            if (multi)
                rocksdb_net_multi_get_async(request, (void*)GCHandle.ToIntPtr(context), &OnComplete);
            else
                rocksdb_net_get_async(request, (void*)GCHandle.ToIntPtr(context), &OnComplete);
        }
        catch
        {
            if (context.IsAllocated)
                context.Free();
            _handle.Dispose();
            throw;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnComplete(void* context)
    {
        GCHandle root = GCHandle.FromIntPtr((nint)context);
        AsyncReadOperation operation = (AsyncReadOperation)root.Target!;
        // Completion must leave the RocksDB callback before reading/freeing the
        // request or running a continuation that could submit another read.
        ThreadPool.UnsafeQueueUserWorkItem(operation, preferLocal: false);
        root.Free();
    }

    protected byte[]? ReadValue(int index)
    {
        nuint length;
        byte found;
        sbyte* error = null;
        sbyte* value = rocksdb_net_read_value((rocksdb_net_read_t*)_handle.DangerousGetHandle(),
            (nuint)index, &length, &found, &error);
        RocksDbInterop.ThrowIfError(error);
        return found == 0 ? null : new ReadOnlySpan<byte>(value, checked((int)length)).ToArray();
    }

    public abstract void Execute();

    protected void Release() => _handle.Dispose();
}

internal sealed class AsyncGetOperation(AsyncReadHandle handle) : AsyncReadOperation(handle)
{
    private readonly TaskCompletionSource<byte[]?> _completion = new(TaskCreationOptions.RunContinuationsAsynchronously);

    internal Task<byte[]?> Task => _completion.Task;

    public override void Execute()
    {
        byte[]? value;
        try
        {
            value = ReadValue(0);
        }
        catch (Exception error)
        {
            Release();
            _completion.SetException(error);
            return;
        }
        Release();
        _completion.SetResult(value);
    }
}

internal sealed class AsyncMultiGetOperation(AsyncReadHandle handle, byte[][] keys) : AsyncReadOperation(handle)
{
    private readonly TaskCompletionSource<KeyValuePair<byte[], byte[]?>[]> _completion = new(TaskCreationOptions.RunContinuationsAsynchronously);

    internal Task<KeyValuePair<byte[], byte[]?>[]> Task => _completion.Task;

    public override void Execute()
    {
        KeyValuePair<byte[], byte[]?>[] values;
        try
        {
            values = new KeyValuePair<byte[], byte[]?>[keys.Length];
            for (int i = 0; i < keys.Length; i++)
                values[i] = new(keys[i], ReadValue(i));
        }
        catch (Exception error)
        {
            Release();
            _completion.SetException(error);
            return;
        }
        Release();
        _completion.SetResult(values);
    }
}
