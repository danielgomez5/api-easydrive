using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProjecte.Models;

namespace APIProjecte.Controllers
{
    public class ZonasController : ControllerBase
    {
        private readonly EasydriveContext _context;

        public ZonasController(EasydriveContext context)
        {
            _context = context;
        }

        // GET: api/zones
        [Route("api/zones")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zona>>> GetZonas()
        {
            return await _context.Zonas.ToListAsync();
        }

        // GET: api/zona/id
        [Route("api/zona/{id}")]
        [HttpGet]
        public async Task<ActionResult<Zona>> GetZona(int id)
        {
            var zona = await _context.Zonas.FindAsync(id);

            if (zona == null)
            {
                return NotFound();
            }

            return zona;
        }

        // PUT: api/zona/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/zona/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutZona(int id, [FromBody]Zona zona)
        {
            if (id != zona.Id)
            {
                return BadRequest();
            }

            _context.Entry(zona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZonaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(zona);
        }

        // POST: api/zona
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/zona")]
        [HttpPost]
        public async Task<ActionResult<Zona>> PostZona(Zona zona)
        {
            _context.Zonas.Add(zona);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZona", new { id = zona.Id }, zona);
        }

        // DELETE: api/zona/id
        [Route("api/zona/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteZona(int id)
        {
            var zona = await _context.Zonas.FindAsync(id);
            if (zona == null)
            {
                return NotFound();
            }

            _context.Zonas.Remove(zona);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ZonaExists(int id)
        {
            return _context.Zonas.Any(e => e.Id == id);
        }

        /******* Mètodes per defecte acabats *******/

        /******* Inici de mètodes personalitzats *******/

        // GET: api/zones_comunitats
        [Route("api/zones_comunitats")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetZonasXComunitat()
        {
            var zones = _context.Zonas.OrderBy(x=>x.ComunitatA).Select(x => x.ComunitatA).Distinct().ToListAsync();

            return await zones;
        }

        // GET: api/zones_ciutat/comunitat
        [Route("api/zones_provincia/{comunitat}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zona>>> GetZonasProvinciaXComunitat(string comunitat)
        {
            var zona = await _context.Zonas.Where(x => x.ComunitatA.Equals(comunitat)).OrderBy(x => x.Provincia).Distinct().ToListAsync();
            if (zona == null)
            {
                return NotFound();
            }

            return zona;
        }

        // GET: api/zones_ciutat/comunitat
        [Route("api/zones_ciutat/{provincia}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zona>>> GetZonasCiutatXProvincia(string provincia)
        {
            var zona = await _context.Zonas.Where(x => x.Provincia.Equals(provincia)).OrderBy(x=>x.Ciutat).ToListAsync();
            if (zona == null)
            {
                return NotFound();
            }

            return zona;
        }

        // GET: api/zona-viatges/id_zona
        [Route("api/zona-viatges/{id_zona}")]
        [HttpGet]
        public async Task<ActionResult<List<Viatge>>> GetViatgesByZona(int id_zona)
        {
            Zona z = _context.Zonas
                .Include(x => x.Viatges)
                .Where(x => x.Id == id_zona).FirstOrDefault();

            if (z != null)
            {
                List<Viatge> viatges = z.Viatges.Where(x => x.IdZona == z.Id).ToList();

                if (viatges != null)
                {
                    return viatges;
                }

                return NotFound($"Encara no hi ha viatges registrats a {z.Ciutat}...");
            }

            return NotFound("Zona no trobada...");
        }


        // GET: api/zona-clients/id_zona
        [Route("api/zona-clients/{id_zona}")]
        [HttpGet]
        public async Task<ActionResult<List<Usuari>>> GetClientsByZona(int id_zona)
        {
            Zona z = _context.Zonas
                .Include(x => x.Usuaris)
                .Where(x => x.Id == id_zona).FirstOrDefault();

            if (z != null)
            {
                List<Usuari> clients = z.Usuaris.Where(x => x.Rol == false).ToList();

                if (clients != null)
                {
                    return clients;
                }

                return new List<Usuari>();
            }

            return NotFound("Zona no trobada...");
        }


        // GET: api/zona-taxistes/id_zona
        [Route("api/zona-taxistes/{id_zona}")]
        [HttpGet]
        public async Task<ActionResult<List<Usuari>>> GetTaxistesByZona(int id_zona)
        {
            Zona z = _context.Zonas
                .Include(x => x.Usuaris)
                .Where(x => x.Id == id_zona).FirstOrDefault();

            if (z != null)
            {
                List<Usuari> clients = z.Usuaris.Where(x => x.Rol == true).ToList();

                if (clients != null)
                {
                    return clients;
                }

                return new List<Usuari>();
            }

            return NotFound("Zona no trobada...");
        }

        // GET: api/zones/filtre/filtrePer
        [Route("api/zones/{filtre}/{filtraPer}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Zona>>> GetZonasFiltre(string filtre, int filtraPer)
        {
            if (filtraPer == 1)
            {
                return await _context.Zonas.Where(x => x.ComunitatA.Contains(filtre)).ToListAsync();
            } 
            else if (filtraPer == 2)
            {
                return await _context.Zonas.Where(x => x.Ciutat.Contains(filtre)).ToListAsync();
            }
            else if (filtraPer == 3)
            {
                return await _context.Zonas.Where(x => x.Provincia.Contains(filtre)).ToListAsync();
            }

            return NotFound("El filtre que intentes utilitzar no està disponible...");
        }
    }
}
