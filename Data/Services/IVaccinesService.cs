using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public interface IVaccinesService : IEntityBaseRepository<Vaccine>
    {
        Task<Vaccine> GetVaccineByIdAsync(int id);
    }
}
