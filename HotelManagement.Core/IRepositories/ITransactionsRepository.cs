using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using HotelManagement.Core.Domains;
namespace HotelManagement.Core.IRepositories
{
    public interface ITransactionsRepository : IGenericRepository<Payment>
    {

        Task<Manager> GetHotelManager(string managerId);
        Task<Hotel> GetAllRoomsTransaction(string hotelId);

        Task<IQueryable<Payment>> GetAllCustomerTransactionsForAHotel(string hotelId, string customerId);

        Task<IQueryable<Customer>> GetAllUsersTransaction();
    }
}


