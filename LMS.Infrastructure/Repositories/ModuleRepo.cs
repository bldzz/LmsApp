using Domain.Contracts;
using Domain.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class ModuleRepo : RepositoryBase<Module>, IModuleRepo
{
    public ModuleRepo(LmsContext context) : base(context)
    {
    }

    // Fetch all modules with related data (eager loading)
    public async Task<IEnumerable<Module>> GetAllModulesAsync(bool trackChanges = false)
    {
        return await FindAll(trackChanges)
            .Include(m => m.Course)
            .Include(m => m.Activities)
            .Include(m => m.Documents)
            .ToListAsync();
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