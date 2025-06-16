using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using System.Collections.Generic;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        public Task<Ticket?> GetTicketByIdAsync(int id);
        

        public  Task<List<Ticket>> GetAllTicketsAbiertosAsync();

        
        public Task<List<Ticket>> GetAllTicketsByUserIdAsync(int id);
    }
}
