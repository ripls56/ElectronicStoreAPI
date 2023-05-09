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
    public class StoreAddressesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public StoreAddressesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/StoreAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoreAddress>>> GetStoreAddresses()
        {
          if (_context.StoreAddresses == null)
          {
              return NotFound();
          }
            return await _context.StoreAddresses.ToListAsync();
        }

        // GET: api/StoreAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoreAddress>> GetStoreAddress(int? id)
        {
          if (_context.StoreAddresses == null)
          {
              return NotFound();
          }
            var storeAddress = await _context.StoreAddresses.FindAsync(id);

            if (storeAddress == null)
            {
                return NotFound();
            }

            return storeAddress;
        }

        // PUT: api/StoreAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoreAddress(int? id, StoreAddress storeAddress)
        {
            if (id != storeAddress.IdStoreAddresses)
            {
                return BadRequest();
            }

            _context.Entry(storeAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreAddressExists(id))
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

        // POST: api/StoreAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoreAddress>> PostStoreAddress(StoreAddress storeAddress)
        {
          if (_context.StoreAddresses == null)
          {
              return Problem("Entity set 'ElectronicStoreContext.StoreAddresses'  is null.");
          }
            _context.StoreAddresses.Add(storeAddress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoreAddress", new { id = storeAddress.IdStoreAddresses }, storeAddress);
        }

        // DELETE: api/StoreAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoreAddress(int? id)
        {
            if (_context.StoreAddresses == null)
            {
                return NotFound();
            }
            var storeAddress = await _context.StoreAddresses.FindAsync(id);
            if (storeAddress == null)
            {
                return NotFound();
            }

            _context.StoreAddresses.Remove(storeAddress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreAddressExists(int? id)
        {
            return (_context.StoreAddresses?.Any(e => e.IdStoreAddresses == id)).GetValueOrDefault();
        }
    }
}
