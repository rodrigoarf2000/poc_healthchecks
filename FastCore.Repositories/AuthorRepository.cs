using FastCore.Repositories.Contexts;
using FastCore.Repositories.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Repositories
{
    public interface IBookRepository
    {
        Task AddAsync(Book entity);
        Task UpdateAsync(Book entity);
        Task DeleteAsync(Expression<Func<Book, bool>> predicate);
        Task<Book> GetItemAsync(int bookId);
        Task<List<Book>> GetAllAsync();
    }

    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public async Task AddAsync(Book entity)
        {
            try
            {
                _context.Set<Book>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Expression<Func<Book, bool>> predicate)
        {
            try
            {
                var collection = _context.Set<Book>().AsNoTracking().Where(predicate).ToList();

                if (collection.Any())
                {
                    foreach (var item in collection)
                    {
                        _context.Set<Book>().Remove(item);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Book>> GetAllAsync()
        {
            try
            {
                var collection = await Task.FromResult(_context.Set<Book>().ToList());
                return collection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Book> GetItemAsync(int bookId)
        {
            try
            {
                var entity = await Task.FromResult(_context.Set<Book>().Where(x => x.BookId == bookId).FirstOrDefault());
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Book entity)
        {
            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
