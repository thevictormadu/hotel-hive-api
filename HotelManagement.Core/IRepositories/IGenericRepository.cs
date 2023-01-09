using System.Linq.Expressions;

namespace HotelManagement.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task AddAsync(T entity);
        Task UpdateAsync<T>(T Value, T entity);
        Task DeleteAsync<T>(T Value);
    }
}
