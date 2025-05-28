using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class CategoriaRepository(RestaurantContext dbContext) : Repository<Categoria>(dbContext), ICategoriaRepository
    {
        public async Task<List<Categoria>> GetCategoriasAsync()
        {
            var categorias = await DbSet.ToListAsync();
            return categorias;
        }
        public async Task<Categoria?> GetCategoriaByIdAsync(int id)
        {
            var categoria = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            return categoria;
        }
    }
}
