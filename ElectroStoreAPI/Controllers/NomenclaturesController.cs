using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class NomenclaturesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public NomenclaturesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Nomenclatures
        /// <summary>
        /// Получение товаров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nomenclature>>> GetNomenclatures()
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            return await _context.Nomenclatures.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Nomenclatures/5
        /// <summary>
        /// Получение товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Nomenclature>> GetNomenclature(int? id)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            var nomenclature = await _context.Nomenclatures.FindAsync(id).ConfigureAwait(false);

            if (nomenclature == null)
            {
                return NotFound();
            }

            return nomenclature;
        }

        // PUT: api/Nomenclatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nomenclature"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutNomenclature(int? id, Nomenclature nomenclature)
        {
            if (id != nomenclature.IdNomenclature)
            {
                return BadRequest();
            }

            _context.Entry(nomenclature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NomenclatureExists(id))
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

        // POST: api/Nomenclatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="nomenclature"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Nomenclature>> PostNomenclature(Nomenclature nomenclature)
        {
            if (_context.Nomenclatures == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Nomenclatures'  is null.");
            }
            _context.Nomenclatures.Add(nomenclature);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetNomenclature", new { id = nomenclature.IdNomenclature }, nomenclature);
        }

        // DELETE: api/Nomenclatures/5
        /// <summary>
        /// Удаление товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteNomenclature(int? id)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            var nomenclature = await _context.Nomenclatures.FindAsync(id).ConfigureAwait(false);
            if (nomenclature == null)
            {
                return NotFound();
            }

            _context.Nomenclatures.Remove(nomenclature);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool NomenclatureExists(int? id)
        {
            return (_context.Nomenclatures?.Any(e => e.IdNomenclature == id)).GetValueOrDefault();
        }
    }
}
