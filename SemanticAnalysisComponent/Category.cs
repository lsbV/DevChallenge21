using System.Text.Json.Serialization;

namespace SemanticAnalysisComponent;

internal sealed class TopicDto
{
    [JsonPropertyName("title")]
    public required string Title { get; set; }

    [JsonPropertyName("points")]
    public required HashSet<string> Points { get; set; }
}