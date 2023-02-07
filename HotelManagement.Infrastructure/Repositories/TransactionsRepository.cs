using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Infrastructure.Repositories
{
    public class TransactionsRepository : GenericRepository<Payment>, ITransactionsRepository
    {
        protected readonly HotelDbContext _db;
        public TransactionsRepository(HotelDbContext db) : base(db)
        {
            //_looger
            _db = db;
           
        }

        public async Task<Manager> GetHotelManager(string managerId)
        {

            // Find the manager with the specified ID and include the hotels, rooms, room types, and bookings associated with the manager
            var manager = await _db.Managers
                .Include(m => m.Hotels)
                .Include(m => m.Hotels).ThenInclude(b => b.Bookings)
                .Include(m => m.Hotels).ThenInclude(rt => rt.RoomTypes)
                    //.ThenInclude(h => h.Bookings).ThenInclude(rt => rt.RoomType)
                .FirstOrDefaultAsync(m => m.Id == managerId);

            return manager; 
        }

        public async Task<Hotel> GetAllRoomsTransaction(string hotelId)
        {
            // Find the hotel with the specified ID and include the rooms, room types, and bookings associated with the hotel
            var hotel = await _db.Hotels
                    .Include(h => h.RoomTypes)
                    .Include(h => h.Bookings)
                    .FirstOrDefaultAsync(h => h.Id == hotelId);

            return hotel;
        }

        public async Task<IQueryable<Payment>> GetAllCustomerTransactionsForAHotel(string hotelId, string customerId)
        {
            var bookings = await _db.Bookings
                            .Include(b => b.Payment)
                            .Where(b => b.Hotel.Id == hotelId && b.Customer.Id == customerId)
                            .ToListAsync();
                                
                                            
            var payment = bookings.Select(b => b.Payment).AsQueryable();
            return payment;
        }
        public async Task<IQueryable<Customer>> GetAllUsersTransaction()
        {
            var paidCustomers =new List<Customer>();

            var allBookings = new List<Booking>();

            var customers = _db.Customers
                            .Include(x => x.AppUser)
                            .Include(x => x.Bookings)
                              .ThenInclude(x => x.Payment)
                            .Where(x => x.Bookings != null);

            foreach (var customer in customers)
            {
                if (customer.Bookings != null)
                {
                    allBookings.AddRange(customer.Bookings);
                }             
            }

            foreach (var booking in allBookings)
            {
                if (booking.Payment != null)
                {
                    paidCustomers.Add(booking.Customer);
                }
            }

            return paidCustomers.AsQueryable();
        }

        public async Task<IQueryable<Payment>> GetAllTransactionForManager(string managerId)
        {
            var payments = new List<Payment>();

            var allBookings = new List<Booking>();

            var customers =await _db.Managers
                            .Include(x => x.Hotels)
                                .ThenInclude(x => x.Bookings)
                                    .ThenInclude(x => x.Payment)
                             .Where(b => b.Id == managerId)
                            .FirstOrDefaultAsync();

            if ( customers != null)
            {
                foreach (var hotels in customers.Hotels)
                {
                    if (hotels.Bookings != null)
                    {
                        allBookings.AddRange(hotels.Bookings);
                    }
                }

                foreach (var booking in allBookings)
                {
                    if (booking.Payment != null)
                    {
                        payments.Add(booking.Payment);
                    }
                }
            }
           

            return payments.AsQueryable();
        }


        public async Task<IQueryable<Payment>> AllUserTransactions(string customerId)
        {
            var payments = new List<Payment>();

            var allBookings = new List<Booking>();

            var customers = await _db.Customers
                                .Include(x => x.Bookings)
                                    .ThenInclude(x => x.Payment)
                             .Where(b => b.Id == customerId)
                            .FirstOrDefaultAsync();

            if (customers != null)
            {
                foreach (var booking in customers.Bookings)
                {
                    if (booking != null)
                    {
                        allBookings.Add(booking);
                    }
                }

                foreach (var booking in allBookings)
                {
                    if (booking.Payment != null)
                    {
                        payments.Add(booking.Payment);
                    }
                }
            }
            

            return payments.AsQueryable();
        }


    }
}


