using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class ProductoRepository(RestaurantContext dbContext) : Repository<Producto>(dbContext), IProductoRepository
    {
        public async Task<List<Producto>> GetProductosByCategoriaAsync(int categoriaId)
        {
            var productos = await DbSet.Where(x => x.CategoriaId == categoriaId).ToListAsync();
            return productos;
        }
        public async Task<Producto?> GetProductoByIdAsync(int productoId)
        {
            var producto = await DbSet.FirstOrDefaultAsync(x=>x.Id == productoId);
            return producto;
        }
    }
}
