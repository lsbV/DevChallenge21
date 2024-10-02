using System.Collections.Immutable;

namespace Core;

public record Call(
    CallId Id,
    EmotionalTone Tone,
    Transcription Transcription,
    ImmutableHashSet<Person> People,
    ImmutableHashSet<Location> Locations,
    ImmutableList<Topic> Topics,
    Status Status)
{
    public static Call Empty => new(
        CallId.Default,
        EmotionalTone.Neutral,
        new Transcription(string.Empty),
        ImmutableHashSet<Person>.Empty,
        ImmutableHashSet<Location>.Empty,
        ImmutableList<Topic>.Empty,
        Status.Processing
    );
}

public enum Status
{
    Processing,
    Completed
}