using System.Text;
using System.Text.Json;
using AutoMapper;
using OllamaSharp;

namespace SemanticAnalysisComponent;

public class OllamaSemanticAnalysisService(OllamaApiClient ollamaApiClient, PromptProvider promptProvider, IMapper mapper) : ISemanticAnalysisService
{
    public async Task<AnalysisReport> AnalyzeAsync(string text)
    {
        var prompt = promptProvider.GetPromptForAnlysis();
        var responseStream = ollamaApiClient.Generate(prompt + text);
        var stringBuilder = new StringBuilder();
        await foreach (var item in responseStream)
        {
            stringBuilder.Append(item?.Response);
        }

        var response = stringBuilder.ToString();

        var analysisReportDto = JsonSerializer.Deserialize<AnalysisReportDto>(response);
        var analysisReport = mapper.Map<AnalysisReport>(analysisReportDto);


        return analysisReport;
    }

}