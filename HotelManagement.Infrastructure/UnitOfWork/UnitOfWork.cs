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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _hotelDbContext;
        private bool _disposed;

    public UnitOfWork (HotelDbContext hotelDbContext)
	{
        _hotelDbContext = hotelDbContext;
	}
       

    public void BeginTransaction()
    {
       _disposed = false;
    }


    public void SaveChanges()
    {
       _hotelDbContext.SaveChangesAsync();
    }

    public void Rollback()
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
