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
            return await _context.Cotxes.ToListAsync();
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
        public async Task<ActionResult<Cotxe>> PostCotxe(Cotxe cotxe)
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
    }
}
