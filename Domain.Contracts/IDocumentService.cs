using LMS.Shared;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Contracts
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetAllAsync(GetCoursesParameters parameters);
        Task<DocumentDto> GetByIdAsync(int id);
        Task<DocumentDto> CreateAsync(DocumentCreationDto creationDto);
        Task<DocumentDto> UpdateAsync(int id, DocumentDto dto);
        Task<DocumentDto> DeleteAsync(int id);
        Task<DocumentDto> PatchAsync(int id, JsonPatchDocument<DocumentDto> patchDoc);
    }
}
