using System;
using System.Collections.Generic;

namespace APIProjecte.Models;

public partial class Cotxe
{
    public string Matricula { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Any { get; set; }

    public string Tipus { get; set; } = null!;

    public int Capacitat { get; set; }

    public string Color { get; set; } = null!;

    public double? HoresTreballades { get; set; }

    public string? FotoFitxaTecnica { get; set; }

    public virtual ICollection<Viatge> Viatges { get; set; } = new List<Viatge>();

    public virtual ICollection<Usuari> IdUsuaris { get; set; } = new List<Usuari>();
}
