using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Response<List<Customer>>> GetTopHotelCustomers(string hotelId);
        //Task<Response<List<Customer>>> GetCustomers(int pageNo);

        Task<IQueryable<Customer>> GetCustomers(int pageNo);

        Task<List<Customer>>GetCustomersByHotel(string hotelId);
        Task<Customer> GetCustomer(string Id);
       
    }
}
