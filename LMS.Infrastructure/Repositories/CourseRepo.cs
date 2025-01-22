using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Infrastructure.Data;
using LMS.Shared.ParamaterContainers;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

            query = IncludeModulesWithDocumentsAndActivities(query, parameters);
            query = IncludeDocuments(query, parameters);
            query = IncludeUsers(query, parameters);

            query = ApplySearchFilters(query, parameters);
            query = ApplyDateFilters(query, parameters);
            query = ApplyPagination(query, parameters);

            var totalCount = await query.CountAsync();

            return await query.ToListAsync(); //TODO: return total count
        }

        // Get a course by ID with related data (eager loading)
        public async Task<Course> GetCourseByIdAsync(int courseId, bool trackChanges = false)
        {
            var course = await FindByCondition(c => c.Id == courseId, trackChanges)
                .Include(c => c.Modules)
                .ThenInclude(m => m.Activities)
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

        //static methods to check the query parameters. split out for readability
        private static IQueryable<Course> IncludeModulesWithDocumentsAndActivities(IQueryable<Course> query, GetCoursesParameters parameters)
        {
            if (!parameters.IncludeModules) return query;

            if (parameters.CascadeIncludeActivities)
            {
                if (parameters.CascadeIncludeActivityDocs && parameters.CascadeIncludeModuleDocs)
                    return query.Include(c => c.Modules)
                               .ThenInclude(m => m.Documents)
                               .Include(c => c.Modules)
                               .ThenInclude(m => m.Activities)
                               .ThenInclude(a => a.Documents);

                if (parameters.CascadeIncludeActivityDocs)
                    return query.Include(c => c.Modules)
                               .ThenInclude(m => m.Activities)
                               .ThenInclude(a => a.Documents);

                if (parameters.CascadeIncludeModuleDocs)
                    return query.Include(c => c.Modules)
                               .ThenInclude(m => m.Documents)
                               .Include(c => c.Modules)
                               .ThenInclude(m => m.Activities);

                return query.Include(c => c.Modules)
                           .ThenInclude(m => m.Activities);
            }

            if (parameters.CascadeIncludeModuleDocs)
                return query.Include(c => c.Modules)
                           .ThenInclude(m => m.Documents);

            return query.Include(c => c.Modules);
        }
        private static IQueryable<Course> IncludeDocuments(IQueryable<Course> query, GetCoursesParameters parameters)
        {
            return parameters.IncludeDocuments
                ? query.Include(c => c.Documents)
                : query;
        }

        private static IQueryable<Course> IncludeUsers(IQueryable<Course> query, GetCoursesParameters parameters)
        {
            return parameters.IncludeUsers
                ? query.Include(c => c.UserCourses)
                       .ThenInclude(uc => uc.User)
                : query;
        }

        private static IQueryable<Course> ApplySearchFilters(IQueryable<Course> query, GetCoursesParameters parameters)
        {
            return !string.IsNullOrWhiteSpace(parameters.SearchTerm)
                ? query.Where(c => c.CourseName.Contains(parameters.SearchTerm))
                : query;
        }

        private static IQueryable<Course> ApplyDateFilters(IQueryable<Course> query, GetCoursesParameters parameters)
        {
            if (parameters.StartDate.HasValue)
                query = query.Where(c => c.StartDate >= parameters.StartDate.Value);

            if (parameters.EndDate.HasValue)
                query = query.Where(c => c.EndDate <= parameters.EndDate.Value);

            return query;
        }

        private static IQueryable<Course> ApplyPagination(IQueryable<Course> query, GetCoursesParameters parameters)
        {
            return query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                        .Take(parameters.PageSize);
        }
    }
}
