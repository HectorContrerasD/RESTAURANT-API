using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class TicketItem
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public int ProductoId { get; set; }

    public int? VarianteId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Subtotal { get; set; }

    public string? Notas { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Estado { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual Variante? Variante { get; set; }
}
