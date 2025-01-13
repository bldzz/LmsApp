using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entites;
using Services.Contracts;
using LMS.Shared.DTOs;

namespace LMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private IServiceManager _serviceManager;

        public CoursesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            return Ok(await _serviceManager.CourseService.GetCourseAsync());
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _serviceManager.CourseService.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, CourseDto course)
        {
            await _serviceManager.CourseService.PutCourse(id, course);

            return NoContent(); //TODO: måste fixa error handling
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(CourseCreationDto course)
        {
            var newCourse = await _serviceManager.CourseService.PostCourse(course);

            return CreatedAtAction("GetCourse", new { id = newCourse.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _serviceManager.CourseService.DeleteCourse(id);
            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _serviceManager.CourseService.GetCourseAsync(id)!=null;
        }
    }
}
