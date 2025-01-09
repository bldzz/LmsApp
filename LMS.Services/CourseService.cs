using AutoMapper;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services;
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

    public Task<CourseDto> GetCourseAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<CourseDto> PostCourse(CourseCreationDto dto)
    {
        throw new NotImplementedException();
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