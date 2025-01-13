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
            throw new NotImplementedException();
        }

        protected override void DeleteEntity(Module entity)
        {
            throw new NotImplementedException();
        }

        protected override Task<Module> FindEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        protected override Task<IEnumerable<Module>> GetAllEntitiesAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task<Module> GetEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateEntity(Module entity)
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> ValidateEntityExistsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
