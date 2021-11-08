using FastCore.Application.Commom;
using FastCore.Application.Entities;
using FastCore.Repositories;
using FastCore.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastCore.Application
{
    public interface ICategoryApplication
    {
        Task AddAsync(CategoryVm model);
        Task UpdateAsync(CategoryVm model);
        Task DeleteAsync(CategoryVm model);
        Task<CategoryVm> GetItemAsync(int categoryId);
        Task<List<CategoryVm>> GetAllAsync();
    }

    public class CategoryApplication : ICategoryApplication
    {
        private readonly IConfiguration _configuration;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplication(IConfiguration configuration,ICategoryRepository categoryRepository)
        {
            _configuration = configuration;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(CategoryVm model)
        {
            var entity = model.ToModelView<Category, CategoryVm>();
            await _categoryRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(CategoryVm model)
        {
            var entity = model.ToModelView<Category, CategoryVm>();
            await _categoryRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(CategoryVm entity)
        {
            await _categoryRepository.DeleteAsync(x => x.CategoryId == entity.CategoryId);
        }

        public async Task<CategoryVm> GetItemAsync(int categoryId)
        {
            var entity = await _categoryRepository.GetItemAsync(categoryId);
            var result = entity.ToViewModel<Category, CategoryVm>();
            return result;
        }

        public async Task<List<CategoryVm>> GetAllAsync()
        {
            var collection = await _categoryRepository.GetAllAsync();
            var result = collection.ToViewModel<List<Category>, List<CategoryVm>>();
            return result;
        }
    }
}
