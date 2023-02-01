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
    public class ManagerRequestRepository : GenericRepository<ManagerRequest>,IManagerRequestRepository
    {
        private readonly HotelDbContext _context;

        public ManagerRequestRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            _context = hotelDbContext;
        }

        public async Task<ManagerRequest> GetManagerRequestById(string managerId)
        {
            var managerRequest = await _context.ManagerRequests.Where(manager => manager.Id == managerId).FirstOrDefaultAsync();
            return managerRequest;
        }

       
    }
}
