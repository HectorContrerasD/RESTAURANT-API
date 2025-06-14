using Microsoft.AspNetCore.Authorization;
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
        //[Authorize]
        //[HttpGet("abierto/mesero")] //Obtiene los tickets abiertos del mesero
        //public async Task<IActionResult> GetOpenTickets()
        //{
        //    try
        //    {
        //        var userId = User.GetId();
        //        if (!userId.HasValue)
        //        {
        //            return Unauthorized();
        //        }
        //        var user = await userRepository.GetUsuarioAsync(userId.Value);
        //        if (user == null)
        //        {
        //            return Unauthorized();
        //        }
        //        var tickets = await ticketRepository.GetAllTicketsByUserIdAsync(user.Id);
        //        var response = tickets.Select(ticket => new
        //        {
        //            ticket.Id,
        //            Mesa = ticket.Mesa != null ? new
        //            {
        //                ticket.Mesa.Id,
        //                ticket.Mesa.Numero,
        //                Disponible = ticket.Mesa.Disponible ?? false
        //            } : null,
        //            Mesero = ticket.Mesero != null ? new
        //            {
        //                ticket.Mesero.Id,
        //                ticket.Mesero.NombreCompleto
        //            } : null,
        //            ticket.Estado,
        //            ticket.Total,
        //            ticket.CreatedAt,
        //            ticket.UpdatedAt,
        //            TicketItems = ticket.TicketItem.Select(item => new
        //            {
        //                item.Id,
        //                Producto = item.Producto != null ? new
        //                {
        //                    item.Producto.Id,
        //                    item.Producto.Nombre,
        //                    item.Producto.PrecioBase
        //                } : null,
        //                Variante = item.Variante != null ? new
        //                {
        //                    item.Variante.Id,
        //                    item.Variante.Nombre,
        //                    item.Variante.PrecioAdicional
        //                } : null,
        //                item.PrecioUnitario,
        //                item.Cantidad,
                       
        //                item.Notas,
        //                item.Subtotal
        //            }).ToList()
        //        });
        //        return Ok(response);

        //    }
        //    catch (Exception error)
        //    {
        //        return Problem(error.Message);
        //    }
        //}

        [HttpGet("abierto")] //Obtiene todos los tickets abiertos (para cocineros)
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
                       
                        item.Notas,
                        item.Subtotal
                    }).ToList()
                }).OrderBy(x=>x.Mesa);
                return Ok(response);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }

        }
        //[HttpGet("cerrado")] //Obtiene todos los tickets cerrados (para cocineros)
        //public async Task<IActionResult> GetTicketsCerrados()
        //{
        //    try
        //    {
        //        var tickets = await ticketRepository.GetAllTicketsCerradosAsync();
        //        var response = tickets.Select(ticket => new
        //        {
        //            ticket.Id,
        //            Mesa = ticket.Mesa != null ? new
        //            {
        //                ticket.Mesa.Id,
        //                ticket.Mesa.Numero,
        //                Disponible = ticket.Mesa.Disponible ?? false
        //            } : null,
        //            Mesero = ticket.Mesero != null ? new
        //            {
        //                ticket.Mesero.Id,
        //                ticket.Mesero.NombreCompleto
        //            } : null,
        //            ticket.Estado,
        //            ticket.Total,
        //            ticket.CreatedAt,
               
        //            TicketItems = ticket.TicketItem.Select(item => new
        //            {
        //                item.Id,
        //                Producto = item.Producto != null ? new
        //                {
        //                    item.Producto.Id,
        //                    item.Producto.Nombre,
        //                    item.Producto.PrecioBase
        //                } : null,
        //                Variante = item.Variante != null ? new
        //                {
        //                    item.Variante.Id,
        //                    item.Variante.Nombre,
        //                    item.Variante.PrecioAdicional
        //                } : null,
        //                item.PrecioUnitario,
        //                item.Cantidad,
                      
        //                item.Notas,
        //                item.Subtotal
        //            }).ToList()
        //        });
        //        return Ok(response);
        //    }
        //    catch (Exception error)
        //    {
        //        return Problem(error.Message);
        //    }
        //}
        //[HttpGet("cancelado")]
        //public async Task<IActionResult> GetTicketsCancelados() //Obtiene todos los tickets cancelados (para cocineros)
        //{
        //    try
        //    {
        //        var tickets = await ticketRepository.GetAllTicketsCanceladosAsync();
        //        var response = tickets.Select(ticket => new
        //        {
        //            ticket.Id,
        //            Mesa = ticket.Mesa != null ? new
        //            {
        //                ticket.Mesa.Id,
        //                ticket.Mesa.Numero,
        //                Disponible = ticket.Mesa.Disponible ?? false
        //            } : null,
        //            Mesero = ticket.Mesero != null ? new
        //            {
        //                ticket.Mesero.Id,
        //                ticket.Mesero.NombreCompleto
        //            } : null,
        //            ticket.Estado,
        //            ticket.Total,
        //            ticket.CreatedAt,
                
        //            TicketItems = ticket.TicketItem.Select(item => new
        //            {
        //                item.Id,
        //                Producto = item.Producto != null ? new
        //                {
        //                    item.Producto.Id,
        //                    item.Producto.Nombre,
        //                    item.Producto.PrecioBase
        //                } : null,
        //                Variante = item.Variante != null ? new
        //                {
        //                    item.Variante.Id,
        //                    item.Variante.Nombre,
        //                    item.Variante.PrecioAdicional
        //                } : null,
        //                item.PrecioUnitario,
        //                item.Cantidad,
        //                item.Notas,
        //                item.Subtotal
        //            }).ToList()
        //        });
        //        return Ok(response);
        //    }
        //    catch (Exception error)
        //    {
        //        return Problem(error.Message);
        //    }
        //}
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketPayload ticketPayload) // Crea un nuevo ticket (para mesero)
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
                
                var mesa = await mesaRepository.GetMesaByIdAsync((int)ticketPayload.MesaId!);
                if (mesa == null)
                    return NotFound();
                var validator = new TicketValidator();
                var validation = await validator.ValidateAsync(ticketPayload);
                if (!validation.IsValid) return BadRequest(validation.Errors.ToPayload());
                var ticket = new Ticket
                {
                    CreatedAt = DateTime.UtcNow,
                   
                    MesaId = mesa.Id,
                    MeseroId = user.Id,
                    Estado = Constants.Abierto,
                    Total = 0
                };
                var itemsCreados = new List<TicketItem>();
                await ticketRepository.InsertAsync(ticket);
                decimal totalTicket = 0;
                foreach (var item in ticketPayload.TicketItems!)
                {
                    var ticketItem = await ProcesarItem(ticket.Id, item);
                    itemsCreados.Add(ticketItem);
                    totalTicket += ticketItem.Subtotal;
                }
                ticket.Total = totalTicket;

                await ticketRepository.UpdateAsync(ticket);
                mesa.Disponible = false;
                await mesaRepository.UpdateAsync(mesa);
                return Ok(ticket.Id);
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
        [HttpGet("ProcesarTicket")] // Procesa un ticket (para cocinero) no se llama, es un método privado para procesar los items del ticket y que swagger muestre todos los métodos
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
                CreatedAt = DateTime.UtcNow
            };
            await ticketItemRepository.InsertAsync(ticketItem);
            return ticketItem;
        }
        [HttpPut("{id}/cerrar")] //para cerrar un ticket (para cocinero)
        public async Task<IActionResult> CloseTicket([FromRoute] int id)
        {
            try
            {
                var ticket = await ticketRepository.GetTicketByIdAsync(id);
                if (ticket == null)
                    return NotFound();
                if (ticket.Estado != Constants.Listo)
                    return BadRequest("El pedido aun no esta listo");
                ticket.Estado = Constants.Cerrado;
               
                var mesa = await mesaRepository.GetMesaByIdAsync(ticket.MesaId);
                if (mesa == null)
                    return NotFound("Mesa no encontrada");
                mesa.Disponible = true; // Marcar la mesa como disponible
                await ticketRepository.UpdateAsync(ticket);
                await mesaRepository.UpdateAsync(mesa);
                return Ok();
            }
            catch (Exception error)
            {
                return Problem(error.Message);
            }
        }
       
    }
}


