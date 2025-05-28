using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ITicketRepository
    {
        public Task<Ticket?> GetTicketByIdAsync(int id);
        public Task<List<Ticket>> GetAllTicketsAsync();
    }
}
