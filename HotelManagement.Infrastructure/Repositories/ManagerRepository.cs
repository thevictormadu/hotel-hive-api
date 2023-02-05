using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class ManagerRepository: GenericRepository<Manager>, IManagerRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public ManagerRepository(HotelDbContext hotelDbContext):base(hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<Manager> GetManager(string Id)
        {
          
            var result =  _hotelDbContext.Managers
                            .Include(c=>c.AppUser)
                            .Where(c => c.Id == Id)
                            .FirstOrDefault(m => m.Id == Id);
            return result;
        }


        public Manager GetBookingPerManager(string Id)
        {

            var result = _hotelDbContext.Managers
                            .Include(c => c.Hotels)
                                .ThenInclude(x=> x.Bookings)
                            .Where(c => c.Id == Id)
                            .FirstOrDefault();
            return result;
        }
    }
}
