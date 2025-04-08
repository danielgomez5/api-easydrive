namespace APIProjecte.Models
{
    public class UsuariDTO
    {
        public string Dni { get; set; } = null!;
        public string Nom { get; set; } = null!;
        public string Cognom { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telefon { get; set; } = null!;
        public DateOnly DataNaixement { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool? Rol { get; set; }
        public string? Horari { get; set; }
        public bool? Disponibilitat { get; set; }
        public int? IdZona { get; set; }

        public IFormFile? FotoPerfil { get; set; }
        public IFormFile? FotoCarnet { get; set; }
    }
}
