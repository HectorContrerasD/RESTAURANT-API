using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/ticket")]
    
    public class TicketController(ITicketRepository ticketRepository, ITicketItemRepository ticketItemRepository) : ControllerBase
    {
        [HttpGet("propio")]
        public async Task<IActionResult> GetOpenTickets()
        {

            var tickets = await ticketRepository.GetAllTicketsAbiertosAsync();
            var response = tickets.Select(ticket => new
            {
                ticket.Id,
                Mesa = ticket.Mesa != null ? new
                {
                    ticket.Mesa.Id,
                    ticket.Mesa.Numero,
                    Disponible = ticket.Mesa.Disponible ?? false
                } : null,
                Mesero = ticket.Mesero != null ? new
                {
                    ticket.Mesero.Id,
                    ticket.Mesero.NombreCompleto
                } : null,
                ticket.Estado,
                ticket.Total,
                ticket.CreatedAt,
                ticket.UpdatedAt,
                TicketItems = ticket.TicketItem.Select(item => new
                {
                    item.Id,
                    Producto = item.Producto != null ? new
                    {
                        item.Producto.Id,
                        item.Producto.Nombre,
                        item.Producto.PrecioBase
                    } : null,
                    Variante = item.Variante != null ? new
                    {
                        item.Variante.Id,
                        item.Variante.Nombre,
                        item.Variante.PrecioAdicional
                    } : null,
                    item.PrecioUnitario,
                    item.Cantidad,
                    item.CreatedAt,
                    item.UpdatedAt,
                    item.Notas,
                    item.Subtotal
                }).ToList()
            });
            return Ok(tickets);
        }
        [HttpGet("abierto")]
        public async Task<IActionResult> GetTicketsAbiertos()
        {
            try
            {
                var tickets = await ticketRepository.GetAllTicketsAbiertosAsync();
                var response = tickets.Select(ticket => new
                {
                    ticket.Id,
                    Mesa = ticket.Mesa != null ? new
                    {
                        ticket.Mesa.Id,
                        ticket.Mesa.Numero,
                        Disponible = ticket.Mesa.Disponible ?? false
                    } : null,
                    Mesero = ticket.Mesero != null ? new
                    {
                        ticket.Mesero.Id,
                        ticket.Mesero.NombreCompleto
                    } : null,
                    ticket.Estado,
                    ticket.Total,
                    ticket.CreatedAt,
                    ticket.UpdatedAt,
                    TicketItems = ticket.TicketItem.Select(item => new
                    {
                        item.Id,
                        Producto = item.Producto != null ? new
                        {
                            item.Producto.Id,
                            item.Producto.Nombre,
                            item.Producto.PrecioBase
                        } : null,
                        Variante = item.Variante != null ? new
                        {
                            item.Variante.Id,
                            item.Variante.Nombre,
                            item.Variante.PrecioAdicional
                        } : null,
                        item.PrecioUnitario,
                        item.Cantidad,
                        item.CreatedAt,
                        item.UpdatedAt,
                        item.Notas,
                        item.Subtotal
                    }).ToList()
                });
                return Ok(tickets);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpGet("cerrado")]
        public async Task<IActionResult> GetTicketsCerrados()
        {
            try
            {
                var tickets = await ticketRepository.GetAllTicketsCerradosAsync();
                var response = tickets.Select(ticket => new
                {
                    ticket.Id,
                    Mesa = ticket.Mesa != null ? new
                    {
                        ticket.Mesa.Id,
                        ticket.Mesa.Numero,
                        Disponible = ticket.Mesa.Disponible ?? false
                    } : null,
                    Mesero = ticket.Mesero != null ? new
                    {
                        ticket.Mesero.Id,
                        ticket.Mesero.NombreCompleto
                    } : null,
                    ticket.Estado,
                    ticket.Total,
                    ticket.CreatedAt,
                    ticket.UpdatedAt,
                    TicketItems = ticket.TicketItem.Select(item => new
                    {
                        item.Id,
                        Producto = item.Producto != null ? new
                        {
                            item.Producto.Id,
                            item.Producto.Nombre,
                            item.Producto.PrecioBase
                        } : null,
                        Variante = item.Variante != null ? new
                        {
                            item.Variante.Id,
                            item.Variante.Nombre,
                            item.Variante.PrecioAdicional
                        } : null,
                        item.PrecioUnitario,
                        item.Cantidad,
                        item.CreatedAt,
                        item.UpdatedAt,
                        item.Notas,
                        item.Subtotal
                    }).ToList()
                });
                return Ok(tickets);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpGet("cancelado")]
        public async Task<IActionResult> GetTicketsCancelados()
        {
            try
            {
                var tickets = await ticketRepository.GetAllTicketsCanceladosAsync();
                var response = tickets.Select(ticket => new
                {
                    ticket.Id,
                    Mesa = ticket.Mesa != null ? new
                    {
                        ticket.Mesa.Id,
                        ticket.Mesa.Numero,
                        Disponible = ticket.Mesa.Disponible ?? false
                    } : null,
                    Mesero = ticket.Mesero != null ? new
                    {
                        ticket.Mesero.Id,
                        ticket.Mesero.NombreCompleto
                    } : null,
                    ticket.Estado,
                    ticket.Total,
                    ticket.CreatedAt,
                    ticket.UpdatedAt,
                    TicketItems = ticket.TicketItem.Select(item => new
                    {
                        item.Id,
                        Producto = item.Producto != null ? new
                        {
                            item.Producto.Id,
                            item.Producto.Nombre,
                            item.Producto.PrecioBase
                        } : null,
                        Variante = item.Variante != null ? new
                        {
                            item.Variante.Id,
                            item.Variante.Nombre,
                            item.Variante.PrecioAdicional
                        } : null,
                        item.PrecioUnitario,
                        item.Cantidad,
                        item.CreatedAt,
                        item.UpdatedAt,
                        item.Notas,
                        item.Subtotal
                    }).ToList()
                });
                return Ok(tickets);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
