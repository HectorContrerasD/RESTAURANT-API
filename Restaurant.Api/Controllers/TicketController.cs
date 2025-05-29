using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Controllers
{
    [Route("api/ticket")]
    
    public class TicketController(ITicketRepository ticketRepository, ITicketItemRepository ticketItemRepository) : ControllerBase
    {
    }
}
