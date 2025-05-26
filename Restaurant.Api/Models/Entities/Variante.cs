using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class Variante
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal? PrecioAdicional { get; set; }

    public bool? Activo { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual ICollection<TicketItem> TicketItem { get; set; } = new List<TicketItem>();
}
