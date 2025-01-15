using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Entites;

namespace LMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCoursesController : ControllerBase
    {
        private readonly LmsContext _context;

        public UserCoursesController(LmsContext context)
        {
            _context = context;
        }

        // GET: api/UserCourses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserCourse>>> GetUserCourses()
        {
            return await _context.UserCourses.ToListAsync();
        }

        // GET: api/UserCourses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserCourse>> GetUserCourse(int id)
        {
            var userCourse = await _context.UserCourses.FindAsync(id);

            if (userCourse == null)
            {
                return NotFound();
            }

            return userCourse;
        }

        // PUT: api/UserCourses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserCourse(int id, UserCourse userCourse)
        {
            if (id != userCourse.Id)
            {
                return BadRequest();
            }

            _context.Entry(userCourse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserCourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserCourses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserCourse>> PostUserCourse(UserCourse userCourse)
        {
            _context.UserCourses.Add(userCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserCourse", new { id = userCourse.Id }, userCourse);
        }

        // DELETE: api/UserCourses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserCourse(int id)
        {
            var userCourse = await _context.UserCourses.FindAsync(id);
            if (userCourse == null)
            {
                return NotFound();
            }

            _context.UserCourses.Remove(userCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserCourseExists(int id)
        {
            return _context.UserCourses.Any(e => e.Id == id);
        }
    }
}
