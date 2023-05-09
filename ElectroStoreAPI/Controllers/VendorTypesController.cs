using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class VendorTypesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public VendorTypesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/VendorTypes
        /// <summary>
        /// Получение типа поставщиков
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorType>>> GetVendorTypes()
        {
            if (_context.VendorTypes == null)
            {
                return NotFound();
            }
            return await _context.VendorTypes.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/VendorTypes/5
        /// <summary>
        /// Получение типа поставщика по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VendorType>> GetVendorType(int? id)
        {
            if (_context.VendorTypes == null)
            {
                return NotFound();
            }
            var vendorType = await _context.VendorTypes.FindAsync(id).ConfigureAwait(false);

            if (vendorType == null)
            {
                return NotFound();
            }

            return vendorType;
        }

        // PUT: api/VendorTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление поставщика по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vendorType"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutVendorType(int? id, VendorType vendorType)
        {
            if (id != vendorType.IdPost)
            {
                return BadRequest();
            }

            _context.Entry(vendorType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorTypeExists(id))
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

        // POST: api/VendorTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление типа поставщика
        /// </summary>
        /// <param name="vendorType"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<VendorType>> PostVendorType(VendorType vendorType)
        {
            if (_context.VendorTypes == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.VendorTypes'  is null.");
            }
            _context.VendorTypes.Add(vendorType);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetVendorType", new { id = vendorType.IdPost }, vendorType);
        }

        // DELETE: api/VendorTypes/5
        /// <summary>
        /// Удаление типа поставщика по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteVendorType(int? id)
        {
            if (_context.VendorTypes == null)
            {
                return NotFound();
            }
            var vendorType = await _context.VendorTypes.FindAsync(id).ConfigureAwait(false);
            if (vendorType == null)
            {
                return NotFound();
            }

            _context.VendorTypes.Remove(vendorType);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool VendorTypeExists(int? id)
        {
            return (_context.VendorTypes?.Any(e => e.IdPost == id)).GetValueOrDefault();
        }
    }
}
