using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Interfaces.Student_Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services.Student_Services
{
    public class StudentServices:IStudent
    { 
        ManagementSytemContext _context = new ManagementSytemContext();

        public StudentServices(ManagementSytemContext context)
        {
            _context = context;
        }
        
        //Get the student courses using student id
        public List<Course> GetCoursesForStudent(int studentId)
        {

            var courseIds = _context.Studentcourses
            .Where(sc => sc.Sid == studentId)
            .Select(sc => sc.Cid)
            .ToList();

            var courses = _context.Courses
                .Where(c => courseIds.Contains(c.Id))
                .ToList();

            return courses;

        }
        //    ManagementSytemContext _context = new ManagementSytemContext();

        //    public StudentServices(ManagementSytemContext context)
        //    {
        //        _context = context;
        //    }
        //    public List<Student> GetStudents()
        //    {
        //        List<Student> students = _context.Students.ToList();
        //        return students;
        //    }
        //    public void AddStudent(Student student)
        //    {
        //        _context.Students.Add(student);
        //        _context.SaveChanges();
        //    }
        //    public Student GetStudentById(int id)
        //    {
        //        Student s = _context.Students.Find(id);
        //        return s;
        //    }
        //    public void UpdateStudent(int id, Student updatedStudent)
        //    {
        //        var existingStudent = _context.Students.Find(id);

        //        if (existingStudent != null)
        //        {
        //            existingStudent.Name = updatedStudent.Name;
        //            existingStudent.Email = updatedStudent.Email;
        //            _context.SaveChanges();
        //        }
        //    }
        //    public bool checkStudent(int id)
        //    { return (_context.Students?.Any(x => x.Id == id)).GetValueOrDefault();
        //    }
        //    public void DeleteStudent(int id)
        //    {
        //        _context.Students.Remove(GetStudentById(id));
        //        _context.SaveChanges();
        //    }
    }
}
