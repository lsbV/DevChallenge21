using System.Collections.Immutable;

namespace Core;

public record Category(CategoryId Id, Title Title, ImmutableHashSet<Point> Points);