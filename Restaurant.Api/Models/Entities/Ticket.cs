using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public int MesaId { get; set; }

    public int MeseroId { get; set; }

    public string? Estado { get; set; }

    public decimal? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Mesa Mesa { get; set; } = null!;

    public virtual Usuario Mesero { get; set; } = null!;

    public virtual ICollection<TicketItem> TicketItem { get; set; } = new List<TicketItem>();
}
