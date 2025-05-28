using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IMesaRepository
    {
        public Task<List<Mesa>> GetMesasDisponiblesAsync();

        public Task<Mesa?> GetMesaByIdAsync(int id);
        
    }
}
