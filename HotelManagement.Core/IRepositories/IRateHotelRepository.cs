using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IRateHotelRepository : IGenericRepository<Rating>
    {
        Task RateHotel(Rating Rating);
    }
}
