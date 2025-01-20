using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.AspNetCore.JsonPatch;

namespace Domain.Contracts
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentDto>> GetAllAsync(GetDocumentsParameters parameters);
        Task<DocumentDto> GetByIdAsync(int id);
        Task<DocumentDto> CreateAsync(DocumentCreationDto creationDto);
        Task<DocumentDto> UpdateAsync(int id, DocumentDto dto);
        Task<DocumentDto> DeleteAsync(int id);
        Task<DocumentDto> PatchAsync(int id, JsonPatchDocument<DocumentDto> patchDoc);
    }
}
