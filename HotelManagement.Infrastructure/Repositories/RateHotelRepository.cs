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
    public class RateHotelRepository : GenericRepository<Rating>, IRateHotelRepository 
    {
        private readonly HotelDbContext _hotelDbContext;

        public RateHotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            _hotelDbContext= hotelDbContext;
        }
        public async Task RateHotel(Rating rating)
        {
            await AddAsync(rating);
            await _hotelDbContext.SaveChangesAsync();

        }
    }
}
