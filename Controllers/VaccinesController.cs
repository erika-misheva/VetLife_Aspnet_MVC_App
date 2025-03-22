using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetLife.Data;
using VetLife.Data.Services;
using VetLife.Models;
using Microsoft.Extensions.Caching.Memory;

namespace VetLife.Controllers
{
    [Authorize]
    public class VaccinesController : Controller
    {
       
        private readonly IVaccinesService _service;
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "VaccinesIndexCacheKey";

        public VaccinesController(IVaccinesService service, AppDbContext context, IMemoryCache cache)
        {
            _service = service;
            _context = context;
            _cache = cache;
        }
        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue(_cacheKey, out IEnumerable<Vaccine> vaccines))
            {
                vaccines = await _service.GetAllAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))   
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));    

                _cache.Set(_cacheKey, vaccines, cacheOptions);
            }

            return View(vaccines);
        }
        public async Task<IActionResult> Details(int id)
        {
            var vaccine = await _service.GetVaccineByIdAsync(id);

            if (vaccine == null)
            {
                return NotFound();
            }

            return View(vaccine);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Name, Manufacturer, DateProduced, ExpiryDate, Stock")] Vaccine vaccine)
        {
            if (!ModelState.IsValid)
            {
                return View(vaccine);
            }
            await _service.AddAsync(vaccine);
            _cache.Remove(_cacheKey);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var vaccine = await _service.GetVaccineByIdAsync(id);

            if (vaccine == null) return View("NotFound");

            return View(vaccine);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Manufacturer, DateProduced, ExpiryDate, Stock")] Vaccine vaccine)
        {
            if (!ModelState.IsValid)
            {
                return View(vaccine);
            }
            await _service.UpdateAsync(id, vaccine);
            _cache.Remove(_cacheKey);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var vaccine = await _service.GetVaccineByIdAsync(id);
                if (vaccine == null) return View("NotFound");


                if (vaccine.PetVaccines?.Any() == true)
                {
                    _context.PetVaccine.RemoveRange(vaccine.PetVaccines);
                }
                await _context.SaveChangesAsync();

                await _service.DeleteAsync(id);

                _cache.Remove(_cacheKey);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the vaccine record.");
                return View("Error");
            }
        }

    }
}
