using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories
{
    public class ProductoRepository(RestaurantContext dbContext) : Repository<Producto>(dbContext)
    {
        public async Task<IEnumerable<Producto>> GetProductosByCategoriaAsync(int categoriaId)
        {
            var productos = await DbSet.Where(x => x.CategoriaId == categoriaId).ToListAsync();
            return productos;
        }
    }
}
