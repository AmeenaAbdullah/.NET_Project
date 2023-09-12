using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Models;
namespace WebApplication1.Interfaces.Student_Interfaces
{
    public interface IStudent
    {
        public List<Course> GetCoursesForStudent(int studentId);
        //public List<Student> GetStudents();
        //public void AddStudent(Student student);
        //public Student GetStudentById(int id);
        //public void UpdateStudent(int id, Student s);
        //public void DeleteStudent(int id);
        //public bool checkStudent(int id);
    }
}
