using AutoMapper;
using Domain.Models.Entites;
using LMS.Shared.DTOs;


namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>();

        CreateMap<Course, CourseDto>();
        CreateMap<Course, CourseDto>();
        CreateMap<CourseDto, Course>();

        CreateMap<Module, ModuleDto>();
        CreateMap<ModuleDto, Module>();
        CreateMap<ModuleCreationDto, Module>();

        CreateMap<UserCourse, UserCourseDto>();
        CreateMap<UserCourseDto, UserCourse>();
        CreateMap<UserCourseCreationDto, UserCourse>();

        CreateMap<Document, DocumentDto>();
        CreateMap<DocumentDto, Document>();
        CreateMap<DocumentCreationDto, Document>();
        
        CreateMap<Activity, ActivityDto>();
        CreateMap<ActivityDto, Activity>();
        CreateMap<ActivityCreationDto, Activity>();
    }
}
