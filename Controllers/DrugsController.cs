using Microsoft.AspNetCore.Mvc;
using VetLife.Data.Services;
using VetLife.Data;
using VetLife.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace VetLife.Controllers
{
    [Authorize]
    public class DrugsController : Controller
    {
        private readonly IDrugsService _service;
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "DrugsIndexCacheKey";

        public DrugsController(IDrugsService service, AppDbContext context, IMemoryCache cache)
        {
            _service = service;
            _context = context;
            _cache = cache;
        }
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue(_cacheKey, out IEnumerable<Drug> drugs))
            {

                drugs = await _service.GetAllAsync(); 

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(_cacheKey, drugs, cacheOptions);
            }
            ViewData["ActivePage"] = "Drugs";
            return View(drugs); 
        }
        public async Task<IActionResult> Details(int id)
        {
            var drug = await _service.GetByIdAsync(id);

            if (drug == null)
            {
                return NotFound();
            }

            return View(drug);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Name, Manufacturer, Dosage, ManufacturedDate, ExpiryDate")] Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return View(drug);
            }
            await _service.AddAsync(drug);
            _cache.Remove(_cacheKey);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var drug = await _service.GetByIdAsync(id);

            if (drug == null) return View("NotFound");

            return View(drug);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Manufacturer, Dosage, ManufacturedDate, ExpiryDate")] Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return View(drug);
            }
            await _service.UpdateAsync(id, drug);
            _cache.Remove(_cacheKey);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var drug = await _service.GetByIdAsync(id);
                if (drug == null) return View("NotFound");


                if (drug.Treatment_Drug?.Any() == true)
                {
                    _context.Treatment_Drug.RemoveRange(drug.Treatment_Drug);
                }
                await _context.SaveChangesAsync();

                await _service.DeleteAsync(id);

                _cache.Remove(_cacheKey);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the drug record.");
                return View("Error");
            }
        }
    }
}
