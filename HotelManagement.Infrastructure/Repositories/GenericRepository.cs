using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
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
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly HotelDbContext _hotelDbContext;
        protected DbSet<T> _dbSet;
        public IQueryable<T> Table => _dbSet;
        public IQueryable<T> TableNoTracking => _dbSet.AsNoTracking();


        public GenericRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
            _dbSet = hotelDbContext.Set<T>();
        }


        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            //await SaveAsync();
        } 

        public virtual async Task DeleteAsync(T entity)
        {
            //var entity = await _dbSet.FindAsync(value);
            _dbSet.Remove(entity);
            // entityEntry.State = EntityState.Deleted;
            //await SaveAsync();
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


        public virtual async Task<T> GetByIdAsync(string value) => await 
            _dbSet.FirstOrDefaultAsync(x => x.Id == value);

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


        public async Task UpdateAsync(T value, T entity)
        {
            var entityUpdate = await _dbSet.FindAsync(value);
            EntityEntry entityEntry = _dbSet.Update(entityUpdate);
            entityEntry.State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            await _hotelDbContext.SaveChangesAsync();
        }

       
    }
}
