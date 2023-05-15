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
    public class NomenclaturesController : ControllerBase
    {
        private readonly ElectronicStoreContext _context;

        /// <inheritdoc />
        public NomenclaturesController(ElectronicStoreContext context)
        {
            _context = context;
        }

        // GET: api/Nomenclatures
        /// <summary>
        /// Получение товаров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<PagedList<Nomenclature>>> GetNomenclatures([FromQuery] PaginateParameters paginateParameters)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            return PagedList<Nomenclature>.ToPagedList(_context.Nomenclatures.Where(n => n.IsDelete == false), paginateParameters.pageNumber, paginateParameters.pageSize);
        }

        // GET: api/Nomenclatures?asd&qwe
        /// <summary>
        /// Получение отфильтрованных по бренду товаров
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("brands")]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<PagedList<Nomenclature>>> GetNomenclatures([FromQuery] PaginateParameters paginateParameters, [FromQuery] HashSet<string>? brands, string? sort = "asc")
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            HashSet<Brand> fetchedBrands = new HashSet<Brand>();
            if (brands == null)
            {
                return BadRequest("Brands must be not null!");
            }
            foreach (var brand in brands)
            {
                var fetchedBrand = await _context.Brands.Where(b => b.NameBrands.ToLower() == brand.ToLower()).FirstOrDefaultAsync()
                    .ConfigureAwait(false);

                if (fetchedBrand == null)
                {
                    return BadRequest("Brand must be the same as brand from {GET: api/brands}!");
                }
                fetchedBrands.Add(fetchedBrand);
            }

            List<Nomenclature> kNomenclatures = new List<Nomenclature>();

            foreach (var fetchedBrand in fetchedBrands)
            {
                switch (sort.ToLower())
                {
                    case "asc":
                        kNomenclatures.AddRange(await _context.Nomenclatures
                            .Where(n => n.BrandsId == fetchedBrand.IdBrands && n.IsDelete == false).OrderBy(n => n.IdNomenclature).ToListAsync()
                            .ConfigureAwait(false));
                        break;
                    case "desc":
                        kNomenclatures.AddRange(await _context.Nomenclatures
                            .Where(n => n.BrandsId == fetchedBrand.IdBrands && n.IsDelete == false).OrderByDescending(n => n.IdNomenclature).ToListAsync()
                            .ConfigureAwait(false));
                        break;
                }

            }
            return PagedList<Nomenclature>.ToPagedList(kNomenclatures.AsQueryable(), paginateParameters.pageNumber, paginateParameters.pageSize);
        }

        // GET: api/Nomenclatures
        /// <summary>
        /// Поиск товаров
        /// </summary>
        /// <returns></returns>
        [HttpGet("{query}")]
        [Authorize(Roles = "client, Продавец, Менеджер, Администратор БД")]
        public async Task<ActionResult<IEnumerable<Nomenclature>>> GetNomenclatures(string? query, [FromQuery] PaginateParameters paginateParameters, string? sort = "asc")
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }

            return sort.ToLower()
            switch
            {
                "asc" => PagedList<Nomenclature>.ToPagedList(_context.Nomenclatures.OrderBy(n => n.NameNomenclature)
                    .Where(n => n.NameNomenclature.Contains(query) && n.IsDelete == false), paginateParameters.pageNumber, paginateParameters.pageSize),
                "desc" => PagedList<Nomenclature>.ToPagedList(_context.Nomenclatures.OrderByDescending(n => n.NameNomenclature)
                    .Where(n => n.NameNomenclature.Contains(query) && n.IsDelete == false), paginateParameters.pageNumber, paginateParameters.pageSize),
                _ => BadRequest()
            };
        }

        // GET: api/Nomenclatures/5
        /// <summary>
        /// Получение товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<Nomenclature>> GetNomenclature(int? id)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            var nomenclature = await _context.Nomenclatures.Where(n => n.IsDelete == false && n.IdNomenclature == id).FirstOrDefaultAsync().ConfigureAwait(false);

            if (nomenclature == null)
            {
                return NotFound();
            }

            return nomenclature;
        }

        // PUT: api/Nomenclatures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Обновление товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nomenclature"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> PutNomenclature(int? id, Nomenclature nomenclature)
        {
            if (id != nomenclature.IdNomenclature)
            {
                return BadRequest(error: "Need to be the same as id in query");
            }

            _context.Entry(nomenclature).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NomenclatureExists(id))
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

        // POST: api/Nomenclatures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="nomenclature"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<ActionResult<Nomenclature>> PostNomenclature(Nomenclature nomenclature)
        {
            if (_context.Nomenclatures == null)
            {
                return Problem("Entity set 'ElectronicStoreContext.Nomenclatures'  is null.");
            }
            _context.Nomenclatures.Add(nomenclature);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return CreatedAtAction("GetNomenclature", new { id = nomenclature.IdNomenclature }, nomenclature);
        }


        // GET: api/Nomenclatures?id=1&2&3&4
        /// <summary>
        /// Восстановление(логическое) товаров по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("restore")]
        [Authorize(Roles = "Продавец, Менеджер, Администратор БД")]
        public async Task<IActionResult> RestoreNomenclatures([FromQuery] List<int>? idList)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }

            if (idList != null)
                foreach (var item in idList)
                {
                    var model = await _context.Nomenclatures.FindAsync(item).ConfigureAwait(false);
                    if (model == null)
                        return BadRequest(error: $"Id:{item} not found");
                    if (model.IsDelete == false)
                        return NotFound("Already restored");
                    model.IsDelete = false;

                }
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Nomenclatures/5
        /// <summary>
        /// Удаление товара по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteNomenclature(int? id)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }
            var nomenclature = await _context.Nomenclatures.FindAsync(id).ConfigureAwait(false);
            if (nomenclature == null)
            {
                return NotFound();
            }

            if (nomenclature.IsDelete == true)
                return NotFound("Already delete");
            nomenclature.IsDelete = true;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        // DELETE: api/Nomenclatures?id=1&2&3&4
        /// <summary>
        /// Удаление товаров по листу id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Менеджер, Администратор БД")]
        public async Task<IActionResult> DeleteNomenclature([FromQuery] List<int>? idList)
        {
            if (_context.Nomenclatures == null)
            {
                return NotFound();
            }

            if (idList != null)
                foreach (var item in idList)
                {
                    var model = await _context.Nomenclatures.FindAsync(item).ConfigureAwait(false);
                    if (model == null)
                        return BadRequest(error: $"Id:{item} not found");
                    if (model.IsDelete == true)
                        return NotFound("Already delete");
                    model.IsDelete = true;
                }
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return NoContent();
        }

        private bool NomenclatureExists(int? id)
        {
            return (_context.Nomenclatures?.Any(e => e.IdNomenclature == id)).GetValueOrDefault();
        }
    }
}
