namespace SemanticAnalysisComponent;

internal sealed class AnalysisReportDto
{
    public required string Transcription { get; set; }
    public required string Tone { get; set; }
    public required HashSet<string> People { get; set; }
    public required HashSet<string> Locations { get; set; }
    public required HashSet<Category> Categories { get; set; }
}