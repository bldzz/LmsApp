using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Contracts
{
    public interface IActivityService
    {
        Task<IEnumerable<ActivityDto>> GetAllAsync(GetActivitiesParameters parameters);
        Task<ActivityDto> GetByIdAsync(int id);
        Task<ActivityDto> CreateAsync(ActivityCreationDto creationDto);
        Task<ActivityDto> UpdateAsync(int id, ActivityDto dto);
        Task<ActivityDto> DeleteAsync(int id);
        Task<ActivityDto> PatchAsync(int id, JsonPatchDocument<ActivityDto> patchDoc);

    }
}