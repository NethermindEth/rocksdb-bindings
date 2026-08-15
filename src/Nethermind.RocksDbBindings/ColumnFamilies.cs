// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Collections;

namespace Nethermind.RocksDbBindings;

public class ColumnFamilies : IEnumerable<ColumnFamilies.Descriptor>
{
    private List<Descriptor> Descriptors { get; } = new List<Descriptor>();

    public static readonly string DefaultName = "default";

    public class Descriptor(string name, ColumnFamilyOptions options)
    {
        public string Name { get; } = name;
        public ColumnFamilyOptions Options { get; } = options;
    }

    public ColumnFamilies(ColumnFamilyOptions? options = null)
    {
        Descriptors.Add(new Descriptor(DefaultName, options ?? new ColumnFamilyOptions()));
    }

    public IEnumerable<string> Names => this.Select(cfd => cfd.Name);

    public IEnumerable<nint> OptionHandles => this.Select(cfd => cfd.Options.Handle);

    public void Add(Descriptor descriptor)
    {
        if (descriptor.Name == DefaultName)
            Descriptors[0] = descriptor;
        else
            Descriptors.Add(descriptor);
    }

    public void Add(string name, ColumnFamilyOptions options) => Add(new Descriptor(name, options));

    public IEnumerator<Descriptor> GetEnumerator() => Descriptors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => Descriptors.GetEnumerator();
}
