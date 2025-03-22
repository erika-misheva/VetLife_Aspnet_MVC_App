using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetLife.Data;
using VetLife.Data.Services;
using VetLife.Models;

namespace VetLife.Controllers
{
    [Authorize]
    public class VaccinesController : Controller
    {
       
        private readonly IVaccinesService _service;
        private readonly AppDbContext _context;

        public VaccinesController(IVaccinesService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewData["ActivePage"] = "Vaccines";
            var vaccines = await _service.GetAllAsync();

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


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Manufacturer, DateProduced, ExpiryDate, Stock")] Vaccine vaccine)
        {
            if (!ModelState.IsValid)
            {
                return View(vaccine);
            }
            await _service.AddAsync(vaccine);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vaccine = await _service.GetVaccineByIdAsync(id);

            if (vaccine == null) return View("NotFound");

            return View(vaccine);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Manufacturer, DateProduced, ExpiryDate, Stock")] Vaccine vaccine)
        {
            if (!ModelState.IsValid)
            {
                return View(vaccine);
            }
            await _service.UpdateAsync(id, vaccine);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
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
