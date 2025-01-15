using Domain.Contracts;
using Domain.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class UserCourseRepo : RepositoryBase<UserCourse>, IUserCourseRepo
{
    public UserCourseRepo(LmsContext context) : base(context)
    {
    }

    // Fetch all UserCourse relationships with related data (eager loading)
    public async Task<IEnumerable<UserCourse>> GetAllUserCoursesAsync(bool trackChanges = false)
    {
        return await FindAll(trackChanges)
            .Include(uc => uc.User)
            .Include(uc => uc.Course)
            .ToListAsync();
    }

    // Fetch a UserCourse relationship by ID with related data (eager loading)
    public async Task<UserCourse?> GetUserCourseByIdAsync(int id, bool trackChanges = false)
    {
        return await FindByCondition(uc => uc.Id == id, trackChanges)
            .Include(uc => uc.User)
            .Include(uc => uc.Course)
            .FirstOrDefaultAsync();
    }

    // Fetch all courses for a specific user
    public async Task<IEnumerable<UserCourse>> GetCoursesByUserIdAsync(string userId, bool trackChanges = false)
    {
        return await FindByCondition(uc => uc.UserId == userId, trackChanges)
            .Include(uc => uc.Course)
            .ToListAsync();
    }

    // Fetch all users for a specific course
    public async Task<IEnumerable<UserCourse>> GetUsersByCourseIdAsync(int courseId, bool trackChanges = false)
    {
        return await FindByCondition(uc => uc.CourseId == courseId, trackChanges)
            .Include(uc => uc.User)
            .ToListAsync();
    }

    // Check if a UserCourse relationship exists by ID
    public async Task<bool> UserCourseExistsAsync(int id)
    {
        return await FindByCondition(uc => uc.Id == id).AnyAsync();
    }
}