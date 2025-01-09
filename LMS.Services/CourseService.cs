using AutoMapper;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services;

using Domain.Models.Entites;
using LMS.Shared.DTOs;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;
    
    public CourseService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _uow = unitOfWork;
    }
    
    public async Task<IEnumerable<CourseDto>> GetCourseAsync()
    {
        var courses =  await _uow.CourseRepo.FindAll().ToListAsync();
        return _mapper.Map<IEnumerable<CourseDto>>(courses);
    }

    public async Task<CourseDto> GetCourseAsync(int id)
    {
        var course = await _uow.CourseRepo.FindByCondition(c => c.Id == id).SingleAsync();
        return _mapper.Map<Course, CourseDto>(course);
    }

    public async Task<CourseDto> PostCourse(CourseCreationDto dto)
    {
        var course = _mapper.Map<CourseCreationDto, Course>(dto);
        _uow.CourseRepo.Create(course);
        await _uow.CompleteASync();
        return _mapper.Map<Course, CourseDto>(course);
    }

    public Task<CourseDto> PutCourse(int id, CourseCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<CourseDto> DeleteCourse(int id)
    {
        throw new NotImplementedException();
    }
}