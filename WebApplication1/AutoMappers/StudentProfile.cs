using AutoMapper;
using WebApplication1.AutoMappers;
using WebApplication1.DTOs.StudentDTO;
using WebApplication1.Models;

namespace WebApplication1.AutoMappers
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDTO, Student>();
            CreateMap<Student, StudentDTO>();
        }
    }
}
