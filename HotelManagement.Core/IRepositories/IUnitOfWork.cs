namespace HotelManagement.Core.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IHotelRepository hotelRepository { get; }

        void SaveChanges();

        void BeginTransaction();

        void Rollback();


    }
}
