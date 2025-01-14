using Domain.Contracts;

namespace Services.Contracts;
public interface IServiceManager
{
    IAuthService AuthService { get; }
    ICourseService CourseService { get; }
    IModuleService ModuleService { get; }
    IDocumentService DocumentService { get; }
    IActivityService ActivityService { get; }
    //IModuleService ModuleService { get; }
}