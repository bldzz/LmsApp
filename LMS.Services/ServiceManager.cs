using Domain.Contracts;
using Services.Contracts;

namespace LMS.Services;
public class ServiceManager : IServiceManager
{

    private readonly Lazy<IAuthService> authService;
    private readonly Lazy<ICourseService> _courseService;
    private readonly Lazy<IModuleService> _moduleService;
    private readonly Lazy<IDocumentService> _documentService;
    private readonly Lazy<IActivityService> _activityService;
    private readonly Lazy<IRoleService> _roleService;
    //private readonly Lazy<IUserCourseService> _userCourseService;

    public IAuthService AuthService => authService.Value;
    public ICourseService CourseService => _courseService.Value;
    public IModuleService ModuleService => _moduleService.Value;
    public IDocumentService DocumentService => _documentService.Value;
    public IActivityService ActivityService => _activityService.Value;
    public IRoleService RoleService => _roleService.Value;
    //public IUserCourseService UserCourseService => _userCourseService.Value;

    public ServiceManager(Lazy<IAuthService> authService, Lazy<ICourseService> courseService, Lazy<IModuleService> moduleService, Lazy<IDocumentService> documentService, Lazy<IActivityService> activityService, Lazy<IRoleService> roleService)
    {
        this.authService = authService;
        _courseService = courseService;
        _moduleService = moduleService;
        _documentService = documentService;
        _activityService = activityService;
        _roleService = roleService;
        //_moduleService = moduleService;
    }
}
