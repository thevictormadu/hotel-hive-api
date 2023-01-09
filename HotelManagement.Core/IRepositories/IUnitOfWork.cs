namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRepository hotelRepository { get; }
        IRoomRepository roomRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        

        void SaveChanges();
     IWishlistRepository wishlist { get; }
     void SaveChanges();

        void BeginTransaction();

        void Rollback();


     
       
    }
}
