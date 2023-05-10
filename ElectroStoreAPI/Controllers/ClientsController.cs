using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public ClientsController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        /// <summary>
        /// Получение клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Client>> GetClients()
        {
            //if (_context.Clients == null)
            //{
            //    return NotFound();
            //}
            return (await _context.Clients.ToListAsync().ConfigureAwait(false))!;
        }

        // GET: api/Nomenclatures/t-shirt?sort=desc
        /// <summary>
        /// Поиск клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("{query}")]
        public async Task<ActionResult<IEnumerable<Client>>> GetNomenclatures(string? query, string? sort = "asc")
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            if (sort.ToLower().Equals("asc"))
            {
                return await _context.Clients.OrderBy(n => n.LoginClient).Where(n => n.LoginClient.Contains(query) || n.PhoneClient.Contains(query)).ToListAsync().ConfigureAwait(false);
            }
            else if (sort.ToLower().Equals("desc"))
            {
                return await _context.Clients.OrderByDescending(n => n.LoginClient).Where(n => n.LoginClient.Contains(query) || n.PhoneClient.Contains(query)).ToListAsync().ConfigureAwait(false);
            }
            return BadRequest();
        }

        // GET: api/Clients/5
        /// <summary>
        /// Получение клиентов по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetClient(int? id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id).ConfigureAwait(false);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление клиента по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int? id, Client client)
        {
            if (id != client.IdClient)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление клиента
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client? client)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Clients'  is null.");
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
        }

        // DELETE: api/Clients/5
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int? id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id).ConfigureAwait(false);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool ClientExists(int? id)
        {
            return (_context.Clients?.Any(e => e.IdClient == id)).GetValueOrDefault();
        }
    }
}
