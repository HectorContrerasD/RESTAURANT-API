using Microsoft.AspNetCore.SignalR;
using Restaurant.Api.Hubs;
using Restaurant.Api.Services.Abstractions;

namespace Restaurant.Api.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly IHubContext<RestaurantHub> hubContext;
        public NotificacionService(IHubContext<RestaurantHub> hubContext)
        {
            this.hubContext = hubContext;
        }
        public async Task NotifyTicketCreated(int ticketId, int mesaNumero)
        {
            var notification = new
            {
                Type = "TicketCreated",
                TicketId = ticketId,
                MesaNumero = mesaNumero,
                Timestamp = DateTime.UtcNow
            };

            await hubContext.Clients.All
                .SendAsync("TicketCreated", notification);

            await hubContext.Clients.All
                .SendAsync("MesaOccupied", new { MesaNumero = mesaNumero, TicketId = ticketId });

            await hubContext.Clients.All   
                .SendAsync("RefreshTicketsList", new { TicketId = ticketId, Action = "Created" });
        }

        public async Task NotifyTicketClosed(int ticketId, int mesaNumero)
        {
            var notification = new
            {
                Type = "TicketClosed",
                TicketId = ticketId,
                MesaNumero = mesaNumero,
                Timestamp = DateTime.UtcNow
            };

            await hubContext.Clients.All
                .SendAsync("TicketClosed", notification);

            await hubContext.Clients.All
                .SendAsync("RefreshTicketsList", new { TicketId = ticketId });
        }

        public async Task NotifyItemStateChanged(int ticketItemId, int ticketId, int mesaNumero, string newState)
        {
            var notification = new
            {
                Type = "ItemStateChanged",
                TicketItemId = ticketItemId,
                TicketId = ticketId,
                MesaNumero = mesaNumero,
                NewState = newState,
                Timestamp = DateTime.UtcNow
            };

            await hubContext.Clients.All
                .SendAsync("ItemStateChanged", notification);

            if (newState == Constants.Listo)
            {
                await hubContext.Clients.All
                    .SendAsync("ItemCompleted", notification);
            }
        }

        public async Task NotifyMesaAvailabilityChanged(int mesaId, int mesaNumero, bool disponible)
        {
            var notification = new
            {
                Type = "MesaAvailabilityChanged",
                MesaId = mesaId,
                MesaNumero = mesaNumero,
                Disponible = disponible,
                Timestamp = DateTime.UtcNow
            };

            await hubContext.Clients.All        
                .SendAsync("MesaAvailabilityChanged", notification);
        }
    }
}
