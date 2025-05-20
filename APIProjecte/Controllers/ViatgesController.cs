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
    public class ViatgesController : ControllerBase
    {
        private readonly EasydriveContext _context;

        public ViatgesController(EasydriveContext context)
        {
            _context = context;
        }

        // GET: api/viatges
        [Route("api/viatges")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viatge>>> GetViatges()
        {
            return await _context.Viatges.ToListAsync();
        }

        // GET: api/viatge/id
        [Route("api/viatge/{id}")]
        [HttpGet]
        public async Task<ActionResult<Viatge>> GetViatge(int id)
        {
            var viatge = await _context.Viatges.FindAsync(id);

            if (viatge == null)
            {
                return NotFound();
            }

            return viatge;
        }

        // PUT: api/viatge/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/viatge/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutViatge(int id, [FromBody] Viatge viatge)
        {
            if (id != viatge.Id)
            {
                return BadRequest();
            }

            _context.Entry(viatge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViatgeExists(id))
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

        // POST: api/viatge
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/viatge")]
        [HttpPost]
        public async Task<ActionResult<Viatge>> PostViatge([FromBody] Viatge viatge)
        {
            _context.Viatges.Add(viatge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetViatge", new { id = viatge.Id }, viatge);
        }

        // DELETE: api/viatge/id
        [Route("api/viatge/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteViatge(int id)
        {
            var viatge = await _context.Viatges.FindAsync(id);
            if (viatge == null)
            {
                return NotFound();
            }

            _context.Viatges.Remove(viatge);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViatgeExists(int id)
        {
            return _context.Viatges.Any(e => e.Id == id);
        }

        /******* Mètodes per defecte acabats *******/

        /******* Inici de mètodes personalitzats *******/

        // GET: api/viatge-reserva/id
        [Route("api/viatge-reserva/{id}")]
        [HttpGet]
        public async Task<ActionResult<Viatge>> GetViatgeByReserva(int id)
        {
            Reserva r = _context.Reservas
        .Include(x => x.Viatges)
            .ThenInclude(v => v.IdTaxistaNavigation)
        .Include(x => x.Viatges)
            .ThenInclude(v => v.IdCotxeNavigation)
        .FirstOrDefault(x => x.Id == id);


            if (r != null)
            {
                Viatge viatge = r.Viatges.Where(x => x.IdReserva == r.Id).FirstOrDefault();

                if (viatge != null)
                {
                    return viatge;
                }

                return NotFound("No s'ha trobat cap viatge associat a aquesta reserva...");
            }

            return NotFound("Número de reserva no trobat");
        }

        [Route("api/viatges-usuari/{id_usuari}")]
        [HttpGet]
        public async Task<ActionResult<List<Viatge>>> GetAllViatgesByUser(string id_usuari)
        {
            Usuari client = _context.Usuaris
                .Include(x => x.Reservas)
                    .ThenInclude(r => r.Viatges)
                    .ThenInclude(v => v.IdTaxistaNavigation)
                .Where(x => x.Dni.Equals(id_usuari))
                .FirstOrDefault();

            if (client == null)
            {
                return NotFound("Usuari no trobat...");
            }

            // Obtener los viajes directamente
            List<Viatge> viatges = client.Reservas
                .Where(r => r.IdEstat == 3) // Estado 3 = Realizada
                .SelectMany(r => r.Viatges)
                .ToList();

            if (viatges.Any())
            {
                return viatges;
            }

            return new List<Viatge>();
        }

        [Route("api/viatges-taxista/{id_usuari}")]
        [HttpGet]
        public async Task<ActionResult<List<Viatge>>> GetAllViatgesByTaxista(string id_usuari)
        {
            Usuari taxista = _context.Usuaris.Where(x => x.Dni == id_usuari)
                .Include(x => x.Viatges).ThenInclude(x => x.IdReservaNavigation)
                .FirstOrDefault();



            if (taxista == null)
            {
                return NotFound("Usuari no trobat...");
            }

            List<Viatge> viatges = taxista.Viatges.ToList();

            if (viatges.Any())
            {
                return viatges;
            }

            return new List<Viatge>();
        }

    }
}