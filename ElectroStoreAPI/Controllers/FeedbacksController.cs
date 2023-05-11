using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public FeedbacksController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        /// <summary>
        /// Получение отзывов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            if (_context.Feedbacks == null)
            {
                return NotFound();
            }
            return await _context.Feedbacks.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Получение отзыва по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<ActionResult<Feedback>> GetFeedback(int? id)
        {
            if (_context.Feedbacks == null)
            {
                return NotFound();
            }
            var feedback = await _context.Feedbacks.FindAsync(id).ConfigureAwait(false);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // GET: api/Feedbacks/{orderNumber}
        /// <summary>
        /// Получение отзыва по номеру заказа
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <returns></returns>
        [HttpGet("{orderNumber}")]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<ActionResult<Feedback>> GetFeedback(string? orderNumber)
        {
            if (_context.Feedbacks == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Where(o => o.NumOrder == orderNumber).FirstOrDefaultAsync().ConfigureAwait(false);

            if (order == null) return NotFound();

            var feedback = await _context.Feedbacks.Where(f => f.OrderId == order.IdOrder).FirstOrDefaultAsync().ConfigureAwait(false);

            if (feedback == null)
            {
                return NotFound();
            }

            return feedback;
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление отзыва по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<IActionResult> PutFeedback(int? id, Feedback feedback)
        {
            if (id != feedback.IdFeedback)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
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

        // POST: api/Feedbacks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление отзыва по id
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            if (_context.Feedbacks == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Feedbacks'  is null.");
            }
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetFeedback", new { id = feedback.IdFeedback }, feedback);
        }

        // DELETE: api/Feedbacks/5
        /// <summary>
        /// Удаление отзыва по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<IActionResult> DeleteFeedback(int? id)
        {
            if (_context.Feedbacks == null)
            {
                return NotFound();
            }
            var feedback = await _context.Feedbacks.FindAsync(id).ConfigureAwait(false);
            if (feedback == null)
            {
                return NotFound();
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Feedbacks?id=1&2&3&4
        /// <summary>
        /// Удаление отзывов по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "client, Администратор БД")]
        public async Task<IActionResult> DeleteFeedbacks([FromQuery] List<int>? idList)
        {
            if (_context.Feedbacks == null)
            {
                return NotFound();
            }

            var models = new List<Feedback?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.Feedbacks.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.Feedbacks.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool FeedbackExists(int? id)
        {
            return (_context.Feedbacks?.Any(e => e.IdFeedback == id)).GetValueOrDefault();
        }
    }
}
