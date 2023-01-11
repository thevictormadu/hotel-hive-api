using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;

namespace HotelManagement.Infrastructure.Repositories
{
    public class TransactionsRepository : GenericRepository<Booking>, ITransactionsRepository
    {
        public TransactionsRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
        }
    }
}

