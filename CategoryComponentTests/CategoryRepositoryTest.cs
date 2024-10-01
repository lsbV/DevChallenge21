using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using CategoryComponent;
using Core;
using Database;
using Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CategoryComponentTests;

[TestClass]
public class CategoryRepositoryTest
{
    private static IMapper _mapper = null!;
    private CategoryRepository _repository = null!;
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
        _repository = new CategoryRepository(_context, _mapper);
    }

    [TestMethod]
    public async Task CreateCategoryAsyncTestShouldPass()
    {
        var category = new Category(new CategoryId(0), new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")));


        var actual = await _repository.CreateCategoryAsync(category);



        Assert.IsTrue(actual.Id != CategoryId.Default);
        Assert.AreEqual(category.Title, actual.Title);
        Assert.IsTrue(category.Points.SetEquals(actual.Points));
    }

    [TestMethod]
    public async Task DeleteCategoryAsyncTestShouldPass()
    {
        var categoryId = new CategoryId(1);

        var expected = await _repository.GetCategoryAsync(categoryId);
        var actual = await _repository.DeleteCategoryAsync(categoryId);

        Assert.AreEqual(expected.Id, actual.Id);
        Assert.AreEqual(expected.Title, actual.Title);
        Assert.IsTrue(expected.Points.SetEquals(actual.Points));
    }

    [TestMethod]
    public async Task UpdateCategoryTestShouldPass()
    {
        var categoryId = new CategoryId(1);

        var category = await _repository.GetCategoryAsync(categoryId);
        var newCategory = category with { Title = new Title("New Title") };

        var actual = await _repository.UpdateCategoryAsync(newCategory);

        Assert.AreEqual(newCategory.Title, actual.Title);
    }

    [TestMethod]
    public async Task GetCategoriesAsyncTestShouldPass()
    {
        var categories = await _context.Categories.ToListAsync();
        var expected = categories.Select(_mapper.Map<Category>);


        var actual = await _repository.GetCategoriesAsync();
        CollectionAssert.AreEqual(expected.ToList(), actual.ToList(), new CategoryComparer());
    }

    private class CategoryComparer : IComparer
    {
        private static int Compare(Category? x, Category? y)
        {
            return x?.Id.Value.CompareTo(y?.Id.Value) ?? 0;
        }

        public int Compare(object? x, object? y)
        {
            if (x is Category xCategory && y is Category yCategory)
            {
                return Compare(xCategory, yCategory);
            }
            return 0;
        }
    }

}