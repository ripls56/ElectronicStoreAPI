using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoriesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public OrderHistoriesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/OrderHistories
        /// <summary>
        /// Получение истории заказов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderHistory>>> GetOrderHistories()
        {
            if (_context.OrderHistories == null)
            {
                return NotFound();
            }
            return await _context.OrderHistories.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/OrderHistories/5
        /// <summary>
        /// Получение истории по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderHistory>> GetOrderHistory(int? id)
        {
            if (_context.OrderHistories == null)
            {
                return NotFound();
            }
            var orderHistory = await _context.OrderHistories.FindAsync(id).ConfigureAwait(false);

            if (orderHistory == null)
            {
                return NotFound();
            }

            return orderHistory;
        }

        // PUT: api/OrderHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление истории заказа по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderHistory"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutOrderHistory(int? id, OrderHistory orderHistory)
        {
            if (id != orderHistory.IdOrderHistory)
            {
                return BadRequest();
            }

            _context.Entry(orderHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
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
        /// <summary>
        /// Добавление истории заказа
        /// </summary>
        /// <param name="orderHistory"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OrderHistory>> PostOrderHistory(OrderHistory orderHistory)
        {
            if (_context.OrderHistories == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.OrderHistories'  is null.");
            }
            _context.OrderHistories.Add(orderHistory);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetOrderHistory", new { id = orderHistory.IdOrderHistory }, orderHistory);
        }

        // DELETE: api/OrderHistories/5
        /// <summary>
        /// Удаление истории заказа по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrderHistory(int? id)
        {
            if (_context.OrderHistories == null)
            {
                return NotFound();
            }
            var orderHistory = await _context.OrderHistories.FindAsync(id).ConfigureAwait(false);
            if (orderHistory == null)
            {
                return NotFound();
            }

            _context.OrderHistories.Remove(orderHistory);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool OrderHistoryExists(int? id)
        {
            return (_context.OrderHistories?.Any(e => e.IdOrderHistory == id)).GetValueOrDefault();
        }
    }
}
