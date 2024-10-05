using AutoMapper;
using Core;
using SemanticAnalysisComponent;

namespace SemanticAnalysisComponentTests;

[TestClass]
public class AutoMapperProfileTest
{
    private static readonly CallId callId = new(1);
    private static IMapper mapper = null!;
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        mapper = new Mapper(configuration);
    }

    [TestMethod]
    public void MapAnalysisReportDtoIntoCall()
    {
        var analysisReportDto = new AnalysisReportDto
        {
            Tone = EmotionalTone.Neutral.ToFriendlyString(),
            Transcription = "transcription",
            People = ["person"],
            Locations = ["location"],
            Topics =
            [
                new TopicDto()
                {
                    Title = "title",
                    Points = ["point"]
                }
            ]
        };
        var call = mapper.Map<Call>(analysisReportDto, opts => { opts.Items[nameof(CallId)] = callId; });
        Assert.AreEqual(EmotionalTone.Neutral, call.Tone);

        Assert.AreEqual("transcription", call.Transcription.Text);
        Assert.AreEqual(1, call.People.Count);
        Assert.AreEqual("person", call.People.First().Name);
        Assert.AreEqual(1, call.Locations.Count);
        Assert.AreEqual("location", call.Locations.First().Address);
        Assert.AreEqual(1, call.Topics.Count);
        Assert.AreEqual("title", call.Topics[0].Title.Value);
        Assert.AreEqual(1, call.Topics[0].Points.Count);
        Assert.AreEqual("point", call.Topics[0].Points.First().Value);
        Assert.AreEqual(Status.Processing, call.Status);


    }

}