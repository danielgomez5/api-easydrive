using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace APIProjecte.Models;

public partial class Reserva
{
    public int Id { get; set; }

    public string? Origen { get; set; }

    public string? Desti { get; set; }

    public DateOnly? DataReserva { get; set; }

    public DateOnly? DataViatge { get; set; }

    public decimal? Preu { get; set; }

    public string? Estat { get; set; }

    public string? IdUsuari { get; set; }

    public int? IdEstat { get; set; }

    public TimeOnly? HoraViatge { get; set; }

    public virtual Estat? IdEstatNavigation { get; set; }

    public virtual Usuari? IdUsuariNavigation { get; set; }

    public virtual ICollection<Viatge> Viatges { get; set; } = new List<Viatge>();
}
