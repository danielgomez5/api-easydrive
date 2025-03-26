using System;
using System.Collections.Generic;

namespace APIProjecte.Models;

public partial class Usuari
{
    public string Dni { get; set; } = null!;

    public string Nom { get; set; } = null!;

    public string Cognom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telefon { get; set; } = null!;

    public DateOnly DataNaixement { get; set; }

    public string PasswordHash { get; set; } = null!;

    public byte[]? FotoPerfil { get; set; }

    public byte[]? FotoCarnet { get; set; }

    public bool? Rol { get; set; }

    public DateTime? Horari { get; set; }

    public bool? Disponibilitat { get; set; }

    public int? IdZona { get; set; }

    public virtual ICollection<DadesPagament> DadesPagaments { get; set; } = new List<DadesPagament>();

    public virtual Zona? IdZonaNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

    public virtual ICollection<Viatge> Viatges { get; set; } = new List<Viatge>();

    public virtual ICollection<Zona> IdZonas { get; set; } = new List<Zona>();

    public virtual ICollection<Cotxe> Matriculas { get; set; } = new List<Cotxe>();
}
