using HotelManagement.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        
        public async Task CompleteAsync()
        {
            
        }
        public void Dispose()
        {
            
        }
    }
}
