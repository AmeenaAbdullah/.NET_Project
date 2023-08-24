using AutoMapper;
using WebApplication1.AutoMappers;
using WebApplication1.DTOs.CourseDTO;
using WebApplication1.Models;

namespace WebApplication1.AutoMappers
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseDTO, Course>();
            CreateMap<Course, CourseDTO>();
        }
    }
}
