using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class CourseService : ServiceBase<Course, CourseDto, CourseCreationDto>, ICourseService
    {
        public CourseService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override void CreateEntity(Course entity)
        {
            _uow.CourseRepo.Create(entity);
        }

        protected override void DeleteEntity(Course entity)
        {
            _uow.CourseRepo.Delete(entity);
        }

        protected override async Task<IEnumerable<Course>> GetAllEntitiesAsync(GetCoursesParameters parameters)
        {
            return await _uow.CourseRepo.GetAllCoursesAsync(parameters);
        }

        protected override async Task<Course?> GetEntityByIdAsync(int id)
        {
            return await _uow.CourseRepo.GetCourseByIdAsync(id);
        }

        protected override void UpdateEntity(Course entity)
        {
            _uow.CourseRepo.Update(entity);
        }

        protected override async Task<bool> ValidateEntityExistsAsync(int id)
        {
            var entity = await _uow.CourseRepo.FindByCondition(m => m.Id == id).SingleOrDefaultAsync();
            return entity != null;
        }
    }
}
