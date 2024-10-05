using System.Collections.Immutable;
using TopicComponent;
using Core;
using Moq;

namespace TopicComponentTests;

[TestClass]
public sealed class TopicServiceTest
{
    private TopicService _service = null!;
    private Mock<ITopicRepository> _mockRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new Mock<ITopicRepository>();
        _service = new TopicService(_mockRepository.Object);
    }

    private static Topic CreateTopic(TopicId id) =>
        new Topic(
            id,
            new Title("Title"),
            ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")),
            CallId.Default);

    [TestMethod]
    public async Task CreateTopicAsyncTestShouldPassCorrectly()
    {
        var Topic = CreateTopic(new TopicId(1));
        var expected = Topic with { Id = new TopicId(1) };
        _mockRepository.Setup(x => x.CreateTopicAsync(Topic)).ReturnsAsync(Topic);

        var actual = await _service.CreateTopicAsync(Topic);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public async Task DeleteTopicAsyncTestShouldPassCorrectly()
    {
        var TopicId = new TopicId(10);
        var Topic = CreateTopic(TopicId);
        _mockRepository.Setup(x => x.DeleteTopicAsync(TopicId)).ReturnsAsync(Topic);

        var actual = await _service.DeleteTopicAsync(TopicId);

        Assert.AreEqual(Topic, actual);
    }

    [TestMethod]
    public async Task GetCategoriesAsyncTestShouldPassCorrectly()
    {
        List<Topic> categories = [CreateTopic(new TopicId(1)),
            CreateTopic(new TopicId(2)),
            CreateTopic(new TopicId(3))
        ];
        _mockRepository.Setup(x => x.GetCategoriesAsync()).ReturnsAsync(categories);

        var actual = await _service.GetCategoriesAsync();

        Assert.AreEqual(categories, actual);
    }

    [TestMethod]
    public async Task GetTopicAsyncTestShouldPassCorrectly()
    {
        var TopicId = new TopicId(10);
        var Topic = CreateTopic(TopicId);
        _mockRepository.Setup(x => x.GetTopicAsync(TopicId)).ReturnsAsync(Topic);
        var actual = await _service.GetTopicAsync(TopicId);
        Assert.AreEqual(Topic, actual);
    }

    [TestMethod]
    public async Task UpdateTopicAsyncTestShouldPassCorrectly()
    {
        var Topic = CreateTopic(new TopicId(1));
        var expected = Topic with { Title = new Title("New Title") };
        _mockRepository.Setup(x => x.UpdateTopicAsync(Topic)).ReturnsAsync(expected);

        var actual = await _service.UpdateTopicAsync(Topic);

        Assert.AreEqual(expected, actual);
    }


}