using System.Collections.Immutable;
using Core;

namespace SemanticAnalysisComponent;

public record AnalysisReport(
    Transcription Transcription,
    EmotionalTone Tone,
    ImmutableHashSet<Name> Names,
    ImmutableHashSet<Location> Locations,
    ImmutableHashSet<Core.Category> Categories
);