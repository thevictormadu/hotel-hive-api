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
        Task<Response<List<GetCustomerDto>>> GetCustomers(int pageNo);
        Task<Response<string>> AddCustomerAddress(AddCustomerAddressDto address);
        Task<Response<List<Customer>>> GetTopHotelCustomers(string hotelId);
    }
}
