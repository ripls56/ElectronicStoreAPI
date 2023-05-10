using ElectroStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroStoreAPI.Controllers
{
    /// <inheritdoc />
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public ProfilesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Profiles
        /// <summary>
        /// Получение профилей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfiles()
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            return await _context.Profiles.ToListAsync().ConfigureAwait(false);
        }

        // GET: api/Profiles/5
        /// <summary>
        /// Получение профиля по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<Profile>> GetProfile(int? id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _context.Profiles.FindAsync(id).ConfigureAwait(false);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/Profiles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление профиля по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "client, Менеджер, Администратор БД")]
        public async Task<IActionResult> PutProfile(int? id, Profile profile)
        {
            if (id != profile.IdProfile)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/Profiles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление профиля
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "client, Менеджер, Администратор БД")]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Profiles'  is null.");
            }
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetProfile", new { id = profile.IdProfile }, profile);
        }

        // DELETE: api/Profiles/5
        /// <summary>
        /// Удаление профиля по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "client, Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteProfile(int? id)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }
            var profile = await _context.Profiles.FindAsync(id).ConfigureAwait(false);
            if (profile == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Profiles?id=1&2&3&4
        /// <summary>
        /// Удаление профилей по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "client, Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteProfiles([FromQuery] List<int>? idList)
        {
            if (_context.Profiles == null)
            {
                return NotFound();
            }

            var models = new List<Profile?>();
            if (idList != null)
                foreach (var item in idList)
                {
                    models.Add(await _context.Profiles.FindAsync(item).ConfigureAwait(false));
                }

            if (models != null)
            {
                _context.Profiles.RemoveRange(models);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                BadRequest(error: "Id's not found");
            }
            return NoContent();
        }

        private bool ProfileExists(int? id)
        {
            return (_context.Profiles?.Any(e => e.IdProfile == id)).GetValueOrDefault();
        }
    }
}
