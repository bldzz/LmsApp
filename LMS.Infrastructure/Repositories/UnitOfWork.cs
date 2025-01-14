using Domain.Contracts;
using LMS.Infrastructure.Data;

namespace LMS.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly LmsContext context;

    public UnitOfWork(LmsContext context, ICourseRepo courseRepo, IModuleRepo moduleRepo, IActivityRepo activityRepo, IDocumentRepo documentRepo, IUserCourseRepo userCourseRepo)
    {
        this.context = context;
        CourseRepo = courseRepo;
        ModuleRepo = moduleRepo;
        ActivityRepo = activityRepo;
        DocumentRepo = documentRepo;
        UserCourseRepo = userCourseRepo;
    }

    public ICourseRepo CourseRepo { get; }
    public IModuleRepo ModuleRepo { get; }
    public IActivityRepo ActivityRepo { get; }
    public IDocumentRepo DocumentRepo { get; }
    public IUserCourseRepo UserCourseRepo { get; }

    public async Task CompleteASync()
    {
        await context.SaveChangesAsync();
    }
}
