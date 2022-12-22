using HotelManagement.Core.Domains;

namespace HotelManagement.Core.IRepositories
{
    public interface IHotelServices
    {
        Task<List<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int Id);

        Task<Hotel> UpdateHotelAsync(int Id);
    }
}
