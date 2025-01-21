using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Infrastructure.Data;
using LMS.Shared.ParamaterContainers;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories
{
    public class CourseRepo : RepositoryBase<Course>, ICourseRepo
    {
        public CourseRepo(LmsContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync(GetCoursesParameters parameters)
        {
            var query = FindAll(parameters.TrackChanges);

            if (parameters.IncludeModules)
            {
                if (parameters.CascadeIncludeActivities)
                {
                    if (parameters.CascadeIncludeActivityDocs && parameters.CascadeIncludeModuleDocs)
                        query = query.Include(c => c.Modules)
                            .ThenInclude(m => m.Documents)
                            .Include(c => c.Modules)
                            .ThenInclude(m => m.Activities)
                            .ThenInclude(a => a.Documents);
                    else if (parameters.CascadeIncludeActivityDocs)
                        query = query.Include(c => c.Modules)
                            .ThenInclude(m => m.Activities)
                            .ThenInclude(a => a.Documents);
                    else if (parameters.CascadeIncludeModuleDocs)
                        query = query.Include(c => c.Modules)
                            .ThenInclude(m => m.Documents)
                            .Include(c => c.Modules)
                            .ThenInclude(m => m.Activities);
                    else
                        query = query.Include(c => c.Modules)
                            .ThenInclude(m => m.Activities);
                }
                else if (parameters.CascadeIncludeModuleDocs)
                    query = query.Include(c => c.Modules)
                        .ThenInclude(m => m.Documents);
                else
                    query = query.Include(c => c.Modules);
            }

            if (parameters.IncludeDocuments)
                query = query.Include(c => c.Documents);

            if (parameters.IncludeUsers)
                query = query.Include(c => c.UserCourses)
                            .ThenInclude(uc => uc.User);

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                query = query.Where(c => c.CourseName.Contains(parameters.SearchTerm));

            if (parameters.StartDate.HasValue)
                query = query.Where(c => c.StartDate >= parameters.StartDate.Value);

            if (parameters.EndDate.HasValue)
                query = query.Where(c => c.EndDate <= parameters.EndDate.Value);

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize);

            return await query.ToListAsync(); //TODO: return total count
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
