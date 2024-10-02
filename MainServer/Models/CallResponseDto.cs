using System.Text.Json.Serialization;

namespace MainServer.Models;

public class CallResponseDto
{
    public required int Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("location")]
    public required string Location { get; set; }
    [JsonPropertyName("text")]
    public required string Text { get; set; }
    [JsonPropertyName("emotion_tone")]
    public required string EmotionTone { get; set; }
    [JsonPropertyName("categories")]
    public required string[] Categories { get; set; }
}