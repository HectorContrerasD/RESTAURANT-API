namespace Restaurant.Api.Services.Abstractions
{
    public interface INotificacionService
    {
        Task NotifyTicketCreated(int ticketId, int mesaNumero);
        Task NotifyTicketClosed(int ticketId, int mesaNumero);
        Task NotifyItemStateChanged(int ticketItemId, int ticketId, int mesaNumero, string newState);
        Task NotifyMesaAvailabilityChanged(int mesaId, int mesaNumero, bool disponible);
    }
}
