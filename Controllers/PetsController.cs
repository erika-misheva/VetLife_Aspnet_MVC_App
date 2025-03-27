using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VetLife.Data;
using VetLife.Data.Enums;
using VetLife.Data.Services;
using VetLife.Data.ViewModels;
using VetLife.Models;

namespace VetLife.Controllers
{
    [Authorize]
    public class PetsController : Controller
    {
        private readonly IPetsService _service;
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly string _cacheKey = "PetsIndexCacheKey";

        public PetsController(IPetsService service, AppDbContext context, IMemoryCache cache)
        {
            _service = service;
            _context = context;
            _cache = cache;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            if (!_cache.TryGetValue(_cacheKey, out IEnumerable<Pet> pets))
            {
             
                pets = await _service.GetAllAsync(p => p.Owner);
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(_cacheKey, pets, cacheOptions);
            }
            ViewData["ActivePage"] = "Pets";
            return View(pets);
        }
        public async Task<IActionResult> Details(int id)
        {
            var pet = await _service.GetPetByIdAsync(id);

            if (pet == null)
            {
                return NotFound();
            }


            return View(pet);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            var owners = _context.Owners
                                    .Select(o => new { o.Id, FullName = o.Name + " " + o.Surname })
                                    .ToList();
            ViewBag.Owners = owners;

            ViewBag.PetTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
            ViewBag.Vaccines = _context.Vaccines.ToList();
            return View(new CreatePetVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(CreatePetVM model)
        {
            if (!ModelState.IsValid)
            {
                var owners = _context.Owners
                                     .Select(o => new { o.Id, FullName = o.Name + " " + o.Surname })
                                     .ToList();
                ViewBag.Owners = owners;
                ViewBag.PetTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
                ViewBag.Vaccines = _context.Vaccines.ToList();
                return View(model);
            }

            var pet = new Pet
            {
                Name = model.Name,
                OwnerId = model.OwnerId,
                PetType = model.PetType,
                ProfilePictureURL = model.ProfilePictureURL,
                BirthDate = model.BirthDate,
                Race = model.Race,
                IsNeutered = model.IsNeutered,
                MedicalHistory = model.MedicalHistory.ToList(),
                Operations = model.Operations.ToList()
            };
            foreach (var vaccineId in model.SelectedVaccines)
            {
                var vaccine = await _context.Vaccines.FindAsync(vaccineId);
                if (vaccine != null)
                {
                    pet.PetVaccines.Add(new PetVaccine
                    {
                        Vaccine = vaccine,
                        AdministeredDate = DateTime.Now, 
                       
                    });
                }
            }

            await _service.AddAsync(pet);

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var pet = await _service.GetPetByIdAsync(id);
            if (pet == null) return View("NotFound");

            var model = new CreatePetVM
            {
                Id = pet.Id,
                Name = pet.Name,
                OwnerId = pet.OwnerId,
                PetType = pet.PetType,
                ProfilePictureURL = pet.ProfilePictureURL,
                BirthDate = pet.BirthDate,
                Race = pet.Race,
                IsNeutered = pet.IsNeutered,
                SelectedVaccines = pet.PetVaccines.Select(pv => pv.VaccineId).ToList(),

            };


            ViewBag.Owners = await _context.Owners
                .Select(o => new { o.Id, FullName = o.Name + " " + o.Surname })
                .ToListAsync();
            ViewBag.PetTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
            ViewBag.Vaccines = await _context.Vaccines.ToListAsync();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,OwnerId,PetType,ProfilePictureURL,BirthDate,Race,IsNeutered,SelectedVaccines,MedicalHistory,Operations")] CreatePetVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Owners = await _context.Owners
                    .Select(o => new { o.Id, FullName = o.Name + " " + o.Surname })
                    .ToListAsync();
                ViewBag.PetTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
                ViewBag.Vaccines = await _context.Vaccines.ToListAsync();
                return View(model);
            }

            var pet = await _service.GetPetByIdAsync(id);
            if (pet == null) return View("NotFound");

            pet.Name = model.Name;
            pet.OwnerId = model.OwnerId;
            pet.PetType = model.PetType;
            pet.ProfilePictureURL = model.ProfilePictureURL;
            pet.BirthDate = model.BirthDate;
            pet.Race = model.Race;
            pet.IsNeutered = model.IsNeutered;

            pet.PetVaccines.Clear();
            foreach (var vaccineId in model.SelectedVaccines)
            {
                pet.PetVaccines.Add(new PetVaccine
                {
                    VaccineId = vaccineId,
                    PetId = pet.Id
                });
            }

            await _service.UpdateAsync(id, pet);
            return RedirectToAction("Details", new { id = pet.Id });
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var pet = await _service.GetPetByIdAsync(id);
                if (pet == null) return View("NotFound");

                if (pet.MedicalHistory?.Any() == true)
                {
                    _context.Treatments.RemoveRange(pet.MedicalHistory);
                }

                if (pet.PetVaccines?.Any() == true)
                {
                    _context.PetVaccine.RemoveRange(pet.PetVaccines);
                }

                if (pet.Operations?.Any() == true)
                {
                    _context.Operations.RemoveRange(pet.Operations);
                }
                await _context.SaveChangesAsync();

                await _service.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            { 
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the pet record.");
                return View("Error");
            }
        }
    }
}

