using System.Text.Json.Serialization;

namespace SemanticAnalysisComponent;

internal sealed class AnalysisReportDto
{
    [JsonPropertyName("transcription")]
    public required string Transcription { get; set; }

    [JsonPropertyName("tone")]
    public required string Tone { get; set; }

    [JsonPropertyName("people")]
    public required HashSet<string> People { get; set; }

    [JsonPropertyName("locations")]
    public required HashSet<string> Locations { get; set; }

    [JsonPropertyName("topics")]
    public required HashSet<TopicDto> Topics { get; set; }
}