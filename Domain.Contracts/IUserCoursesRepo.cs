using Domain.Models.Entites;

namespace Domain.Contracts;

public interface IUserCourseRepo : IRepositoryBase<UserCourse>
{
    // Fetch all UserCourse relationships with related data
    Task<IEnumerable<UserCourse>> GetAllUserCoursesAsync(bool trackChanges = false);

    // Fetch a UserCourse relationship by ID with related data
    Task<UserCourse?> GetUserCourseByIdAsync(int id, bool trackChanges = false);

    // Fetch all courses for a specific user
    Task<IEnumerable<UserCourse>> GetCoursesByUserIdAsync(string userId, bool trackChanges = false);

    // Fetch all users for a specific course
    Task<IEnumerable<UserCourse>> GetUsersByCourseIdAsync(int courseId, bool trackChanges = false);

    // Check if a UserCourse relationship exists by ID
    Task<bool> UserCourseExistsAsync(int id);
}