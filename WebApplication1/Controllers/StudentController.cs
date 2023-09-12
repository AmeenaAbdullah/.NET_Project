using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.DTOs.StudentDTO;
using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Interfaces.Student_Interfaces;
using WebApplication1.Models;
using WebApplication1.Services.Base_Services;
using WebApplication1.Services.Student_Services;

namespace WebApplication1.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(StudentController));
        private readonly IMapper _mapper;
        //  private readonly IStudent _studentService;
        private readonly IBase<Student> _studentService;
        private readonly IStudent _studentService2;

        public StudentController(IBase<Student> studentService, IMapper mapper, IStudent studentService2)
        {
            _studentService = studentService;
            _mapper = mapper;
            _studentService2 = studentService2;
           
        }
        [HttpGet("{studentId}/courses")]
        public ActionResult<List<Course>> GetCoursesForStudent(int studentId)
        {
            var courses = _studentService2.GetCoursesForStudent(studentId);
            return Ok(courses);
        }
        [HttpGet]
        public ActionResult<List<Student>> GetStudents()
        {
            try
            {
                IEnumerable<Student> students = _studentService.GetAll();
                if (students == null)
                {
                    _logger.Warn("No students found.");
                    return NotFound("No students found.");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while retrieving students: {ex.Message}", ex);
               return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] StudentDTO student)
        {
            try
            {
                Student studenttemp = _mapper.Map<Student>(student);
                _studentService.Add(studenttemp);
                _logger.Info("Student Added Succesfully");
                return Ok("Student added successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while adding the student: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {

            try
            {
                var student = _studentService.GetById(id);

                if (student == null)
                {
                    _logger.Warn("Student not found.");
                    return NotFound("Student not found.");
                }

                return Ok(student);

            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while getting student by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);

            }
          
        }
        [HttpPut("update/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentDTO updatedStudent)
        {
            try
            { 
                Student student = _mapper.Map<Student>(updatedStudent);
                if (_studentService.checkEntity(id) == false)
                {
                    _logger.Warn($"No student found by ID: {id}");
                    return NotFound();
                }
                _studentService.Update(id, student);
                return Ok("Student updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.Error($"An error occurred while updating student by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteStudent(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();
                if (_studentService.checkEntity(id) == false)
                {
                    _logger.Warn($"No student found of ID : {id}");
                    return NotFound();
                }    
                _studentService.Delete(id);
                return Ok("Student Deleted Successfully");
            }
            catch(Exception ex)
            {
                _logger.Error($"An error occurred while deleting student by ID: {ex.Message}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
