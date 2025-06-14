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
                    Ticket = item.Ticket != null ? new
                    {
                        item.Ticket.Id,
                        Mesa = item.Ticket.Mesa != null ? new
                        {
                            item.Ticket.Mesa.Numero
                        }:null,
                    } : null,
                   
                    Producto = item.Producto != null ? new
                    {
                        item.Producto.Id,
                        item.Producto.Nombre
                    } : null,
                    Variante = item.Variante != null ? new
                    {
                        item.Variante.Id,
                        item.Variante.Nombre

                    } : null
                }) ;

                return Ok(response);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        [HttpPut("{id}/preparacion")]
        public async Task<IActionResult> UpdateItemEnPreparacion([FromRoute] int id)
        {
            try
            {
                var item = await ticketItemRepository.GetByIdAsync(id);
                if (item == null) return NotFound();
                item.Estado = Constants.Preparacion;
                await ticketItemRepository.UpdateAsync(item);
                return Ok();
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        [HttpPut("{id}/listo")]
        public async Task<IActionResult> UpdateItemListo([FromRoute] int id)
        {
            try
            {
                var item = await ticketItemRepository.GetByIdAsync(id);
                if (item == null) return NotFound();
                item.Estado = Constants.Listo;
                await ticketItemRepository.UpdateAsync(item);
                return Ok();
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}
