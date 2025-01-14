namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICourseRepo CourseRepo { get; }
    IModuleRepo ModuleRepo { get; }
    IActivityRepo ActivityRepo { get; }
    IDocumentRepo DocumentRepo { get; }
    IUserCourseRepo UserCourseRepo { get; }
    Task CompleteASync();
}