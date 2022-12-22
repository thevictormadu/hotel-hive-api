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



        public async Task<Room> GetHotelRoomByRoomId( string RoomId)
        {
            try
            {

                var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id.ToLower().Trim() == RoomId.ToLower().Trim());
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
        public async Task<IEnumerable<Room>> GetRoomsByAvailability(string HotelNmae, string RoomType)
        {
            try
            {
                var hotel = await _context.Hotels.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == HotelNmae.ToLower().Trim());
                var ListOfRoomTypes = await _context.RoomTypes.Where(x => x.HotelId.ToLower().Trim() == hotel.Id.ToLower().Trim()).ToListAsync();
                List<Room> rooms = new List<Room>();
                var ListOfAllrooms = await _context.Rooms.ToListAsync();
                foreach (var item in ListOfRoomTypes)
                {
                    var room = ListOfAllrooms.Where(x=>x.RoomTypeId == item.Id).FirstOrDefault();
                    if (room != null && room.IsBooked==false)
                    {
                        rooms.Add(room);
                    }
                }

                if (rooms != null)
                {
                    return rooms;
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
