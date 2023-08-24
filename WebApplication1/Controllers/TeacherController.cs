using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.DTOs.TeacherDTO;
using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Interfaces.Teacher_Interfaces;
using WebApplication1.Models;
using WebApplication1.Services.Teacher_Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : ControllerBase
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TeacherController));
        private readonly IMapper _mapper;
        private readonly IBase<Teacher> _TeacherService;

        public TeacherController(IBase<Teacher> TeacherService, IMapper mapper)
        {
            _TeacherService = TeacherService;
            _mapper = mapper;

        }

        [HttpGet]
        public ActionResult<List<Teacher>> GetTeachers()
        {
            try
            {
                IEnumerable<Teacher> Teachers = _TeacherService.GetAll();
                if (Teachers == null)
                {
                    _logger.Warn("No Teachers found.");
                    return NotFound("No Teachers found.");
                }
                return Ok(Teachers);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while retrieving Teachers: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddTeacher([FromBody] TeacherDTO Teacher)
        {
            try
            {
                Teacher Teachertemp = _mapper.Map<Teacher>(Teacher);
                _TeacherService.Add(Teachertemp);
                return Ok("Teacher added successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while adding the Teacher: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {

            try
            {
                var Teacher = _TeacherService.GetById(id);

                if (Teacher == null)
                {
                    _logger.Warn("Teacher not found.");
                    return NotFound("Teacher not found.");
                }

                return Ok(Teacher);

            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while getting Teacher by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);

            }

        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateTeacher(int id, [FromBody] TeacherDTO updatedTeacher)
        {
            try
            {
                Teacher Teacher = _mapper.Map<Teacher>(updatedTeacher);
                if (_TeacherService.checkEntity(id) == false)
                {
                    _logger.Warn($"No Teacher found by ID: {id}");
                    return NotFound();
                }
                _TeacherService.Update(id, Teacher);
                return Ok("Teacher updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while updating Teacher by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTeacher(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();
                if (_TeacherService.checkEntity(id) == false)
                {
                    _logger.Warn($"No Teacher found of ID : {id}");
                    return NotFound();
                }
                _TeacherService.Delete(id);
                return Ok("Teacher Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while deleting Teacher by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
