// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

using System;
using System.Runtime.InteropServices;

namespace Nethermind.RocksDbBindings;

public class RocksDbException : Exception
{
    public RocksDbException(string message)
        : base(message)
    {
    }
}
