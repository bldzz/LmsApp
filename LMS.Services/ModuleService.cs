using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class ModuleService : ServiceBase<Module, ModuleDto, ModuleCreationDto, GetModulesParameters>, IModuleService
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

        protected override async Task<IEnumerable<Module>> GetAllEntitiesAsync(GetModulesParameters parameters)
        {
            return await _uow.ModuleRepo.GetAllModulesAsync(parameters);
        }

        protected override async Task<Module?> GetEntityByIdAsync(int id)
        {
            return await _uow.ModuleRepo.GetModuleByIdAsync(id);
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
