using Core;
using SemanticAnalysisComponent;

namespace CallComponent;

public class CallService(ISemanticAnalysisService analysisService, ITranscriptionService transcriptionService, ICallRepository repository) : ICallService
{
    public async Task<CallId> ProcessCallAsync(Audio audio)
    {
        var transcription = await transcriptionService.TranscribeAsync(audio);
        var categories = await analysisService.AnalyzeAsync(transcription);

    }

    public async Task<Call> GetCallByIdAsync(CallId id)
    {

    }


}