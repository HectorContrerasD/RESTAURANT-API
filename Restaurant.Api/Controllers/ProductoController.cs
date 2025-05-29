using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/producto")]

    public class ProductoController(IProductoRepository productoRepository, ICategoriaRepository categoriaRepository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriductosByCategoriaAsync([FromRoute] int id)
        {
            try
            {
                var categoria = await categoriaRepository.GetCategoriaByIdAsync(id);
                if (categoria == null) return NotFound("Categoria no encontrada");
                var productos = await productoRepository.GetProductosByCategoriaAsync(id);
                var response = productos.Select(producto => new
                {
                    producto.Nombre,
                    producto.PrecioBase,
                    producto.Id
                });
                return Ok(response);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}
