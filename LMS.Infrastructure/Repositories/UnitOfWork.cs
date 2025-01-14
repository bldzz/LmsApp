using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext context;

    public UnitOfWork(LmsContext context, ICourseRepo courseRepo, IModuleRepo moduleRepo)
    {
        this.context = context;
        CourseRepo = courseRepo;
        ModuleRepo = moduleRepo;
    }

    public ICourseRepo CourseRepo { get; }
    public IModuleRepo ModuleRepo { get; }

    public async Task CompleteASync()
    {
        await context.SaveChangesAsync();
    }
}
