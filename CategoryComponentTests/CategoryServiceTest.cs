using System.Collections.Immutable;
using CategoryComponent;
using Core;
using Moq;

namespace CategoryComponentTests;

[TestClass]
public sealed class CategoryServiceTest
{
    private CategoryService _service = null!;
    private Mock<ICategoryRepository> _mockRepository = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new Mock<ICategoryRepository>();
        _service = new CategoryService(_mockRepository.Object);
    }

    private static Category CreateCategory(CategoryId id) =>
        new Category(
            id,
            new Title("Title"),
            ImmutableHashSet.Create(new Point("Point1"), new Point("Point1"))
        );

    [TestMethod]
    public async Task CreateCategoryAsyncTestShouldPassCorrectly()
    {
        var category = CreateCategory(new CategoryId(1));
        var expected = category with { Id = new CategoryId(1) };
        _mockRepository.Setup(x => x.CreateCategoryAsync(category)).ReturnsAsync(category);

        var actual = await _service.CreateCategoryAsync(category);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public async Task DeleteCategoryAsyncTestShouldPassCorrectly()
    {
        var categoryId = new CategoryId(10);
        var category = CreateCategory(categoryId);
        _mockRepository.Setup(x => x.DeleteCategoryAsync(categoryId)).ReturnsAsync(category);

        var actual = await _service.DeleteCategoryAsync(categoryId);

        Assert.AreEqual(category, actual);
    }

    [TestMethod]
    public async Task GetCategoriesAsyncTestShouldPassCorrectly()
    {
        List<Category> categories = [CreateCategory(new CategoryId(1)),
            CreateCategory(new CategoryId(2)),
            CreateCategory(new CategoryId(3))
        ];
        _mockRepository.Setup(x => x.GetCategoriesAsync()).ReturnsAsync(categories);

        var actual = await _service.GetCategoriesAsync();

        Assert.AreEqual(categories, actual);
    }

    [TestMethod]
    public async Task GetCategoryAsyncTestShouldPassCorrectly()
    {
        var categoryId = new CategoryId(10);
        var category = CreateCategory(categoryId);
        _mockRepository.Setup(x => x.GetCategoryAsync(categoryId)).ReturnsAsync(category);
        var actual = await _service.GetCategoryAsync(categoryId);
        Assert.AreEqual(category, actual);
    }

    [TestMethod]
    public async Task UpdateCategoryAsyncTestShouldPassCorrectly()
    {
        var category = CreateCategory(new CategoryId(1));
        var expected = category with { Title = new Title("New Title") };
        _mockRepository.Setup(x => x.UpdateCategoryAsync(category)).ReturnsAsync(expected);

        var actual = await _service.UpdateCategoryAsync(category);

        Assert.AreEqual(expected, actual);
    }


}