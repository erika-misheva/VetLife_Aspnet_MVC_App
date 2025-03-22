using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public class DrugsService : EntityBaseRepository<Drug>, IDrugsService
    {
        public DrugsService(AppDbContext context) : base(context)
        {
        }
    }
}
