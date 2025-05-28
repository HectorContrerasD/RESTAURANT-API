using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class TicketRepository(RestaurantContext dbContext) : Repository<Ticket>(dbContext), ITicketRepository
    {
        public async Task<Ticket?> GetTicketByIdAsync(int id)
        {
            var ticket = await DbSet.Include(x=>x.Mesa).Include(x=>x.Mesero)
                .Include(x=>x.TicketItem).ThenInclude(x=>x.Producto)
                .Include(x => x.TicketItem).ThenInclude(x => x.Variante)
                .FirstOrDefaultAsync(x => x.Id == id);
            return ticket;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            var tickets = await DbSet.Include(x => x.Mesa).Include(x => x.Mesero)
                .Include(x => x.TicketItem).ThenInclude(x => x.Producto)
                .Include(x => x.TicketItem).ThenInclude(x => x.Variante).ToListAsync();
            return tickets;
        }
    }
}
