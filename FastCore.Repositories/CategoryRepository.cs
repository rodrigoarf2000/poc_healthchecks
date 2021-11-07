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
    public interface ICategoryRepository
    {
        Task AddAsync(Category entity);
        Task UpdateAsync(Category entity);
        Task DeleteAsync(Expression<Func<Category, bool>> predicate);
        Task<Category> GetItemAsync(int bookId);
        Task<List<Category>> GetAllAsync();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly BookContext _context;

        public async Task AddAsync(Category entity)
        {
            try
            {
                _context.Set<Category>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(Expression<Func<Category, bool>> predicate)
        {
            try
            {
                var collection = _context.Set<Category>().AsNoTracking().Where(predicate).ToList();

                if (collection.Any())
                {
                    foreach (var item in collection)
                    {
                        _context.Set<Category>().Remove(item);
                    }

                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Category>> GetAllAsync()
        {
            try
            {
                var collection = await Task.FromResult(_context.Set<Category>().ToList());
                return collection;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Category> GetItemAsync(int bookId)
        {
            try
            {
                var entity = await Task.FromResult(_context.Set<Category>().Where(x => x.CategoryId == bookId).FirstOrDefault());
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Category entity)
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
