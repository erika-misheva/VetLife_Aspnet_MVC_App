using Microsoft.EntityFrameworkCore;
using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public class PetsService : EntityBaseRepository<Pet>, IPetsService
    {
        private readonly AppDbContext _context;
        public PetsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pet> GetPetByIdAsync(int id)
        {
            var petDetails = await _context.Pets
                                            .Include(p => p.Owner)
                                            .Include(p => p.MedicalHistory)
                                                .ThenInclude(mh => mh.Disease)
                                            .Include(p => p.MedicalHistory)
                                                .ThenInclude(mh => mh.Treatment_Drug)
                                                     .ThenInclude(td => td.Drug)
                                            .Include(p => p.PetVaccines)
                                                .ThenInclude(pv => pv.Vaccine)
                                            .Include(p => p.Operations)
                                            .FirstOrDefaultAsync(x => x.Id == id);

            return petDetails;
        }

        public override async Task UpdateAsync(int id, Pet pet)
        {
            var existingPet = await GetPetByIdAsync(id);

            if (existingPet == null)
                throw new KeyNotFoundException($"Pet with ID {id} not found.");

            _context.Entry(existingPet).CurrentValues.SetValues(pet);

            await _context.SaveChangesAsync();
        }

    }
}
