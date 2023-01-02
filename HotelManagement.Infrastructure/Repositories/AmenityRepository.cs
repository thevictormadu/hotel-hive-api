using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class AmenityRepository : GenericRepository<Amenity>, IAmenityRepository
    {
        // private readonly ILogger _logger;
        protected readonly HotelDbContext _db;
        public AmenityRepository(HotelDbContext db) : base(db)

        {
            // _logger = logger;
            _db = db;
        }

        public async Task<Amenity> UpdateAsync(Amenity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _db.Amenities.Update(entity);
            //await _db.SaveChangesAsync();
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Amenity entity)
        {
            //var entity = await _dbSet.FindAsync(value);
            _dbSet.Remove(entity);
            // entityEntry.State = EntityState.Deleted;
            //await SaveAsync();
        }
    }
}
