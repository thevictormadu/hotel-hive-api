using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{ 
    public interface IUnitOfWork : IDisposable
    {
        
            //A method that is responsible for communicating our changes to the database
            Task CompleteAsync();

            public IHotelRepository hotel { get; }

            int Save();
        
    }
}
