﻿using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class StoreAddressesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public StoreAddressesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/StoreAddresses
        /// <summary>
        /// Получение адресов магазинов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StoreAddress>>> GetStoreAddresses()
        {
            if (_context.StoreAddresses == null)
            {
                return NotFound();
            }
            return await _context.StoreAddresses.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/StoreAddresses/5
        /// <summary>
        /// Получение адреса магазина по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<StoreAddress>> GetStoreAddress(int? id)
        {
            if (_context.StoreAddresses == null)
            {
                return NotFound();
            }
            var storeAddress = await _context.StoreAddresses.FindAsync(id).ConfigureAwait(false);

            if (storeAddress == null)
            {
                return NotFound();
            }

            return storeAddress;
        }

        // PUT: api/StoreAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление адреса магазина по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="storeAddress"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutStoreAddress(int? id, StoreAddress storeAddress)
        {
            if (id != storeAddress.IdStoreAddresses)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(storeAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
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
        /// <summary>
        /// Добавление адреса магазина
        /// </summary>
        /// <param name="storeAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<ActionResult<StoreAddress>> PostStoreAddress(StoreAddress storeAddress)
        {
            if (_context.StoreAddresses == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.StoreAddresses'  is null.");
            }
            _context.StoreAddresses.Add(storeAddress);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetStoreAddress", new { id = storeAddress.IdStoreAddresses }, storeAddress);
        }

        // DELETE: api/StoreAddresses/5
        /// <summary>
        /// Удаление адреса магазина по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteStoreAddress(int? id)
        {
            if (_context.StoreAddresses == null)
            {
                return NotFound();
            }
            var storeAddress = await _context.StoreAddresses.FindAsync(id).ConfigureAwait(false);
            if (storeAddress == null)
            {
                return NotFound();
            }

            _context.StoreAddresses.Remove(storeAddress);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/StoreAddresses?id=1&2&3&4
        /// <summary>
        /// Удаление адресов магазинов по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteStoreAddresses([FromQuery] List<int>? idList)
        {
            if (_context.StoreAddresses == null)
            {
                return NotFound();
            }

            var models = new List<StoreAddress?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.StoreAddresses.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.StoreAddresses.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool StoreAddressExists(int? id)
        {
            return (_context.StoreAddresses?.Any(e => e.IdStoreAddresses == id)).GetValueOrDefault();
        }
    }
}
