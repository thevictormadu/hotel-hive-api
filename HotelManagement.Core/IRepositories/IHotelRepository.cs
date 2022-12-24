using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IHotelRepository: IGenericRepository<Hotel>
    {
        Task<List<ICollection<Rating>>> GetHotelsRating(string HotelName);
        Task<IEnumerable<Room>> GetRoomsByAvailability(string HotelNmae, string RoomType);
        Task<Room> GetHotelRoomByRoomId(string RoomId);
    }
}
