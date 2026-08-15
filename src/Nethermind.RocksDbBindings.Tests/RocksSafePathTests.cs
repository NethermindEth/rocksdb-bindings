// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices.Marshalling;

namespace Nethermind.RocksDbBindings.Tests;

public class RocksSafePathTests
{
    private static unsafe string? ReadBack(RocksSafePath path)
        => Utf8StringMarshaller.ConvertToManaged((byte*)path.Handle);

    [Test]
    public async Task Constructor_NullPath_Throws()
        => await Assert.That(() => new RocksSafePath(null!)).ThrowsExactly<ArgumentNullException>();

    [Test]
    public async Task Constructor_AllocatesANonNullHandle()
    {
        using var path = new RocksSafePath("/var/lib/rocksdb");

        await Assert.That(path.Handle).IsNotEqualTo(nint.Zero);
    }

    [Test]
    public async Task Constructor_MarshalsThePathAsUtf8()
    {
        using var path = new RocksSafePath("/var/lib/rocksdb");

        await Assert.That(ReadBack(path)).IsEqualTo("/var/lib/rocksdb");
    }

    [Test]
    public async Task Constructor_MarshalsNonAsciiPaths()
    {
        using var path = new RocksSafePath("/data/日本語/ünïcødé");

        await Assert.That(ReadBack(path)).IsEqualTo("/data/日本語/ünïcødé");
    }

    [Test]
    public async Task Constructor_MarshalsTheEmptyPath()
    {
        using var path = new RocksSafePath(string.Empty);

        await Assert.That(ReadBack(path)).IsEqualTo(string.Empty);
    }

    /// <remarks>
    /// Disposal is deliberately a no-op: rocksdb keeps some of these strings without copying
    /// them, so the memory is tied to the lifetime of the database instead. Freeing it here
    /// would leave rocksdb reading freed memory.
    /// </remarks>
    [Test]
    public async Task Dispose_LeavesTheStringAllocated()
    {
        var path = new RocksSafePath("/var/lib/rocksdb");

        path.Dispose();

        using (Assert.Multiple())
        {
            await Assert.That(path.Handle).IsNotEqualTo(nint.Zero);
            await Assert.That(ReadBack(path)).IsEqualTo("/var/lib/rocksdb");
        }
    }
}
