using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class ActivityService : ServiceBase<Activity, ActivityDto, ActivityCreationDto>, IActivityService
    {
        public ActivityService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override void CreateEntity(Activity entity)
        {
            _uow.ActivityRepo.Create(entity);
        }

        protected override void DeleteEntity(Activity entity)
        {
            _uow.ActivityRepo.Delete(entity);
        }

        protected override async Task<IEnumerable<Activity>> GetAllEntitiesAsync(GetCoursesParameters parameters)
        {
            return await _uow.ActivityRepo.GetAllActivitiesAsync();
        }

        protected override async Task<Activity?> GetEntityByIdAsync(int id)
        {
            return await _uow.ActivityRepo.GetActivityByIdAsync(id);
        }

        protected override void UpdateEntity(Activity entity)
        {
            _uow.ActivityRepo.Update(entity);
        }

        protected override async Task<bool> ValidateEntityExistsAsync(int id)
        {
            var entity = await _uow.ActivityRepo.FindByCondition(m => m.Id == id).SingleOrDefaultAsync();
            return entity != null;
        }
    }
}
