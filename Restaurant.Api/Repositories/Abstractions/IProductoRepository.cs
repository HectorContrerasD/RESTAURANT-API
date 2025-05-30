using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IProductoRepository: IRepository<Producto>
    {
        public Task<List<Producto>> GetProductosByCategoriaAsync(int categoriaId);
        public Task<Producto?> GetProductoByIdAsync(int productoId);
    }
}
