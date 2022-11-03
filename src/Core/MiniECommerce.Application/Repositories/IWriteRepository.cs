using MiniECommerce.Domain.Entities.Common;

namespace MiniECommerce.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        bool Remove(T entity);
        Task<bool> RemoveAsync(string id);
        void RemoveRange(T entities);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}
