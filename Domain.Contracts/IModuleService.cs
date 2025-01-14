using LMS.Shared.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDto>> GetAllAsync();
        Task<ModuleDto> GetByIdAsync(int id);
        Task<ModuleDto> CreateAsync(ModuleCreationDto creationDto);
        Task<ModuleDto> UpdateAsync(int id, ModuleDto dto);
        Task<ModuleDto> DeleteAsync(int id);
        Task<ModuleDto> PatchAsync(int id, JsonPatchDocument<ModuleDto> patchDoc);
    }
}

