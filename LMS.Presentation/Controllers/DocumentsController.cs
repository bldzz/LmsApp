using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entites;
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
                return NotFound();
            }

            return Ok(document);
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(int id, DocumentDto document)
        {
            await _serviceManager.DocumentService.UpdateAsync(id, document);
            return NoContent();
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DocumentDto>> PostDocument(DocumentCreationDto document)
        {
            var newDocument = await _serviceManager.DocumentService.CreateAsync(document);

            return CreatedAtAction("GetDocument", new { id = newDocument.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            await _serviceManager.DocumentService.DeleteAsync(id);
            return NoContent();
        }

        private bool DocumentExists(int id)
        {
            return _serviceManager.DocumentService.GetByIdAsync(id)!=null;
        }
    }
}
