using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetLife.Data;
using VetLife.Data.Services;
using VetLife.Models;

namespace VetLife.Controllers
{
    [Authorize]
    public class OwnersController(IOwnersService ownersService, AppDbContext appDbContext) : Controller
    {
        private readonly IOwnersService _ownersService = ownersService;
        private readonly AppDbContext _appDbContext = appDbContext;

        public async Task<IActionResult> Index()
        {
            ViewData["ActivePage"] = "Owners";
            var owners = await _ownersService.GetAllAsync(o => o.Pets);
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Surname, Id, ProfilePictureURL, Age, Pets")] Owner owner)
        {
            if (!ModelState.IsValid)
            {
                return View(owner);
            }
            await _ownersService.AddAsync(owner);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var owner = await _ownersService.GetOwnerByIdAsync(id);

            if (owner == null) return View("NotFound");

            return View(owner);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name, Surname, Id, ProfilePictureURL, Age, Pets")] Owner owner)
        {
            if (!ModelState.IsValid)
            {
                owner.Pets = [.. _appDbContext.Pets.Where(p => p.OwnerId == id)];
                return View(owner);
            }
            await _ownersService.UpdateAsync(id, owner);
            return RedirectToAction(nameof(Index));
        }

   
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownersService.GetOwnerByIdAsync(id);
            if (owner == null) return View("NotFound");
            _appDbContext.Pets.RemoveRange(owner.Pets);
            await _ownersService.DeleteAsync(id);
           
            return RedirectToAction(nameof(Index));
        }
    }
}
