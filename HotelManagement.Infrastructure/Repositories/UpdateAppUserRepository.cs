using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
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
    public class UpdateAppUserRepository : GenericRepository<AppUser>, IUpdateUserAppRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public UpdateAppUserRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }

      
        
       

        public async Task UpdateAsync(AppUser user)
        {
            _hotelDbContext.Entry(user).State = EntityState.Modified;
            await _hotelDbContext.SaveChangesAsync();
        }
    }
}
