using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface ITransactionsRepository : IGenericRepository<Payment>
    {

        Task<Manager> GetHotelManager(string managerId);
        Task<Hotel> GetAllRoomsTransaction(string hotelId);
        Task<List<Customer>> GetAllUsersTransaction();
    }
}
