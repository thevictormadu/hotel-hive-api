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
		private IHotelRepository _hotelRepository;
		private IRoomRepository _roomRepository;
        private IAmenityRepository _amenityRepository;
		private ITransactionsRepository _transactionRepository;
		private IManagerRepository _managerRepository;
        private ICustomerRepository _customerRepository;
        private IBookingRepository _bookingRepository;
        private IWishlistRepository _wishlistRepository;
        private IManagerRequestRepository _managerRequestRepository;
        private IRateHotelRepository _rateHotelRepository;
        private IUpdateUserAppRepository _updateAppUserRepository;
        public UnitOfWork(HotelDbContext hotelDbContext)
		{
            _hotelDbContext = hotelDbContext;
        }

		//private IAmenityRepository _amenityRepository
		//public UnitOfWork(HotelDbContext hotelDbContext)
       // private IAmenityRepository _amenityRepository;
		//private IBookingRepository _bookingRepository;
		private IReviewRepository _reviewRepository;


		public IHotelRepository hotelRepository =>
			_hotelRepository ??= new HotelRepository(_hotelDbContext );
		public IRoomRepository roomRepository =>
			_roomRepository ??= new RoomRespository(_hotelDbContext);
		


        public IWishlistRepository wishlist =>
          _wishlistRepository ??= new WishlistRepository(_hotelDbContext);

        public ICustomerRepository customerRepository =>
         _customerRepository ??= new CustomerRepository(_hotelDbContext);

        public IManagerRequestRepository managerRequestRepository =>
         _managerRequestRepository ??= new ManagerRequestRepository(_hotelDbContext);
        public IManagerRepository managerRepository =>
			_managerRepository ??= new ManagerRepository( _hotelDbContext );

		public IReviewRepository reviewRepository =>
			_reviewRepository ??= new ReviewRepository(_hotelDbContext);

        public IAmenityRepository AmenityRepository =>
         _amenityRepository ??= new AmenityRepository(_hotelDbContext);
        public IUpdateUserAppRepository UpdateAppUserRepository =>
         _updateAppUserRepository ??= new UpdateAppUserRepository(_hotelDbContext);


        //IReviewRepository IUnitOfWork.ReviewRepository => 
        //    _reviewRepository ??= new ReviewRepository(_hotelDbContext);




        public IBookingRepository bookingRepository =>
            _bookingRepository ??= new BookingRepository(_hotelDbContext);

        public ITransactionsRepository transactionRepository =>
		 _transactionRepository ??= new TransactionsRepository(_hotelDbContext);

        public IRateHotelRepository rateHotelRepository =>
         _rateHotelRepository ??= new RateHotelRepository(_hotelDbContext);

        public void BeginTransaction()

		{
			_disposed = false;
		}

       
    public async void SaveChanges()
    {
     await   _hotelDbContext.SaveChangesAsync();
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

		


        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
