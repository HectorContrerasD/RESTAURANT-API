using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ICategoriaRepository: IRepository<Categoria>
    {
        public Task<List<Categoria>> GetCategoriasAsync();

        public Task<Categoria?> GetCategoriaByIdAsync(int id);
        
    }
}
