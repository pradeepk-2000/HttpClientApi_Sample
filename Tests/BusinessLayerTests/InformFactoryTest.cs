using Moq;
using PerfectAPI.BusinessLayer.Interfaces;
using PerfectAPI.BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfect.Api.Tests.BusinessLayerTests
{
    public class InformFactoryTest
    {
        private Mock<IInformationFactory> _informFactory;

        public InformFactoryTest()
        {
                _informFactory = new Mock<IInformationFactory>();
        }

        [Fact]
        public async void GetStatus_Test_ReturnsTrue()
        {
            //Arrange
            StatusRequestModel statusRequestModel = new StatusRequestModel { StatusId = 1 };
            StatusResponseModel responseModel = new StatusResponseModel { Status = true };
            _informFactory.Setup(x=>x.CheckStatus(It.IsAny<StatusRequestModel>())).ReturnsAsync(responseModel);

            //Act
            var result =  _informFactory.Object.CheckStatus(statusRequestModel).Result;

            _informFactory.Verify(x => x.CheckStatus(It.IsAny<StatusRequestModel>()), Times.Exactly(1));

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
        }
        [Fact]
        public async void GetStatus_Test_ReturnsFasle()
        {
            //Arrange
            StatusRequestModel statusRequestModel = new StatusRequestModel { StatusId = 1 };
            StatusResponseModel responseModel = new StatusResponseModel { Status = false };
            _informFactory.Setup(x => x.CheckStatus(It.IsAny<StatusRequestModel>())).ReturnsAsync(responseModel);

            //Act
            var result = _informFactory.Object.CheckStatus(statusRequestModel).Result;

            _informFactory.Verify(x => x.CheckStatus(It.IsAny<StatusRequestModel>()), Times.Exactly(1)); //not required

            //Assert
            Assert.NotNull(result);
            Assert.False(result.Status);
        }
    }
}
