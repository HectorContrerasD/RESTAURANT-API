using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class Mesa
{
    public int Id { get; set; }

    public int Numero { get; set; }

    public bool? Activa { get; set; }

    public virtual ICollection<Ticket> Ticket { get; set; } = new List<Ticket>();
}
