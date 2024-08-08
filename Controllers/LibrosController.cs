using BibliotecaISTLC.Context;
using BibliotecaISTLC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaISTLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaIstlcContext _context;

        public LibrosController(BibliotecaIstlcContext context)
        {
            _context = context;
        }

        // GET: api/Libros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibros()
        {
            return await _context.Libros.ToListAsync();
        }

        // GET: api/Libros/5
        [HttpGet("{id_libro}")]
        public async Task<ActionResult<Libro>> GetLibro(int id_libro)
        {
            var libro = await _context.Libros.FindAsync(id_libro);

            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // PUT: api/Libros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_libro}")]
        public async Task<IActionResult> PutLibro(int id_libro, Libro libro)
        {
            if (id_libro != libro.IdLibros)
            {
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id_libro))
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

        // POST: api/Libros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Libro>> PostLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LibroExists(libro.IdLibros))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLibro", new { id_libro = libro.IdLibros }, libro);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id_libro}")]
        public async Task<IActionResult> DeleteLibro(int id_libro)
        {
            var libro = await _context.Libros.FindAsync(id_libro);
            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LibroExists(int id_libro)
        {
            return _context.Libros.Any(e => e.IdLibros == id_libro);
        }
    }
}
