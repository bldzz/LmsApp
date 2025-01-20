using AutoMapper;
using Domain.Contracts;
using LMS.Shared.DTOs;
using Domain.Models.Entites;
using Microsoft.EntityFrameworkCore;
using LMS.Shared;

namespace LMS.Services
{
    public class UserCourseService : ServiceBase<UserCourse, UserCourseDto, UserCourseCreationDto>, IUserCourseService
    {
        public UserCourseService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override void CreateEntity(UserCourse entity)
        {
            _uow.UserCourseRepo.Create(entity);
        }

        protected override void DeleteEntity(UserCourse entity)
        {
            _uow.UserCourseRepo.Delete(entity);
        }

        protected override async Task<IEnumerable<UserCourse>> GetAllEntitiesAsync(GetCoursesParameters parameters)
        {
            return await _uow.UserCourseRepo.FindAll().ToListAsync();
        }

        protected override async Task<UserCourse> GetEntityByIdAsync(int id)
        {
            return await _uow.UserCourseRepo.FindByCondition(m => m.Id == id).SingleAsync();
        }

        protected override void UpdateEntity(UserCourse entity)
        {
            _uow.UserCourseRepo.Update(entity);
        }

        protected override async Task<bool> ValidateEntityExistsAsync(int id)
        {
            var entity = await _uow.UserCourseRepo.FindByCondition(m => m.Id == id).SingleOrDefaultAsync();
            return entity != null;
        }
    }
}
