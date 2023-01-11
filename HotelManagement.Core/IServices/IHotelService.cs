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
 
        Task<Response<string>> AddHotel(string Manager_ID, AddHotelDto addHotelDto);

        Task<Response<string>> DeleteHotelById(string id);
        Task<Response<GetRoomDto>> GetAvailableRoomsBy(string HotelName, string roomId);
        Task<Response<List<GetHotelByRatingsDto>>> GetHotelByState(string State);

    }
}
