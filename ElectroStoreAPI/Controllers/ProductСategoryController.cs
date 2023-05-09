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
    public class ProductСategoryController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public ProductСategoryController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/ProductСategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductСategory>>> GetProductСategories()
        {
          if (_context.ProductСategories == null)
          {
              return NotFound();
          }
            return await _context.ProductСategories.ToListAsync();
        }

        // GET: api/ProductСategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductСategory>> GetProductСategory(int? id)
        {
          if (_context.ProductСategories == null)
          {
              return NotFound();
          }
            var productСategory = await _context.ProductСategories.FindAsync(id);

            if (productСategory == null)
            {
                return NotFound();
            }

            return productСategory;
        }

        // PUT: api/ProductСategory/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductСategory(int? id, ProductСategory productСategory)
        {
            if (id != productСategory.IdProductСategories)
            {
                return BadRequest();
            }

            _context.Entry(productСategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductСategory(int? id)
        {
            if (_context.ProductСategories == null)
            {
                return NotFound();
            }
            var productСategory = await _context.ProductСategories.FindAsync(id);
            if (productСategory == null)
            {
                return NotFound();
            }

            _context.ProductСategories.Remove(productСategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductСategoryExists(int? id)
        {
            return (_context.ProductСategories?.Any(e => e.IdProductСategories == id)).GetValueOrDefault();
        }
    }
}
