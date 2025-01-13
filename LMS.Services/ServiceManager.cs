using Domain.Contracts;
using Services.Contracts;

namespace LMS.Services;
public class ServiceManager : IServiceManager
{

    private readonly Lazy<IAuthService> authService;
    private readonly Lazy<ICourseService> _courseService;
    private readonly Lazy<IModuleService> _moduleService;

    public IAuthService AuthService => authService.Value;
    public ICourseService CourseService => _courseService.Value;
    public IModuleService ModuleService => _moduleService.Value;

    public ServiceManager(Lazy<IAuthService> authService, Lazy<ICourseService> courseService, Lazy<IModuleService> moduleService)
    {
        this.authService = authService;
        _courseService = courseService;
        _moduleService = moduleService;
    }
}
