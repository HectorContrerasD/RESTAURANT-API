﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<List<TicketItem>> GetAllTicketItems()
        {
            var ticketItems = await DbSet.Include(x=>x.Ticket).ThenInclude(x=>x.Mesa).Include(x=>x.Variante).Include(x=>x.Producto).OrderBy(x=>x.CreatedAt).Where(x=>x.Producto.ACocina ==true && x.Estado != Constants.Listo).ToListAsync();
            return ticketItems;
        }
        public async Task<TicketItem> GetItemByIdAsync(int id)
        {
            var item = await DbSet.Include(x => x.Ticket).ThenInclude(x => x.Mesa).Include(x => x.Variante).Include(x => x.Producto).FirstOrDefaultAsync(x => x.Id == id);
            return item;

		}
    }
}
