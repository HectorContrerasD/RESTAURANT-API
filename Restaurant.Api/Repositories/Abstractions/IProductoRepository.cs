using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IProductoRepository
    {
        public Task<List<Producto>> GetProductosByCategoriaAsync(int categoriaId);
    }
}
