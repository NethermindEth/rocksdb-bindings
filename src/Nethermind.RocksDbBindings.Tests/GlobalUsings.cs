// SPDX-FileCopyrightText: 2026 Demerzel Solutions Limited
// SPDX-License-Identifier: MIT

// TUnit's IsEquivalentTo ignores order unless CollectionOrdering.Matching is passed. Every
// sequence this suite asserts on is ordered — key iteration, replayed batch operations, byte
// content — so the enum is used throughout and imported here.
global using TUnit.Assertions.Enums;
