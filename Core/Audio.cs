using System.Collections.Immutable;

namespace Core;

public record Audio(ImmutableArray<byte> Data);