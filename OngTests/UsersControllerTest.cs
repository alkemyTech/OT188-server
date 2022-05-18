using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngTests
{
    [TestClass]
    public class UsersControllerTest
    {
        private readonly UsersController _controller;
        private readonly Mock<IUsersBusiness> _usersBusiness = new Mock<IUsersBusiness>();
        private readonly Mock<IAmazonS3Helper> _amazonS3 = new Mock<IAmazonS3Helper>();

        public UsersControllerTest()
        {
            _controller = new UsersController(_usersBusiness.Object);
        }


        [TestMethod()]
        public async Task GetAll_Returns_200AndUserDtoList()
        {
            //arrange
            var users = new List<UserDto>()
            {
                new UserDto()
                {
                    FirstName = "User 1",
                    LastName = "User 1 Lastname",
                    Email = "test@google.com",
                    Rol = "Administrator",
                    Photo = "photo1"
                },
                new UserDto()
                {
                    FirstName = "User 2",
                    LastName = "User 2 Lastname",
                    Email = "test@boca.com",
                    Rol = "Administrator",
                    Photo = "photo2"
                }
            };

            _usersBusiness.Setup(x => x.GetUsers(true)).ReturnsAsync(users).Verifiable();

            var result = await _controller.GetAll();

            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(users, okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<UserDto>));

            _usersBusiness.Verify(u => u.GetUsers(true), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_Throws_500AndException()
        {
            _usersBusiness.Setup(x => x.GetUsers(true)).ThrowsAsync(new Exception()).Verifiable();

            var response = await _controller.GetAll();

            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(string));

            _usersBusiness.Verify(u => u.GetUsers(true), Times.Once);
        }

        [TestMethod]
        public async Task Delete_Returns_200AndMessage()
        {
            var id = 1;
            _usersBusiness.Setup(x => x.DeleteUser(id)).ReturnsAsync(new Response<string>("Succes", message: "Entity Deleted")).Verifiable();

            var response = await _controller.Delete(id);
            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<string>));

            _usersBusiness.Verify(u => u.DeleteUser(id), Times.Once);
        }

        [TestMethod]
        public async Task Delete_Returns_403AndResponse()
        {
            var id = 1;
            _usersBusiness.Setup(x => x.DeleteUser(id)).ReturnsAsync(new Response<string>("Error", false, null, message: "User not found")).Verifiable();

            var response = await _controller.Delete(id);

            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(403, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<string>));

            _usersBusiness.Verify(u => u.DeleteUser(id), Times.Once);
        }

        [TestMethod]
        public async Task Delete_Throws_500AndException()
        {
            var id = 1;
            _usersBusiness.Setup(x => x.DeleteUser(id)).ThrowsAsync(new InvalidOperationException()).Verifiable();

            var response = await _controller.Delete(id);

            var result = (IStatusCodeActionResult)response;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);

            _usersBusiness.Verify(u => u.DeleteUser(id), Times.Once);
        }

        [TestMethod]
        public async Task Update_Returns_200AndUpdatedItem()
        {
            var id = 1;
            var file = new Mock<IFormFile>();

            //file.Setup(x => x.FileName).Returns("photoName");
            //_amazonS3.Setup(x => x.UploadFileAsync(file.Object)).ReturnsAsync("photoUrl").Verifiable();

            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("photoUrl").Verifiable();

            var userDto = new RegisterDto()
            {
                FirstName = "User 1",
                LastName = "User 1 Lastname",
                Email = "hola@hola.com",
                Password = "123456",
                Photo = file.Object
            };

            var userOutDto = new UserOutDTO()
            {
                FirstName = "User 1",
                LastName = "User 1 Lastname",
                Email = "hola@hola.com",
                Photo = await _amazonS3.Object.UploadFileAsync(file.Object)
            };

            _usersBusiness.Setup(x => x.UpdateUserAsync(id, userDto)).ReturnsAsync(new Response<UserOutDTO>(userOutDto, true, null, "Success!"));

            var response = await _controller.Put(id, userDto);

            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<UserOutDTO>));

            _usersBusiness.Verify(u => u.UpdateUserAsync(id, userDto), Times.Once);
            _amazonS3.Verify(u => u.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
        }

        [TestMethod]
        public async Task Update_Returns_404AndMessage()
        {
            var id = 999999;
            var file = new Mock<IFormFile>();

            //_amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("photoUrl").Verifiable();

            var userDto = new RegisterDto()
            {
                FirstName = "",
                LastName = "User 1 Lastname",
                Email = "hola@hola.com",
                Password = "123456",
                Photo = file.Object
            };

            _usersBusiness.Setup(x => x.UpdateUserAsync(id, userDto))
                          .ReturnsAsync(new Response<UserOutDTO>(null, false, null, "entity not found"));

            var response = await _controller.Put(id, userDto);

            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<UserOutDTO>));

            _usersBusiness.Verify(u => u.UpdateUserAsync(id, userDto), Times.Once);
        }

        [TestMethod]
        public async Task Update_Returns_500AndMessage()
        {
            var id = 1;
            var file = new Mock<IFormFile>();

            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("photoUrl").Verifiable();

            var userDto = new RegisterDto()
            {
                FirstName = "User 1",
                LastName = "User 1 Lastname",
                Email = "hola@hola.com",
                Password = "123456",
                Photo = file.Object
            };

            var userOutDto = new UserOutDTO()
            {
                FirstName = "User 1",
                LastName = "User 1 Lastname",
                Email = "hola@hola.com",
                Photo = await _amazonS3.Object.UploadFileAsync(file.Object)
            };

            _usersBusiness.Setup(x => x.UpdateUserAsync(id, userDto)).ThrowsAsync(new Exception());

            var response = await _controller.Put(id, userDto);

            var result = response as ObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<string>));

            _usersBusiness.Verify(u => u.UpdateUserAsync(id, userDto), Times.Once);
            _amazonS3.Verify(u => u.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
        }
    }
}
