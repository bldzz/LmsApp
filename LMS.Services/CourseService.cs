using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class CourseService : ServiceBase<Course, CourseDto, CourseCreationDto, GetCoursesParameters>, ICourseService
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

        public async Task AddUserAsync(int courseId, string userId)
        {
            //var course = await _uow.CourseRepo.GetCourseByIdAsync(courseId);
            //if (course == null)
            //    throw new KeyNotFoundException(); //TODO: improve error handling
            //// Check if user exists
            //var user = await _uow.UserRepo.GetUserAsync(userId);
            //if (user == null)
            //    throw new KeyNotFoundException(); //TODO: improve error handling

            //var existingUserCourse = await _uow.UserCourseRepo.FindExistingRelationship(courseId, userId);

            //if (existingUserCourse != null)
            //    throw new AccessViolationException(); //TODO: improve error handling

            //var userCourse = new UserCourse
            //{
            //    UserId = userId,
            //    CourseId = courseId
            //};

            //_uow.UserCourses.Add(userCourse);
            //await _uow.CompleteASync();
            _uow.UserCourseRepo.Create(new UserCourse { UserId = userId, CourseId = courseId });
            _uow.CompleteASync();

            return;
        }

    }
}
