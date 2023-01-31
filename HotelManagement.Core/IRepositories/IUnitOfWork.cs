namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {

      
        //IReviewRepository ReviewRepository { get; }
     

        IHotelRepository hotelRepository { get; }
        IRoomRepository roomRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        IWishlistRepository wishlist { get; }
        ICustomerRepository customerRepository { get; }
        IManagerRequestRepository managerRequestRepository { get; }
        IManagerRepository managerRepository { get; }
        IReviewRepository reviewRepository { get; }

        IBookingRepository bookingRepository { get; }
        ITransactionsRepository transactionRepository { get; }
        IRateHotelRepository rateHotelRepository { get; }

        public IUpdateUserAppRepository UpdateAppUserRepository { get; }

        void SaveChanges();

        void BeginTransaction();

        void Rollback();
        Task SaveChangesAsync();
    }
}
