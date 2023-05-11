using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAddressesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public EmployeeAddressesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/EmployeeAddresses
        /// <summary>
        /// Получение рабочих адресов сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<IEnumerable<EmployeeAddress>>> GetEmployeeAddresses()
        {
            if (_context.EmployeeAddresses == null)
            {
                return NotFound();
            }
            return await _context.EmployeeAddresses.ToListAsync();
        }

        // GET: api/EmployeeAddresses/5
        /// <summary>
        /// Получение рабочих адреса сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<EmployeeAddress>> GetEmployeeAddress(int? id)
        {
            if (_context.EmployeeAddresses == null)
            {
                return NotFound();
            }
            var employeeAddress = await _context.EmployeeAddresses.FindAsync(id).ConfigureAwait(false);

            if (employeeAddress == null)
            {
                return NotFound();
            }

            return employeeAddress;
        }

        // PUT: api/EmployeeAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление рабочего адреса сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeAddress"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutEmployeeAddress(int? id, EmployeeAddress employeeAddress)
        {
            if (id != employeeAddress.IdEmployeeAddresses)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(employeeAddress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeAddressExists(id))
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

        // POST: api/EmployeeAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление рабочего адреса сотрудника
        /// </summary>
        /// <param name="employeeAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<ActionResult<EmployeeAddress>> PostEmployeeAddress(EmployeeAddress employeeAddress)
        {
            if (_context.EmployeeAddresses == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.EmployeeAddresses'  is null.");
            }
            _context.EmployeeAddresses.Add(employeeAddress);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetEmployeeAddress", new { id = employeeAddress.IdEmployeeAddresses }, employeeAddress);
        }

        // DELETE: api/EmployeeAddresses/5
        /// <summary>
        /// Удаление адреса сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteEmployeeAddress(int? id)
        {
            if (_context.EmployeeAddresses == null)
            {
                return NotFound();
            }
            var employeeAddress = await _context.EmployeeAddresses.FindAsync(id).ConfigureAwait(false);
            if (employeeAddress == null)
            {
                return NotFound();
            }

            _context.EmployeeAddresses.Remove(employeeAddress);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/EmployeeAddresses?id=1&2&3&4
        /// <summary>
        /// Удаление адресов сотрудников по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteEmployeeAddresses([FromQuery] List<int>? idList)
        {
            if (_context.EmployeeAddresses == null)
            {
                return NotFound();
            }

            var models = new List<EmployeeAddress?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.EmployeeAddresses.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.EmployeeAddresses.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool EmployeeAddressExists(int? id)
        {
            return (_context.EmployeeAddresses?.Any(e => e.IdEmployeeAddresses == id)).GetValueOrDefault();
        }
    }
}
