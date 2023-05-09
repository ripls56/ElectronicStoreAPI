using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public BrandsController(ElectronicStoreContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получение всех брендов
        /// </summary>
        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            return await _context.Brands.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Получение бренда по id
        /// </summary>
        // GET: api/Brands/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Brand>> GetBrand(int? id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id).ConfigureAwait(false);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        /// <summary>
        /// Обновление бренда по id
        /// </summary>
        // PUT: api/Brands/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutBrand(int? id, Brand brand)
        {
            if (id != brand.IdBrands)
            {
                return BadRequest();
            }

            _context.Entry(brand).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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

        /// <summary>
        /// Добавить новый бренд
        /// </summary>
        /// <remarks>
        /// Id должно быть null
        /// </remarks>
        /// <param name="brand"></param>
        /// <returns></returns>
        // POST: api/Brands
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            if (_context.Brands == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Brands' is null.", title: "Must not be null");
            }
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetBrand", new { id = brand.IdBrands }, brand);
        }

        /// <summary>
        /// Удаление бренда по id
        /// </summary>
        // DELETE: api/Brands/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteBrand(int? id)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }
            var brand = await _context.Brands.FindAsync(id).ConfigureAwait(false);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool BrandExists(int? id)
        {
            return (_context.Brands?.Any(e => e.IdBrands == id)).GetValueOrDefault();
        }
    }
}
