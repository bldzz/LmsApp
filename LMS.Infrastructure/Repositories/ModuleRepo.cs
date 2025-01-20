using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared.ParamaterContainers;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class ModuleRepo : RepositoryBase<Module>, IModuleRepo
{
    public ModuleRepo(LmsContext context) : base(context)
    {
    }

    // Fetch all modules with related data (eager loading)
    public async Task<IEnumerable<Module>> GetAllModulesAsync(GetModulesParameters parameters)
    {
            var query = FindAll(parameters.TrackChanges);

            if (parameters.IncludeActivities)
                query = query.Include(c => c.Activities);

            if (parameters.IncludeDocuments)
                query = query.Include(c => c.Documents);

            //if (parameters.IncludeUsers)
            //    query = query.Include(c => c.UserCourses)
            //                .ThenInclude(uc => uc.User);

            var totalCount = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
                query = query.Where(c => c.ModuleName.Contains(parameters.SearchTerm));

            if (parameters.StartDate.HasValue)
                query = query.Where(c => c.StartDate >= parameters.StartDate.Value);

            if (parameters.EndDate.HasValue)
                query = query.Where(c => c.EndDate <= parameters.EndDate.Value);

            query = query.Skip((parameters.PageNumber - 1) * parameters.PageSize)
                         .Take(parameters.PageSize);

            return await query.ToListAsync(); //TODO: return total count
    }

    // Fetch a module by ID with related data (eager loading)
    public async Task<Module?> GetModuleByIdAsync(int moduleId, bool trackChanges = false)
    {
        return await FindByCondition(m => m.Id == moduleId, trackChanges)
            .Include(m => m.Course)
            .Include(m => m.Activities)
            .Include(m => m.Documents)
            .FirstOrDefaultAsync();
    }

    // Fetch all modules for a specific course
    public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId, bool trackChanges = false)
    {
        return await FindByCondition(m => m.CourseId == courseId, trackChanges)
            .Include(m => m.Activities)
            .Include(m => m.Documents)
            .ToListAsync();
    }

    // Check if a module exists by ID
    public async Task<bool> ModuleExistsAsync(int moduleId)
    {
        return await FindByCondition(m => m.Id == moduleId).AnyAsync();
    }
}