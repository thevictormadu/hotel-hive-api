using HotelManagement.Core.DTOs;

namespace HotelManagement.Core.IServices
{
    public interface IHotelService
    {
     //   Task<Response<List<GetHotelsDto>>> GetHotels();
        Task<Response<UpdateHotelDto>> UpdateHotel(UpdateHotelDto update, string Id);
      //  Task<Response<GetHotelsDto>> GetHotelById(string Id);
    }
}
