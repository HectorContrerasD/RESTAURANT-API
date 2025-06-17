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
                .Include(x=>x.TicketItem)
                .Include(x=>x.TicketItem).ThenInclude(x=>x.Producto)
                .Include(x => x.TicketItem).ThenInclude(x => x.Variante)
                .FirstOrDefaultAsync(x => x.Id == id);
            return ticket;
        }

        
        public async Task<List<Ticket>> GetAllTicketsAbiertosAsync()
        {
            var tickets = await DbSet.Include(x => x.Mesa).Include(x => x.Mesero)
                .Include(x => x.TicketItem).ThenInclude(x => x.Producto)
                .Include(x => x.TicketItem).ThenInclude(x => x.Variante).Where(x=>x.Estado ==Constants.Abierto).ToListAsync();
            return tickets;
        }
        
        public async Task<List<Ticket>> GetAllTicketsByUserIdAsync(int id)
        {
            var tickets = await DbSet.Include(x => x.Mesa).Include(x => x.Mesero)
                .Include(x => x.TicketItem).ThenInclude(x => x.Producto)
                .Include(x => x.TicketItem).ThenInclude(x => x.Variante).Where(x => x.MeseroId == id).ToListAsync();
            return tickets;
        }
    }
}
