using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;

namespace HotelManagement.Core.IServices
{
    public interface IHotelService
    {
        Task<Response<List<GetHotelsDto>>> GetHotels();
        Task<Response<UpdateHotelDto>> UpdateHotel(UpdateHotelDto update, string Id);
        Task<Response<GetHotelsDto>> GetHotelById(string Id);
        Task<Response<List<GetRoomDto>>> GetRoomsByAvailability(string HotelNmae, string RoomType);
        Task<Response<List<GetHotelByRatingsDto>>> GetHotelRating(string HotelName);
        Task<Response<Hotel>> Create(AddHotelDto hotelDto);
        Task<Response<List<GetRoomDto>>> GetHotelRoomsById(string HotelName, string RoomId);

    }
}
