using Microsoft.EntityFrameworkCore;
using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public class OwnersService : EntityBaseRepository<Owner>, IOwnersService
    {
        private readonly AppDbContext _context;
        public OwnersService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Owner> GetOwnerByIdAsync(int id)
        {
            var ownerDetails = await _context.Owners
                                             .Include(o => o.Pets)
                                                 .ThenInclude(p => p.PetVaccines)
                                             .Include(o => o.Pets)
                                                 .ThenInclude(p => p.MedicalHistory)
                                             .FirstOrDefaultAsync(x => x.Id == id);

            return ownerDetails;

        }

        public override async Task UpdateAsync(int id, Owner owner)
        {
            var existingOwner = await _context.Owners
                                               .Include(o => o.Pets)
                                               .FirstOrDefaultAsync(o => o.Id == id);
            if (existingOwner == null)
                throw new KeyNotFoundException($"Owner with ID {id} not found.");

            _context.Entry(existingOwner).CurrentValues.SetValues(owner);

            existingOwner.Pets.Clear();
            foreach (var pet in owner.Pets)
            {
                var existingPet = existingOwner.Pets.FirstOrDefault(p => p.Id == pet.Id);
                if (existingPet != null)
                {
                    _context.Entry(existingPet).CurrentValues.SetValues(pet);
                }
                else
                {
                    existingOwner.Pets.Add(pet);
                }
            }

            await _context.SaveChangesAsync();
        }

    }
}
