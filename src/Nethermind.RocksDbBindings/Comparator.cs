// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings;

/// <summary>Orders the keys of a database or column family.</summary>
/// <remarks>
/// RocksDB calls this on every comparison it makes, so implementations belong on the byte
/// representation and should avoid allocating. An exception thrown here cannot unwind through the
/// RocksDB frames that called it and terminates the process.
/// </remarks>
public interface IComparator
{
    /// <summary>
    /// Identifies the ordering this comparator imposes. RocksDB records it when a database is
    /// created and refuses to open that database under a comparator reporting a different name,
    /// so two comparators must share a name only when they order keys identically.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Orders <paramref name="a"/> against <paramref name="b"/>: negative when it sorts first,
    /// positive when it sorts last, zero when the two are equivalent.
    /// </summary>
    /// <remarks>
    /// The spans point into RocksDB's own memory and are valid only for the duration of the call.
    /// </remarks>
    int Compare(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b);
}

/// <summary>Orders keys as the strings they decode to.</summary>
/// <remarks>
/// Decoding allocates two strings per comparison, on a path RocksDB takes for every read,
/// iteration step and compaction merge. Implement <see cref="IComparator"/> over the bytes where
/// that cost matters.
/// </remarks>
public sealed class StringComparator : IComparator
{
    private readonly Comparison<string> _compare;

    /// <param name="name">
    /// Identifies the ordering to RocksDB. Required, because instances of this type differ only in
    /// the arguments they were given: nothing about one distinguishes it from another that orders
    /// keys differently. Give each distinct ordering its own name.
    /// </param>
    /// <param name="comparer">
    /// Defaults to <see cref="StringComparer.Ordinal"/>. Avoid culture-sensitive comparers: their
    /// ordering follows the machine's current culture, so a database written on one machine can be
    /// ordered differently on another while still reporting <paramref name="name"/> and passing the
    /// comparator check that would otherwise catch it.
    /// </param>
    /// <param name="encoding">
    /// Decodes keys before they are compared. Defaults to UTF-8, matching the rest of the bindings.
    /// </param>
    public StringComparator(string name, IComparer<string>? comparer = null, Encoding? encoding = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
        Encoding = encoding ?? Encoding.UTF8;
        _compare = (comparer ?? StringComparer.Ordinal).Compare;
    }

    /// <param name="name">
    /// Identifies the ordering to RocksDB. A case-sensitive and a case-insensitive comparator
    /// order keys differently, so they need separate names.
    /// </param>
    /// <param name="ignoreCase">
    /// Selects <see cref="StringComparer.OrdinalIgnoreCase"/> over
    /// <see cref="StringComparer.Ordinal"/>. Both are culture independent.
    /// </param>
    /// <param name="encoding">
    /// Decodes keys before they are compared. Defaults to UTF-8, matching the rest of the bindings.
    /// </param>
    public StringComparator(string name, bool ignoreCase, Encoding? encoding = null)
        : this(name, ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal, encoding)
    {
    }

    /// <inheritdoc />
    public string Name { get; }

    /// <summary>Decodes keys before they are compared.</summary>
    public Encoding Encoding { get; }

    /// <inheritdoc />
    public int Compare(ReadOnlySpan<byte> a, ReadOnlySpan<byte> b)
        => Compare(Encoding.GetString(a), Encoding.GetString(b));

    /// <summary>Orders two decoded keys.</summary>
    public int Compare(string a, string b) => _compare(a, b);
}
