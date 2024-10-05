using AutoMapper;
using Core;
using OllamaSharp;
using SemanticAnalysisComponent;

namespace SemanticAnalysisComponentTests;

[TestClass]
public sealed class SemanticAnalysisServiceTests
{
    private static OllamaSemanticAnalysisService _semanticAnalysisService = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var uri = new Uri("http://localhost:11434");
        var modelName = "llama3.1:latest";
        var ollama = new OllamaApiClient(uri)
        {
            SelectedModel = modelName
        };
        var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>()));
        _semanticAnalysisService = new OllamaSemanticAnalysisService(ollama, new PromptProvider(), mapper);

    }


    [Ignore]
    public async Task AnalyzeAsync_WhenCalledWithText_ReturnsAnalysis()
    {
        // Arrange
        const string text = "Good morning, this is Olena Petrova calling from Odessa. I’m planning a trip to Libya for business purposes, and I have some concerns. Can you provide me with the latest travel advisories for Libya? I’ve heard that there might be some security risks, and I want to make sure I’m well-prepared.Good morning, Ms. Petrova. Yes, Libya is currently classified as a high-risk travel destination. We advise Ukrainian citizens to avoid non-essential travel to the region due to ongoing conflicts and safety concerns. If you must travel, we highly recommend registering with our embassy in Tripoli and ensuring you have all necessary emergency contacts.Thank you for the information. I’ll definitely register. I’m also wondering, do I need any special documentation or permits to enter the country, aside from my visa?In addition to your visa, you'll need to ensure that you have a business invitation from a registered organization in Libya. Furthermore, we recommend that you verify your vaccination status and carry proof of health insurance that covers emergency evacuation.I understand. Is there any additional advice you can offer regarding safety while I’m there?Yes, we strongly advise that you limit your movements to essential areas and avoid public gatherings. Stay in contact with our embassy, and if possible, consider hiring local security for transportation. Please keep updated with the latest advisories during your stay.Thank you, this has been very helpful. I’ll follow your recommendations.";
        var transcription = new Transcription(text);
        // Act
        var actual = await _semanticAnalysisService.AnalyzeAsync(transcription, new CallId(0));
        // Assert

        Assert.IsNotNull(actual);
    }
}