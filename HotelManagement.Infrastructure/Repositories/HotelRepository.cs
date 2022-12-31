using HotelManagement.Infrastructure.Context;

namespace HotelManagement.Infrastructure.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        public HotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
        }

    }
}
