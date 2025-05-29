using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Payloads;
using Restaurant.Api.Repositories.Abstractions;
using Restaurant.Api.Services.Utils;

namespace Restaurant.Api.Controllers
{
    [Route("api/ticket")]

    public class TicketController(ITicketRepository ticketRepository, ITicketItemRepository ticketItemRepository, IUserRepository userRepository) : ControllerBase
    {
        [HttpGet("propio")]
        public async Task<IActionResult> GetOpenTickets()
        {
            try
            {
                var userId = User.GetId();
                if (!userId.HasValue)
                {
                    return Unauthorized();
                }
                var user = await userRepository.GetUsuarioAsync(userId.Value);
                if (user == null)
                {
                    return Unauthorized();
                }
                var tickets = await ticketRepository.GetAllTicketsByUserIdAsync(user.Id);
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
            catch (Exception error)
            {
                return Problem(error.Message);
            }
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
            catch (Exception error)
            {
                return Problem(error.Message);
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
            catch (Exception error)
            {
                return Problem(error.Message);
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
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketPayload ticketPayload)
        {
            try
            {
                var userId = User.GetId();
                if (!userId.HasValue)
                {
                    return Unauthorized();
                }
                var user = await userRepository.GetUsuarioAsync(userId.Value);
                if (user == null)
                {
                    return Unauthorized();
                }
                return null;


            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
    }
}
