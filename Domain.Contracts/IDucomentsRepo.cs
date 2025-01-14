using Domain.Models.Entites;

namespace Domain.Contracts;

public interface IDocumentRepo : IRepositoryBase<Document>
{
    // Fetch all documents with related data
    Task<IEnumerable<Document>> GetAllDocumentsAsync(bool trackChanges = false);

    // Fetch a document by ID with related data
    Task<Document?> GetDocumentByIdAsync(int documentId, bool trackChanges = false);

    // Fetch all documents for a specific user
    Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(string userId, bool trackChanges = false);

    // Fetch all documents for a specific course
    Task<IEnumerable<Document>> GetDocumentsByCourseIdAsync(int courseId, bool trackChanges = false);

    // Fetch all documents for a specific module
    Task<IEnumerable<Document>> GetDocumentsByModuleIdAsync(int moduleId, bool trackChanges = false);

    // Fetch all documents for a specific activity
    Task<IEnumerable<Document>> GetDocumentsByActivityIdAsync(int activityId, bool trackChanges = false);

    // Check if a document exists by ID
    Task<bool> DocumentExistsAsync(int documentId);
}