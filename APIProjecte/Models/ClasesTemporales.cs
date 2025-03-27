namespace APIProjecte.Models
{
    public class ClasesTemporales
    {
        public class Comunidad
        {
            public string Label { get; set; }
            public List<Provincia> Provinces { get; set; }
        }

        public class Provincia
        {
            public string Label { get; set; }
            public List<Town> Towns { get; set; }
        }

        public class Town
        {
            public string Label { get; set; }
        }
    }
}
