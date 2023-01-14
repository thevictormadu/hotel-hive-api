using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        protected readonly HotelDbContext _context;
        public CustomerRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            _context = hotelDbContext;
        }

        public async Task<List<Customer>> GetCustomersByHotel(string hotelId)
        {
            try
            {
              var customers = await _context.Customers
             .Where(c => c.Bookings.Any(b => b.HotelId == hotelId))
             .ToListAsync();
                return customers;
            }
            catch (Exception)
            {
               

                throw;
            }

        }
    }
}
