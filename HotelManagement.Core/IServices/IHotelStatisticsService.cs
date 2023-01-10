using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IHotelStatisticsService
    {
        Task<int> GetTotalNumberOfHotels();
        // Task<int> GetTotalNumberOfRooms(Hotel hotel);
        Task<int> GetTotalNumberOfRooms(string Id);
        // Task<int> GetTotalNumberOfRoomsOccupied(Hotel hotel);
        Task<int> GetTotalNumberOfRoomsOccupied(string Id);
        // Task<int> GetTotalNumberOfRoomsUnoccupied(Hotel hotel);
        Task<int> GetTotalNumberOfRoomsUnoccupied(string Id);
    }
}
