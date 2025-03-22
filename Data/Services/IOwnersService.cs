using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public interface IOwnersService : IEntityBaseRepository<Owner>
    {
        Task<Owner> GetOwnerByIdAsync(int id);
    }
}
