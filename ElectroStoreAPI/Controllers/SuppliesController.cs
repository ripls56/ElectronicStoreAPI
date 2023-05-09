using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public SuppliesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Supplies
        /// <summary>
        /// Получение поставщиков
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supply>>> GetSupplies()
        {
            if (_context.Supplies == null)
            {
                return NotFound();
            }
            return await _context.Supplies.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Supplies/5
        /// <summary>
        /// Получение поставщика по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Supply>> GetSupply(int? id)
        {
            if (_context.Supplies == null)
            {
                return NotFound();
            }
            var supply = await _context.Supplies.FindAsync(id).ConfigureAwait(false);

            if (supply == null)
            {
                return NotFound();
            }

            return supply;
        }

        // PUT: api/Supplies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление поставщика по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="supply"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutSupply(int? id, Supply supply)
        {
            if (id != supply.IdSupplies)
            {
                return BadRequest();
            }

            _context.Entry(supply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplyExists(id))
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

        // POST: api/Supplies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление поставщика по id
        /// </summary>
        /// <param name="supply"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Supply>> PostSupply(Supply supply)
        {
            if (_context.Supplies == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Supplies'  is null.");
            }
            _context.Supplies.Add(supply);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetSupply", new { id = supply.IdSupplies }, supply);
        }

        // DELETE: api/Supplies/5
        /// <summary>
        /// Удаление поставщика по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSupply(int? id)
        {
            if (_context.Supplies == null)
            {
                return NotFound();
            }
            var supply = await _context.Supplies.FindAsync(id).ConfigureAwait(false);
            if (supply == null)
            {
                return NotFound();
            }

            _context.Supplies.Remove(supply);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool SupplyExists(int? id)
        {
            return (_context.Supplies?.Any(e => e.IdSupplies == id)).GetValueOrDefault();
        }
    }
}
