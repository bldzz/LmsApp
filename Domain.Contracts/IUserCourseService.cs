using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Contracts
{
    public interface IUserCourseService
    {
        Task<IEnumerable<UserCourseDto>> GetAllAsync(GetCoursesParameters parameters);
        Task<UserCourseDto> GetByIdAsync(int id);
        Task<UserCourseDto> CreateAsync(UserCourseCreationDto creationDto);
        Task<UserCourseDto> UpdateAsync(int id, UserCourseDto dto);
        Task<UserCourseDto> DeleteAsync(int id);
        Task<UserCourseDto> PatchAsync(int id, JsonPatchDocument<UserCourseDto> patchDoc);
    }
}
