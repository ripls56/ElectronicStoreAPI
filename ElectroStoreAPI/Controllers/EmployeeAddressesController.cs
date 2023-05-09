using ElectroStoreAPI.Models;
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
        public async Task<IActionResult> PutEmployeeAddress(int? id, EmployeeAddress employeeAddress)
        {
            if (id != employeeAddress.IdEmployeeAddresses)
            {
                return BadRequest();
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
        /// Добавоение рабочего адреса сотрудника
        /// </summary>
        /// <param name="employeeAddress"></param>
        /// <returns></returns>
        [HttpPost]
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
        [HttpDelete("{id}")]
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

        private bool EmployeeAddressExists(int? id)
        {
            return (_context.EmployeeAddresses?.Any(e => e.IdEmployeeAddresses == id)).GetValueOrDefault();
        }
    }
}
