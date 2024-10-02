using System.Collections.Immutable;

namespace Core;

public record Topic(TopicId Id, Title Title, ImmutableHashSet<Point> Points, CallId CallId);