using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IUpdateUserAppRepository : IGenericRepository<AppUser>
    {
        Task UpdateAsync(AppUser user);
    }
}
