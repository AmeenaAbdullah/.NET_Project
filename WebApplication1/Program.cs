using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using WebApplication1.AutoMappers;
using WebApplication1.Services.Course_Services;
using WebApplication1.Services.Student_Services;
using WebApplication1.Services.Teacher_Services;
using WebApplication1.Interfaces.Course_Interfaces;
using WebApplication1.Interfaces.Student_Interfaces;
using WebApplication1.Interfaces.Teacher_Interfaces;
using WebApplication1.Models;
using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Services.Base_Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IStudent, StudentServices>();
//builder.Services.AddScoped<ITeacher, TeacherServices>();
//builder.Services.AddScoped<ICourse,CourseServices>();
builder.Services.AddScoped(typeof(IBase<>), typeof(BaseFeature<>));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program), typeof(StudentProfile));
builder.Services.AddAutoMapper(typeof(Program), typeof(TeacherProfile));
builder.Services.AddAutoMapper(typeof(Program), typeof(CourseProfile));
builder.Services.AddSwaggerGen();
XmlConfigurator.Configure(new FileInfo("log4net.config"));
builder.Services.AddDbContext<ManagementSytemContext>();
builder.Services.AddLogging(loggingBuilder =>
{
    // Configure log4net
    loggingBuilder.AddLog4Net();
});

//Add auto mapper
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
