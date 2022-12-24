using HotelManagement.Core.Domains;

namespace HotelManagement.Core.IServices
{
    public interface IHotelService
    {
        public Task<List<Hotel>> GetHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int Id);

        public Task<Hotel> UpdateHotelAsync(int Id);
    }
}
