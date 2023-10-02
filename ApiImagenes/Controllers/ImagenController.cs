using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiImagenes.Data;
using ApiImagenes.Models;
using ApiImagenes.Services;

namespace ApiImagenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly ApiImagenesContext _context;
        private ServiceS3 service;
        public ImagenController(ApiImagenesContext context, ServiceS3 service)
        {
            _context = context;
            this.service = service;
        }

        // GET: api/Imagen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Imagen>>> GetImagens()
        {
            return await _context.Imagens.ToListAsync();
        }

        // GET: api/Imagen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Imagen>> GetImagen(int id)
        {
            if (_context.Imagens == null)
            {
                return NotFound();
            }
            var imagen = await _context.Imagens.FindAsync(id);

            if (imagen == null)
            {
                return NotFound();
            }

            return imagen;
        }

        // PUT: api/Imagen/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagen(int id, Imagen imagen)
        {
            if (id != imagen.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenExists(id))
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

        // POST: api/Imagen
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Imagen>> PostImagen([FromForm]Imagen imagen)
        {
            if (_context.Imagens == null)
            {
                return Problem("Entity set 'ApiImagenesContext.Imagens'  is null.");
            }
            List<string> imagenes = await this.service.GetFilesAsync();
            string extension =imagen.Imagen2.FileName.Split(".")[1];
            string fileName = imagenes.Count + "." + extension;

            using (Stream stream =imagen.Imagen2.OpenReadStream())
            {
                await this.service.UploadFileAsync(stream, fileName);
            }
            imagen=new Imagen
            {
                Imagen1 = fileName
            };
            _context.Imagens.Add(imagen);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagen", new { id = imagen.Id }, imagen);
        }

        // DELETE: api/Imagen/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagen(int id)
        {
            if (_context.Imagens == null)
            {
                return NotFound();
            }
            var imagen = await _context.Imagens.FindAsync(id);
            if (imagen == null)
            {
                return NotFound();
            }

            _context.Imagens.Remove(imagen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagenExists(int id)
        {
            return (_context.Imagens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
