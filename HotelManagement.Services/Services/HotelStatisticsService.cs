using HotelManagement.Core;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class HotelStatisticsService : IHotelStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelStatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> GetTotalNumberOfHotels()
        {
            var hotels = await _unitOfWork.hotelRepository.GetAllAsync();
            var noOfHotels = hotels.Count();
            return noOfHotels;
        }

        public async Task<int> GetTotalNumberOfRooms(string Id)
        {
            var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            if (getHotel == null)
            {
                return -1;
            }
            var noOfRoomsInTheHotel = getHotel.RoomTypes.Count();
            return noOfRoomsInTheHotel;


        }

        public async Task<int> GetTotalNumberOfRoomsOccupied(string Id)
        {
            var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            if (getHotel == null)
            {
              
                return -1;
            }
            var noOfRoomsInTheHotelOccupied = getHotel.RoomTypes.Where(x => x.Available != 0).Count();
            return noOfRoomsInTheHotelOccupied;
            

        }

        public async Task<int> GetTotalNumberOfRoomsUnoccupied(string Id)
        {
            var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            if (getHotel == null)
            {
                return -1;
            }
            var noOfRoomsInTheHotelUnoccupied = getHotel.RoomTypes.Where(x => x.Available == 0).Count();
            return noOfRoomsInTheHotelUnoccupied;


        }
    }
}
