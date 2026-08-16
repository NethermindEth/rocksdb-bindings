// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.CompilerServices;

using Nethermind.RocksDbBindings.Native;

namespace Nethermind.RocksDbBindings.Tests;

/// <remarks>
/// Most of the C API spells booleans <c>unsigned char</c>, but a few functions use C's
/// <c>_Bool</c> and are generated as C# <see cref="bool" />. Those are a single byte wide only
/// because of <c>[assembly: DisableRuntimeMarshalling]</c>; without it the runtime marshals
/// <c>bool</c> as a four-byte <c>BOOL</c> and tests all four bytes of a return whose upper bits
/// the ABI leaves unspecified. On the build this was measured against they hold junk, turning a
/// native false into true, but that is not guaranteed anywhere, so the attribute is asserted
/// directly as well. No generator option can widen these to an explicit byte and removing the
/// attribute is not a build error, so this stands in for the missing signal.
/// </remarks>
public class NativeBoolTests
{
    private static unsafe bool Enabled(nuint bufferSize)
    {
        var wbm = RocksDbNative.rocksdb_write_buffer_manager_create(bufferSize, allow_stall: false);

        try
        {
            return RocksDbNative.rocksdb_write_buffer_manager_enabled(wbm);
        }
        finally
        {
            RocksDbNative.rocksdb_write_buffer_manager_destroy(wbm);
        }
    }

    /// <remarks>
    /// The deterministic half of the guard. Whether a widened <c>bool</c> actually misreads
    /// depends on upper return-register bits the ABI leaves unspecified, so the behavioural
    /// tests below can only catch the regression on builds that happen to leave them dirty.
    /// This one catches it everywhere.
    /// </remarks>
    [Test]
    public async Task TheBindingsAssembly_DisablesRuntimeMarshalling()
        => await Assert.That(typeof(RocksDbNative).Assembly
            .IsDefined(typeof(DisableRuntimeMarshallingAttribute), inherit: false)).IsTrue();

    /// <remarks>
    /// Paired with the false case below so that neither passes vacuously: on its own a reader
    /// stuck at either polarity would satisfy one of them.
    /// </remarks>
    [Test]
    public async Task Enabled_IsTrue_WhenTheBufferIsSized()
        => await Assert.That(Enabled(1 << 20)).IsTrue();

    /// <remarks>
    /// The only polarity a widened <c>bool</c> can get wrong.
    /// </remarks>
    [Test]
    public async Task Enabled_IsFalse_WhenTheBufferIsUnsized()
        => await Assert.That(Enabled(0)).IsFalse();
}
