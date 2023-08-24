using log4net;
using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Interfaces.Teacher_Interfaces;
using WebApplication1.Models;
namespace WebApplication1.Services.Teacher_Services
{
    public class TeacherServices :ITeacher
    {
    //    ManagementSytemContext _context = new ManagementSytemContext();
    //    private readonly ILog _logger = LogManager.GetLogger(typeof(TeacherServices));

    //    public TeacherServices(ManagementSytemContext context)
    //    {
    //        _context = context;

    //    }
    //    public List<Teacher> GetTeachers()
    //    {

    //        List<Teacher> Teachers = _context.Teachers.ToList();
    //        return Teachers;
    //    }
    //    public void AddTeacher(Teacher Teacher)
    //    {
    //        _context.Teachers.Add(Teacher);
    //        _context.SaveChanges();
    //    }
    //    public Teacher GetTeacherById(int id)
    //    {
    //        Teacher s = _context.Teachers.Find(id);
    //        return s;

    //    }
    //    public void UpdateTeacher(int id, Teacher updatedTeacher)
    //    {
    //        var existingTeacher = _context.Teachers.Find(id);

    //        if (existingTeacher != null)
    //        {
    //            existingTeacher.Name = updatedTeacher.Name;
    //            existingTeacher.Email= updatedTeacher.Email;

    //            _context.SaveChanges();
    //        }
    //    }
    //    public bool checkTeacher(int id)
    //    {      return (_context.Teachers?.Any(x => x.Id == id)).GetValueOrDefault();
    //    }
    //    public void DeleteTeacher(int id)
    //    {
    //        _context.Teachers.Remove(GetTeacherById(id));
    //        _context.SaveChanges();
    //    }
    }
}
