using AutoMapper;
using WebApplication1.AutoMappers;
using WebApplication1.DTOs.TeacherDTO;
using WebApplication1.Models;

namespace WebApplication1.AutoMappers
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<Teacher, TeacherDTO>();
        }
    }
}
