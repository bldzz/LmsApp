using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using LMS.Shared.DTOs;
using LMS.Shared.ParamaterContainers;

namespace LMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public ActivitiesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityDto>>> GetActivities([FromQuery] GetActivitiesParameters parameters)
        {
            return Ok(await _serviceManager.ActivityService.GetAllAsync(parameters));
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityDto>> GetActivity(int id)
        {
            var activity = await _serviceManager.ActivityService.GetByIdAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            return Ok(activity);
        }

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, ActivityDto activity)
        {
            await _serviceManager.ActivityService.UpdateAsync(id, activity);
            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityDto>> PostActivity(ActivityCreationDto activity)
        {
            var newActivity = await _serviceManager.ActivityService.CreateAsync(activity);

            return CreatedAtAction("GetActivity", new { id = newActivity.Id }, activity);
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            await _serviceManager.ActivityService.DeleteAsync(id);
            return NoContent();
        }

        private bool ActivityExists(int id)
        {
            return _serviceManager.ActivityService.GetByIdAsync(id)!=null;
        }
    }
}
