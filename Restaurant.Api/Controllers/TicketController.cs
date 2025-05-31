using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Payloads;
using Restaurant.Api.Repositories.Abstractions;
using Restaurant.Api.Repositories.Validators;
using Restaurant.Api.Services.Utils;

namespace Restaurant.Api.Controllers
{
    [Route("api/ticket")]

    public class TicketController(ITicketRepository ticketRepository,
        IVarianteRepository varianteRepository,
        IMesaRepository mesaRepository, 
        IProductoRepository productoRepository, 
        ITicketItemRepository ticketItemRepository, 
        IUserRepository userRepository) : ControllerBase
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
                

                var mesa = await mesaRepository.GetMesaByIdAsync((int)ticketPayload.MesaId);
                if (mesa == null)
                    return NotFound();
                var validator = new TicketValidator();
                var validation = await validator.ValidateAsync(ticketPayload);
                if (!validation.IsValid) return BadRequest(validation.Errors.ToPayload());
                var ticket = new Ticket
                {
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    MesaId = mesa.Id,
                    MeseroId = user.Id,
                    Estado = Constants.Abierto,
                    Total = 0
                };
                var itemsCreados = new List<TicketItem>();
                await ticketRepository.InsertAsync(ticket);
                decimal totalTicket = 0;
                foreach (var item in ticketPayload.TicketItems)
                {
                    var ticketItem = await ProcesarItem(ticket.Id, item);
                    itemsCreados.Add(ticketItem);
                    totalTicket += ticketItem.Subtotal;
                }

                return Ok(ticket.Id);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        [HttpGet("ProcesarTicket")]
        private async Task<TicketItem> ProcesarItem(int id, TicketItemPayload item)
        {
            var producto = await productoRepository.GetProductoByIdAsync((int)item.ProductoId!);
            if (producto == null)
                throw new Exception("Producto no encontrado");
            decimal precioUnitario = producto.PrecioBase;
            if (item.VarianteId.HasValue)
            {
                var variante = await varianteRepository.GetByIdAsync((int)item.VarianteId);
                if (variante == null)
                    throw new Exception("Variante no encontrada");
                precioUnitario += (decimal)variante.PrecioAdicional!;
            }
            int cantidad = item.Cantidad ?? 1;
            decimal subtotal = precioUnitario * cantidad;
            var ticketItem = new TicketItem
            {
                ProductoId = producto.Id,
                VarianteId = item.VarianteId,
                PrecioUnitario = precioUnitario,
                Cantidad = cantidad,
                Notas = item.Notas,
                Subtotal = subtotal,
                TicketId = id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await ticketItemRepository.InsertAsync(ticketItem);
            return ticketItem;
        }
    }
}
