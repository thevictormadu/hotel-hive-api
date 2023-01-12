using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class TransactionsRepository : GenericRepository<Payment>, ITransactionsRepository
    {
        protected readonly HotelDbContext _db;
        public TransactionsRepository(HotelDbContext db) : base(db)
        {
            //_looger
            _db = db;
           
        }

        public async Task<Manager> GetHotelManager(string managerId)
        {

            // Find the manager with the specified ID and include the hotels, rooms, room types, and bookings associated with the manager
            var manager = await _db.Managers
                .Include(m => m.Hotels)
                    .ThenInclude(h => h.Bookings).ThenInclude(rt => rt.RoomType)
                .FirstOrDefaultAsync(m => m.Id == managerId);

            return manager; 
        }

        public async Task<Hotel> GetAllRoomsTransaction(string hotelId)
        {
            // Find the hotel with the specified ID and include the rooms, room types, and bookings associated with the hotel
            var hotel = await _db.Hotels
                    .Include(h => h.RoomTypes)
                    .Include(h => h.Bookings)
                    .FirstOrDefaultAsync(h => h.Id == hotelId);

            return hotel;
        }

        public async Task<IQueryable<Payment>> GetAllCustomerTransactionsForAHotel(string hotelId, string customerId)
        {
            var bookings = await _db.Bookings
                            .Include(b => b.Payment)
                            .Where(b => b.Hotel.Id == hotelId && b.Customer.Id == customerId)
                            .ToListAsync();
                                
                                
            
            var payment = bookings.Select(b => b.Payment).AsQueryable();
            return payment;
        }
    }
}


