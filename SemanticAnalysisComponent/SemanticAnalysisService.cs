using System.Text;
using OllamaSharp;

namespace SemanticAnalysisComponent;

public class SemanticAnalysisService(OllamaApiClient ollamaApiClient, PromptProvider promptProvider)
{
    public async Task<string> AnalyzeAsync(string text)
    {
        var prompt = promptProvider.GetPromptForAnlysis();
        var responseStream = ollamaApiClient.Generate(prompt + text);
        var stringBuilder = new StringBuilder();
        await foreach (var item in responseStream)
        {
            stringBuilder.Append(item?.Response);
        }

        var response = stringBuilder.ToString();
        return response;


    }

}