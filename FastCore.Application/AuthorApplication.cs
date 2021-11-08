using FastCore.Application.Commom;
using FastCore.Application.Entities;
using FastCore.Repositories;
using FastCore.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastCore.Application
{
    public interface IAuthorApplication
    {
        Task AddAsync(AuthorVm model);
        Task UpdateAsync(AuthorVm model);
        Task DeleteAsync(AuthorVm model);
        Task<AuthorVm> GetItemAsync(int bookId);
        Task<List<AuthorVm>> GetAllAsync();
    }

    public class AuthorApplication : IAuthorApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthorRepository _bookRepository;

        public AuthorApplication(IConfiguration configuration, IAuthorRepository bookRepository)
        {
            _configuration = configuration;
            _bookRepository = bookRepository;
        }

        public async Task AddAsync(AuthorVm model)
        {
            var entity = model.ToModelView<Author, AuthorVm>();
            await _bookRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(AuthorVm model)
        {
            var entity = model.ToModelView<Author, AuthorVm>();
            await _bookRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(AuthorVm entity)
        {
            await _bookRepository.DeleteAsync(x => x.AuthorId == entity.AuthorId);
        }

        public async Task<AuthorVm> GetItemAsync(int bookId)
        {
            var entity = await _bookRepository.GetItemAsync(bookId);
            var result = entity.ToViewModel<Author, AuthorVm>();
            return result;
        }

        public async Task<List<AuthorVm>> GetAllAsync()
        {
            var collection = await _bookRepository.GetAllAsync();
            var result = collection.ToViewModel<List<Author>, List<AuthorVm>>();
            return result;
        }
    }
}
