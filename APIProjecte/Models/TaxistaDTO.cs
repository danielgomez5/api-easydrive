namespace APIProjecte.Models
{
    public class TaxistaDTO
    {
        public string DNI { get; set; }
        public string Nom { get; set; }
        public string Cognom { get; set; }
        public int ViatgesRealitzats { get; set; }

        public TaxistaDTO(Usuari u)
        {
            this.DNI = u.Dni;
            this.Nom = u.Nom;
            this.Cognom = u.Cognom;
            this.ViatgesRealitzats = u.Viatges?.Count ?? 0;
        }
    }
}
