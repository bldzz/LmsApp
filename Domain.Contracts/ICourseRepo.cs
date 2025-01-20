using Domain.Models.Entites;
using LMS.Shared;

namespace Domain.Contracts;

public interface ICourseRepo : IRepositoryBase<Course>
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(GetCoursesParameters parameters);
    Task<Course> GetCourseByIdAsync(int courseId, bool trackChanges = false);
    Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId, bool trackChanges = false);
    Task<bool> CourseExistsAsync(int courseId);
}