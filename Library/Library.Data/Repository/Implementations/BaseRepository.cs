using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class BaseRepository<T>: IBaseRepository<T> where T: class, new()
    {
        private readonly LibraryContext _context;

        public BaseRepository(LibraryContext context)
        {
            _context = context;
        }

        protected IQueryable<T> GetQuery()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return GetQuery();
        }

        public async Task<T> InsertAsync(T entity)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
