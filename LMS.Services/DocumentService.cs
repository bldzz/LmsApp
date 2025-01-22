using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entites;
using LMS.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LMS.Services
{
    public class DocumentService : ServiceBase<Document, DocumentDto, DocumentCreationDto>, IDocumentService
    {
        private readonly string _uploadsFolder;

        public DocumentService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
            if (!Directory.Exists(_uploadsFolder))
            {
                Directory.CreateDirectory(_uploadsFolder);
            }
        }

        protected override void CreateEntity(Document entity)
        {
            _uow.DocumentRepo.Create(entity);
        }

        protected override void DeleteEntity(Document entity)
        {
            if (!string.IsNullOrEmpty(entity.FilePath) && File.Exists(entity.FilePath))
            {
                File.Delete(entity.FilePath);
            }

            _uow.DocumentRepo.Delete(entity);
        }

        protected override async Task<IEnumerable<Document>> GetAllEntitiesAsync()
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

        public override async Task<DocumentDto> CreateAsync(DocumentCreationDto creationDto)
        {
            if (creationDto.File == null || creationDto.File.Length == 0)
                throw new InvalidOperationException("File is required.");

            // Generate a unique file name and save path
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(creationDto.File.FileName)}";
            var filePath = Path.Combine(_uploadsFolder, uniqueFileName);

            // Save the file to the server's file system
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await creationDto.File.CopyToAsync(stream);
            }

            // Map DTO to entity and save reference in database
            var document = _mapper.Map<Document>(creationDto);
            document.FilePath = filePath;
            document.UploadTime = DateTime.UtcNow;

            CreateEntity(document);
            await _uow.CompleteASync();

            return _mapper.Map<DocumentDto>(document);
        }
    }
}