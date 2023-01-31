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
    public class RoomRespository: GenericRepository<Room>,IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public RoomRespository(HotelDbContext hotelDbContext):base(hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async void Add(string Roomtype_ID, Room room)
        {
            room.RoomTypeId = Roomtype_ID;
            await AddAsync(room);
        }

        public async Task<Room> DeleteAsync(string Id)
        {
            var room = await _hotelDbContext.Rooms.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (room == null) return null;
            _hotelDbContext.Rooms.Remove(room);
            return room;
        }
    }
}
