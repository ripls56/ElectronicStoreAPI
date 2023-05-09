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
    public class VendorTypesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public VendorTypesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/VendorTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendorType>>> GetVendorTypes()
        {
          if (_context.VendorTypes == null)
          {
              return NotFound();
          }
            return await _context.VendorTypes.ToListAsync();
        }

        // GET: api/VendorTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VendorType>> GetVendorType(int? id)
        {
          if (_context.VendorTypes == null)
          {
              return NotFound();
          }
            var vendorType = await _context.VendorTypes.FindAsync(id);

            if (vendorType == null)
            {
                return NotFound();
            }

            return vendorType;
        }

        // PUT: api/VendorTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendorType(int? id, VendorType vendorType)
        {
            if (id != vendorType.IdPost)
            {
                return BadRequest();
            }

            _context.Entry(vendorType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        [HttpPost]
        public async Task<ActionResult<VendorType>> PostVendorType(VendorType vendorType)
        {
          if (_context.VendorTypes == null)
          {
              return Problem("Entity set 'ElectronicStoreContext.VendorTypes'  is null.");
          }
            _context.VendorTypes.Add(vendorType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendorType", new { id = vendorType.IdPost }, vendorType);
        }

        // DELETE: api/VendorTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendorType(int? id)
        {
            if (_context.VendorTypes == null)
            {
                return NotFound();
            }
            var vendorType = await _context.VendorTypes.FindAsync(id);
            if (vendorType == null)
            {
                return NotFound();
            }

            _context.VendorTypes.Remove(vendorType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorTypeExists(int? id)
        {
            return (_context.VendorTypes?.Any(e => e.IdPost == id)).GetValueOrDefault();
        }
    }
}
