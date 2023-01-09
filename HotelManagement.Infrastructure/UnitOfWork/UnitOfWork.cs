using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using HotelManagement.Infrastructure.Repositories;

namespace HotelManagement.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HotelDbContext _hotelDbContext;
        private bool _disposed;
        private  IWishlistRepository _wishlistRepository;

    public UnitOfWork (HotelDbContext hotelDbContext)
	{
        _hotelDbContext = hotelDbContext;
	}
       

    public void BeginTransaction()
    {
       _disposed = false;
    }

        public IWishlistRepository wishlist =>
            _wishlistRepository ??= new WishlistRepository(_hotelDbContext);
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
