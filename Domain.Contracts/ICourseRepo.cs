using Domain.Models.Entites;

namespace Domain.Contracts;

public interface ICourseRepo : IRepositoryBase<Course>
{
    public interface ICourseRepo : IRepositoryBase<Course>
    {
        Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges = false);
        Task<Course> GetCourseByIdAsync(int courseId, bool trackChanges = false);
        Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId, bool trackChanges = false);
        Task<bool> CourseExistsAsync(int courseId);
    }
}