using AutoMapper;
using LMS.Shared.DTOs;
using LMS.Shared.User;

namespace LMS.Infrastructure.Data;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserForRegistrationDto, ApplicationUser>();
    }
}
