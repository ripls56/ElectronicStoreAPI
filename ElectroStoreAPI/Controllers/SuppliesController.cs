using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
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
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutSupply(int? id, Supply supply)
        {
            if (id != supply.IdSupplies)
            {
                return BadRequest(error: "Need to be the same as id in query");
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
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

        // DELETE: api/Supplies?id=1&2&3&4
        /// <summary>
        /// Удаление поставщиков на складе по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteSupplies([FromQuery] List<int>? idList)
        {
            if (_context.Supplies == null)
            {
                return NotFound();
            }

            var models = new List<Supply?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.Supplies.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.Supplies.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool SupplyExists(int? id)
        {
            return (_context.Supplies?.Any(e => e.IdSupplies == id)).GetValueOrDefault();
        }
    }
}
