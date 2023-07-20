using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CategoryManager : ICategoryService
    {
        private readonly IRepositoryManager _manager;

        public CategoryManager(IRepositoryManager repositoryManager)
        {
            _manager = repositoryManager;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool trackChanges)
        {
            return await _manager.CategoryRepository.GetAllCategoriesAsync(trackChanges);
        }

        public async Task<Category> GetCategoryByIdAsync(int id, bool trackChanges)
        {
            return await _manager.CategoryRepository.GetCategoryByIdAsync(id, trackChanges);
        }
    }
}
