using Microsoft.EntityFrameworkCore;
using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public class VaccinesService : EntityBaseRepository<Vaccine>, IVaccinesService
    {
        private readonly AppDbContext _context;
        public VaccinesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Vaccine> GetVaccineByIdAsync(int id)
        {
            var vaccineDetails = await _context.Vaccines
                                             .Include(v => v.PetVaccines)
                                             .ThenInclude(vm => vm.Pet)
                                             .FirstOrDefaultAsync(x => x.Id == id);

            return vaccineDetails;

        }
    }
}
