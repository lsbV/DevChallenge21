using Core;

namespace CategoryComponent;

internal interface ICategoryRepository
{
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<Category> DeleteCategoryAsync(CategoryId categoryId);
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<Category> GetCategoryAsync(CategoryId categoryId);
    public Task<Category> UpdateCategoryAsync(Category category);
}