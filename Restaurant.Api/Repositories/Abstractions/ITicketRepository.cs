using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using System.Collections.Generic;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface ITicketRepository
    {
        public Task<Ticket?> GetTicketByIdAsync(int id);
        public Task<List<Ticket>> GetAllTicketsCerradosAsync();

        public  Task<List<Ticket>> GetAllTicketsAbiertosAsync();

        public Task<List<Ticket>> GetAllTicketsCanceladosAsync();
        public Task<List<Ticket>> GetAllTicketsByUserIdAsync(int id);
    }
}
