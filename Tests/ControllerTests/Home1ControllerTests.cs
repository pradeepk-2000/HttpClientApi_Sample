using Microsoft.AspNetCore.Http;
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

namespace Perfect.Api.Tests.ControllerTests
{
    public class Home1ControllerTests
    {
        private Mock<IInformationFactory> _informFactory;
        private Mock<ILogger<Home1Controller>> _logger;
        private Home1Controller _controller;

        public Home1ControllerTests()
        {
               _informFactory = new Mock<IInformationFactory>();
                _logger = new Mock<ILogger<Home1Controller>>();
        }


        [Fact]
        public async void GetStatus_Test_Returns_Successful()
        {
            //Arrange
            StatusRequestModel statusRequestModel = new StatusRequestModel { StatusId = 1 };
            StatusResponseModel statusResponseModel = new StatusResponseModel { Status=true };

            _informFactory.Setup(x=>x.CheckStatus(It.IsAny<StatusRequestModel>())).ReturnsAsync(statusResponseModel);
            _controller = new Home1Controller(_logger.Object,_informFactory.Object);

            //Act
            var result = await _controller.CheckStatus(statusRequestModel); //send model or type like below

            var okResult = result as ObjectResult;

            //assert
            Assert.NotNull(okResult);
            Assert.True(okResult is OkObjectResult);
            Assert.IsType<ServiceResponse<StatusResponseModel>>(okResult.Value);
        }

        [Fact]
        public async void GetStatus_Test_Retrurns_InternalServerError()
        {
            
            _informFactory.Setup(x => x.CheckStatus(It.IsAny<StatusRequestModel>())).ThrowsAsync(new Exception());
            _controller = new Home1Controller(_logger.Object, _informFactory.Object);

            //Act
            var result = await _controller.CheckStatus(It.IsAny<StatusRequestModel>());
            var okResult = result as ObjectResult;

            //Assert

            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status500InternalServerError, okResult.StatusCode);
        }

        [Fact]
        public async void GetStatus_Test_Returns_NotFound()
        {
            _informFactory.Setup(x=> x.CheckStatus(It.IsAny<StatusRequestModel>())).ReturnsAsync(new StatusResponseModel());
            _controller = new Home1Controller(_logger.Object, _informFactory.Object);

            //Act
            var result = await _controller.CheckStatus(It.IsAny<StatusRequestModel>());
            var okResult = result as NotFoundObjectResult;

            //Assert

            Assert.NotNull(okResult);
            Assert.True(okResult is NotFoundObjectResult);
            Assert.IsType<NotFoundObjectResult>(okResult);
        }
    }
}
