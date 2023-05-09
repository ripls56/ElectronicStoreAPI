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
    public class OrderHistoriesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public OrderHistoriesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/OrderHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHistory>>> GetOrderHistories()
        {
          if (_context.OrderHistories == null)
          {
              return NotFound();
          }
            return await _context.OrderHistories.ToListAsync();
        }

        // GET: api/OrderHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderHistory>> GetOrderHistory(int? id)
        {
          if (_context.OrderHistories == null)
          {
              return NotFound();
          }
            var orderHistory = await _context.OrderHistories.FindAsync(id);

            if (orderHistory == null)
            {
                return NotFound();
            }

            return orderHistory;
        }

        // PUT: api/OrderHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderHistory(int? id, OrderHistory orderHistory)
        {
            if (id != orderHistory.IdOrderHistory)
            {
                return BadRequest();
            }

            _context.Entry(orderHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderHistoryExists(id))
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

        // POST: api/OrderHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderHistory>> PostOrderHistory(OrderHistory orderHistory)
        {
          if (_context.OrderHistories == null)
          {
              return Problem("Entity set 'ElectronicStoreContext.OrderHistories'  is null.");
          }
            _context.OrderHistories.Add(orderHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderHistory", new { id = orderHistory.IdOrderHistory }, orderHistory);
        }

        // DELETE: api/OrderHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderHistory(int? id)
        {
            if (_context.OrderHistories == null)
            {
                return NotFound();
            }
            var orderHistory = await _context.OrderHistories.FindAsync(id);
            if (orderHistory == null)
            {
                return NotFound();
            }

            _context.OrderHistories.Remove(orderHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderHistoryExists(int? id)
        {
            return (_context.OrderHistories?.Any(e => e.IdOrderHistory == id)).GetValueOrDefault();
        }
    }
}
