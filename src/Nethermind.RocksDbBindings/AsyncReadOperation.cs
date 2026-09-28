// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks.Sources;

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
    private AsyncReadHandle? _handle = handle;

    protected void Initialize(AsyncReadHandle handle) => _handle = handle;

    internal void Start(bool multi)
    {
        GCHandle context = default;
        try
        {
            context = GCHandle.Alloc(this);
            rocksdb_net_read_t* request = (rocksdb_net_read_t*)_handle!.DangerousGetHandle();
            if (multi)
                rocksdb_net_multi_get_async(request, (void*)GCHandle.ToIntPtr(context), &OnComplete);
            else
                rocksdb_net_get_async(request, (void*)GCHandle.ToIntPtr(context), &OnComplete);
        }
        catch
        {
            if (context.IsAllocated)
                context.Free();
            Release();
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
        => TryReadValue(index, out ReadOnlySpan<byte> value) ? value.ToArray() : null;

    protected bool TryReadValue(int index, out ReadOnlySpan<byte> buffer)
    {
        nuint length;
        byte found;
        sbyte* error = null;
        sbyte* value = rocksdb_net_read_value((rocksdb_net_read_t*)_handle!.DangerousGetHandle(),
            (nuint)index, &length, &found, &error);
        RocksDbInterop.ThrowIfError(error);
        buffer = found == 0 ? default : new ReadOnlySpan<byte>(value, checked((int)length));
        return found != 0;
    }

    public abstract void Execute();

    protected void Release()
    {
        _handle!.Dispose();
        _handle = null;
    }
}

internal sealed class AsyncGetOperation<T, TDeserializer> : AsyncReadOperation, IValueTaskSource<T?>
    where TDeserializer : ISpanDeserializer<T>
{
    [ThreadStatic]
    private static AsyncGetOperation<T, TDeserializer>? s_cached;

    private ManualResetValueTaskSourceCore<T?> _completion = new() { RunContinuationsAsynchronously = true };
    private TDeserializer _deserializer;

    private AsyncGetOperation(AsyncReadHandle handle, TDeserializer deserializer) : base(handle)
        => _deserializer = deserializer;

    internal static AsyncGetOperation<T, TDeserializer> Rent(AsyncReadHandle handle, TDeserializer deserializer)
    {
        AsyncGetOperation<T, TDeserializer>? operation = s_cached;
        if (operation is null)
            return new(handle, deserializer);

        s_cached = null;
        operation.Initialize(handle);
        operation._deserializer = deserializer;
        return operation;
    }

    internal ValueTask<T?> ValueTask => new(this, _completion.Version);

    public override void Execute()
    {
        T? value;
        try
        {
            value = TryReadValue(0, out ReadOnlySpan<byte> buffer) ? _deserializer.Deserialize(buffer) : default;
        }
        catch (Exception error)
        {
            Release();
            _deserializer = default!;
            _completion.SetException(error);
            return;
        }
        Release();
        _deserializer = default!;
        _completion.SetResult(value);
    }

    public T? GetResult(short token)
    {
        // Only the consumer returns the operation to the pool, after native resources have been released.
        if (_completion.GetStatus(token) == ValueTaskSourceStatus.Pending)
            throw new InvalidOperationException("The read has not completed.");
        try
        {
            return _completion.GetResult(token);
        }
        finally
        {
            _completion.Reset();
            s_cached = this;
        }
    }

    public ValueTaskSourceStatus GetStatus(short token) => _completion.GetStatus(token);

    public void OnCompleted(Action<object?> continuation, object? state, short token, ValueTaskSourceOnCompletedFlags flags)
        => _completion.OnCompleted(continuation, state, token, flags);
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
