using System.Collections.Immutable;

namespace Core;

public record Call(
    CallId Id,
    Name Name,
    Location Location,
    EmotionalTone Tone,
    Transcription Transcription,
    ImmutableHashSet<Category> Categories);