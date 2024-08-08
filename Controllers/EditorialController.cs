using BibliotecaISTLC.Context;
using BibliotecaISTLC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaISTLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorialController : ControllerBase
    {
        private readonly BibliotecaIstlcContext _context;

        public EditorialController(BibliotecaIstlcContext context)
        {
            _context = context;
        }

        // GET: api/Editorial
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditorials()
        {
            return await _context.Editorials.ToListAsync();
        }

        // GET: api/Editorial/5
        [HttpGet("{id_editorial}")]
        public async Task<ActionResult<Editorial>> GetEditorial(int id_editorial)
        {
            var editorial = await _context.Editorials.FindAsync(id_editorial);

            if (editorial == null)
            {
                return NotFound();
            }

            return editorial;
        }

        // PUT: api/Editorial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_editorial}")]
        public async Task<IActionResult> PutEditorial(int id_editorial, Editorial editorial)
        {
            if (id_editorial != editorial.IdEditorial)
            {
                return BadRequest();
            }

            _context.Entry(editorial).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditorialExists(id_editorial))
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

        // POST: api/Editorial
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Editorial>> PostEditorial(Editorial editorial)
        {
            _context.Editorials.Add(editorial);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EditorialExists(editorial.IdEditorial))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEditorial", new { id_editorial = editorial.IdEditorial }, editorial);
        }

        // DELETE: api/Editorial/5
        [HttpDelete("{id_editorial}")]
        public async Task<IActionResult> DeleteEditorial(int id_editorial)
        {
            var editorial = await _context.Editorials.FindAsync(id_editorial);
            if (editorial == null)
            {
                return NotFound();
            }

            _context.Editorials.Remove(editorial);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EditorialExists(int id_editorial)
        {
            return _context.Editorials.Any(e => e.IdEditorial == id_editorial);
        }
    }
}
