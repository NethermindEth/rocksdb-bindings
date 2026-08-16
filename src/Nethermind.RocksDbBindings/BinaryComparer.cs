// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public class BinaryComparer : IEqualityComparer<byte[]>, IComparer<byte[]>
{
    public static BinaryComparer Default { get; } = new BinaryComparer();

    public int Compare(byte[]? a1, byte[]? a2)
    {
        if (a1 is null)
            return a2 is null ? 0 : -1;
        if (a2 is null)
            return 1;

        return a1.AsSpan().SequenceCompareTo(a2);
    }

    public bool Equals(byte[]? a1, byte[]? a2)
    {
        if (ReferenceEquals(a1, a2))
            return true;
        if (a1 is null || a2 is null)
            return false;

        return a1.AsSpan().SequenceEqual(a2);
    }

    public static bool PrefixEquals(byte[] a1, byte[] a2, int prefix) =>
        ReferenceEquals(a1, a2) ||
        a1.AsSpan(0, Math.Min(prefix, a1.Length)).SequenceEqual(a2.AsSpan(0, Math.Min(prefix, a2.Length)));

    public int GetHashCode(byte[] obj)
    {
        var hash = new HashCode();
        hash.AddBytes(obj);
        return hash.ToHashCode();
    }
}
