using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class Producto
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal PrecioBase { get; set; }

    public bool? Activo { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;

    public virtual ICollection<TicketItem> TicketItem { get; set; } = new List<TicketItem>();

    public virtual ICollection<Variante> Variante { get; set; } = new List<Variante>();
}
