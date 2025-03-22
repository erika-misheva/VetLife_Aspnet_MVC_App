using Microsoft.AspNetCore.Mvc;
using VetLife.Data.Services;
using VetLife.Data;
using VetLife.Models;
using Microsoft.AspNetCore.Authorization;

namespace VetLife.Controllers
{
    [Authorize]
    public class DrugsController : Controller
    {
        private readonly IDrugsService _service;
        private readonly AppDbContext _context;

        public DrugsController(IDrugsService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["ActivePage"] = "Drugs";
            var drugs = await _service.GetAllAsync();

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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Manufacturer, Dosage, ManufacturedDate, ExpiryDate")] Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return View(drug);
            }
            await _service.AddAsync(drug);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var drug = await _service.GetByIdAsync(id);

            if (drug == null) return View("NotFound");

            return View(drug);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Manufacturer, Dosage, ManufacturedDate, ExpiryDate")] Drug drug)
        {
            if (!ModelState.IsValid)
            {
                return View(drug);
            }
            await _service.UpdateAsync(id, drug);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
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
