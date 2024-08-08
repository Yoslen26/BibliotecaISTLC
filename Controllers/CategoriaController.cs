using BibliotecaISTLC.Context;
using BibliotecaISTLC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BibliotecaISTLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly BibliotecaIstlcContext _context;

        public CategoriaController(BibliotecaIstlcContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
        {
            return await _context.Categorias.ToListAsync();
        }

        // GET: api/Categoria/5
        [HttpGet("{id_categoria}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id_categoria)
        {
            var categoria = await _context.Categorias.FindAsync(id_categoria);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id_categoria}")]
        public async Task<IActionResult> PutCategoria(int id_categoria, Categoria categoria)
        {
            if (id_categoria != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id_categoria))
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

        // POST: api/Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CategoriaExists(categoria.IdCategoria))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCategoria", new { id_categoria = categoria.IdCategoria }, categoria);
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id_categoria}")]
        public async Task<IActionResult> DeleteCategoria(int id_categoria)
        {
            var categoria = await _context.Categorias.FindAsync(id_categoria);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id_categoria)
        {
            return _context.Categorias.Any(e => e.IdCategoria == id_categoria);
        }
    }
}
