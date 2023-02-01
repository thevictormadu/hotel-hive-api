using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface ICustomerService
    {
        //Task<IQueryable<List<GetCustomerDto>>> GetCustomers(int pageNo);
        Task<Response<IEnumerable<GetCustomerDto>>> GetCustomers(int pageNo);
        Task<Response<string>> AddCustomerAddress(AddCustomerAddressDto address);
        Task<Response<List<Customer>>> GetTopHotelCustomers(string hotelId);
        Task<Response<List<GetCustomersByHotelDto>>>GetCustomersByHotelId(string hotelId);
        Task<Response<Customer>> GetCustomer(string Id);
    }
}
