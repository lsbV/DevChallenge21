using AutoMapper;
using Core;
using Core.Exceptions;
using Database;
using Database.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CategoryComponent;

internal class CategoryRepository(AppDbContext context, IMapper mapper) : ICategoryRepository
{
    public async Task<Category> CreateCategoryAsync(Category category)
    {
        category = category with { Id = CategoryId.Default };
        var dbCategory = mapper.Map<DbCategory>(category);
        await context.Categories.AddAsync(dbCategory);
        await context.SaveChangesAsync();
        context.Entry(dbCategory).State = EntityState.Detached;
        return mapper.Map<Category>(dbCategory);
    }

    public async Task<Category> DeleteCategoryAsync(CategoryId categoryId)
    {
        var dbCategory = await context.Categories.FindAsync(categoryId.Value);
        if (dbCategory is null)
        {
            throw new EntityNotExistException(nameof(Category), categoryId);
        }
        context.Categories.Remove(dbCategory);
        await context.SaveChangesAsync();
        return mapper.Map<Category>(dbCategory);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var dbCategories = await context.Categories.ToListAsync();
        return dbCategories.Select(mapper.Map<Category>);
    }

    public async Task<Category> GetCategoryAsync(CategoryId categoryId)
    {
        var dbCategory = await context.Categories.FindAsync(categoryId.Value);
        if (dbCategory is null)
        {
            throw new EntityNotExistException(nameof(Category), categoryId);
        }
        return mapper.Map<Category>(dbCategory);
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        var dbCategory = mapper.Map<DbCategory>(category);
        var foundCategory = await context.Categories.FindAsync(category.Id.Value);
        if (foundCategory is null)
        {
            throw new EntityNotExistException(nameof(Category), category.Id);
        }
        context.Entry(foundCategory).CurrentValues.SetValues(dbCategory);
        await context.SaveChangesAsync();
        context.Entry(dbCategory).State = EntityState.Detached;
        return mapper.Map<Category>(dbCategory);
    }
}