using Core;

namespace SemanticAnalysisComponent;

public interface ISemanticAnalysisService
{
    Task<Call> AnalyzeAsync(Transcription transcription, CallId callId);
}