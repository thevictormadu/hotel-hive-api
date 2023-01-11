using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class ManagerRepository: GenericRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(HotelDbContext hotelDbContext):base(hotelDbContext)
        {
                
        }
    }
}
