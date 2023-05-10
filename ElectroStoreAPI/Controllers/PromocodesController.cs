using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class PromocodesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public PromocodesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Promocodes
        /// <summary>
        /// Получение промокодов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Promocode>>> GetPromocodes()
        {
            if (_context.Promocodes == null)
            {
                return NotFound();
            }
            return await _context.Promocodes.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Promocodes/5
        /// <summary>
        /// Получение промокода по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Promocode>> GetPromocode(int? id)
        {
            if (_context.Promocodes == null)
            {
                return NotFound();
            }
            var promocode = await _context.Promocodes.FindAsync(id).ConfigureAwait(false);

            if (promocode == null)
            {
                return NotFound();
            }

            return promocode;
        }

        // PUT: api/Promocodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление промокода по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="promocode"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutPromocode(int? id, Promocode promocode)
        {
            if (id != promocode.IdPromocode)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(promocode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PromocodeExists(id))
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

        // POST: api/Promocodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление промокода
        /// </summary>
        /// <param name="promocode"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<ActionResult<Promocode>> PostPromocode(Promocode promocode)
        {
            if (_context.Promocodes == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Promocodes'  is null.");
            }
            _context.Promocodes.Add(promocode);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetPromocode", new { id = promocode.IdPromocode }, promocode);
        }

        // DELETE: api/Promocodes/5
        /// <summary>
        /// Удаление промокода по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeletePromocode(int? id)
        {
            if (_context.Promocodes == null)
            {
                return NotFound();
            }
            var promocode = await _context.Promocodes.FindAsync(id).ConfigureAwait(false);
            if (promocode == null)
            {
                return NotFound();
            }

            _context.Promocodes.Remove(promocode);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Promocodes?id=1&2&3&4
        /// <summary>
        /// Удаление промокодов по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeletePromocodes([FromQuery] List<int>? idList)
        {
            if (_context.Promocodes == null)
            {
                return NotFound();
            }

            var models = new List<Promocode?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.Promocodes.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.Promocodes.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool PromocodeExists(int? id)
        {
            return (_context.Promocodes?.Any(e => e.IdPromocode == id)).GetValueOrDefault();
        }
    }
}
