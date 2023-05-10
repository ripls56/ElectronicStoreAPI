using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public StocksController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Stocks
        /// <summary>
        /// Получение кол-ва товаров на складе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Stock>>> GetStocks()
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            return await _context.Stocks.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Stocks/5
        /// <summary>
        /// Получение кол-ва товара по на складе по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Stock>> GetStock(int? id)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks.FindAsync(id).ConfigureAwait(false);

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }

        // PUT: api/Stocks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление кол-ва товара на складе по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutStock(int? id, Stock stock)
        {
            if (id != stock.IdStock)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(stock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление кол-ва товара на склад
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<ActionResult<Stock>> PostStock(Stock stock)
        {
            if (_context.Stocks == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Stocks'  is null.");
            }
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetStock", new { id = stock.IdStock }, stock);
        }

        // DELETE: api/Stocks/5
        /// <summary>
        /// Удаление кол-ва товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock(int? id)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks.FindAsync(id).ConfigureAwait(false);
            if (stock == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Stocks?id=1&2&3&4
        /// <summary>
        /// Удаление товаров на складе по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteStocks([FromQuery] List<int>? idList)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }

            var models = new List<Stock?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.Stocks.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.Stocks.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool StockExists(int? id)
        {
            return (_context.Stocks?.Any(e => e.IdStock == id)).GetValueOrDefault();
        }
    }
}
