using System;
using System.Collections.Generic;

namespace APIProjecte.Models;

public partial class DadesPagament
{
    public int Id { get; set; }

    public string? NumeroTarjeta { get; set; }

    public string? Titular { get; set; }

    public DateOnly? DataExpiracio { get; set; }

    public string? IdUsuari { get; set; }

    public virtual Usuari? IdUsuariNavigation { get; set; }
}
