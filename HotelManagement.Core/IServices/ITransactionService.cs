using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface ITransactionService
    {
        Task<Response<List<RoomTransactionDTO>>>GetRoomTransactionsByManger(string managerId);
        Task<Response<List<RoomTransactionDTO>>> GetAllRoomsTransactions(string hotelId);
        Task<Response<List<PaymentDTO>>> GetAllCustomerTransactionForAnHotel(string customerId, string hotelId, int pageNumber, int pageSize);

        Task<Response<List<GetCustomerDto>>> GetAllUsersTransactionAsync();


    }
}
