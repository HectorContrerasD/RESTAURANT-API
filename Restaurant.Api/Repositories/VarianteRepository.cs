using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class VarianteRepository(RestaurantContext dbContext) : Repository<Variante>(dbContext), IVarianteRepository
    {
        public async Task<List<Variante>> GetVariantesByProductoAsync(int productoId)
        {
            var variantes = await DbSet.Where(x => x.ProductoId == productoId).ToListAsync();
            return variantes;
        }
    }
}
