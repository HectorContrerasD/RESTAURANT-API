using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class Repository<T>(DbContext dbContext) : IRepository<T> where T : class
    {
        public DbContext DbContext => dbContext;

        protected DbSet<T> DbSet = dbContext.Set<T>();

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var model = await DbSet.FindAsync(id);
            return model;
        }

        public virtual async Task InsertAsync(T model)
        {
            DbSet.Add(model);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T model)
        {
            DbSet.Update(model);
            await DbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T model)
        {
            DbSet.Remove(model);
            await DbContext.SaveChangesAsync();
        }
    }
}
