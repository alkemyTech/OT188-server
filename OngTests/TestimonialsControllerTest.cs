using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OngProject.Core.Helper;
using Exception = System.Exception;

namespace OngTests
{
    [TestClass]
    public class TestimonialsControllerTest
    {
        private readonly TestimonialsController _controller;
        private readonly Mock<ITestimonialsBusiness> _mockBusiness = new();
        private readonly Mock<IAmazonS3Helper> _amazonS3 = new();
        
        public TestimonialsControllerTest()
        {
            _controller = new TestimonialsController(_mockBusiness.Object);
        }

        [TestMethod]
        public async Task GetAll_returns_200andDtoListPaginated()
        {
            // Arrange

            var testimonyOutDtos = new List<TestimonyOutDto>()
            {
                new()
                {
                    Name = "testimony 1",
                    Image = "image 1",
                    Description = "descripcion 1"
                },
                new()
                {
                    Name = "testimony 2",
                    Image = "image 2",
                    Description = "descripcion 2"
                }
            };
            var pagedListParams = new PagedListParams()
            {
                PageNumber = 1,
                PageSize = 2
            };
            
            var paged = new PagedList<TestimonyOutDto>(testimonyOutDtos, testimonyOutDtos.Count,
                pagedListParams.PageNumber, pagedListParams.PageSize);
            
            var pagedResponse = new PagedListResponse<TestimonyOutDto>(paged, "www.api.com/");

            _mockBusiness.Setup(x => x.GetAll(pagedListParams))
                .ReturnsAsync(new Response<PagedListResponse<TestimonyOutDto>>(pagedResponse))
                .Verifiable();

            // Act
            var result = await _controller.GetAll(pagedListParams);
            var okResult = result as OkObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<PagedListResponse<TestimonyOutDto>>));

            _mockBusiness.Verify(x => x.GetAll(pagedListParams), Times.Once);
        }

        [TestMethod]
        public async Task GetAll_trows_500andException()
        {
            // Arrange
            var pagedParams = new PagedListParams()
            {
                PageNumber = 1,
                PageSize = 2
            };
            _mockBusiness.Setup(x => x.GetAll(pagedParams))
                .Throws(new Exception()).Verifiable();
            
            // Act
            var response = await _controller.GetAll(pagedParams);
            var result = response as ObjectResult;
            
            // Assert
            
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<string>));
            
            _mockBusiness.Verify(x => x.GetAll(pagedParams), Times.Once);
            
        }

        [TestMethod]
        public async Task GetAll_returns_400andResponse()
        {
            // Arrange
            var pagedParams = new PagedListParams()
            {
                PageNumber = 10,
                PageSize = 10
            };
            _mockBusiness.Setup(x => x.GetAll(pagedParams))
                .ReturnsAsync(new Response<PagedListResponse<TestimonyOutDto>>(null,false,null, message: "There are no testimonials with the parameters given"))
                .Verifiable();
            
            // Act
            var response = await _controller.GetAll(pagedParams);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<PagedListResponse<TestimonyOutDto>>));

            _mockBusiness.Verify(x => x.GetAll(pagedParams), Times.Once);


        }
        
        [TestMethod]
        public async Task Delete_returns_200AndResponse()
        {
            // Arrange
            var id = 1;

            // Act
            _mockBusiness.Setup(x => x.DeleteTestimonial(id))
                .ReturnsAsync(new Response<string>("Success", message: "Entity Deleted"))
                .Verifiable();

            var result = await _controller.Delete(id);
            var okResult = result as OkObjectResult;

            // Assert

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<string>));

            _mockBusiness.Verify(x => x.DeleteTestimonial(id), Times.Once);
        }

        [TestMethod]
        public async Task Delete_returns_400AndResponse()
        {
            // Arrange
            var id = 1;

            // Act
            _mockBusiness.Setup(x => x.DeleteTestimonial(id))
                .ReturnsAsync(new Response<string>("Error",false,null, message: "Entity not found"))
                .Verifiable();

            var response = await _controller.Delete(id);
            var result = (IStatusCodeActionResult)response;

            // Assert

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            _mockBusiness.Verify(x => x.DeleteTestimonial(id), Times.Once);
        }

        [TestMethod]
        public async Task Delete_trows_500AndException()
        {
            // Arrange
            var id = 1;
            _mockBusiness.Setup(x => x.DeleteTestimonial(id))
                .ThrowsAsync(new InvalidOperationException())
                .Verifiable();

            // Act
            var response = await _controller.Delete(id);

            var result = (IStatusCodeActionResult)response;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);

            _mockBusiness.Verify(u => u.DeleteTestimonial(id), Times.Once);
        }

        [TestMethod]
        public async Task Post_returns_200AndResponse()
        {
            // Arrange
            var image = new Mock<IFormFile>();
            
            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("imageUrl").Verifiable();

            var testimonyDto = new NewTestimonyDto
            {
                Name = "testimony",
                Description = "description",
                Image = image.Object
            };

            var testimonyOutDto = new TestimonyOutDto
            {
                Name = "testimony",
                Description = "description",
                Image = await _amazonS3.Object.UploadFileAsync(image.Object)
            };

            _mockBusiness.Setup(x => x.InsertTestimonial(testimonyDto))
                .ReturnsAsync(new Response<TestimonyOutDto>(testimonyOutDto,true,null,"The Testimony has been created"))
                .Verifiable();
            
            // Act
            var response = await _controller.Post(testimonyDto);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<TestimonyOutDto>));

            _mockBusiness.Verify(x => x.InsertTestimonial(testimonyDto), Times.Once);
            _amazonS3.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
            
        }

        [TestMethod]
        public async Task Post_trows_500AndException()
        {
            // Arrange

            var image = new Mock<IFormFile>();

            var testimonyDto = new NewTestimonyDto
            {
                Name = "testimony",
                Description = "description",
                Image = image.Object
            };
            
            _mockBusiness.Setup(x => x.InsertTestimonial(testimonyDto))
                .ThrowsAsync(new Exception())
                .Verifiable();
            
            // Act
            var response = await _controller.Post(testimonyDto);
            var result = (IStatusCodeActionResult)response;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);

            _mockBusiness.Verify(x => x.InsertTestimonial(testimonyDto), Times.Once);
        }

        [TestMethod]
        public async Task Put_returns_200AndResponse()
        {
            // Arrange
            var id = 1;
            var image = new Mock<IFormFile>();
            
            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("imageUrl").Verifiable();

            var testimonyDto = new TestimonyInputDto()
            {
                Name = "testimony",
                Description = "description",
                Image = image.Object
            };

            var testimonyOutDto = new TestimonyOutDto
            {
                Name = "testimony",
                Description = "description",
                Image = await _amazonS3.Object.UploadFileAsync(image.Object)
            };

            _mockBusiness.Setup(x => x.UpdateTestimonial(id,testimonyDto))
                .ReturnsAsync(new Response<TestimonyOutDto>(testimonyOutDto,true,null,"The Testimony has been update"))
                .Verifiable();
            
            // Act
            var response = await _controller.Put(id,testimonyDto);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<TestimonyOutDto>));

            _mockBusiness.Verify(x => x.UpdateTestimonial(id,testimonyDto), Times.Once);
            _amazonS3.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);

        }

        [TestMethod]
        public async Task Put_returns_404AndResponse()
        {
            // Arrange
            var id = 111;
            var image = new Mock<IFormFile>();
            
            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("imageUrl").Verifiable();

            var testimonyDto = new TestimonyInputDto()
            {
                Name = "testimony",
                Description = "description",
                Image = image.Object
            };

            var testimonyOutDto = new TestimonyOutDto
            {
                Name = "testimony",
                Description = "description",
                Image = await _amazonS3.Object.UploadFileAsync(image.Object)
            };

            _mockBusiness.Setup(x => x.UpdateTestimonial(id,testimonyDto))
                .ReturnsAsync(new Response<TestimonyOutDto>(null,false,null,"not found"))
                .Verifiable();
            
            // Act
            var response = await _controller.Put(id,testimonyDto);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(Response<TestimonyOutDto>));

            _mockBusiness.Verify(x => x.UpdateTestimonial(id,testimonyDto), Times.Once);
            _amazonS3.Verify(x => x.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);

        }

        [TestMethod]
        public async Task Put_trows_500AndException()
        {
            // Arrange
            var id = 1;
            var image = new Mock<IFormFile>();
            
            _amazonS3.Setup(x => x.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("imageUrl").Verifiable();

            var testimonyDto = new TestimonyInputDto()
            {
                Name = "testimony",
                Description = "description",
                Image = image.Object
            };

            _mockBusiness.Setup(x => x.UpdateTestimonial(id,testimonyDto))
                .ThrowsAsync(new Exception())
                .Verifiable();
            
            // Act
            var response = await _controller.Put(id,testimonyDto);
            var result = (IStatusCodeActionResult)response;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            

            _mockBusiness.Verify(x => x.UpdateTestimonial(id,testimonyDto), Times.Once);

        }
    }
}
