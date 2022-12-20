using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class HotelRepository : GenericRepository<Hotel>,IHotelRepository
    {
        private readonly HotelDbContext _context;

        public HotelRepository(HotelDbContext context)
        {
            _context = context;
        }



        public async Task<Room> GetHotelRoomByRoomId(string HotelName, int RoomId)
        {
            try
            {
                var ListOfHotel = await _context.Hotels.Where(x => x.Name.ToLower().Trim() == HotelName.ToLower().Trim()).Select(x => x).ToListAsync();
                var roomtype = ListOfHotel.Select(x => x.RoomTypes).FirstOrDefault();
                var ListOfRooms = roomtype.Select(x => x.Room).FirstOrDefault();
                var room = ListOfRooms.Where(x => x.Id == RoomId).FirstOrDefault();
                return room;
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<List<ICollection<Rating>>> GetHotelsRating(string HotelName)
        {
            try
            {
                var ListOfHotelRating = await _context.Hotels
                    .Where(x => x.Name.ToLower().Trim() == HotelName.ToLower().Trim()).Select(x => x.Ratings).ToListAsync();
               return ListOfHotelRating;
            }
            catch (Exception ex)
            {

                return null;
            }
            
        }
        public async Task<IEnumerable<IEnumerable<Room>>> GetRoomsByAvailability(string HotelNmae)
        {
            try
            {
                var ListOfRoomType = await _context.Hotels.Where(x => x.Name == HotelNmae).Select(x => x.RoomTypes).ToListAsync();
                var ListOfRooms = ListOfRoomType.Select(x => x.Select(X => X.Room));
                var ListOfRoomsAvailable = ListOfRooms.Select(x => x.Select(x => x.Where(x => x.IsBooked == false))).SelectMany(x => x);

                if (ListOfRoomsAvailable != null)
                {
                    return ListOfRoomsAvailable;
                }
            }
            catch (Exception ex)
            {
                return null;

            }

            return null;
            
        }

    }
}
