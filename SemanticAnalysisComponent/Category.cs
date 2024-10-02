namespace SemanticAnalysisComponent;

internal sealed class Category
{
    public required string Title { get; set; }
    public required HashSet<string> Points { get; set; }
}