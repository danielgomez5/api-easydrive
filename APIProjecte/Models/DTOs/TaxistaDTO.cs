namespace APIProjecte.Models.DTOs
{
    public class TaxistaDTO
    {
        public string DNI { get; set; }
        public string Nom { get; set; }
        public string Cognom { get; set; }
        public int ViatgesRealitzats { get; set; }

        public TaxistaDTO(Usuari u)
        {
            DNI = u.Dni;
            Nom = u.Nom;
            Cognom = u.Cognom;
            ViatgesRealitzats = u.Viatges?.Count ?? 0;
        }
    }
}
