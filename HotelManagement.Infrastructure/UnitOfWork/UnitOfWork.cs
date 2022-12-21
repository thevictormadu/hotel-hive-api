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
        _hotelDbContext.Database.RollbackTransaction();
    }
      

    protected virtual void Dispose(bool disposing)
    {

          if (!_disposed)
          {
              if (disposing)
              {
                _hotelDbContext.Dispose();
              }
          }

            _disposed = true;
    }

    public void Dispose()
    {
       Dispose(true);
       GC.SuppressFinalize(this);
    }

        
	}         
}
/*public class UserRepository : IUserRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public UserRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Insert(User user)
    {
        _unitOfWork.BeginTransaction();
        try
        {
            // Insert user into the database
            _unitOfWork.Commit();
        }
        catch
        {
            _unitOfWork.Rollback();
            throw;
        }
    }

    // Other repository methods not shown
}

*/