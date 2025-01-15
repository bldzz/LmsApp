using LMS.Shared.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Contracts
{
    public interface IUserCourseService
    {
        Task<IEnumerable<UserCourseDto>> GetAllAsync();
        Task<UserCourseDto> GetByIdAsync(int id);
        Task<UserCourseDto> CreateAsync(UserCourseCreationDto creationDto);
        Task<UserCourseDto> UpdateAsync(int id, UserCourseDto dto);
        Task<UserCourseDto> DeleteAsync(int id);
        Task<UserCourseDto> PatchAsync(int id, JsonPatchDocument<UserCourseDto> patchDoc);
    }
}
