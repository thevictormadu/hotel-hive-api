namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRepository hotelRepository { get; }
        IAmenityRepository AmenityRepository { get; }

        void SaveChanges();

        void BeginTransaction();

        void Rollback();


     
       
    }
}
