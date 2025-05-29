using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class TicketItemsRepository(RestaurantContext dbContext) : Repository<TicketItem>(dbContext), ITicketItemRepository
    {
        public async Task<List<TicketItem>> GetTicketItemsByTicketIdAsync(int ticketId)
        {
            var ticketItems = await DbSet.Where(x => x.TicketId == ticketId).ToListAsync();
            return ticketItems;
        }
    }
}
