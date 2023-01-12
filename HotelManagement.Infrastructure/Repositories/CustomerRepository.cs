using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly HotelDbContext _hotelDbContext;

        public CustomerRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

        public async Task<Response<List<Customer>>> GetTopHotelCustomers(string hotelId)
        {
            var topCustomers = _hotelDbContext.Customers
                                .Include(b => b.AppUser)
                                .Select(c => new {
                                    Customer = c,
                                    TotalAmount = c.Bookings.Where(a => a.Hotel.Id == hotelId)
                                                            .Select(b => b.Payment.Amount)
                                                            .Sum()
                                })
                                .OrderByDescending(c => c.TotalAmount)
                                .Select(c => c.Customer)
                                .ToList();
            return new Response<List<Customer>> { Data = topCustomers };
        }
    }
}
