using ElectroStoreAPI.Models;
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
        public async Task<IActionResult> PutPromocode(int? id, Promocode promocode)
        {
            if (id != promocode.IdPromocode)
            {
                return BadRequest();
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
        [HttpDelete("{id}")]
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

        private bool PromocodeExists(int? id)
        {
            return (_context.Promocodes?.Any(e => e.IdPromocode == id)).GetValueOrDefault();
        }
    }
}
