// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings;

public interface IComparator
{
    /// <summary>
    /// Identifies the ordering this comparator imposes. RocksDB records it when a database is
    /// created and refuses to open that database under a comparator reporting a different name,
    /// so two comparators must share a name only when they order keys identically.
    /// </summary>
    string Name { get; }

    int Compare(nint a, nuint alen, nint b, nuint blen);
}

public abstract class StringComparatorBase(Encoding? encoding = null, string? name = null) : IComparator
{
    public Encoding Encoding { get; } = encoding ?? Encoding.UTF8;

    /// <inheritdoc />
    /// <remarks>
    /// Defaults to the concrete type's name. Pass an explicit name to the constructor when one
    /// type can be configured to order keys in more than one way, as <see cref="StringComparator" />
    /// can: without it every configuration of that type reports the same name and rocksdb cannot
    /// tell one ordering from another.
    /// </remarks>
    public string Name => name ?? GetType().Name;

    public abstract int Compare(string a, string b);

    public unsafe int Compare(nint a, nuint alen, nint b, nuint blen)
    {
        var astr = Encoding.GetString((byte*)a, (int)alen);
        var bstr = Encoding.GetString((byte*)b, (int)blen);
        return Compare(astr, bstr);
    }
}

public class StringComparator : StringComparatorBase
{
    public Comparison<string> CompareFunc { get; }

    /// <summary>
    /// Orders keys with <paramref name="comparer" />.
    /// </summary>
    /// <param name="name">
    /// Identifies the ordering to rocksdb. Required, unlike on the other
    /// <see cref="StringComparatorBase" /> subclasses, because this one takes its ordering as an
    /// argument: nothing about the instance distinguishes one configuration from another, so no
    /// default could tell them apart. Give each distinct ordering its own name.
    /// </param>
    /// <param name="comparer">
    /// Defaults to <see cref="StringComparer.Ordinal" />. Avoid culture-sensitive comparers: their
    /// ordering follows the machine's current culture, so a database written on one machine can be
    /// ordered differently on another while still reporting <paramref name="name" /> and passing
    /// the comparator check that would otherwise catch it.
    /// </param>
    /// <param name="encoding">
    /// Decodes keys before they are compared. Defaults to UTF-8, matching the rest of the bindings.
    /// </param>
    public StringComparator(string name, IComparer<string>? comparer = null, Encoding? encoding = null)
        : base(encoding, name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        CompareFunc = (comparer ?? StringComparer.Ordinal).Compare;
    }

    /// <summary>
    /// Orders keys ordinally, optionally ignoring case.
    /// </summary>
    /// <param name="name">
    /// Identifies the ordering to rocksdb. Two comparators that order keys differently must not
    /// share a name, so a case-sensitive and a case-insensitive one need separate names.
    /// </param>
    /// <param name="ignoreCase">
    /// Selects <see cref="StringComparer.OrdinalIgnoreCase" /> over
    /// <see cref="StringComparer.Ordinal" />. Both are culture independent.
    /// </param>
    /// <param name="encoding">
    /// Decodes keys before they are compared. Defaults to UTF-8, matching the rest of the bindings.
    /// </param>
    public StringComparator(string name, bool ignoreCase, Encoding? encoding = null)
        : this(name, ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal, encoding)
    {
    }

    public override int Compare(string a, string b) => CompareFunc(a, b);
}
