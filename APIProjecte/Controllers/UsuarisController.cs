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

        [Route("api/usuari")]
        [HttpPost]
        public async Task<ActionResult<Usuari>> PostUsuari([FromBody] Usuari usuari)
        {
            //Usuari usuari = new Usuari
            //{
            //    Dni = usuariDto.Dni,
            //    Nom = usuariDto.Nom,
            //    Cognom = usuariDto.Cognom,
            //    Email = usuariDto.Email,
            //    Telefon = usuariDto.Telefon,
            //    DataNaixement = usuariDto.DataNaixement,
            //    PasswordHash = usuariDto.PasswordHash,
            //    Rol = usuariDto.Rol,
            //    Horari = usuariDto.Horari,
            //    Disponibilitat = usuariDto.Disponibilitat,
            //    IdZona = usuariDto.IdZona
            //};

            //// Guardar imágenes en carpeta "Photos"
            //if (usuariDto.FotoPerfil != null)
            //{
            //    var perfilFileName = $"{Guid.NewGuid()}_{usuariDto.FotoPerfil.FileName}";
            //    var perfilPath = Path.Combine("Photos", perfilFileName);

            //    using (var stream = new FileStream(perfilPath, FileMode.Create))
            //    {
            //        await usuariDto.FotoPerfil.CopyToAsync(stream);
            //    }

            //    usuari.FotoPerfil = perfilFileName;
            //}

            //if (usuariDto.FotoCarnet != null)
            //{
            //    var carnetFileName = $"{Guid.NewGuid()}_{usuariDto.FotoCarnet.FileName}";
            //    var carnetPath = Path.Combine("Photos", carnetFileName);

            //    using (var stream = new FileStream(carnetPath, FileMode.Create))
            //    {
            //        await usuariDto.FotoCarnet.CopyToAsync(stream);
            //    }

            //    usuari.FotoCarnet = carnetFileName;
            //}

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
        [Route("api/usuari/{id_usuari}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuari(string id_usuari)
        {
            var usuari = await _context.Usuaris.FindAsync(id_usuari);
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
        public async Task<ActionResult<DadesPagament>> GetDadesPagamentByUsuari(string id_usuari)
        {
            Usuari client = _context.Usuaris
                .Include(x => x.DadesPagaments)
                .Where(x => x.Dni.Equals(id_usuari)).FirstOrDefault();

            if (client != null)
            {
                DadesPagament dp = client.DadesPagaments.FirstOrDefault();

                if (dp != null)
                {
                    return dp;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return NotFound("Usuari no trobat");
            }
        }

        [Route("api/taxistes-top5")]
        [HttpGet]
        public async Task<ActionResult<List<TaxistaDTO>>> GetTop5Taxistes()
        {
            List<Usuari> taxistes = await _context.Usuaris
                .Include(x => x.Viatges)
                .Where(x => x.Rol == true)
                .OrderByDescending(x => x.Viatges.Count)
                .Take(5)
                .ToListAsync();

            List<TaxistaDTO> dtoList = taxistes.Select(t => new TaxistaDTO(t)).ToList();

            return dtoList;
        }

        // GET: api/usuaris/filtre/filtrePer
        [Route("api/usuaris/{filtre}/{filtraPer}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuari>>> GetUsuarisFiltre(string filtre, int filtraPer)
        {
            if (filtraPer == 1)
            {
                return await _context.Usuaris.Where(x => x.Dni.StartsWith(filtre)).ToListAsync();
            }
            else if (filtraPer == 2)
            {
                return await _context.Usuaris.Where(x => x.Nom.Contains(filtre)).ToListAsync();
            }

            return NotFound("El filtre que intentes utilitzar no està disponible...");
        }

        [Route("api/usuari_image/{id}")]
        [HttpPut]
        public async Task<IActionResult> PutUsuariImage(string id, IFormFile? f_perfil, IFormFile? f_tecnica)
        {
            Usuari user = _context.Usuaris.Where(x => x.Dni == id).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            if (f_perfil != null)
            {
                var perfilFileName = $"{Guid.NewGuid()}_{f_perfil.FileName}";
                var perfilPath = Path.Combine("Photos", perfilFileName);

                using (var stream = new FileStream(perfilPath, FileMode.Create))
                {
                    await f_perfil.CopyToAsync(stream);
                }

                user.FotoPerfil = perfilFileName;
            }

            if (f_tecnica != null)
            {
                var carnetFileName = $"{Guid.NewGuid()}_{f_tecnica.FileName}";
                var carnetPath = Path.Combine("Photos", carnetFileName);

                using (var stream = new FileStream(carnetPath, FileMode.Create))
                {
                    await f_tecnica.CopyToAsync(stream);
                }

                user.FotoCarnet = carnetFileName;
            }
                _context.Entry(user).State = EntityState.Modified;

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
    }
}
