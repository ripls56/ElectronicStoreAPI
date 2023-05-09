using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectroStoreAPI.Models;

namespace ElectroStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoyaltyCardsController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public LoyaltyCardsController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/LoyaltyCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoyaltyCard>>> GetLoyaltyCards()
        {
          if (_context.LoyaltyCards == null)
          {
              return NotFound();
          }
            return await _context.LoyaltyCards.ToListAsync();
        }

        // GET: api/LoyaltyCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoyaltyCard>> GetLoyaltyCard(int? id)
        {
          if (_context.LoyaltyCards == null)
          {
              return NotFound();
          }
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id);

            if (loyaltyCard == null)
            {
                return NotFound();
            }

            return loyaltyCard;
        }

        // PUT: api/LoyaltyCards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoyaltyCard(int? id, LoyaltyCard loyaltyCard)
        {
            if (id != loyaltyCard.IdLoyaltyCard)
            {
                return BadRequest();
            }

            _context.Entry(loyaltyCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoyaltyCardExists(id))
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

        // POST: api/LoyaltyCards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoyaltyCard>> PostLoyaltyCard(LoyaltyCard loyaltyCard)
        {
          if (_context.LoyaltyCards == null)
          {
              return Problem("Entity set 'ElectronicStoreContext.LoyaltyCards'  is null.");
          }
            _context.LoyaltyCards.Add(loyaltyCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLoyaltyCard", new { id = loyaltyCard.IdLoyaltyCard }, loyaltyCard);
        }

        // DELETE: api/LoyaltyCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoyaltyCard(int? id)
        {
            if (_context.LoyaltyCards == null)
            {
                return NotFound();
            }
            var loyaltyCard = await _context.LoyaltyCards.FindAsync(id);
            if (loyaltyCard == null)
            {
                return NotFound();
            }

            _context.LoyaltyCards.Remove(loyaltyCard);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoyaltyCardExists(int? id)
        {
            return (_context.LoyaltyCards?.Any(e => e.IdLoyaltyCard == id)).GetValueOrDefault();
        }
    }
}
