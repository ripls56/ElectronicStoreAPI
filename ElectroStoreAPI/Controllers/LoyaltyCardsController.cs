using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyCardsController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public LoyaltyCardsController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyCards
        /// <summary>
        /// Получение карт лояльности
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyCard>>> GetLoyaltyCards()
        {
            if (_context.LoyaltyCards == null)
            {
                return NotFound();
            }
            return await _context.LoyaltyCards.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/LoyaltyCards/5
        /// <summary>
        /// Получение карты лояльности по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyCard>> GetLoyaltyCard(int? id)
        {
            if (_context.LoyaltyCards == null)
            {
                return NotFound();
            }
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id).ConfigureAwait(false);

            if (loyaltyCard == null)
            {
                return NotFound();
            }

            return loyaltyCard;
        }

        // PUT: api/LoyaltyCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление карты лояльности по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loyaltyCard"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutLoyaltyCard(int? id, LoyaltyCard loyaltyCard)
        {
            if (id != loyaltyCard.IdLoyaltyCard)
            {
                return BadRequest();
            }

            _context.Entry(loyaltyCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltyCardExists(id))
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/LoyaltyCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление карты лояльности
        /// </summary>
        /// <param name="loyaltyCard"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LoyaltyCard>> PostLoyaltyCard(LoyaltyCard loyaltyCard)
        {
            if (_context.LoyaltyCards == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.LoyaltyCards'  is null.");
            }
            _context.LoyaltyCards.Add(loyaltyCard);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetLoyaltyCard", new { id = loyaltyCard.IdLoyaltyCard }, loyaltyCard);
        }

        // DELETE: api/LoyaltyCards/5
        /// <summary>
        /// Удаление карты лояльности по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLoyaltyCard(int? id)
        {
            if (_context.LoyaltyCards == null)
            {
                return NotFound();
            }
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id).ConfigureAwait(false);
            if (loyaltyCard == null)
            {
                return NotFound();
            }

            _context.LoyaltyCards.Remove(loyaltyCard);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool LoyaltyCardExists(int? id)
        {
            return (_context.LoyaltyCards?.Any(e => e.IdLoyaltyCard == id)).GetValueOrDefault();
        }
    }
}
