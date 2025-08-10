using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleCourseManagerApi.Dtos.Request;
using SimpleCourseManagerApi.Interfaces;

namespace SimpleCourseManagerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [HttpGet]
        public IActionResult GetAllCourses() {
            return Ok(new {Courses = _courseService.GetAllCourses()});
        }
        [HttpGet("{id}")]
        public IActionResult GetOnebyId([FromRoute] int id)
        {
            var course = _courseService.GetCourseById(id);
            if (course is null) return NotFound();
            return Ok(new {Course = course});
        }
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CourseRequestDto courseRequest)
        {
            var result = _courseService.CreateCourse(courseRequest);
            if(!result.Success)
            {
                return BadRequest(new {Errors = result.Errors});
            }
            return CreatedAtAction(nameof(GetOnebyId), new { id = result.Data?.Id }, result.Data);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse([FromRoute] int id)
        {
            var result = _courseService.DeleteCourse(id);
            if (!result) return NotFound();
            return NoContent();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCourse([FromRoute] int id, CourseRequestDto courseRequest)
        {
            var result = _courseService.EditCourse(id, courseRequest);
            if(!result.Success && result.Errors is null)
            {
                return NotFound();
            }
            else if(!result.Success && result.Errors is not null)
            {
                return BadRequest(new { Errors = result.Errors });
            }

            return NoContent();
        }
    }
}
