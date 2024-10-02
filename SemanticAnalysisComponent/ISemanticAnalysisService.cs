namespace SemanticAnalysisComponent;

public interface ISemanticAnalysisService
{
    Task<AnalysisReport> AnalyzeAsync(string text);
}