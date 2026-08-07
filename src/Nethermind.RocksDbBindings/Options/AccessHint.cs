// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

namespace Nethermind.RocksDbBindings;

public enum AccessHint
{
    None,
    Normal,
    Sequential,
    WillNeed
}
