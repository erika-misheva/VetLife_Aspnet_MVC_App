using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public interface IPetsService : IEntityBaseRepository<Pet>
    {
        Task<Pet> GetPetByIdAsync(int id);
    }
}
