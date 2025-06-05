using System;
using System.Collections.Generic;

namespace Restaurant.Api.Models.Entities;

public partial class Sesion
{
    public int Id { get; set; }

    public int UsuarioId { get; set; }

    public string Hash { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
