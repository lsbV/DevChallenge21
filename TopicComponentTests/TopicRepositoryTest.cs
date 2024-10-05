using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using TopicComponent;
using Core;
using Database;
using Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace TopicComponentTests;

[TestClass]
public class TopicRepositoryTest
{
    private static IMapper _mapper = null!;
    private TopicRepository _repository = null!;
    private AppDbContext _context = null!;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        _mapper = new Mapper(configuration);
    }

    [TestInitialize]
    public void TestInitialize()
    {
        _context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options);
        _context.Database.EnsureCreated();
        _repository = new TopicRepository(_context, _mapper);
    }

    [TestMethod]
    public async Task CreateTopicAsyncTestShouldPass()
    {
        var topic = new Topic(new TopicId(0), new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")), CallId.Default);


        var actual = await _repository.CreateTopicAsync(topic);



        Assert.IsTrue(actual.Id != TopicId.Default);
        Assert.AreEqual(topic.Title, actual.Title);
        Assert.IsTrue(topic.Points.SetEquals(actual.Points));
    }

    [TestMethod]
    public async Task DeleteTopicAsyncTestShouldPass()
    {
        var topicId = new TopicId(1);
        await _repository.CreateTopicAsync(new Topic(topicId, new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")), CallId.Default));

        var expected = await _repository.GetTopicAsync(topicId);
        var actual = await _repository.DeleteTopicAsync(topicId);

        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.Title, actual.Title);
        Assert.IsTrue(expected.Points.SetEquals(actual.Points));
    }

    [TestMethod]
    public async Task UpdateTopicTestShouldPass()
    {
        var topicId = new TopicId(1);

        await _repository.CreateTopicAsync(new Topic(topicId, new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")), CallId.Default));

        var topic = await _repository.GetTopicAsync(topicId);
        var newTopic = topic with { Title = new Title("New Title") };

        var actual = await _repository.UpdateTopicAsync(newTopic);

        Assert.AreEqual(newTopic.Title, actual.Title);
    }

    [TestMethod]
    public async Task GetTopicsAsyncTestShouldPass()
    {
        var topics = await _context.Topics.ToListAsync();
        var expected = topics.Select(_mapper.Map<Topic>);


        var actual = await _repository.GetCategoriesAsync();
        CollectionAssert.AreEqual(expected.ToList(), actual.ToList(), new TopicComparer());
    }

    private class TopicComparer : IComparer
    {
        private static int Compare(Topic? x, Topic? y)
        {
            return x?.Id.Value.CompareTo(y?.Id.Value) ?? 0;
        }

        public int Compare(object? x, object? y)
        {
            if (x is Topic xTopic && y is Topic yTopic)
            {
                return Compare(xTopic, yTopic);
            }
            return 0;
        }
    }

}