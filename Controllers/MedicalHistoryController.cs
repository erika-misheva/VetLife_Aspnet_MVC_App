using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using VetLife.Data;
using VetLife.Data.ViewModels;
using VetLife.Models;


namespace VetLife.Controllers
{
    [Authorize]
    public class MedicalHistoryController : Controller
    {
        private readonly AppDbContext _context;
  

        public MedicalHistoryController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create(int petId)
        {
            var drugs = _context.Drugs.ToList();

            var viewModel = new TreatmentCreateViewModel
            {
                PetId = petId,
                AvailableDrugs = drugs
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(TreatmentCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AvailableDrugs = _context.Drugs.ToList();
                return View(viewModel);
            }

            var disease = viewModel.Disease;
            _context.Diseases.Add(disease);
            _context.SaveChanges();

            var treatment = viewModel.Treatment;
            treatment.PetId = viewModel.PetId;
            treatment.DiseaseId = disease.Id;

            foreach (var drugId in viewModel.SelectedDrugIds)
            {
                treatment.Treatment_Drug.Add(new Treatment_Drug { DrugId = drugId });
            }

            _context.Treatments.Add(treatment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Pets", new { id = viewModel.PetId });
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            var treatment = _context.Treatments
                .Include(t => t.Disease)
                .Include(t => t.Treatment_Drug)
                .ThenInclude(td => td.Drug)
                .FirstOrDefault(t => t.Id == id);

            if (treatment == null) return NotFound();

            var viewModel = new TreatmentEditViewModel
            {
                Id = treatment.Id,
                PetId = treatment.PetId,
                Disease = treatment.Disease,
                Treatment = treatment,
                AvailableDrugs = _context.Drugs.ToList(),
                SelectedDrugIds = treatment.Treatment_Drug.Select(td => td.DrugId).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(TreatmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.AvailableDrugs = _context.Drugs.ToList();
                return View(viewModel);
            }

            var treatment = _context.Treatments
                .Include(t => t.Disease)
                .Include(t => t.Treatment_Drug)
                .FirstOrDefault(t => t.Id == viewModel.Id);

            if (treatment == null) return NotFound();

            treatment.Disease.Name = viewModel.Disease.Name;
            treatment.Disease.Severity = viewModel.Disease.Severity;
            treatment.Disease.StartTime = viewModel.Disease.StartTime;
            treatment.Disease.EndTime = viewModel.Disease.EndTime;
            treatment.Disease.Symptoms = viewModel.Disease.Symptoms;

            treatment.Notes = viewModel.Treatment.Notes;
            treatment.Recommendations = viewModel.Treatment.Recommendations;

            _context.Treatment_Drug.RemoveRange(treatment.Treatment_Drug);
            treatment.Treatment_Drug = viewModel.SelectedDrugIds
                .Select(drugId => new Treatment_Drug { DrugId = drugId, TreatmentId = treatment.Id })
                .ToList();

            _context.SaveChanges();

            return RedirectToAction("Details", "Pets", new { id = viewModel.PetId });
        }

        public IActionResult Details(int id)
        {
            var treatment = _context.Treatments
                .Include(t => t.Disease)
                .Include(t => t.Treatment_Drug)
                .ThenInclude(td => td.Drug)
                .FirstOrDefault(t => t.Id == id);

            if (treatment == null) return NotFound();

            var pet = _context.Pets.FirstOrDefault(p => p.Id == treatment.PetId);

            var viewModel = new TreatmentDetailsViewModel
            {
                TreatmentId = treatment.Id,
                PetId = treatment.PetId,
                PetName = pet?.Name,
                Disease = treatment.Disease,
                Treatment = treatment,
                PrescribedDrugs = treatment.Treatment_Drug.Select(td => td.Drug).ToList()
            };

            return View(viewModel);
        }
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            var treatment = _context.Treatments
                .Include(t => t.Treatment_Drug)
                .Include(t => t.Disease)
                .FirstOrDefault(t => t.Id == id);

            if (treatment == null)
            {
                return NotFound();
            }
            _context.Treatment_Drug.RemoveRange(treatment.Treatment_Drug);

            if (treatment.Disease != null)
            {
                _context.Diseases.Remove(treatment.Disease);
            }

            _context.Treatments.Remove(treatment);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the treatment.");
                return RedirectToAction("Details", "Pets", new { id = treatment.PetId });
            }

            return RedirectToAction("Details", "Pets", new { id = treatment.PetId });
        }

    }
}
