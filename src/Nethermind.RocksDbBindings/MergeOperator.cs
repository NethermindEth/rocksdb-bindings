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
    /// This function performs merge(left_op, right_op)
    /// when both the operands are themselves merge operation types.
    /// Save the result in *new_value and return true. If it is impossible
    /// or infeasible to combine the two operations, return false instead.
    /// This is called to combine two-merge operands (if possible)
    /// </summary>
    /// <param name="key">The key that's associated with this merge operation</param>
    /// <param name="operands">the sequence of merge operations to apply, front() first</param>
    /// <param name="success">Client is responsible for filling the merge result here</param>
    /// <returns></returns>
    public delegate byte[] PartialMergeFunc(ReadOnlySpan<byte> key, OperandsEnumerator operands, out bool success);

    /// <summary>
    /// Gives the client a way to express the read -> modify -> write semantics.
    /// Called when a Put/Delete is the *existing_value (or nullptr)
    /// </summary>
    /// <param name="key">The key that's associated with this merge operation.</param>
    /// <param name="hasExistingValue">false indicates that the key does not exist before this op</param>
    /// <param name="existingValue">empty when <paramref name="hasExistingValue"/> is false, which is
    /// also how an existing zero-length value arrives, so test that parameter rather than the length</param>
    /// <param name="operands">the sequence of merge operations to apply, front() first.</param>
    /// <param name="success">Client is responsible for filling the merge result here</param>
    /// <returns></returns>
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


    private class MergeOperatorImpl(string name, MergeOperators.PartialMergeFunc partialMerge, MergeOperators.FullMergeFunc fullMerge) : IMergeOperator
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
