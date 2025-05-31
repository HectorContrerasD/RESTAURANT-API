using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/variante")]

    public class VarianteController(IVarianteRepository varianteRepository, IProductoRepository productoRepository) : ControllerBase
    {
        [HttpGet("producto/{id}")] // obtiene las variantes por el ID del producto (para mesero)
        public async Task<IActionResult> GetVariantesByProductoAsync([FromRoute] int id)
        {
            try
            {
                var producto = await productoRepository.GetProductoByIdAsync(id);
                if (producto == null) return NotFound("Producto no encontrado");
                var variantes = await varianteRepository.GetVariantesByProductoAsync(id);
                var response = variantes.Select(variante => new
                {
                    variante.Id,
                    variante.Nombre,
                    variante.PrecioAdicional
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
