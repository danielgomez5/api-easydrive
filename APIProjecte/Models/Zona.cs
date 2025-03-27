using System;
using System.Collections.Generic;

namespace APIProjecte.Models;

public partial class Zona
{
    public int Id { get; set; }

    public string? ComunitatA { get; set; }

    public string? Ciutat { get; set; }

    public string? Provincia { get; set; }

    public string? Pais { get; set; }

    public virtual ICollection<Usuari> Usuaris { get; set; } = new List<Usuari>();

    public virtual ICollection<Viatge> Viatges { get; set; } = new List<Viatge>();

    public virtual ICollection<Usuari> IdTaxista { get; set; } = new List<Usuari>();
}
