﻿using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Contracts;

public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllAsync(GetCoursesParameters parameters);
    Task<CourseDto> GetByIdAsync(int id);
    Task<CourseDto> CreateAsync(CourseCreationDto creationDto);
    Task<CourseDto> UpdateAsync(int id, CourseDto dto);
    Task<CourseDto> DeleteAsync(int id);
    Task<CourseDto> PatchAsync(int id, JsonPatchDocument<CourseDto> patchDoc);
    Task AddUserAsync(int courseId, string userId);
}