// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public interface ISpanDeserializer<T>
{
    T Deserialize(ReadOnlySpan<byte> buffer);
}
