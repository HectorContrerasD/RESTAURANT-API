﻿using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ITicketItemRepository:IRepository<TicketItem>
    {
        public Task<List<TicketItem>> GetTicketItemsByTicketIdAsync(int ticketId);
        public Task<List<TicketItem>> GetAllTicketItems();
        public Task<TicketItem> GetItemByIdAsync(int id);

	}
}
