using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetLife.Data;
using VetLife.Data.Services;
using VetLife.Data.ViewModels;
using VetLife.Models;

namespace VetLife.Controllers
{
    [Authorize]
    public class OperationsController : Controller
    {
        private readonly IOperationsService _service;
        private readonly AppDbContext _context;

        public OperationsController(IOperationsService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Create(int petId)
        {

            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
            {
                return NotFound();  
            }

            var viewModel = new CreateOperationVM
            {
                PetId = petId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOperationVM viewModel)
        {

            if (ModelState.IsValid)
            {
      
                var operation = new Models.Operation
                {
                    Name = viewModel.Name,
                    Date = viewModel.Date,
                    Description = viewModel.Description,
                    Surgeon = viewModel.Surgeon,
                    PetId = viewModel.PetId,
                    Pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == viewModel.PetId)
                };
                await _service.AddAsync(operation);

                return RedirectToAction("Details", "Pets", new { id = viewModel.PetId });
            }

            ViewBag.PetId = viewModel.PetId;
            return View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var operation = await _service.GetByIdAsync(id);
            if (operation == null)
            {
                return NotFound();
            }
            return View(operation);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var operation = await _service.GetByIdAsync(id);
            if (operation == null)
            {
                return NotFound();
            }

            // Map the retrieved entity to the view model
            var viewModel = new CreateOperationVM
            {
                Id = operation.Id,
                Name = operation.Name,
                Date = operation.Date,
                Description = operation.Description,
                Surgeon = operation.Surgeon,
                PetId = operation.PetId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CreateOperationVM operationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(operationVM);
            }

            var pet = await _context.Pets.FirstOrDefaultAsync(p => p.Id == operationVM.PetId);

            var operation = new Models.Operation
            {
                Id = operationVM.Id,
                Name = operationVM.Name,
                Date = operationVM.Date,
                Description = operationVM.Description,
                Surgeon = operationVM.Surgeon,
                PetId = operationVM.PetId,
                Pet = pet
            };

            try
            {
                await _service.UpdateAsync(operation.Id, operation);
                return RedirectToAction("Details", "Pets", new { id = operation.PetId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the operation. Please try again.");
                return View(operationVM);
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var operation = _context.Operations.Find(id);
            if (operation != null)
            {
                _context.Operations.Remove(operation);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", "Pets", new { id = operation.PetId });
        }
    }
}
