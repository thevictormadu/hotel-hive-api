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
<<<<<<< HEAD
        private IAmenityRepository _amenityRepository;
		private ITransactionsRepository _transactionRepository;
        public UnitOfWork(HotelDbContext hotelDbContext)
=======
		private IAmenityRepository _amenityRepository;
		//public UnitOfWork(HotelDbContext hotelDbContext)
       // private IAmenityRepository _amenityRepository;
		private IBookingRepository _bookingRepository;
		public UnitOfWork(HotelDbContext hotelDbContext)
>>>>>>> cee4b77ddffcd30a6a829434f8c9a5d9e504d6a2
		{
            _hotelDbContext = hotelDbContext;
        }

		private ICustomerRepository _customerRepository;
		private ITransactionsRepository _transactionsRepository;

		//public UnitOfWork(HotelDbContext hotelDbContext)
		//{
		//	_hotelDbContext = hotelDbContext;

		//}
		public IHotelRepository hotelRepository =>
			_hotelRepository ??= new HotelRepository(_hotelDbContext );
		public IRoomRepository roomRepository =>
			_roomRepository ??= new RoomRespository(_hotelDbContext);

        public ICustomerRepository customerRepository =>
         _customerRepository ??= new CustomerRepository(_hotelDbContext);

        public IAmenityRepository AmenityRepository =>
         _amenityRepository ??= new AmenityRepository(_hotelDbContext);

<<<<<<< HEAD
		public ITransactionsRepository transactionRepository =>
		 _transactionRepository ??= new TransactionsRepository(_hotelDbContext);

		public void BeginTransaction()
=======
		public IBookingRepository bookingRepository =>
			_bookingRepository ??= new BookingRepository(_hotelDbContext);
        public ITransactionsRepository transactionsRepository =>
            _transactionsRepository ??= new TransactionsRepository(_hotelDbContext);
        public void BeginTransaction()
>>>>>>> cee4b77ddffcd30a6a829434f8c9a5d9e504d6a2
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

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
  