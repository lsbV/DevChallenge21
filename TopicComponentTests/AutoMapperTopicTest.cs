using System.Collections.Immutable;
using AutoMapper;
using TopicComponent;
using Core;
using Database.DbModels;

namespace TopicComponentTests;

[TestClass]
public class AutoMapperTopicTest
{
    private static IMapper _mapper = null!;
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        _mapper = new Mapper(configuration);
    }

    [TestMethod]
    public void AutoMapperShouldMapTopicIntoDbTopic()
    {
        var topic = new Topic(new TopicId(1), new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")), CallId.Default);

        var dbTopic = _mapper.Map<DbTopic>(topic);

        Assert.AreEqual(topic.Id.Value, dbTopic.Id);
        Assert.AreEqual(topic.Title.Value, dbTopic.Title);
        Assert.IsTrue(dbTopic.Points.SetEquals(topic.Points.Select(p => p.Value)));

    }

    [TestMethod]
    public void AutoMapperShouldMapDbTopicIntoTopic()
    {
        var dbTopic = new DbTopic { Id = 1, Title = "Title", Points = ["Tag1", "Tag2"], CallId = 1 };

        var topic = _mapper.Map<Topic>(dbTopic);

        Assert.AreEqual(dbTopic.Id, topic.Id.Value);
        Assert.AreEqual(dbTopic.Title, topic.Title.Value);

    }
}