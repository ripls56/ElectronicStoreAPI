using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "client, Менеджер, Продавец, Администратор БД")]
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
        [Authorize(Roles = "client, Менеджер, Продавец, Администратор БД")]
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutBrand(int? id, Brand brand)
        {
            if (id != brand.IdBrands)
            {
                return BadRequest(error: "Need to be the same as id in query");
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
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
        [Authorize(Roles = "Менеджер, Администратор БД")]
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

        // DELETE: api/Brands?id=1&2&3&4
        /// <summary>
        /// Удаление товаров по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteBrands([FromQuery] List<int>? idList)
        {
            if (_context.Brands == null)
            {
                return NotFound();
            }

            var models = new List<Brand?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.Brands.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.Brands.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool BrandExists(int? id)
        {
            return (_context.Brands?.Any(e => e.IdBrands == id)).GetValueOrDefault();
        }
    }
}
