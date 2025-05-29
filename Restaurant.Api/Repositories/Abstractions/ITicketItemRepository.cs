using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ITicketItemRepository
    {
        public Task<List<TicketItem>> GetTicketItemsByTicketIdAsync(int ticketId);
    }
}
