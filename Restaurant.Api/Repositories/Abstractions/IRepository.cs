using Microsoft.EntityFrameworkCore;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IRepository<T>
    {
        public DbContext DbContext { get; }

        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(int id);
        public Task InsertAsync(T model);
        public Task UpdateAsync(T model);
        public Task DeleteAsync(T model);
    }
}
