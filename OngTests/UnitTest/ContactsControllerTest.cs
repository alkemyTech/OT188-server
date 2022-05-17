using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess;
using OngProject.Entities;
using OngTests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests.UnitTest
{
    [TestClass]
    public class ContactsControllerTest
    {
        [TestMethod]
        public async Task GetAll_Returns_200AndContactDto()
        {
            //Arrange
            var dbContext = DbContextHelper.UseDbContext(false);
            var unit = new UnitOfWorkHelper(dbContext);
            await unit._unitOfWork.ContactRepository.Add(new Contact()
            {
                Email = "asd",
                Name = "asd",
                Phone = 123
            });
            await unit._unitOfWork.SaveChangesAsync();

            var mapper = new EntityMapper(null);

            var business = new ContactsBusiness(unit._unitOfWork, mapper, null, null);

            //Act
            var controller = new ContactsController(business);
            var result = await controller.GetAll();
            var oResult = result as ObjectResult;
            var response = oResult.Value as Response<IEnumerable<ContactDto>>;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Succeeded);
            Assert.AreEqual(1, response.Data.Count());
        }

        [TestMethod]
        public async Task GetAll_Returns_404NotFound()
        {
            //Arrange
            var dbContext = DbContextHelper.UseDbContext(false);
            var unit = new UnitOfWorkHelper(dbContext);

            var mapper = new EntityMapper(null);

            var business = new ContactsBusiness(unit._unitOfWork, mapper, null, null);

            //Act
            var controller = new ContactsController(business);
            var result = await controller.GetAll();
            var oResult = result as ObjectResult;
            var response = oResult.Value as Response<string>;

            //Assert
            Assert.AreEqual(404,oResult.StatusCode);
            Assert.IsNull(response.Data);
            Assert.IsFalse(response.Succeeded);
        }
        [TestMethod]
        public async Task GetAll_Returns_500()
        {
            //Arrange
            var dbContext = new OngProjectDbContext(new ModelBuilder());

            var unit = new UnitOfWorkHelper(dbContext);

            var mapper = new EntityMapper(null);

            var business = new ContactsBusiness(unit._unitOfWork, mapper, null, null);

            //Act
            var controller = new ContactsController(business);
            var result = await controller.GetAll();
            var oResult = result as ObjectResult;
            var response = oResult.Value as Response<string>;

            //Assert
            Assert.AreEqual(500, oResult.StatusCode);
            Assert.IsNull(response.Data);
            Assert.IsFalse(response.Succeeded);
        }

        [TestMethod]
        public async Task InsertContacts_Return_200OK()
        {
            //Arrange
            var dbContext = DbContextHelper.UseDbContext(false);
            var unit = new UnitOfWorkHelper(dbContext);
            var mapper = new EntityMapper(null);
            var config = new ConfigurationMockHelper().BuildConfiguration().Object;//new ConfigurationHelper().Config;
            var email = new SendgridEmailServices(config);
            var business = new ContactsBusiness(unit._unitOfWork, mapper, email, config);
            var controller = new ContactsController(business);
            var contactDto = new RegisterContactDto
            {
                Email = "brianfranco9466@gmail.com",
                Message = "mensaje nuevo.",
                Name = "nuevo",
                Phone = 123
            };

            //Act
            var result = await controller.Post(contactDto);
            var oResult = result as ObjectResult;
            var response = oResult.Value as Response<RegisterContactDto>;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Succeeded);
            Assert.AreEqual(contactDto, response.Data);
        }

        [TestMethod]
        public async Task InsertContacts_return_500()
        {
            //Arrange
            var dbContext = new OngProjectDbContext(new ModelBuilder());
            var unit = new UnitOfWorkHelper(dbContext);
            var mapper = new EntityMapper(null);
            var config = new ConfigurationMockHelper().BuildConfiguration().Object;//new ConfigurationHelper().Config;
            var email = new SendgridEmailServices(config);
            var business = new ContactsBusiness(unit._unitOfWork, mapper, email, config);
            var controller = new ContactsController(business);
            var contactDto = new RegisterContactDto
            {
                Email = "brianfranco9466@gmail.com",
                Message = "mensaje nuevo.",
                Name = "nuevo",
                Phone = 123
            };

            //Act
            var result = await controller.Post(contactDto);
            var oResult = result as ObjectResult;
            var response = oResult.Value as Response<string>;

            //Assert
            Assert.IsNotNull(response);
            Assert.IsNull(response.Data);
            Assert.IsFalse(response.Succeeded);
            Assert.AreEqual(500, oResult.StatusCode);
        }

    }
}
