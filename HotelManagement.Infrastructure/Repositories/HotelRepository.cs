using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;

namespace HotelManagement.Infrastructure.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelServices
    {
        private readonly IHotelServices hotelRepository;
        public HotelRepository(HotelDbContext context, IHotelServices _hotelRepository)
        {
            hotelRepository = _hotelRepository;
        }

        public async Task<Hotel> GetHotelByIdAsync(int Id)
        {
            var hotel = await GetByIdAsync(Id);
            if (hotel == null) { return null; }
            return hotel;
        }

        public async Task<List<Hotel>> GetHotelsAsync()
        {
            var hotels = await GetAllAsync();
            return hotels.ToList();
        }

        public async Task<Hotel> UpdateHotelAsync(int Id)
        {
            var hotel = await GetByIdAsync(Id);
            if (hotel == null) { return null; }
            return hotel;
        }
    }
}
