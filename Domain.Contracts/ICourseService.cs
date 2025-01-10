using LMS.Shared.DTOs;

namespace Domain.Contracts;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetCourseAsync();
    Task<CourseDto> GetCourseAsync(int id);
    Task<CourseDto> PostCourse(CourseCreationDto dto);  
    Task<CourseDto> PutCourse(int id, CourseDto dto);
    Task<CourseDto> DeleteCourse(int id);
}