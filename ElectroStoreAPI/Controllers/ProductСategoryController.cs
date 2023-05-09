using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class ProductСategoryController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public ProductСategoryController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/ProductСategory
        /// <summary>
        /// Получение категории товаров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductСategory>>> GetProductСategories()
        {
            if (_context.ProductСategories == null)
            {
                return NotFound();
            }
            return await _context.ProductСategories.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/ProductСategory/5
        /// <summary>
        /// Получение категории товаров по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductСategory>> GetProductСategory(int? id)
        {
            if (_context.ProductСategories == null)
            {
                return NotFound();
            }
            var productСategory = await _context.ProductСategories.FindAsync(id).ConfigureAwait(false);

            if (productСategory == null)
            {
                return NotFound();
            }

            return productСategory;
        }

        // PUT: api/ProductСategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление категории товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productСategory"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProductСategory(int? id, ProductСategory productСategory)
        {
            if (id != productСategory.IdProductСategories)
            {
                return BadRequest();
            }

            _context.Entry(productСategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductСategoryExists(id))
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

        // POST: api/ProductСategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление категории товара
        /// </summary>
        /// <param name="productСategory"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ProductСategory>> PostProductСategory(ProductСategory productСategory)
        {
            if (_context.ProductСategories == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.ProductСategories'  is null.");
            }
            _context.ProductСategories.Add(productСategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductСategory", new { id = productСategory.IdProductСategories }, productСategory);
        }

        // DELETE: api/ProductСategory/5
        /// <summary>
        /// Удаление категории товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductСategory(int? id)
        {
            if (_context.ProductСategories == null)
            {
                return NotFound();
            }
            var productСategory = await _context.ProductСategories.FindAsync(id).ConfigureAwait(false);
            if (productСategory == null)
            {
                return NotFound();
            }

            _context.ProductСategories.Remove(productСategory);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool ProductСategoryExists(int? id)
        {
            return (_context.ProductСategories?.Any(e => e.IdProductСategories == id)).GetValueOrDefault();
        }
    }
}
