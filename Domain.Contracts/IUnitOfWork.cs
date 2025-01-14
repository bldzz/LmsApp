namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICourseRepo CourseRepo { get; }
    IModuleRepo ModuleRepo { get; }
    Task CompleteASync();
}