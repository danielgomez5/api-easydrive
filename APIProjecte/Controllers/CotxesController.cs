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
    public class CotxesController : ControllerBase
    {
        private readonly EasydriveContext _context;

        public CotxesController(EasydriveContext context)
        {
            _context = context;
        }

        // GET: api/cotxes
        [Route("api/cotxes")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotxe>>> GetCotxes()
        {
            return await _context.Cotxes.Include(x => x.IdUsuaris).ToListAsync();
        }

        // GET: api/cotxe/id
        [Route("api/cotxe/{id}")]
        [HttpGet]
        public async Task<ActionResult<Cotxe>> GetCotxe(string id)
        {
            var cotxe = await _context.Cotxes.FindAsync(id);

            if (cotxe == null)
            {
                return NotFound();
            }

            return cotxe;
        }

        // PUT: api/cotxe/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/cotxe/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutCotxe(string id, Cotxe cotxe)
        {
            if (id != cotxe.Matricula)
            {
                return BadRequest();
            }

            _context.Entry(cotxe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotxeExists(id))
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

        // POST: api/cotxe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("api/cotxe")]
        [HttpPost]
        public async Task<ActionResult<Cotxe>> PostCotxe([FromBody] Cotxe cotxe)
        {
            _context.Cotxes.Add(cotxe);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CotxeExists(cotxe.Matricula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCotxe", new { id = cotxe.Matricula }, cotxe);
        }

        // DELETE: api/cotxe/id
        [Route("api/cotxe/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCotxe(string id)
        {
            var cotxe = await _context.Cotxes.FindAsync(id);
            if (cotxe == null)
            {
                return NotFound();
            }

            _context.Cotxes.Remove(cotxe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CotxeExists(string id)
        {
            return _context.Cotxes.Any(e => e.Matricula == id);
        }


        /******* Mètodes per defecte acabats *******/

        /******* Inici de mètodes personalitzats *******/

        // GET: api/cotxes-taxista/id_taxista
        [Route("api/cotxes-taxista/{id_taxista}")]
        [HttpGet]
        public async Task<ActionResult<List<Cotxe>>> GetCotxesByTaxista(string id_taxista)
        {
            List<Cotxe> cotxes = new List<Cotxe>();

            Usuari taxista = _context.Usuaris
                .Include(x => x.Matriculas)
                .Where(x => x.Dni.Equals(id_taxista)).FirstOrDefault();


            if (taxista != null)
            {
                cotxes = taxista.Matriculas.ToList();

                if (cotxes != null)
                {
                    return cotxes;
                }
                
                return NotFound("Aquest taxista no té cotxes registrats...");
            }
         

            return NotFound("Taxista no trobat");
        }

        // GET: api/cotxes/filtre/filtrePer
        [Route("api/cotxes/{filtre}/{filtraPer}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cotxe>>> GetCotxesFiltre(string filtre, int filtraPer)
        {
            if (filtraPer == 1)
            {
                return await _context.Cotxes.Where(x => x.Matricula.StartsWith(filtre)).OrderBy(x => x.Matricula).ToListAsync();
            }
            else if (filtraPer == 2)
            {
                return await _context.Cotxes.Where(x => x.Marca.StartsWith(filtre)).OrderBy(x => x.Marca).ThenBy(x => x.Model).ToListAsync();
            }

            return NotFound("El filtre que intentes utilitzar no està disponible...");
        }

        // PUT: api/cotxe/id
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Route("api/cotxe_ftecnic/{id}")]
        //[HttpPut]
        //public async Task<IActionResult> PutCotxeFitxaTecnica(string id, IFormFile f_tecnic)
        //{
        //    Cotxe cotxe  = _context.Cotxes.Where(x=>x.Matricula == id).FirstOrDefault(); 
        //    if (cotxe == null)
        //    {
        //        return NotFound();
        //    }

        //    if (f_tecnic != null)
        //    {
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await f_tecnic.CopyToAsync(memoryStream);  // Leer el archivo en memoria
        //            cotxe.FotoFitxaTecnica = memoryStream.ToArray();  // Convertir el archivo a byte[]
        //        }
        //    }

        //    _context.Entry(cotxe).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CotxeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

    }
}
