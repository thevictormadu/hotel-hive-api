using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;

namespace HotelManagement.Infrastructure.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        

        public HotelRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
           
        }

        public async void AddHotel(string Manager_ID, Hotel hotel)
        {
            hotel.ManagerId = Manager_ID;
            
           await AddAsync(hotel);    
        }

        public void UpdateAsync(Hotel hotel)
        {
            throw new NotImplementedException();
        }
    }
}
