using System.Collections.Immutable;
using Core;
using MainServer.Controllers;
using Microsoft.Extensions.Logging;
using SemanticAnalysisComponent;

namespace CallComponent;

public class CallService(
    ISemanticAnalysisService analysisService,
    ITranscriptionService transcriptionService,
    ICallRepository repository
    ) : ICallService
{
    public async Task<CallId> ProcessCallAsync(Audio audio)
    {
        var callId = await repository.SaveCallAsync(Call.Empty);
        var transcription = await transcriptionService.TranscribeAsync(audio);
        var call = await analysisService.AnalyzeAsync(transcription, callId);
        await repository.UpdateCallAsync(call with { Status = Status.Completed });
        return callId;
    }

    public async Task<Call> GetCallByIdAsync(CallId id)
    {
        var call = await repository.GetCallByIdAsync(id);
        if (call.Status is Status.Processing)
        {
            throw new ProcessingNotFinishedException("");
        }
        return call;
    }
}