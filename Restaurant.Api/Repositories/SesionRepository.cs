using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class SesionRepository(RestaurantContext dbContext): Repository<Sesion>(dbContext), ISesionRepository
    {
        public async Task<Sesion?> GetByHashAsync(string hash)
        {
            var sesion = await DbSet.Include(x=>x.Usuario).FirstOrDefaultAsync(s => s.Hash == hash);
            return sesion;
        }
        public async Task DeleteSesionByUsuarioIdAsync(int usuarioId)
        {
           var sesiones = await DbSet.Where(s => s.UsuarioId == usuarioId).ToListAsync();
            if (sesiones.Any())
            {
                DbSet.RemoveRange(sesiones);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
