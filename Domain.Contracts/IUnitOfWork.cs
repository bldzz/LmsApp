namespace Domain.Contracts;

public interface IUnitOfWork
{
    ICourseRepo CourseRepo { get; }
    Task CompleteASync();
}