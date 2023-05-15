using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public EmployeesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        /// <summary>
        /// Получение сотрудников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees.Where(e => e.IsDelete == false).ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Employees/5
        /// <summary>
        /// Получение сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<Employee>> GetEmployee(int? id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.Where(e => e.IsDelete == false && e.IdEmployee == id).FirstOrDefaultAsync().ConfigureAwait(false);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обнрвление сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<IActionResult> PutEmployee(int? id, Employee employee)
        {
            if (id != employee.IdEmployee)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            try
            {
                if (_context.Employees == null)
                {
                    return Problem("Entity set 'ElectronicStoreContext.Employees'  is null.");
                }
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync().ConfigureAwait(false);

                return CreatedAtAction("GetEmployee", new { id = employee.IdEmployee }, employee);
            }
            catch
            {
                return NotFound();
            }
            finally
            {
                Response.OnCompleted(async () =>
                {
                    new TokenController(_context).Authorize(authParams: new AuthParams(login: employee.LoginEmployee, employee.PasswordEmployee)).ConfigureAwait(false);
                });
            }
        }

        // GET: api/Employees/restore?id=1&2&3&4
        /// <summary>
        /// Восстановление(логическое) сотрудников по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("restore")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> RestoreEmployees([FromQuery] List<int>? idList)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }

            if (idList != null)
                foreach (var item in idList)
                {
                    var model = await _context.Employees.FindAsync(item).ConfigureAwait(false);
                    if (model == null)
                        return BadRequest(error: $"Id:{item} not found");
                    if (model.IsDelete == false)
                        return NotFound("Already restored");
                    model.IsDelete = false;

                }
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Employees/5
        /// <summary>
        /// Удаление сотрудника по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id).ConfigureAwait(false);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Employees?id=1&2&3&4
        /// <summary>
        /// Удаление(логическое) сотрудников по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] List<int>? idList)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }

            if (idList != null)
                foreach (var item in idList)
                {
                    var model = await _context.Employees.FindAsync(item).ConfigureAwait(false);
                    if (model == null)
                        return BadRequest(error: $"Id:{item} not found");
                    if (model.IsDelete == true)
                        return NotFound("Already delete");
                    model.IsDelete = true;

                }
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool EmployeeExists(int? id)
        {
            return (_context.Employees?.Any(e => e.IdEmployee == id)).GetValueOrDefault();
        }
    }
}
