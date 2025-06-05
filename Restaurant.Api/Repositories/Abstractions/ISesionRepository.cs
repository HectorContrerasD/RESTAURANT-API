using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ISesionRepository : IRepository<Sesion>
    {
        public  Task<Sesion?> GetByHashAsync(string hash);

        public  Task DeleteSesionByUsuarioIdAsync(int usuarioId);
       
    }
}