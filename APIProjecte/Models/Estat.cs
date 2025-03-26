using System;
using System.Collections.Generic;

namespace APIProjecte.Models;

public partial class Estat
{
    public int Id { get; set; }

    public string? Estat1 { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
