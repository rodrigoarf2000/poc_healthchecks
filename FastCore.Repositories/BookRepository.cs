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
    public interface IAuthorRepository
    {
        Task AddAsync(Author entity);
        Task UpdateAsync(Author entity);
        Task DeleteAsync(Expression<Func<Author, bool>> predicate);
        Task<Author> GetItemAsync(int bookId);
        Task<List<Author>> GetAllAsync();
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookContext _context;

        public async Task AddAsync(Author entity)
        {
            try
            {
                _context.Set<Author>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Expression<Func<Author, bool>> predicate)
        {
            try
            {
                var collection = _context.Set<Author>().AsNoTracking().Where(predicate).ToList();

                if (collection.Any())
                {
                    foreach (var item in collection)
                    {
                        _context.Set<Author>().Remove(item);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Author>> GetAllAsync()
        {
            try
            {
                var collection = await Task.FromResult(_context.Set<Author>().ToList());
                return collection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Author> GetItemAsync(int bookId)
        {
            try
            {
                var entity = await Task.FromResult(_context.Set<Author>().Where(x => x.AuthorId == bookId).FirstOrDefault());
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Author entity)
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
