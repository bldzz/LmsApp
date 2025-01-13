using AutoMapper;
using Domain.Contracts;
using LMS.Shared.DTOs;
using System.Reflection;

namespace LMS.Services
{
    internal class ModuleService : ServiceBase<Module, ModuleDto, ModuleCreationDto>
    {
        public ModuleService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override void CreateEntity(Module entity)
        {
            _uow.ModuleRepo.Create(entity);
        }

        protected override void DeleteEntity(Module entity)
        {
            _uow.ModuleRepo.Delete(entity);
        }

        protected override async Task<Module> FindEntityByIdAsync(int id)
        {
            return await _uow.ModuleRepo.FindByCondition(m => m.Id == id).SingleAsync();
        }

        protected override async Task<IEnumerable<Module>> GetAllEntitiesAsync()
        {
            return await _uow.ModuleRepo.FindAll().ToListAsync();
        }

        protected override async Task<Module> GetEntityByIdAsync(int id)
        {
            return await _uow.ModuleRepo.FindByCondition(m => m.Id == id).SingleAsync();
        }

        protected override void UpdateEntity(Module entity)
        {
            _uow.ModuleRepo.Update(entity);
        }

        protected override async Task<bool> ValidateEntityExistsAsync(int id)
        {
            var entity = await _uow.ModuleRepo.FindByCondition(m => m.Id == id).SingleOrDefaultAsync();
            return entity != null;
        }
    }
}
