using System.Collections.Immutable;

namespace Core;

public record Call(
    CallId Id,
    Name Name,
    Location Location,
    EmotionalTone Tone,
    Transaction Transaction,
    ImmutableHashSet<Title> Categories);