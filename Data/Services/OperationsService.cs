using VetLife.Data.Base;
using VetLife.Models;

namespace VetLife.Data.Services
{
    public class OperationsService : EntityBaseRepository<Operation>, IOperationsService
    {
        public OperationsService(AppDbContext context) : base(context)
        {
        }
    }
}
