using Domain.Models.Entites;

namespace Domain.Contracts;

public interface IActivityRepo : IRepositoryBase<Activity>
{
    // Fetch all activities with related data
    Task<IEnumerable<Activity>> GetAllActivitiesAsync(bool trackChanges = false);

    // Fetch an activity by ID with related data
    Task<Activity?> GetActivityByIdAsync(int activityId, bool trackChanges = false);

    // Fetch all activities for a specific module
    Task<IEnumerable<Activity>> GetActivitiesByModuleIdAsync(int moduleId, bool trackChanges = false);

    // Check if an activity exists by ID
    Task<bool> ActivityExistsAsync(int activityId);
}