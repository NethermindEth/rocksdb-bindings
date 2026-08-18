// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class ColumnFamiliesTests
{
    // The native pointer is reachable only through a lease, which is all this needs it for.
    private static nint Handle(ColumnFamilyOptions options)
    {
        using var lease = options.Lease(out nint handle);

        return handle;
    }

    [Test]
    public async Task NewCollection_ContainsOnlyTheDefaultFamily()
        => await Assert.That(new ColumnFamilies().Names).IsEquivalentTo(new[] { ColumnFamilies.DefaultName }, CollectionOrdering.Matching);

    [Test]
    public async Task NewCollection_UsesTheSuppliedOptionsForTheDefaultFamily()
    {
        var options = new ColumnFamilyOptions();

        var families = new ColumnFamilies(options);

        await Assert.That(families.Single().Options).IsSameReferenceAs(options);
    }

    [Test]
    public async Task NewCollection_WithoutOptions_StillGivesTheDefaultFamilyAHandle()
    {
        using var lease = new ColumnFamilies().LeaseOptions();

        await Assert.That(lease.Handles.Single()).IsNotEqualTo(nint.Zero);
    }

    [Test]
    public async Task Add_AppendsAFamilyAfterTheDefaultOne()
    {
        var families = new ColumnFamilies();

        families.Add("blocks", new ColumnFamilyOptions());
        families.Add("receipts", new ColumnFamilyOptions());

        await Assert.That(families.Names).IsEquivalentTo(new[] { ColumnFamilies.DefaultName, "blocks", "receipts" }, CollectionOrdering.Matching);
    }

    /// <remarks>
    /// The default family is always present, so adding it again has to replace the seeded
    /// descriptor rather than produce a duplicate RocksDB would reject.
    /// </remarks>
    [Test]
    public async Task Add_WithTheDefaultName_ReplacesTheSeededDescriptor()
    {
        var families = new ColumnFamilies();
        var replacement = new ColumnFamilyOptions();

        families.Add(ColumnFamilies.DefaultName, replacement);

        using (Assert.Multiple())
        {
            await Assert.That(families.Names).IsEquivalentTo(new[] { ColumnFamilies.DefaultName }, CollectionOrdering.Matching);
            await Assert.That(families.Single().Options).IsSameReferenceAs(replacement);
        }
    }

    [Test]
    public async Task Add_WithTheDefaultName_KeepsItFirst()
    {
        var families = new ColumnFamilies();
        families.Add("blocks", new ColumnFamilyOptions());

        families.Add(ColumnFamilies.DefaultName, new ColumnFamilyOptions());

        await Assert.That(families.Names).IsEquivalentTo(new[] { ColumnFamilies.DefaultName, "blocks" }, CollectionOrdering.Matching);
    }

    /// <remarks>
    /// RocksDB rejects an open that names a family twice, so a repeated name reconfigures the one
    /// already there instead of queueing a duplicate.
    /// </remarks>
    [Test]
    public async Task Add_WithARepeatedName_ReplacesTheOptionsInPlace()
    {
        var families = new ColumnFamilies();
        families.Add("blocks", new ColumnFamilyOptions());
        var replacement = new ColumnFamilyOptions();

        families.Add("blocks", replacement);

        using (Assert.Multiple())
        {
            await Assert.That(families.Names).IsEquivalentTo(new[] { ColumnFamilies.DefaultName, "blocks" }, CollectionOrdering.Matching);
            using var lease = families.LeaseOptions();

            await Assert.That(lease.Handles.Last()).IsEqualTo(Handle(replacement));
        }
    }

    [Test]
    public async Task LeasedOptionHandles_LineUpWithNames()
    {
        var blocks = new ColumnFamilyOptions();
        var families = new ColumnFamilies();
        families.Add("blocks", blocks);

        using var lease = families.LeaseOptions();

        await Assert.That(lease.Handles.Last()).IsEqualTo(Handle(blocks));
    }

    /// <remarks>
    /// A null reaches native code as a zero handle at open, far from the call that supplied it, so
    /// every route into a descriptor has to reject one.
    /// </remarks>
    [Test]
    public async Task Add_WithNulls_ThrowsAtTheCallThatSuppliedThem()
    {
        var families = new ColumnFamilies();

        using (Assert.Multiple())
        {
            await Assert.That(() => families.Add("blocks", null!)).Throws<ArgumentNullException>();
            await Assert.That(() => families.Add(null!, new ColumnFamilyOptions())).Throws<ArgumentNullException>();
            await Assert.That(() => families.Add(null!)).Throws<ArgumentNullException>();
            await Assert.That(() => new ColumnFamilies.Descriptor("blocks", null!)).Throws<ArgumentNullException>();
        }
    }

    [Test]
    public async Task Descriptor_KeepsTheNameItWasGiven()
        => await Assert.That(new ColumnFamilies.Descriptor("blocks", new ColumnFamilyOptions()).Name).IsEqualTo("blocks");
}
