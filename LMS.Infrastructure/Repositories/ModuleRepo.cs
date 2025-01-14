using Domain.Contracts;
using Domain.Models.Entites;

namespace LMS.Infrastructure.Repositories
{
    public class ModuleRepo : RepositoryBase<Module>, IModuleRepo
    {
        public ModuleRepo(LmsContext context) : base(context)
        {
        }
    }
}
