using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces.Course_Interfaces;
using WebApplication1.Models;
using WebApplication1.DTOs.CourseDTO;
using log4net;
using WebApplication1.Services.Course_Services;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using WebApplication1.Interfaces.Base_Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(CourseController));
        private readonly IMapper _mapper;
        private readonly IBase<Course> _CourseService;

        public CourseController(IBase<Course> CourseService, IMapper mapper)
        {
            _CourseService = CourseService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Course>> GetCourses()
        {
            try
            {
                IEnumerable<Course> courses = _CourseService.GetAll();

                if (courses == null )
                {
                    _logger.Warn("No courses found.");
                    return NotFound("No courses found.");
                }

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while getting courses: {ex.Message}", ex);
                return BadRequest("An error occurred while retrieving courses.");
            }
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] CourseDTO course)
        {
            try
            {
                Course courseTemp = _mapper.Map<Course>(course);
                _CourseService.Add(courseTemp);
                _logger.Info($"Course '{courseTemp.Title}' added successfully.");
                return Ok("Course added successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while adding a course: {ex.Message}", ex);
                return BadRequest("An error occurred while adding a course.");
            }
        }

        [HttpDelete]
        public IActionResult DeleteCourse(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.Warn("Invalid course id provided.");
                    return BadRequest("Invalid course id provided.");
                }
                if (!_CourseService.checkEntity(id))
                    return NotFound($"Course with {id} is not exist");
                _CourseService.Delete(id);
                _logger.Info($"Course with ID {id} deleted successfully.");
                return Ok("Course deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while deleting a course: {ex.Message}", ex);
                return BadRequest("An error occurred while deleting a course.");
            }
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateCourse(int id, [FromBody] CourseDTO updatedCourse)
        {
            try
            {
                Course Course = _mapper.Map<Course>(updatedCourse);
                if (_CourseService.checkEntity(id) == false)
                {
                    _logger.Warn($"No Course found by ID: {id}");
                    return NotFound();
                }
                _CourseService.Update(id, Course);
                return Ok("Course updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while updating Course by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
