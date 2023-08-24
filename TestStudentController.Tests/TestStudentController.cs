using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplication1.Controllers;
using WebApplication1.DTOs.StudentDTO;
using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Interfaces.Student_Interfaces;
using WebApplication1.Models;
using Xunit;
namespace TestStudentController.Tests
{
    public class TestStudentController
    {
        /**********GETSTUDENT FUNCTION TEST CASES***********/
        [Fact]
        public void GetStudents_ReturnsListOfStudents()
        {
            // Arrange
            // ( This is a list of student objects that you expect the GetStudents method to return.
            var students = new List<Student>
            {
                new Student { Id = 1, Name = "Student 1" },
                new Student { Id = 2, Name = "Student 2" }
            };

            //(creates a mock implementation of the IStudent service using Moq.
            //It sets up the behavior of the GetStudents method to return the students list.

            var studentServiceMock = new Mock<IBase<Student>>();
            studentServiceMock.Setup(service => service.GetAll()).Returns(students);

            //This creates an instance of the StudentController class.
            //We pass in the mock service (studentServiceMock) and
            //null for IMapper since it's not used in this test.
            var controller = new StudentController(studentServiceMock.Object, null, null);

            // Act
            var result = controller.GetStudents();

            // Assert
            var objectResult = Assert.IsType<ActionResult<List<Student>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(objectResult.Result);
            var model = Assert.IsAssignableFrom<List<Student>>(okResult.Value);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void GetStudents_ReturnsNotFoundForEmptyList()
        {
            // Arrange
            var studentServiceMock = new Mock<IBase<Student>>();
            studentServiceMock.Setup(service => service.GetAll()).Returns((List<Student>)null);

            var controller = new StudentController(studentServiceMock.Object, null, null);

            // Act
            var result = controller.GetStudents();


            // Assert
            var objectResult = Assert.IsType<ActionResult<List<Student>>>(result);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(objectResult.Result);
            Assert.Equal("No students found.", notFoundResult.Value);
        }
        [Fact]
        public void GetStudents_ReturnsBadRequestOnException()
        {
            // Arrange
            var studentServiceMock = new Mock<IBase<Student>>();
            studentServiceMock.Setup(service => service.GetAll()).Throws(new Exception("Some error."));

            var controller = new StudentController(studentServiceMock.Object, null, null);

            // Act
            var result = controller.GetStudents();

            // Assert
            var objectResult = Assert.IsType<ActionResult<List<Student>>>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(objectResult.Result);
            Assert.Equal("Some error.", badRequestResult.Value);
        }


        /**********ADDSTUDENT FUNCTION TEST CASES***********/

        [Fact]
        public void AddStudent_ReturnsOkResult()
        {
            // Arrange
            var studentDTO = new StudentDTO { Name = "Ameena", Email = "ameena@gmail.com" };
            var studentServiceMock = new Mock<IBase<Student>>();
            var mapperMock = new Mock<IMapper>();

            // Mock the mapping behavior
            mapperMock.Setup(mapper => mapper.Map<Student>(studentDTO))
                      .Returns(new Student { Name = studentDTO.Name, Email = studentDTO.Email });

            var controller = new StudentController(studentServiceMock.Object, mapperMock.Object, null);

            // Act
            var result = controller.AddStudent(studentDTO);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Student added successfully.", objectResult.Value);

        }
        [Fact]
        public void AddStudent_ReturnsBadRequestOnException()
        {

            // Arrange
            var studentDTO = new StudentDTO { Name = "Ameena", Email = "ameena@gmail.com" };
            var studentServiceMock = new Mock<IBase<Student>>();
            var mapperMock = new Mock<IMapper>();

            // Mock the mapping behavior
            mapperMock.Setup(mapper => mapper.Map<Student>(studentDTO))
                      .Throws(new Exception("Some error."));

            var controller = new StudentController(studentServiceMock.Object, mapperMock.Object, null);

            // Act
            var result = controller.AddStudent(studentDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error.", badRequestResult.Value);
        }


        /**********UPDATESTUDENT FUNCTION TEST CASES***********/
        [Fact]
        public void UpdateStudent_ReturnsOkResult()
        {
            // Arrange
            int studentId = 1;
            var updatedStudentDTO = new StudentDTO { Name = "Updated Name", Email = "updated@example.com" };
            var studentServiceMock = new Mock<IBase<Student>>();
            var mapperMock = new Mock<IMapper>();

            // Mock the mapping behavior
            mapperMock.Setup(mapper => mapper.Map<Student>(updatedStudentDTO))
                      .Returns(new Student { Id = studentId, Name = updatedStudentDTO.Name, Email = updatedStudentDTO.Email });

            studentServiceMock.Setup(service => service.checkEntity(studentId)).Returns(true);
            var controller = new StudentController(studentServiceMock.Object, mapperMock.Object, null);

            // Act
            var result = controller.UpdateStudent(studentId, updatedStudentDTO);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Student updated successfully.", objectResult.Value);
        }
        [Fact]
        public void UpdateStudent_ReturnsNotFoundForNonExistingStudent()
        {
            // Arrange
            int studentId = 1;
            var updatedStudentDTO = new StudentDTO { Name = "Updated Name", Email = "updated@example.com" };
            var studentServiceMock = new Mock<IBase<Student>>();
            var mapperMock = new Mock<IMapper>();

            // Mock the mapping behavior
            mapperMock.Setup(mapper => mapper.Map<Student>(updatedStudentDTO))
                      .Returns(new Student { Name = updatedStudentDTO.Name, Email = updatedStudentDTO.Email });

            studentServiceMock.Setup(service => service.checkEntity(studentId)).Returns(false);
            var controller = new StudentController(studentServiceMock.Object, mapperMock.Object, null);

            // Act
            var result = controller.UpdateStudent(studentId, updatedStudentDTO);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public void UpdateStudent_ReturnsBadRequestOnException()
        {
            // Arrange
            int studentId = 1;
            var updatedStudentDTO = new StudentDTO { Name = "Updated Name", Email = "updated@example.com" };
            var studentServiceMock = new Mock<IBase<Student>>();
            var mapperMock = new Mock<IMapper>();

            // Mock the mapping behavior
            mapperMock.Setup(mapper => mapper.Map<Student>(updatedStudentDTO))
                      .Throws(new Exception("Some error."));

            studentServiceMock.Setup(service => service.checkEntity(studentId)).Returns(true);
            var controller = new StudentController(studentServiceMock.Object, mapperMock.Object, null);

            // Act
            var result = controller.UpdateStudent(studentId, updatedStudentDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error.", badRequestResult.Value);
        }


        /*********DELETESTUDENT FUNCTION TEST CASES***********/
        [Fact]
        public void DeleteStudent_ReturnsOkResult()
        {
            //Arrange
            int studentId = 1;
            var studentServiceMock = new Mock<IBase<Student>>();
            studentServiceMock.Setup(service => service.checkEntity(studentId)).Returns(true);
            var controller = new StudentController(studentServiceMock.Object, null, null);

            // Act
            var result = controller.DeleteStudent(studentId);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Student Deleted Successfully", objectResult.Value);

        }
        [Fact]
        public void DeleteStudent_ReturnsBadRequestOnException()
        {
            // Arrange
            int studentId = 1;
            var studentServiceMock = new Mock<IBase<Student>>();
            studentServiceMock.Setup(service => service.checkEntity(studentId)).Returns(true);

            // Set up the Delete method to throw an exception
            studentServiceMock.Setup(service => service.Delete(studentId)).Throws(new Exception("Some error."));

            var controller = new StudentController(studentServiceMock.Object, null, null);

            // Act
            var result = controller.DeleteStudent(studentId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Some error.", badRequestResult.Value);

        }
        [Fact]
        public void DeleteStudent_ReturnsNotFoundForNonExistingStudent()
        {
            //Arrange
            int studentId = 1;
            var studentServiceMock = new Mock<IBase<Student>>();
            studentServiceMock.Setup(service => service.checkEntity(studentId)).Returns(false);
            var controller = new StudentController(studentServiceMock.Object, null, null);

            // Act
            var result = controller.DeleteStudent(studentId);
           
            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);

        }

    }
}