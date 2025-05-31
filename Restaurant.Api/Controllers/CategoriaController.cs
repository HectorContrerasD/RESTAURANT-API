using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/categoria")]
   
    public class CategoriaController(ICategoriaRepository categoriaRepository) : ControllerBase
    {
        [HttpGet]// obtiene todas las categorías (para mesero)
        public async Task<IActionResult> GetCategoriasAsync()
        {
            try
            {
                var categorias = await categoriaRepository.GetCategoriasAsync();
                var response = categorias.Select(categoria => new
                {
                    categoria.Id,
                    categoria.Nombre
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
