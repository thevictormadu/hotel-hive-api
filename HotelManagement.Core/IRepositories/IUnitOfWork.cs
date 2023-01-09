namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {

      
     IReviewRepository ReviewRepository { get; }
     

        IHotelRepository hotelRepository { get; }
        IRoomRepository roomRepository { get; }
        IAmenityRepository AmenityRepository { get; }
        

        void SaveChanges();


        void BeginTransaction();

        void Rollback();
        Task SaveChangesAsync();
    }
}
