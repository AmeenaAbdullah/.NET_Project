using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces.Course_Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services.Course_Services
{
   public class CourseServices:ICourse
    {

    //    ManagementSytemContext _context = new ManagementSytemContext();
    //    public CourseServices(ManagementSytemContext context)
    //    {
    //        _context = context;
    //    }
    //    public List<Course> GetCourses()
    //    {
    //        List<Course> Courses = _context.Courses.ToList();
    //        return Courses;
    //    }
    //    public void AddCourse(Course Course)
    //    {
    //        _context.Courses.Add(Course);
    //        _context.SaveChanges();
    //    }
    //    public Course GetCourse(int id)
    //    {
    //        return _context.Courses.Find(id);

    //    }
    //    public bool checkCourse(int id)
    //    { return (_context.Courses?.Any(x => x.Id == id)).GetValueOrDefault();
    //    }
    //    public void DeleteCourse(int id, DbSet<T> entity) {
    //    _context.Courses.Remove(_context.Entry.Find(id));
    //    _context.SaveChanges();
    //    }
    //    public void UpdateCourse(int id, Course course)
    //    {
    //        var existingStudent = _context.Courses.Find(id);

    //        if (existingStudent != null)
    //        {
    //            existingStudent.Title = course.Title;
    //           _context.SaveChanges();
    //        }
    //    }
    }
}
