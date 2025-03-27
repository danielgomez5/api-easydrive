using APIProjecte.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using static APIProjecte.Models.ClasesTemporales;

namespace APIProjecte.Controllers
{
    public class ObtenirZones
    {
        private readonly EasydriveContext _context;

        public ObtenirZones(EasydriveContext context)
        {
            _context = context;
        }

        public void ImportarDatosDesdeJson(string rutaJson)
        {
            try
            {
                string jsonData = File.ReadAllText(rutaJson);


                List<Comunidad> comunidades = JsonConvert.DeserializeObject<List<Comunidad>>(jsonData);

                if (comunidades != null)
                {
                    foreach (Comunidad comunidad in comunidades)
                    {
                        foreach (Provincia provincia in comunidad.Provinces)
                        {
                            foreach (Town town in provincia.Towns)
                            {
                                var nuevaZona = new Zona
                                {
                                    ComunitatA = comunidad.Label,
                                    Provincia = provincia.Label,
                                    Ciutat = town.Label,
                                    Pais = "España"
                                };

                                _context.Zonas.Add(nuevaZona);
                            }
                        }
                    }

                    // Guardar en la base de datos
                    _context.SaveChanges();
                    Console.WriteLine("Datos importados correctamente.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al importar datos: {ex.Message}");
            }
        }
    }
}
