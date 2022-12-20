using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;
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
        private readonly HotelDbContext _context;

        public UnitOfWork(HotelDbContext context)
        {
            _context = context;
            hotel = new HotelRepository(_context);
        }
        public IHotelRepository hotel { get; private set; }


        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            
        }
        
    }
}
