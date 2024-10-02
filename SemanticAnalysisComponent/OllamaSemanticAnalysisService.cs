using System.Text;
using System.Text.Json;
using AutoMapper;
using Core;
using OllamaSharp;

namespace SemanticAnalysisComponent;

public class OllamaSemanticAnalysisService(OllamaApiClient ollamaApiClient, PromptProvider promptProvider, IMapper mapper) : ISemanticAnalysisService
{
    public async Task<Call> AnalyzeAsync(Transcription transcription, CallId callId)
    {
        var prompt = promptProvider.GetPromptForAnlysis();
        var responseStream = ollamaApiClient.Generate(prompt + transcription.Text);
        var stringBuilder = new StringBuilder();
        await foreach (var item in responseStream)
        {
            stringBuilder.Append(item?.Response);
        }

        var response = stringBuilder.ToString();

        var analysisReportDto = JsonSerializer.Deserialize<AnalysisReportDto>(response);
        var analysisReport = mapper.Map<Call>(analysisReportDto, opts =>
        {
            opts.Items[nameof(CallId)] = callId;
        });


        return analysisReport;
    }

}