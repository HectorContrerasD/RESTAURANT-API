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

            // Notificar a cocineros sobre nuevo ticket
            await hubContext.Clients.Group("Cocineros")
                .SendAsync("TicketCreated", notification);

            // Notificar a meseros sobre cambio en mesa y actualizar lista de tickets
            await hubContext.Clients.Group("Meseros")
                .SendAsync("MesaOccupied", new { MesaNumero = mesaNumero, TicketId = ticketId });

            // Notificar para actualizar la lista de tickets abiertos
            await hubContext.Clients.Group("Meseros")
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

            // Notificar a meseros sobre mesa liberada y actualización de lista de tickets
            await hubContext.Clients.Group("Meseros")
                .SendAsync("TicketClosed", notification);

            // Notificar para actualizar la lista de tickets abiertos
            await hubContext.Clients.Group("Meseros")
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

            // Notificar a meseros sobre cambios de estado de items
            await hubContext.Clients.Group("Meseros")
                .SendAsync("ItemStateChanged", notification);

            // Si el item está listo, también notificar a cocineros
            if (newState == Constants.Listo)
            {
                await hubContext.Clients.Group("Cocineros")
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

            // Notificar a meseros sobre cambios en disponibilidad de mesas
            await hubContext.Clients.Group("Meseros")
                .SendAsync("MesaAvailabilityChanged", notification);
        }
    }
}
