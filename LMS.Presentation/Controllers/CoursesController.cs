using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entites;
using Services.Contracts;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Http;

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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, [FromBody] CourseDto course)
        {
            // Validate the course ID in the request matches the DTO
            if (id != course.Id)
            {
                return BadRequest("Course ID mismatch");
            }

            try
            {
                // Call the service to update the course
                var updatedCourse = await _serviceManager.CourseService.PutCourse(id, course);

                // Return the updated course
                return Ok(updatedCourse);
            }
            catch (InvalidDataException)
            {
                // If course not found or invalid data, return 404 Not Found
                return NotFound($"Course with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception and return 500 Internal Server Error
                Console.Error.WriteLine($"Error updating course: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the course.");
            }
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
