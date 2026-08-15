// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings.Tests;

public class ColumnFamiliesTests
{
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
        => await Assert.That(new ColumnFamilies().OptionHandles.Single()).IsNotEqualTo(nint.Zero);

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
    /// descriptor rather than produce a duplicate rocksdb would reject.
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

    [Test]
    public async Task OptionHandles_LineUpWithNames()
    {
        var blocks = new ColumnFamilyOptions();
        var families = new ColumnFamilies();
        families.Add("blocks", blocks);

        await Assert.That(families.OptionHandles.Last()).IsEqualTo(blocks.Handle);
    }

    [Test]
    public async Task Descriptor_KeepsTheNameItWasGiven()
        => await Assert.That(new ColumnFamilies.Descriptor("blocks", new ColumnFamilyOptions()).Name).IsEqualTo("blocks");
}
