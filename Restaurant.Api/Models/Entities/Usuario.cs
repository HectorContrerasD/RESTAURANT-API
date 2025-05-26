using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public bool? Activo { get; set; }

    public virtual ICollection<Ticket> Ticket { get; set; } = new List<Ticket>();
}
