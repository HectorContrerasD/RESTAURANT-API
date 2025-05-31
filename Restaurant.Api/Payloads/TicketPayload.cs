using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Payloads
{
    public class TicketPayload
    {
        public int? Id { get; set; }

        public int? MesaId { get; set; }

        public int? MeseroId { get; set; }

        public string? Estado { get; set; }

        public decimal? Total { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        
        
        public List<TicketItemPayload>? TicketItems { get; set; }
    }
    public class TicketItemPayload
    {
        public int? Id { get; set; }
        public int? TicketId { get; set; }
        public int? ProductoId { get; set; }
        public int? VarianteId { get; set; }
        public decimal? Precio { get; set; }
        public int? Cantidad { get; set; }

        public string? Notas { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
