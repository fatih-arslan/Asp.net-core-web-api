using Entities.Models;

namespace Services.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(bool trackChanges);
        Task<Category> GetCategoryByIdAsync(int id, bool trackChanges);

    }
}
