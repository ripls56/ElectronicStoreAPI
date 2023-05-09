using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefectInformationsController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        public DefectInformationsController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/DefectInformations
        /// <summary>
        /// Получение информации о заказах с браком
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefectInformation>>> GetDefectInformations()
        {
            if (_context.DefectInformations == null)
            {
                return NotFound();
            }
            return await _context.DefectInformations.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/DefectInformations/5
        /// <summary>
        /// Получение информации о заказе с браком по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<DefectInformation>> GetDefectInformation(int? id)
        {
            if (_context.DefectInformations == null)
            {
                return NotFound();
            }
            var defectInformation = await _context.DefectInformations.FindAsync(id).ConfigureAwait(false);

            if (defectInformation == null)
            {
                return NotFound();
            }

            return defectInformation;
        }

        // PUT: api/DefectInformations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление информации о заказе с браком
        /// </summary>
        /// <param name="id"></param>
        /// <param name="defectInformation"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutDefectInformation(int? id, DefectInformation defectInformation)
        {
            if (id != defectInformation.IdDefectInformation)
            {
                return BadRequest();
            }

            _context.Entry(defectInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DefectInformationExists(id))
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

        // POST: api/DefectInformations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление информации о браке в заказе
        /// </summary>
        /// <param name="defectInformation"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DefectInformation>> PostDefectInformation(DefectInformation defectInformation)
        {
            if (_context.DefectInformations == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.DefectInformations'  is null.");
            }
            _context.DefectInformations.Add(defectInformation);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetDefectInformation", new { id = defectInformation.IdDefectInformation }, defectInformation);
        }

        // DELETE: api/DefectInformations/5
        /// <summary>
        /// Удаление информации о браке в заказе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDefectInformation(int? id)
        {
            if (_context.DefectInformations == null)
            {
                return NotFound();
            }
            var defectInformation = await _context.DefectInformations.FindAsync(id).ConfigureAwait(false);
            if (defectInformation == null)
            {
                return NotFound();
            }

            _context.DefectInformations.Remove(defectInformation);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool DefectInformationExists(int? id)
        {
            return (_context.DefectInformations?.Any(e => e.IdDefectInformation == id)).GetValueOrDefault();
        }
    }
}
