using System.Collections.Immutable;
using Core;

namespace SemanticAnalysisComponent;

public record AnalysisReport(
    Transcription Transcription,
    EmotionalTone Tone,
    ImmutableHashSet<Person> Names,
    ImmutableHashSet<Location> Locations,
    ImmutableHashSet<Core.Topic> Categories
);