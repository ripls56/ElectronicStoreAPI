using ElectroStoreAPI.Core;
using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<List<Client>>> GetClients([FromQuery]PaginateParameters paginateParameters)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            return PagedList<Client>.ToPagedList(_context.Clients!, paginateParameters.pageNumber, paginateParameters.pageSize);
        }

        // GET: api/Clients/asd?sort=desc
        /// <summary>
        /// Поиск клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet("{query}")]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<IEnumerable<Client>>> GetNomenclatures(string? query, [FromQuery]PaginateParameters paginateParameters, string? sort = "asc")
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }

            return sort.ToLower() switch
            {
                "asc" => PagedList<Client>.ToPagedList(
                    _context.Clients.OrderBy(n => n.LoginClient)
                        .Where(n => n.LoginClient.Contains(query) || n.PhoneClient.Contains(query)),
                    paginateParameters.pageNumber, paginateParameters.pageSize),
                "desc" => PagedList<Client>.ToPagedList(
                    _context.Clients.OrderByDescending(n => n.LoginClient)
                        .Where(n => n.LoginClient.Contains(query) || n.PhoneClient.Contains(query)),
                    paginateParameters.pageNumber, paginateParameters.pageSize),
                _ => BadRequest()
            };
        }

        // GET: api/Clients/5
        /// <summary>
        /// Получение клиентов по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
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
        [HttpPut("{id:int}")]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<IActionResult> PutClient(int? id, Client client)
        {
            if (id != client.IdClient)
            {
                return BadRequest(error: "Need to be the same as id in query");
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
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<ActionResult<Client>> PostClient(Client? client)
        {
            try
            {
                if (_context.Clients == null)
                {
                    return Problem("Entity set 'ElectronicStoreContext.Clients'  is null.");
                }

                _context.Clients.Add(client);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return CreatedAtAction("GetClient", new { id = client.IdClient }, client);
            }
            catch
            {
                return NotFound();
            }
            finally
            {
                Response.OnCompleted(async () =>
                {
                    new TokenController(_context).Authorize(authParams: new AuthParams(login: client.LoginClient, client.PasswordClient)).ConfigureAwait(false);
                });
            }
        }

        // DELETE: api/Clients/5
        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
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

            client.IsDelete = true;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Clients?id=1&2&3&4
        /// <summary>
        /// Удаление(логическое) клиентов по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteClient([FromQuery] List<int>? idList)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }

            if (idList != null)
                foreach (var item in idList)
                {
                    var model = await _context.Clients.FindAsync(item).ConfigureAwait(false);
                    if (model == null)
                        return BadRequest(error: $"Id:{item} not found");
                    if (model.IsDelete == true)
                        return NotFound("Already delete");
                    model.IsDelete = true;

                }
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool ClientExists(int? id)
        {
            return (_context.Clients?.Any(e => e.IdClient == id)).GetValueOrDefault();
        }
    }
}
