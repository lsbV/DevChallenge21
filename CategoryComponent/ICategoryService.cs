using Core;

namespace CategoryComponent;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetCategoriesAsync();
    public Task<Category> GetCategoryAsync(CategoryId categoryId);
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<Category> UpdateCategoryAsync(Category category);
    public Task<Category> DeleteCategoryAsync(CategoryId categoryId);
}