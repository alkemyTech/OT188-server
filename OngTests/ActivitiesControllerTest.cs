using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using System;
using System.Threading.Tasks;

namespace OngTests
{
    [TestClass]
    public class ActivitiesControllerTest
    {
        private readonly ActivitiesController _activitiesController;
        private readonly Mock<IActivitiesBusiness> _activitiesBusiness= new Mock<IActivitiesBusiness>();
        private readonly Mock<IAmazonS3Helper> _amazonS3 = new Mock<IAmazonS3Helper>();

        public ActivitiesControllerTest()
        {
            _activitiesController = new ActivitiesController(_activitiesBusiness.Object);
        }

        [TestMethod]
        public async Task Insert_Should_Return_200_And_ActivityOutDto()
        {
            //Arrange
            var file = new Mock<IFormFile>();
            var activityDto = new NewActivityDto()
            {
                Name = "Dancing",
                Content = "Dancing Activity",
                Image = file.Object

            };
            var activityOutDto = new ActivityOutDTO()
            {
                Name = "Dancing",
                Content = "Dancing Activity",
                Image = await _amazonS3.Object.UploadFileAsync(file.Object)
            };

            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("photoUrl").Verifiable();
            _activitiesBusiness.Setup(x => x.InsertActivity(It.IsAny<NewActivityDto>())).ReturnsAsync(new Response<ActivityOutDTO>(activityOutDto)).Verifiable();

            //Act
            var response = await _activitiesController.Insert(activityDto);
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<ActivityOutDTO>));

            _amazonS3.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
            _activitiesBusiness.Verify(x => x.InsertActivity(It.IsAny<NewActivityDto>()), Times.Once);
        }

        [TestMethod]
        public async Task Insert_Should_Return_500_If_Server_Fails()
        {
            //Arrange
            var file = new Mock<IFormFile>();
            var activityDto = new NewActivityDto()
            {
                Name = "Dancing",
                Content = "Dancing Activity",
                Image = file.Object

            };
            var activityOutDto = new ActivityOutDTO()
            {
                Name = "Dancing",
                Content = "Dancing Activity",
                Image = await _amazonS3.Object.UploadFileAsync(file.Object)
            };

            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("photoUrl").Verifiable();
            _activitiesBusiness.Setup(x => x.InsertActivity(It.IsAny<NewActivityDto>())).Throws(new Exception()).Verifiable();

            //Act
            var response = await _activitiesController.Insert(activityDto);
            var result = response as ObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<NewActivityDto>));

            _amazonS3.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
            _activitiesBusiness.Verify(x => x.InsertActivity(It.IsAny<NewActivityDto>()), Times.Once);
        }

    }
}
