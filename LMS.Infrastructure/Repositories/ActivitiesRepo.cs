using Domain.Contracts;
using Domain.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class ActivityRepo : RepositoryBase<Activity>, IActivityRepo
{
    public ActivityRepo(LmsContext context) : base(context)
    {
    }

    // Fetch all activities with related data (eager loading)
    public async Task<IEnumerable<Activity>> GetAllActivitiesAsync(bool trackChanges = false)
    {
        return await FindAll(trackChanges)
            .Include(a => a.Module) // Include related Module
            .Include(a => a.Documents) // Include related Documents
            .ToListAsync();
    }

    // Fetch an activity by ID with related data (eager loading)
    public async Task<Activity?> GetActivityByIdAsync(Guid activityId, bool trackChanges = false)
    {
        return await FindByCondition(a => a.Id == activityId, trackChanges)
            .Include(a => a.Module) // Include related Module
            .Include(a => a.Documents) // Include related Documents
            .FirstOrDefaultAsync();
    }

    // Fetch all activities for a specific module
    public async Task<IEnumerable<Activity>> GetActivitiesByModuleIdAsync(int moduleId, bool trackChanges = false)
    {
        return await FindByCondition(a => a.ModuleId == moduleId, trackChanges)
            .Include(a => a.Documents) // Include related Documents
            .ToListAsync();
    }

    // Check if an activity exists by ID
    public async Task<bool> ActivityExistsAsync(Guid activityId)
    {
        return await FindByCondition(a => a.Id == activityId).AnyAsync();
    }
}