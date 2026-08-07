// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Loader;

namespace Nethermind.RocksDbBindings.Native;

public static partial class RocksDbNative
{
    private const string LibraryName = "rocksdb";

    static RocksDbNative() => AssemblyLoadContext.Default.ResolvingUnmanagedDll += OnResolvingUnmanagedDll;

    private static nint OnResolvingUnmanagedDll(Assembly context, string name)
    {
        if (context != typeof(RocksDbNative).Assembly || !LibraryName.Equals(name, StringComparison.Ordinal))
            return nint.Zero;

        string platform;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            name = $"lib{name}.so";
            platform = "linux";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            name = $"lib{name}.dylib";
            platform = "osx";
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            name = $"{name}.dll";
            platform = "win";
        }
        else
            throw new PlatformNotSupportedException();

        var arch = RuntimeInformation.ProcessArchitecture.ToString().ToLowerInvariant();

        return NativeLibrary.Load($"runtimes/{platform}-{arch}/native/{name}", context, DllImportSearchPath.AssemblyDirectory);
    }
}
