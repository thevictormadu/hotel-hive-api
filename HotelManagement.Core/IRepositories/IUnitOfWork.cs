using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{ 
    public interface IUnitOfWork : IDisposable
    {

     void SaveChanges();

     void BeginTransaction();

     void Rollback();

            
    }
}
