using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class DocumentService : ServiceBase<Document, DocumentDto, DocumentCreationDto, GetDocumentsParameters>, IDocumentService
    {
        public DocumentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        protected override void CreateEntity(Document entity)
        {
            _uow.DocumentRepo.Create(entity);
        }

        protected override void DeleteEntity(Document entity)
        {
            _uow.DocumentRepo.Delete(entity);
        }

        protected override async Task<IEnumerable<Document>> GetAllEntitiesAsync(GetDocumentsParameters parameters)
        {
            return await _uow.DocumentRepo.GetAllDocumentsAsync();
        }

        protected override async Task<Document?> GetEntityByIdAsync(int id)
        {
            return await _uow.DocumentRepo.GetDocumentByIdAsync(id);
        }

        protected override void UpdateEntity(Document entity)
        {
            _uow.DocumentRepo.Update(entity);
        }

        protected override async Task<bool> ValidateEntityExistsAsync(int id)
        {
            var entity = await _uow.DocumentRepo.FindByCondition(m => m.Id == id).SingleOrDefaultAsync();
            return entity != null;
        }
    }
}
