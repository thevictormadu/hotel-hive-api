using HotelManagement.Core.Domains;
using HotelManagement.Core.IServices;

namespace HotelManagement.Services.Services
{
    public class HotelServices : IHotelService
    {
        Task<Hotel> IHotelService.GetHotelByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<List<Hotel>> IHotelService.GetHotelsAsync()
        {
            throw new NotImplementedException();
        }

        Task<Hotel> IHotelService.UpdateHotelAsync(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
