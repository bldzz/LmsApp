using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using LMS.Shared.ParamaterContainers;

namespace LMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ModulesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModuleDto>>> GetModules([FromQuery] GetModulesParameters parameters)
        {
            return Ok(await _serviceManager.ModuleService.GetAllAsync(parameters));
        }

        // GET: api/Modules/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDto>> GetModule(int id)
        {
            var module = await _serviceManager.ModuleService.GetByIdAsync(id);

            if (module == null)
            {
                return NotFound();
            }

            return Ok(module);
        }

        // PUT: api/Modules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModule(int id, ModuleDto module)
        {
            await _serviceManager.ModuleService.UpdateAsync(id, module);
            return NoContent();
        }

        // POST: api/Modules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModuleDto>> PostModule(ModuleCreationDto module)
        {
            var newModule = await _serviceManager.ModuleService.CreateAsync(module);

            return CreatedAtAction("GetModule", new { id = newModule.Id }, module);
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            await _serviceManager.ModuleService.DeleteAsync(id);
            return NoContent();
        }

        private bool ModuleExists(int id)
        {
            return _serviceManager.ModuleService.GetByIdAsync(id)!=null;
        }
    }
}
