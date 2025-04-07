using System;
using System.Collections.Generic;

namespace APIProjecte.Models;

public partial class Viatge
{
    public int Id { get; set; }

    public int? Durada { get; set; }

    public decimal? Distancia { get; set; }

    public decimal? Valoracio { get; set; }

    public string? Comentari { get; set; }

    public int? IdZona { get; set; }

    public string? IdTaxista { get; set; }

    public int? IdReserva { get; set; }

    public string? IdCotxe { get; set; }

    public virtual Cotxe? IdCotxeNavigation { get; set; }

    public virtual Reserva? IdReservaNavigation { get; set; }

    public virtual Usuari? IdTaxistaNavigation { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
}
