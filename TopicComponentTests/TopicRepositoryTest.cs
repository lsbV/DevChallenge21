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
        var Topic = new Topic(new TopicId(0), new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")));


        var actual = await _repository.CreateTopicAsync(Topic);



        Assert.IsTrue(actual.Id != TopicId.Default);
        Assert.AreEqual(Topic.Title, actual.Title);
        Assert.IsTrue(Topic.Points.SetEquals(actual.Points));
    }

    [TestMethod]
    public async Task DeleteTopicAsyncTestShouldPass()
    {
        var TopicId = new TopicId(1);

        var expected = await _repository.GetTopicAsync(TopicId);
        var actual = await _repository.DeleteTopicAsync(TopicId);

        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.Title, actual.Title);
        Assert.IsTrue(expected.Points.SetEquals(actual.Points));
    }

    [TestMethod]
    public async Task UpdateTopicTestShouldPass()
    {
        var TopicId = new TopicId(1);

        var Topic = await _repository.GetTopicAsync(TopicId);
        var newTopic = Topic with { Title = new Title("New Title") };

        var actual = await _repository.UpdateTopicAsync(newTopic);

        Assert.AreEqual(newTopic.Title, actual.Title);
    }

    [TestMethod]
    public async Task GetCategoriesAsyncTestShouldPass()
    {
        var categories = await _context.Topics.ToListAsync();
        var expected = categories.Select(_mapper.Map<Topic>);


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