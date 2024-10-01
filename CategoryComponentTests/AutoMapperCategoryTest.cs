using System.Collections.Immutable;
using AutoMapper;
using CategoryComponent;
using Core;
using Database.DbModels;

namespace CategoryComponentTests;

[TestClass]
public class AutoMapperCategoryTest
{
    private static IMapper _mapper = null!;
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        _mapper = new Mapper(configuration);
    }

    [TestMethod]
    public void AutoMapperShouldMapCategoryIntoDbCategory()
    {
        var category = new Category(new CategoryId(1), new Title("Title"), ImmutableHashSet.Create(new Point("Point1"), new Point("Point1")));

        var dbCategory = _mapper.Map<DbCategory>(category);

        Assert.AreEqual(category.Id.Value, dbCategory.Id);
        Assert.AreEqual(category.Title.Value, dbCategory.Title);
        Assert.IsTrue(dbCategory.Points.SetEquals(category.Points.Select(p => p.Value)));

    }

    [TestMethod]
    public void AutoMapperShouldMapDbCategoryIntoCategory()
    {
        var dbCategory = new DbCategory { Id = 1, Title = "Title", Points = ["Tag1", "Tag2"] };

        var category = _mapper.Map<Category>(dbCategory);

        Assert.AreEqual(dbCategory.Id, category.Id.Value);
        Assert.AreEqual(dbCategory.Title, category.Title.Value);
        //Assert.IsTrue(dbCategory.Points.SetEquals(category.Points.Select(p => p.Value)));

    }
}