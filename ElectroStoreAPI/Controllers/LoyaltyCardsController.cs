using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
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
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutLoyaltyCard(int? id, LoyaltyCard loyaltyCard)
        {
            if (id != loyaltyCard.IdLoyaltyCard)
            {
                return BadRequest(error: "Need to be the same as id in query");
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
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

        // DELETE: api/LoyaltyCards?id=1&2&3&4
        /// <summary>
        /// Удаление карт лояльности по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteLoyaltyCards([FromQuery] List<int>? idList)
        {
            if (_context.LoyaltyCards == null)
            {
                return NotFound();
            }

            var models = new List<LoyaltyCard?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.LoyaltyCards.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.LoyaltyCards.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool LoyaltyCardExists(int? id)
        {
            return (_context.LoyaltyCards?.Any(e => e.IdLoyaltyCard == id)).GetValueOrDefault();
        }
    }
}
