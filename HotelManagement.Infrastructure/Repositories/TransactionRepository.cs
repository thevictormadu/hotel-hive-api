using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class TransactionRepository : GenericRepository<Payment>, ITransactionRepository
    {
        protected readonly HotelDbContext _db;
        public TransactionRepository(HotelDbContext db) : base(db)
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

    }
}
