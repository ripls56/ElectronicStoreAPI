using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class ClientPromocodesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public ClientPromocodesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/ClientPromocodes
        /// <summary>
        /// Получение использованных клиентами промокодов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientPromocode>>> GetClientPromocodes()
        {
            if (_context.ClientPromocodes == null)
            {
                return NotFound();
            }
            return await _context.ClientPromocodes.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/ClientPromocodes/5
        /// <summary>
        /// Получение использованного клиентами промокода по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientPromocode>> GetClientPromocode(int? id)
        {
            if (_context.ClientPromocodes == null)
            {
                return NotFound();
            }
            var clientPromocode = await _context.ClientPromocodes.FindAsync(id).ConfigureAwait(false);

            if (clientPromocode == null)
            {
                return NotFound();
            }

            return clientPromocode;
        }

        /// <summary>
        /// Изменение использованных клиентами промокодов
        /// </summary>
        // PUT: api/ClientPromocodes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutClientPromocode(int? id, ClientPromocode clientPromocode)
        {
            if (id != clientPromocode.IdClientPromocode)
            {
                return BadRequest();
            }

            _context.Entry(clientPromocode).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientPromocodeExists(id))
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
        /// Добавление использованных клиентами промокодов
        /// </summary>
        // POST: api/ClientPromocodes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientPromocode>> PostClientPromocode(ClientPromocode clientPromocode)
        {
            if (_context.ClientPromocodes == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.ClientPromocodes'  is null.");
            }
            _context.ClientPromocodes.Add(clientPromocode);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetClientPromocode", new { id = clientPromocode.IdClientPromocode }, clientPromocode);
        }

        // DELETE: api/ClientPromocodes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClientPromocode(int? id)
        {
            if (_context.ClientPromocodes == null)
            {
                return NotFound();
            }
            var clientPromocode = await _context.ClientPromocodes.FindAsync(id).ConfigureAwait(false);
            if (clientPromocode == null)
            {
                return NotFound();
            }

            _context.ClientPromocodes.Remove(clientPromocode);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool ClientPromocodeExists(int? id)
        {
            return (_context.ClientPromocodes?.Any(e => e.IdClientPromocode == id)).GetValueOrDefault();
        }
    }
}
