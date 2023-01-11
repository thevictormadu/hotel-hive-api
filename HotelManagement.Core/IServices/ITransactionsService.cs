using HotelManagement.Core.Domains;

namespace HotelManagement.Core.IServices
{
    public interface ITransactionsServices
    {
        Task<Response<List<PaymentDTO>>> GetAllUserTransactionForAnHotel(string hotelId, string customerId, int pageNumber, int pageSize);
    }
}


