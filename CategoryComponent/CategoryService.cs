using Core;

namespace CategoryComponent;

internal class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<Category> CreateCategoryAsync(Category category)
    {
        return await repository.CreateCategoryAsync(category);
    }

    public async Task<Category> DeleteCategoryAsync(CategoryId categoryId)
    {
        return await repository.DeleteCategoryAsync(categoryId);
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await repository.GetCategoriesAsync();
    }

    public Task<Category> GetCategoryAsync(CategoryId categoryId)
    {
        return repository.GetCategoryAsync(categoryId);
    }

    public Task<Category> UpdateCategoryAsync(Category category)
    {
        return repository.UpdateCategoryAsync(category);
    }
}