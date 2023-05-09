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
    public class NomenclatureOrdersController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public NomenclatureOrdersController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/NomenclatureOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NomenclatureOrder>>> GetNomenclatureOrders()
        {
          if (_context.NomenclatureOrders == null)
          {
              return NotFound();
          }
            return await _context.NomenclatureOrders.ToListAsync();
        }

        // GET: api/NomenclatureOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NomenclatureOrder>> GetNomenclatureOrder(int? id)
        {
          if (_context.NomenclatureOrders == null)
          {
              return NotFound();
          }
            var nomenclatureOrder = await _context.NomenclatureOrders.FindAsync(id);

            if (nomenclatureOrder == null)
            {
                return NotFound();
            }

            return nomenclatureOrder;
        }

        // PUT: api/NomenclatureOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNomenclatureOrder(int? id, NomenclatureOrder nomenclatureOrder)
        {
            if (id != nomenclatureOrder.IdNomenclatureOrder)
            {
                return BadRequest();
            }

            _context.Entry(nomenclatureOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NomenclatureOrderExists(id))
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

        // POST: api/NomenclatureOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NomenclatureOrder>> PostNomenclatureOrder(NomenclatureOrder nomenclatureOrder)
        {
          if (_context.NomenclatureOrders == null)
          {
              return Problem("Entity set 'ElectronicStoreContext.NomenclatureOrders'  is null.");
          }
            _context.NomenclatureOrders.Add(nomenclatureOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNomenclatureOrder", new { id = nomenclatureOrder.IdNomenclatureOrder }, nomenclatureOrder);
        }

        // DELETE: api/NomenclatureOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNomenclatureOrder(int? id)
        {
            if (_context.NomenclatureOrders == null)
            {
                return NotFound();
            }
            var nomenclatureOrder = await _context.NomenclatureOrders.FindAsync(id);
            if (nomenclatureOrder == null)
            {
                return NotFound();
            }

            _context.NomenclatureOrders.Remove(nomenclatureOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NomenclatureOrderExists(int? id)
        {
            return (_context.NomenclatureOrders?.Any(e => e.IdNomenclatureOrder == id)).GetValueOrDefault();
        }
    }
}
