﻿using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;
using PerfectAPI.Controllers;
using PerfectAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ControllerTests
{
    public class EmployeeManagementTest
    {
        private  Mock<IEmployeeFactory> _employeeFactory;
        private  Mock<ILogger<HomeController>> _logger;
        private HomeController controller;
        public EmployeeManagementTest()
        {
            _employeeFactory = new Mock<IEmployeeFactory>();
            _logger = new Mock<ILogger<HomeController>>();
            controller = new HomeController(_employeeFactory.Object, _logger.Object);
        }

        [Fact]
        public async Task GetAllEmployeesTest_ReturnsOkResponse()
        {
            //Arrange
            _employeeFactory.Setup(x => x.GetAllEmployeeDetails()).Returns(Task.FromResult(new List<EmployeeDetails>()));
            
            //Act
            var response = await  controller.GetAllEmployeeDetails();

            var result = response as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.True(result is OkObjectResult);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<ServiceResponse<List<EmployeeDetails>>>(result.Value);
        }

        [Fact]
        public async Task GetAllEmployeesTest_ReturnsInternalServerError()
        {
            //Arrange
            _employeeFactory.Setup(x => x.GetAllEmployeeDetails()).Throws(new Exception("Internal Server Error"));

            //Act
            var response = await controller.GetAllEmployeeDetails();

            var result = response as ObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(500,result.StatusCode);
            Assert.Equal("Internal Server Error", result.Value);
        }

        [Fact]
        public async Task GetEmployeesTest_ReturnsOkResponse()
        {
            //Arrange
            _employeeFactory.Setup(x => x.GetEmployeeDetails(It.IsAny<EmployeeDetailsRequestModel>())).Returns(Task.FromResult(new EmployeeDetails { Designation="Developer", EmpId=1002, EmpName="xyxyx",JoiningDate=new DateTime(2024-09-12),Qualification="MBA"}));

            //Act
            var response = await controller.GetEmployeeDetails(It.IsAny<EmployeeDetailsRequestModel>());

            var result = response as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<ServiceResponse<EmployeeDetails>>(result.Value);
        }

        [Fact]
        public async Task  GetEmployeesTest_ReturnsNotFound()
        {
            //Arrange
            _employeeFactory.Setup(x => x.GetEmployeeDetails(It.IsAny<EmployeeDetailsRequestModel>())).Returns(Task.FromResult(new EmployeeDetails { }));

            //Act
            var response = await controller.GetEmployeeDetails(It.IsAny<EmployeeDetailsRequestModel>());

            var result = response as NotFoundObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, result.StatusCode);
            Assert.IsType<ServiceResponse<EmployeeDetails>>(result.Value);
        }

        [Fact]
        public async Task AddNewEmployee_ReturnsOkResponse()
        {
            // Arrange
            var newEmployee = new NewEmployeeDetailsRequestModel
            {
                
                EmpName = "John",
                Designation = "Developer",
                Qualification = "B.Tech",
                JoiningDate = DateTime.Now
            };

            _employeeFactory.Setup(x => x.AddNewEmployee(It.IsAny<NewEmployeeDetailsRequestModel>())).Returns(Task.FromResult(true));

            // Act
            var response = await controller.AddNewEmployee(newEmployee);

            var result = response as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<ServiceResponse<object>>(result.Value);
        }

        [Fact]
        public async Task AddNewEmployeeTest_ReturnsInternalServerError()
        {
            // Arrange
            var newEmployee = new NewEmployeeDetailsRequestModel
            {
                EmpName = "John",
                Designation = "Developer",
                Qualification = "B.Tech",
                JoiningDate = DateTime.Now
            };

            _employeeFactory.Setup(x => x.AddNewEmployee(It.IsAny<NewEmployeeDetailsRequestModel>())).ThrowsAsync(new Exception("Internal Server Error"));

            // Act
            var response = await controller.AddNewEmployee(newEmployee);

            var result = response as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Internal Server Error", result.Value);
        }

        [Fact]
        public async Task UpdateDesignation_ReturnsOkResponse()
        {
            // Arrange
            var request = new UpdateEmployeDesignationRequestModel
            {
                EmpId = 1002,
                Designation = "Senior Developer"
            };

            _employeeFactory.Setup(x => x.UpdateDesignation(It.IsAny<UpdateEmployeDesignationRequestModel>())).Returns(Task.FromResult(true));

            // Act
            var response = await controller.UpdateEmployeeDesignation(request);

            var result = response as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<ServiceResponse<object>>(result.Value);
        }

        [Fact]
        public async Task UpdateDesignation_ReturnsNotFound()
        {
            // Arrange
            var request = new UpdateEmployeDesignationRequestModel
            {
                EmpId = 1002,
                Designation = "Senior Developer"
            };

            _employeeFactory.Setup(x => x.UpdateDesignation(It.IsAny<UpdateEmployeDesignationRequestModel>())).Returns(Task.FromResult(false));

            // Act
            var response = await controller.UpdateEmployeeDesignation(request);

            var result = response as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.IsType<ServiceResponse<object>>(result.Value);
        }

        [Fact]
        public async Task UpdateDesignation_ReturnsInternalServerError()
        {
            // Arrange
            var request = new UpdateEmployeDesignationRequestModel
            {
                EmpId = 1002,
                Designation = "Senior Developer"
            };
            _employeeFactory.Setup(x => x.UpdateDesignation(It.IsAny<UpdateEmployeDesignationRequestModel>())).ThrowsAsync(new Exception("Internal Server Error"));

            // Act
            var response =await  controller.UpdateEmployeeDesignation(request);

            var result = response as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
            Assert.Equal("Internal Server Error", result.Value);
        }

    }
}
