using AutoMapper;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services;

using Domain.Models.Entites;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.VisualBasic;

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
        var createdCourse = await _uow.CourseRepo.FindByCondition(c => c.Id == course.Id).SingleAsync();
        var returnDto = _mapper.Map<CourseDto>(createdCourse);
        return returnDto;
    }

    public async Task<CourseDto> PutCourse(int id, CourseDto dto)
    {
        if(id != dto.Id)
        {
            throw new InvalidDataException();
        }
        var existingCourse = await _uow.CourseRepo.FindByCondition(c => c.Id == id).SingleAsync();
        if (existingCourse == null)  throw new InvalidDataException(); 
        existingCourse.CourseName = dto.CourseName;
        existingCourse.StartDate = dto.StartDate;
        existingCourse.EndDate = dto.EndDate;
        _uow.CourseRepo.Update(existingCourse);
        await _uow.CompleteASync();
        var updatedCourse = await _uow.CourseRepo.FindByCondition(c => c.Id != id).SingleAsync();
        var returnDto = _mapper.Map<CourseDto>(updatedCourse);
        return returnDto;
    }

    public async Task<CourseDto> DeleteCourse(int id)
    {
        var courseToDelete = await _uow.CourseRepo.FindByCondition(c => c.Id == id).SingleAsync();
        if (courseToDelete == null) throw new InvalidDataException();
        _uow.CourseRepo.Delete(courseToDelete);
        await _uow.CompleteASync();
        var shouldBeNull = await _uow.CourseRepo.FindByCondition(c => c.Id == id).SingleAsync();
        if (shouldBeNull != null) throw new InvalidOperationException();
        var placeholderDto = new CourseDto { Id = id, CourseName = "", StartDate = DateTime.Now, EndDate = DateTime.Now}; // TODO: fulhack, placeholder tills custom results är gjord
        return placeholderDto;
    }

    public async Task<CourseDto> PatchCourse(int id, JsonPatchDocument<CourseDto> patchDoc)
    {
        var existingCourse = await _uow.CourseRepo.FindByCondition(c => c.Id == id).SingleAsync();
        if (existingCourse == null) throw new InvalidDataException();

        var courseDto = _mapper.Map<CourseDto>(existingCourse);
        patchDoc.ApplyTo(courseDto);

        _mapper.Map(courseDto, existingCourse);
        _uow.CourseRepo.Update(existingCourse);
        await _uow.CompleteASync();

        var updatedCourse = await _uow.CourseRepo.FindByCondition(c => c.Id == id).SingleAsync();
        return _mapper.Map<CourseDto>(updatedCourse);
    }
}