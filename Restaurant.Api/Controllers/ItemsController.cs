using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/pedido")]
    [ApiController]
    public class ItemsController(ITicketItemRepository ticketItemRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetTicketItems()
        {
            try
            {
                var items = await ticketItemRepository.GetAllTicketItems();
                var response = items.Select(item => new
                {
                    item.Id,
                    item.Notas,
                    item.Cantidad,
                    item.CreatedAt,
                    Producto = item.Producto!= null? new
                    {
                        item.Producto.Id,
                        item.Producto.Nombre
                    }:null,
                    Variante = item.Variante!=null? new
                    {
                        item.Variante.Id,
                        item.Variante.Nombre
                     
                    } : null
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
