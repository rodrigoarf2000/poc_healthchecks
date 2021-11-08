using FastCore.Application.Commom;
using FastCore.Application.Entities;
using FastCore.Repositories;
using FastCore.Repositories.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastCore.Application
{
    public interface IBookApplication
    {
        Task AddAsync(BookVm model);
        Task UpdateAsync(BookVm model);
        Task DeleteAsync(BookVm model);
        Task<BookVm> GetItemAsync(int bookId);
        Task<List<BookVm>> GetAllAsync();
        Task<BookVm> GetItemByIsbnAsync(string isbn);
    }

    public class BookApplication : IBookApplication
    {
        private readonly IConfiguration _configuration;
        private readonly IBookRepository _bookRepository;

        public BookApplication(IConfiguration configuration, IBookRepository bookRepository)
        {
            _configuration = configuration;
            _bookRepository = bookRepository;
        }

        public async Task AddAsync(BookVm model)
        {
            var entity = model.ToModelView<Book, BookVm>();
            await _bookRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(BookVm model)
        {
            var entity = model.ToModelView<Book, BookVm>();
            await _bookRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(BookVm entity)
        {
            await _bookRepository.DeleteAsync(x => x.BookId == entity.BookId);
        }

        public async Task<BookVm> GetItemAsync(int bookId)
        {
            var entity = await _bookRepository.GetItemAsync(bookId);
            var result = entity.ToViewModel<Book, BookVm>();
            return result;
        }

        public async Task<BookVm> GetItemByIsbnAsync(string isbn)
        {
            var entity = await _bookRepository.GetItemByFiltersAsync(x => x.Isbn == isbn);
            var result = entity.ToViewModel<Book, BookVm>();
            return result;
        }

        public async Task<List<BookVm>> GetAllAsync()
        {
            var collection = await _bookRepository.GetAllAsync();
            var result = collection.ToViewModel<List<Book>, List<BookVm>>();
            return result;
        }
    }
}
