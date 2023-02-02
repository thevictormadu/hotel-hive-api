using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IManagerRepository:IGenericRepository<Manager>
    {
        Task<Manager> GetManager(string Id);

        Manager GetBookingPerManager(string Id);
    }
}
