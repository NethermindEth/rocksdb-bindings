// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Text;

namespace Nethermind.RocksDbBindings;

public interface Comparator
{
    string Name { get; }
    int Compare(nint a, nuint alen, nint b, nuint blen);
}

public abstract class StringComparatorBase(Encoding? encoding = null, string? name = null, nint state = default(nint)) : Comparator
{
    public Encoding Encoding { get; } = encoding ?? Encoding.UTF8;

    public string Name { get; } = name ?? typeof(StringComparatorBase).Name;

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

    public StringComparator(IComparer<string>? comparer = null, Encoding? encoding = null, string? name = null)
        : base(encoding, name)
    {
        if (comparer == null)
            comparer = StringComparer.CurrentCulture;
        CompareFunc = comparer.Compare;
    }

    public StringComparator(bool ignoreCase, Encoding? encoding = null, string? name = null)
        : this(ignoreCase ? StringComparer.CurrentCultureIgnoreCase : StringComparer.CurrentCulture, encoding, name)
    {
    }

    public override int Compare(string a, string b) => CompareFunc(a, b);
}
