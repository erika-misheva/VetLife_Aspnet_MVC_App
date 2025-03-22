using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VetLife.Data;
using VetLife.Data.Services;
using VetLife.Models;

namespace VetLife.Controllers
{
    [Authorize]
    public class OwnersController: Controller
    {
        private readonly IOwnersService _ownersService;
        private readonly AppDbContext _appDbContext;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "OwnersIndexCacheKey";

        public OwnersController(IOwnersService ownersService, AppDbContext appDbContext, IMemoryCache cache)
        {
            _ownersService = ownersService;
            _appDbContext = appDbContext;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            if (!_cache.TryGetValue(_cacheKey, out IEnumerable<Owner> owners))
            {
                ViewData["ActivePage"] = "Owners";
                owners = await _ownersService.GetAllAsync(o => o.Pets);
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(_cacheKey, owners, cacheOptions);
            }

            return View(owners);
        }
        public async Task<IActionResult> Details(int id)
        {
            var owner = await _ownersService.GetOwnerByIdAsync(id);

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create([Bind("Name, Surname, Id, ProfilePictureURL, Age, Pets")] Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return View(owner);
            }
            await _ownersService.AddAsync(owner);
            _cache.Remove(_cacheKey);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var owner = await _ownersService.GetOwnerByIdAsync(id);

            if (owner == null) return View("NotFound");

            return View(owner);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, Surname, Id, ProfilePictureURL, Age, Pets")] Owner owner)
        {
            if (!ModelState.IsValid)
            {
                owner.Pets = [.. _appDbContext.Pets.Where(p => p.OwnerId == id)];
                return View(owner);
            }
            await _ownersService.UpdateAsync(id, owner);
            _cache.Remove(_cacheKey);
            return RedirectToAction(nameof(Index));
        }

   
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownersService.GetOwnerByIdAsync(id);
            if (owner == null) return View("NotFound");
            _appDbContext.Pets.RemoveRange(owner.Pets);
            await _ownersService.DeleteAsync(id);
            _cache.Remove(_cacheKey);

            return RedirectToAction(nameof(Index));
        }
    }
}
