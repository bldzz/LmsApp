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
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            return Ok(await _serviceManager.DocumentService.GetAllAsync());
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
            var document = await _serviceManager.DocumentService.GetByIdAsync(id);

            if (document == null)
                return NotFound(new { message = $"Document with ID {id} not found." });

            return Ok(document);
        }

        // PUT: api/Documents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, DocumentDto document)
        {
            await _serviceManager.DocumentService.UpdateAsync(id, document);
            return NoContent();
        }

        // POST: api/Documents/upload
        [HttpPost("upload")]
        [Consumes("multipart/form-data")] // Explicitly tell Swagger this is a file upload
        public async Task<IActionResult> UploadDocument([FromForm] FileUploadDto uploadDto)
        {
            if (uploadDto.File == null || uploadDto.File.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Example logic to save the file
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(uploadDto.File.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await uploadDto.File.CopyToAsync(stream);
            }

            return Ok(new { FileName = uniqueFileName, FilePath = filePath });
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
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