using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        void AddHotel(string Manager_ID, Hotel hotel);
        void UpdateAsync(Hotel hotel);
    }
}
