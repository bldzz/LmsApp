using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using LMS.Shared.DTOs;

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
            {
                return NotFound(new { message = $"Document with ID {id} not found." });
            }

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
            var document = await _serviceManager.DocumentService.GetByIdAsync(id);

            if (document == null || string.IsNullOrEmpty(document.FilePath))
                return NotFound(new { message = $"Document with ID {id} not found or does not have an associated file." });

            try
            {
                var memoryStream = new MemoryStream();

                using (var stream = new FileStream(document.FilePath, FileMode.Open, FileAccess.Read))
                {
                    await stream.CopyToAsync(memoryStream);
                }

                memoryStream.Position = 0;

                var contentType = "application/octet-stream"; // Default content type; adjust as needed.
                var fileName = Path.GetFileName(document.FilePath);

                return File(memoryStream, contentType, fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while downloading the document.", details = ex.Message });
            }
        }
    }
}
