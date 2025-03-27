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
    public class UsuarisController : ControllerBase
    {
        private readonly EasydriveContext _context;

        public UsuarisController(EasydriveContext context)
        {
            _context = context;
        }

        // GET: api/usuaris
        [Route("api/usuaris")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuaris()
        {
            return await _context.Usuaris.ToListAsync();
        }

        // GET: api/usuari/id
        [Route("api/usuari/{id}")]
        [HttpGet]
        public async Task<ActionResult<Usuari>> GetUsuari(string id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);

            if (usuari == null)
            {
                return NotFound();
            }

            return usuari;
        }

        // PUT: api/usuari/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/usuari/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutUsuari(string id, Usuari usuari)
        {
            if (id != usuari.Dni)
            {
                return BadRequest();
            }

            _context.Entry(usuari).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/usuari
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/usuari")]
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostUsuari(Usuari usuari)
        {
            _context.Usuaris.Add(usuari);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsuariExists(usuari.Dni))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsuari", new { id = usuari.Dni }, usuari);
        }

        // DELETE: api/usuari/id
        [Route("api/usuari/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuari(string id)
        {
            var usuari = await _context.Usuaris.FindAsync(id);
            if (usuari == null)
            {
                return NotFound();
            }

            _context.Usuaris.Remove(usuari);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariExists(string id)
        {
            return _context.Usuaris.Any(e => e.Dni == id);
        }

        /******* Mètodes per defecte acabats *******/

        /******* Inici de mètodes personalitzats *******/

        // GET: api/usuaris-client
        [Route("api/usuaris-client")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuarisClient()
        {
            List<Usuari> clients = new List<Usuari>();

            clients = _context.Usuaris
                .Include(x => x.DadesPagaments)
                .Include(x => x.Reservas)
                .Where(x => x.Rol == false).ToList();

            if (clients != null)
            {
                return clients;
            }

            return NotFound("No s'han trobat usuaris");
        }

        // GET: api/usuaris-taxista
        [Route("api/usuaris-taxista")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuarisTaxista()
        {
            List<Usuari> taxistes = new List<Usuari>();

            taxistes = _context.Usuaris
                .Include(x => x.Matriculas)
                .Include(x => x.Viatges)
                .Where(x => x.Rol == true).ToList();

            if (taxistes != null)
            {
                return taxistes;
            }

            return NotFound("No s'han trobat taxistes");
        }

        // GET: api/usuari-pagaments/id_usuari
        [Route("api/usuari-pagaments/{id_usuari}")]
        [HttpGet]
        public async Task<ActionResult<List<DadesPagament>>> GetDadesPagamentByUsuari(string id_usuari)
        {
            Usuari client = _context.Usuaris
                .Include(x => x.DadesPagaments)
                .Where(x => x.Dni.Equals(id_usuari)).FirstOrDefault();

            if (client != null)
            {
                List<DadesPagament> dp = client.DadesPagaments.ToList();

                if (dp != null)
                {
                    return dp;
                }
                else
                {
                    return NotFound("No s'han trobat dades de pagament associades a aquest usuari...");
                }
            }
            else
            {
                return NotFound("Usuari no trobat");
            }
        }

        // GET: api/taxistes-top5
        [Route("api/taxistes-top5")]
        [HttpGet]
        public async Task<ActionResult<List<Usuari>>> GetTop5Taxistes()
        {
            List<Usuari> taxistes = _context.Usuaris
                .Include(x => x.Viatges)
                .Where(x => x.Rol == true).OrderByDescending(x => x.Viatges).ToList();

            if (taxistes != null)
            {
                return taxistes;
            }

            return NotFound("No s'han trobat taxistes"); 
        }
    }
}
