using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories
{
    public class CourseRepo : RepositoryBase<Course>, ICourseRepo
    {
        public CourseRepo(LmsContext context) : base(context)
        {
        }

        // Get all courses with related data (eager loading)
        public async Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges)
                .Include(c => c.Modules) // Include related Modules
                .Include(c => c.UserCourses) // Include User-Course relationships
                    .ThenInclude(uc => uc.User) // Include related Users in UserCourses
                .Include(c => c.Documents) // Include related Documents
                .ToListAsync();
        }

        // Get a course by ID with related data (eager loading)
        public async Task<Course> GetCourseByIdAsync(int courseId, bool trackChanges = false)
        {
            var course = await FindByCondition(c => c.Id == courseId, trackChanges)
                .Include(c => c.Modules)
                .Include(c => c.UserCourses)
                .ThenInclude(uc => uc.User)
                .Include(c => c.Documents)
                .FirstOrDefaultAsync();

            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");
            }

            return course;
        }


        // Get courses by a specific user (many-to-many relationship)
        public async Task<IEnumerable<Course>> GetCoursesByUserIdAsync(string userId, bool trackChanges = false)
        {
            return await FindByCondition(c => c.UserCourses.Any(uc => uc.UserId == userId), trackChanges)
                .Include(c => c.Modules)
                .Include(c => c.Documents)
                .ToListAsync();
        }

        // Check if a course exists by ID
        public async Task<bool> CourseExistsAsync(int courseId)
        {
            return await FindByCondition(c => c.Id == courseId).AnyAsync();
        }
    }
}
