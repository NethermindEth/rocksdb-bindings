// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Collections;

namespace Nethermind.RocksDbBindings;

/// <summary>
/// The column families to open a database with, each paired with the options it is opened under.
/// </summary>
/// <remarks>
/// Opening a database means naming every family it holds, so a set built here always starts with
/// the default family. Configure that one by passing its options to the constructor or by adding
/// <see cref="DefaultName"/> again; families keep the order they are added in, which is the order
/// their handles come back in.
/// </remarks>
public class ColumnFamilies : IEnumerable<ColumnFamilies.Descriptor>
{
    /// <summary>The name rocksdb gives the family that every database has.</summary>
    public const string DefaultName = "default";

    private readonly List<Descriptor> _descriptors = [];

    public ColumnFamilies(ColumnFamilyOptions? defaultOptions = null)
        => _descriptors.Add(new Descriptor(DefaultName, defaultOptions ?? new ColumnFamilyOptions()));

    public IEnumerable<string> Names => _descriptors.Select(descriptor => descriptor.Name);

    public IEnumerable<nint> OptionHandles => _descriptors.Select(descriptor => descriptor.Options.Handle);

    /// <summary>
    /// Adds a family, or replaces the options of one already named. Repeating
    /// <see cref="DefaultName"/> reconfigures the default family rather than adding a second one,
    /// which rocksdb would reject.
    /// </summary>
    public void Add(Descriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(descriptor);

        var existing = _descriptors.FindIndex(candidate => candidate.Name == descriptor.Name);
        if (existing < 0)
            _descriptors.Add(descriptor);
        else
            _descriptors[existing] = descriptor;
    }

    /// <inheritdoc cref="Add(Descriptor)"/>
    public void Add(string name, ColumnFamilyOptions options) => Add(new Descriptor(name, options));

    public IEnumerator<Descriptor> GetEnumerator() => _descriptors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>One family: its name, and the options it is opened under.</summary>
    public sealed class Descriptor(string name, ColumnFamilyOptions options)
    {
        // Guarded here rather than in Add, so both overloads and a directly constructed descriptor
        // fail at the call that supplied the null instead of at the open that dereferences it.
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));

        public ColumnFamilyOptions Options { get; } = options ?? throw new ArgumentNullException(nameof(options));
    }
}
