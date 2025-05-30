using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IVarianteRepository:IRepository<Variante>  
    {
        public Task<List<Variante>> GetVariantesByProductoAsync(int productoId);
    }
}
