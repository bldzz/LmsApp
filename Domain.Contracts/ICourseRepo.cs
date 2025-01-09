using Domain.Models.Entites;

namespace Domain.Contracts;

public interface ICourseRepo : IRepositoryBase<Course>
{
    /*
    Task<IEnumerable<Course>> GetCoursesAsync();
    Task<Course> GetCourseAsync(int id);
    Task<bool> AnyCourseAsync(int id);
    void AddCourse(Course course);
    void RemoveCourse(Course course);
    void UpdateCourse(Course course);
    */
}