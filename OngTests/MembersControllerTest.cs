using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.Pagination;
using OngTests.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace OngTests
{
    [TestClass]
    public class MembersControllerTest
    {
        [TestMethod]
        public async Task GetAllMembersTest()
        {
            // Arrange
            HostString Url = new HostString("www.api.com:8080");
            var MockHttpContex = new Mock<HttpContext>();
            MockHttpContex.Setup(x => x.Request.Scheme).Returns("https");
            MockHttpContex.Setup(x => x.Request.Host).Returns(Url);
            MockHttpContex.Setup(x => x.Request.Path).Returns("/Members");

            ContextHelper.httpContext.HttpContext = MockHttpContex.Object;

            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var memberController = new MembersController(memberBusiness);

            // Act
            PagedListParams pagedListParams = new PagedListParams();
            pagedListParams.PageNumber = 1;
            pagedListParams.PageSize = 5;
            var response = await memberController.GetAll(pagedListParams);
            var objectResult = response as ObjectResult;
            var result = objectResult.Value as Response<PagedListResponse<NewMemberDTO>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result);
            Assert.IsInstanceOfType(result, typeof(List<NewMemberDTO>));
        }

        [TestMethod]
        public async Task AddSuccessfullyTest()
        {
            // Arrange
            var memberDTO = new NewMemberDTO()
            {
                Name = "nombre de prueba",
                Description = "Descripcion de prueba",
            };
            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var membmerController = new MembersController(memberBusiness);

            // Act
            var response = await membmerController.Insert(memberDTO);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Response<NewMemberDTO>;

            // Assert
            Assert.IsNotNull(resultDTO);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(NewMemberDTO));
        }

        [TestMethod]
        public async Task AddingAnExistingFailedTest()
        {
            // Arrange
            var memberDTO = new NewMemberDTO()
            {
                Name = "Name 1",
                Description = "Descripcion de prueba",
            };
            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var mebmerController = new MembersController(memberBusiness);

            // Act
            var response = await mebmerController.Insert(memberDTO);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public async Task AddAMemberWithNullNameTest()
        {
            // Arrange     
            var memberDTO = new NewMemberDTO()
            {
                Name = null,
                Description = "Descripcion de prueba",
            };

            // Act
            var errorcount = checkValidationProperties(memberDTO).Count;

            // Assert
            Assert.AreNotEqual(0, errorcount);
        }

        [TestMethod]
        public async Task UpdateMemberTest()
        {
            // Arrange
            int IdMember = 1;
            var memberDTO = new NewMemberDTO()
            {
                Name = "Name 1",
                Description = "Descripcion de prueba",
                FacebookUrl = "FacebookContact",
                InstagramUrl = "InstagramContact",
                LinkedinUrl = "LinkedinContact"
            };
            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var mebmerController = new MembersController(memberBusiness);

            // Act
            var response = await mebmerController.Put(IdMember, memberDTO);
            var result = response as ObjectResult;
            var resultDTO = result.Value as Response<NewMemberDTO>;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(resultDTO.Data, typeof(NewMemberDTO));
        }

        [TestMethod]
        public async Task DeleteSuccessfullyTest()
        {
            // Arrange            
            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var mebmerController = new MembersController(memberBusiness);
            var member = await ContextHelper.UnitOfWork.MemberRepository.GetById(3);

            // Act
            var response = await mebmerController.Delete(member.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(member.IsDeleted);
        }

        [TestMethod]
        public async Task DeleteNotFoundTest()
        {
            // Arrange            
            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var mebmerController = new MembersController(memberBusiness);

            // Act
            var response = await mebmerController.Delete(100);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public async Task DeleteAlreadyDeletedmemberTest()
        {
            // Arrange            
            var memberBusiness = new MembersBusiness(ContextHelper.UnitOfWork, ContextHelper.EntityMapper, ContextHelper.httpContext);
            var mebmerController = new MembersController(memberBusiness);
            var member = await ContextHelper.UnitOfWork.MemberRepository.GetById(1);

            // Act
            var response = await mebmerController.Delete(member.Id);
            var result = response as ObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.IsTrue(member.IsDeleted);
        }

        public IList<ValidationResult> checkValidationProperties(object model)
        {
            var result = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, result, true);
            if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

            return result;
        }
    }
}
