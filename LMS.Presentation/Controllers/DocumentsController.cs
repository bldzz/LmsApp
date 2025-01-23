using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public DocumentsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Documents
        [HttpGet]
        // [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            return Ok(await _serviceManager.DocumentService.GetAllAsync());
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        // [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
            var document = await _serviceManager.DocumentService.GetByIdAsync(id);

            if (document == null)
                return NotFound(new { message = $"Document with ID {id} not found." });

            return Ok(document);
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> PutDocument(int id, DocumentDto document)
        {
            await _serviceManager.DocumentService.UpdateAsync(id, document);
            return NoContent();
        }

        // POST: api/Documents/upload
        [HttpPost("upload")]
        // [Authorize(Roles = "Admin, Teacher")]
        public async Task<ActionResult<DocumentDto>> UploadDocument([FromForm] DocumentCreationDto document)
        {
            try
            {
                var newDocument = await _serviceManager.DocumentService.CreateAsync(document);
                return CreatedAtAction("GetDocument", new { id = newDocument.Id }, newDocument);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while uploading the document.", details = ex.Message });
            }
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin, Teacher")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            try
            {
                await _serviceManager.DocumentService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the document.", details = ex.Message });
            }
        }

        // GET: api/Documents/5/download
        [HttpGet("{id}/download")]
        public async Task<IActionResult> DownloadDocument(int id)
        {
            try
            {
                // Retrieve document details using your service or repository
                var document = await _serviceManager.DocumentService.GetByIdAsync(id);

                if (document == null)
                {
                    return NotFound(new { message = "Document not found." });
                }

                // Ensure the file exists on the server
                if (string.IsNullOrEmpty(document.FilePath) || !System.IO.File.Exists(document.FilePath))
                {
                    return NotFound(new { message = "File not found on the server." });
                }

                // Read file bytes
                var fileBytes = await System.IO.File.ReadAllBytesAsync(document.FilePath);

                // Return file with proper headers
                // Add the header using the indexer
                Response.Headers["Access-Control-Expose-Headers"] = "Content-Disposition";
                return File(fileBytes, document.ContentType, document.Name);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while downloading the document.", details = ex.Message });
            }
        }
    }
}