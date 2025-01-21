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
        CreateMap<CourseDto, Course>();
        CreateMap<CourseCreationDto, Course>();

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

        CreateMap<UserCourse, UserCourseDto>();

        CreateMap<ApplicationUser, ApplicationUserDto>();
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.Users, opt => opt
            .MapFrom(src => src.UserCourses.Select(uc => new ApplicationUserDto
            {
                Id = uc.User.Id,
                Name = uc.User.FirstName + " " + uc.User.LastName,
                CourseId = uc.CourseId
            })));
    }
}
