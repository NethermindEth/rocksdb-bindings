// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

public interface IMergeOperator
{
    string Name { get; }
    nint PartialMerge(nint key, nuint keyLength, nint operandsList, nint operandsListLength, int numOperands, out byte success, out nint newValueLength);
    nint FullMerge(nint key, nuint keyLength, nint existingValue, nuint existingValueLength, nint operandsList, nint operandsListLength, int numOperands, out byte success, out nint newValueLength);
    void DeleteValue(nint value, nuint valueLength);
}

public static class MergeOperators
{
    /// <summary>
    /// Combines operands into a single operand, without a base value. The operands come oldest
    /// first. Report <c>success</c> as false when they cannot be combined; RocksDB then keeps
    /// them separate and merges them later.
    /// </summary>
    public delegate byte[] PartialMergeFunc(ReadOnlySpan<byte> key, OperandsEnumerator operands, out bool success);

    /// <summary>
    /// Applies operands, oldest first, to the stored value, producing the value a read returns.
    /// Report <c>success</c> as false to fail the merge, which surfaces as a corruption error.
    /// </summary>
    /// <remarks>
    /// <c>existingValue</c> is empty both when the key has no stored value and when the stored
    /// value is itself empty, so branch on <c>hasExistingValue</c> instead of its length.
    /// </remarks>
    public delegate byte[] FullMergeFunc(ReadOnlySpan<byte> key, bool hasExistingValue, ReadOnlySpan<byte> existingValue, OperandsEnumerator operands, out bool success);


    public static IMergeOperator Create(
        string name,
        PartialMergeFunc partialMerge,
        FullMergeFunc fullMerge) => new MergeOperatorImpl(name, partialMerge, fullMerge);

    public ref struct OperandsEnumerator
    {
        private ReadOnlySpan<nint> _operandsList;
        private ReadOnlySpan<long> _operandsListLength;

        public OperandsEnumerator(ReadOnlySpan<nint> operandsList, ReadOnlySpan<long> operandsListLength)
        {
            _operandsList = operandsList;
            _operandsListLength = operandsListLength;
        }

        public int Count => _operandsList.Length;
        public unsafe ReadOnlySpan<byte> Get(int index) => new Span<byte>((void*)_operandsList[index], (int)_operandsListLength[index]);
    }


    private sealed class MergeOperatorImpl(string name, MergeOperators.PartialMergeFunc partialMerge, MergeOperators.FullMergeFunc fullMerge) : IMergeOperator
    {
        public string Name { get; } = name;
        private PartialMergeFunc PartialMerge { get; } = partialMerge;
        private FullMergeFunc FullMerge { get; } = fullMerge;

        unsafe nint IMergeOperator.PartialMerge(nint key, nuint keyLength, nint operandsList, nint operandsListLength, int numOperands, out byte success, out nint newValueLength)
        {
            var keySpan = new ReadOnlySpan<byte>((void*)key, (int)keyLength);
            var operandsListSpan = new ReadOnlySpan<nint>((void*)operandsList, numOperands);
            var operandsListLengthSpan = new ReadOnlySpan<long>((void*)operandsListLength, numOperands);
            var operands = new OperandsEnumerator(operandsListSpan, operandsListLengthSpan);

            var value = PartialMerge(keySpan, operands, out var _success);

            var ret = (nint)NativeMemory.Alloc((nuint)value.Length);
            value.CopyTo(new Span<byte>((void*)ret, value.Length));
            newValueLength = value.Length;

            success = RocksDbInterop.Bool(_success);

            return ret;
        }

        unsafe nint IMergeOperator.FullMerge(nint key, nuint keyLength, nint existingValue, nuint existingValueLength, nint operandsList, nint operandsListLength, int numOperands, out byte success, out nint newValueLength)
        {
            var keySpan = new ReadOnlySpan<byte>((void*)key, (int)keyLength);
            var operandsListSpan = new ReadOnlySpan<nint>((void*)operandsList, numOperands);
            var operandsListLengthSpan = new ReadOnlySpan<long>((void*)operandsListLength, numOperands);
            var operands = new OperandsEnumerator(operandsListSpan, operandsListLengthSpan);
            bool hasExistingValue = existingValue != nint.Zero;
            var existingValueSpan = hasExistingValue ? new ReadOnlySpan<byte>((void*)existingValue, (int)existingValueLength) : [];

            var value = FullMerge(keySpan, hasExistingValue, existingValueSpan, operands, out var _success);

            var ret = (nint)NativeMemory.Alloc((nuint)value.Length);
            value.CopyTo(new Span<byte>((void*)ret, value.Length));
            newValueLength = value.Length;

            success = RocksDbInterop.Bool(_success);

            return ret;
        }

        unsafe void IMergeOperator.DeleteValue(nint value, nuint valueLength) => NativeMemory.Free((void*)value);
    }
}
