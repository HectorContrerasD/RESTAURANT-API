using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IVarianteRepository
    {
        public Task<List<Variante>> GetVariantesByProductoAsync(int productoId);
    }
}
