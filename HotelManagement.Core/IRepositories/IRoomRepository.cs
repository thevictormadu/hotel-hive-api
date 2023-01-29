using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IRoomRepository: IGenericRepository<Room>
    {
        void Add(string Roomtype_ID, Room room);
        Task<Room> DeleteAsync(string Id);

    }
}
