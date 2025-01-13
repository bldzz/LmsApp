using Domain.Contracts;
using Domain.Models.Entites;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Repositories;

public class DocumentRepo : RepositoryBase<Document>, IDocumentRepo
    {
        public DocumentRepo(LmsContext context) : base(context)
        {
        }

        // Fetch all documents with related data (eager loading)
        public async Task<IEnumerable<Document>> GetAllDocumentsAsync(bool trackChanges = false)
        {
            return await FindAll(trackChanges)
                .Include(d => d.User)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .Include(d => d.Activity)
                .ToListAsync();
        }

        // Fetch a document by ID with related data (eager loading)
        public async Task<Document?> GetDocumentByIdAsync(int documentId, bool trackChanges = false)
        {
            return await FindByCondition(d => d.Id == documentId, trackChanges)
                .Include(d => d.User)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .Include(d => d.Activity)
                .FirstOrDefaultAsync();
        }

        // Fetch all documents for a specific user
        public async Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(string userId, bool trackChanges = false)
        {
            return await FindByCondition(d => d.UserId == userId, trackChanges)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .Include(d => d.Activity)
                .ToListAsync();
        }

        // Fetch all documents for a specific course
        public async Task<IEnumerable<Document>> GetDocumentsByCourseIdAsync(int courseId, bool trackChanges = false)
        {
            return await FindByCondition(d => d.CourseId == courseId, trackChanges)
                .Include(d => d.User)
                .Include(d => d.Module)
                .Include(d => d.Activity)
                .ToListAsync();
        }

        // Fetch all documents for a specific module
        public async Task<IEnumerable<Document>> GetDocumentsByModuleIdAsync(int moduleId, bool trackChanges = false)
        {
            return await FindByCondition(d => d.ModuleId == moduleId, trackChanges)
                .Include(d => d.User)
                .Include(d => d.Course)
                .Include(d => d.Activity)
                .ToListAsync();
        }

        // Fetch all documents for a specific activity
        public async Task<IEnumerable<Document>> GetDocumentsByActivityIdAsync(Guid activityId, bool trackChanges = false)
        {
            return await FindByCondition(d => d.ActivityId == activityId, trackChanges)
                .Include(d => d.User)
                .Include(d => d.Course)
                .Include(d => d.Module)
                .ToListAsync();
        }

        // Check if a document exists by ID
        public async Task<bool> DocumentExistsAsync(int documentId)
        {
            return await FindByCondition(d => d.Id == documentId).AnyAsync();
        }
    }