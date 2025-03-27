using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetLife.Data.Services;
using VetLife.Data;
using VetLife.Data.ViewModels;
using VetLife.Models;
using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Authorization;

namespace VetLife.Controllers
{
    [Authorize]
    public class PetVaccineController : Controller
    {
        private readonly AppDbContext _context;

        public PetVaccineController(IVaccinesService service, AppDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(string id)
        {
            if (id.Contains("-")) 
            {
                var parts = id.Split("-");
                if (parts.Length == 2 && int.TryParse(parts[0], out int vaccineId) && int.TryParse(parts[1], out int petId))
                {
                    var petVaccine = _context.PetVaccine.FirstOrDefault(pv => pv.PetId == petId && pv.VaccineId == vaccineId);
                    if (petVaccine == null)
                    {
                        return NotFound();
                    }
                    _context.PetVaccine.Remove(petVaccine);
                    _context.SaveChanges();
                    return RedirectToAction("Details", "Pets", new { id = petId });
                }
            }
            else 
            {
                if (int.TryParse(id, out int operationId))
                {
                    var operation = _context.Operations.Find(operationId);
                    if (operation == null)
                    {
                        return NotFound();
                    }
                    _context.Operations.Remove(operation);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return BadRequest("Invalid ID format.");
        }

        public IActionResult Manage(int petId)
        {
            var pet = _context.Pets
                .Include(p => p.PetVaccines)
                .ThenInclude(pv => pv.Vaccine)
                .FirstOrDefault(p => p.Id == petId);

            if (pet == null)
            {
                return NotFound();
            }

            var availableVaccines = _context.Vaccines
       .Where(v => v.Stock > 0 && !v.PetVaccines.Any(pv => pv.PetId == petId))
       .ToList();

            var selectedVaccines = pet.PetVaccines
                .Select(pv => pv.Vaccine)
                .ToList();

            var model = new ManagePetVaccinesViewModel
            {
                PetId = petId,
                AvailableVaccines = availableVaccines,
                SelectedVaccines = selectedVaccines
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddVaccine(ManagePetVaccinesViewModel model)
        {
            var vaccine = _context.Vaccines.FirstOrDefault(v => v.Id == model.SelectedVaccineId);

            if (vaccine == null || vaccine.Stock <= 0)
            {
                TempData["ErrorMessage"] = "Vaccine not available.";
                return RedirectToAction("Manage", new { petId = model.PetId });
            }

            var petVaccine = _context.PetVaccine
                .FirstOrDefault(pv => pv.PetId == model.PetId && pv.VaccineId == model.SelectedVaccineId);

            if (petVaccine == null)
            {
                _context.PetVaccine.Add(new PetVaccine
                {
                    PetId = model.PetId,
                    VaccineId = model.SelectedVaccineId,
                    AdministeredDate = model.AdministeredDate,
                    NextDoseDate = model.NextDoseDate
                });
                vaccine.Stock--;
                _context.SaveChanges();
            }

            return RedirectToAction("Manage", new { petId = model.PetId });
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteVaccine(int petId, int vaccineId)
        {
            var petVaccine = _context.PetVaccine
                .FirstOrDefault(pv => pv.PetId == petId && pv.VaccineId == vaccineId);

            if (petVaccine != null)
            {
                _context.PetVaccine.Remove(petVaccine);
                _context.SaveChanges();
            }

            return RedirectToAction("Manage", new { petId });
        }


    }
}
