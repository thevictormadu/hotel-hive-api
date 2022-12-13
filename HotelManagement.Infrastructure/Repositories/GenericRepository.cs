using HotelManagement.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DbSet<T> _dbSet;

        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            EntityEntry entityEntry = _dbSet.Remove(entity);
            entityEntry.State = EntityState.Deleted;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();

        }


        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> query = _dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.FirstOrDefaultAsync();
        }


        public async Task UpdateAsync(int id, T entity)
        {
            var entityUpdate = await _dbSet.FindAsync(id);
            EntityEntry entityEntry = _dbSet.Update(entityUpdate);
            entityEntry.State = EntityState.Modified;
        } 
    }
}
