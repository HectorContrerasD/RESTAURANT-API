using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class MesaRepository(RestaurantContext dbContext) : Repository<Mesa>(dbContext), IMesaRepository
    {
        public async Task<List<Mesa>> GetMesasDisponiblesAsync()
        {
            var mesas = await DbSet.Where(x => x.Disponible == true).ToListAsync();
            return mesas;
        }
        public async Task<Mesa?> GetMesaByIdAsync(int id)
        {
            var mesa = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            return mesa;
        }
    }
}
