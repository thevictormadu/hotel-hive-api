using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IAmenityRepository : IGenericRepository<Amenity>
    {
        // CREATE
        Task<Amenity> UpdateAsync(Amenity entity);
        Task SaveChangesAsync();
      
        Task DeleteAsync(Amenity entity);

    }
}
