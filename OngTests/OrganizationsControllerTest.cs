﻿using Microsoft.AspNetCore.Mvc;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests
{
    [TestClass]
    public class OrganizationsControllerTest
    {
        private readonly Mock<IOrganizationsBusiness> _organizationsBusiness = new();
        private readonly OrganizationsController _organizationsController;
        
        public OrganizationsControllerTest()
        {
            _organizationsController = new OrganizationsController(_organizationsBusiness.Object);
        }

        [TestMethod()]
        public async Task GetAll_Returns_200AndListOrganizationDTO()
        {
            List<OrganizationDTO> listOrganizationDTOs = new List<OrganizationDTO>
            {
                new OrganizationDTO
                {
                    Name = "organization1",
                    ImageUrl = "image_url",
                    Phone = "1234567890",
                    Address = "organization@gmail.com",
                    Slides =  new List<PublicSlideDTO>()
                    {
                        new PublicSlideDTO
                        {
                            ImageUrl = "Image_url_slide",
                            Text = "text_Slide",
                            Order = 1
                        }
                    },
                    InstagramUrl = "instagram_url",
                    LinkedinUrl = "Linkedin_Url",
                    FacebookUrl = "Facebook_Url"

                }

            };
            _organizationsBusiness.Setup(o => o.GetOrganizations(true))
                                  .ReturnsAsync(new Response<IEnumerable<OrganizationDTO>>(listOrganizationDTOs, true, null, "Ok"))
                                  .Verifiable();

            var result = await _organizationsController.GetAll();

            //var okResult = result as Response<IEnumerable<OrganizationDTO>>;
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsInstanceOfType(okResult.Value, typeof(Response<IEnumerable<OrganizationDTO>>));

            _organizationsBusiness.Verify(u => u.GetOrganizations(true), Times.Once);


        }
        [TestMethod()]

        public async Task GetAll_Returns_400AndResponse()
        {
            List<OrganizationDTO> listOrganizationDTOs = new List<OrganizationDTO> { };

            _organizationsBusiness.Setup(o => o.GetOrganizations(true))
                                 .ReturnsAsync(new Response<IEnumerable<OrganizationDTO>>(null, false, null, "Not Found"))
                                 .Verifiable();

            var result = await _organizationsController.GetAll();

            var response = result as ObjectResult;

            _organizationsBusiness.Verify(u => u.GetOrganizations(true), Times.Once);

            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(Response<IEnumerable<OrganizationDTO>>));

        }

        [TestMethod()]

        public async Task GetAll_Returns_500AndResponse()
        {
            _organizationsBusiness.Setup(x => x.GetOrganizations(true))
                                  .ThrowsAsync(new InvalidOperationException())
                                  .Verifiable();


            var result = await _organizationsController.GetAll();
            var response = (IStatusCodeActionResult)result;

            Assert.IsNotNull(response);
            Assert.AreEqual(500, response.StatusCode);

            _organizationsBusiness.Verify(u => u.GetOrganizations(true), Times.Once);
        }




        [TestMethod()]

        public async Task GetById_Returns_200AndOrganizationDTO()
        {
            var organization = new OrganizationDTO
            {
                Name = "ONG-SomosMás",
                Address = "Su dirección",
                Phone = "1234567890",
                Slides = new List<PublicSlideDTO>()
                    {
                        new PublicSlideDTO
                        {
                            ImageUrl = "Image_url_slide",
                            Text = "text_Slide",
                            Order = 1
                        }
                    },
                FacebookUrl = "SomosMasFacebook",
                InstagramUrl = "SomosMasInstagram",
                LinkedinUrl = "SomosMasLinkedIn",
            };
            var id = 1;

            _organizationsBusiness.Setup(o => o.GetOrganization(id))
                                  .ReturnsAsync(new Response<OrganizationDTO>(organization, true, null, "Ok"))
                                  .Verifiable();

            var result = await _organizationsController.Get(id);

            var response = result as OkObjectResult;


            _organizationsBusiness.Verify(u => u.GetOrganization(id), Times.Once);

            //Assert.IsNotNull(okResult);
            //Assert.AreEqual(200, okResult.StatusCode);
            //Assert.IsInstanceOfType(okResult.Value, typeof(Response<IEnumerable<OrganizationDTO>>));



        }
    }


}
//public async Task<IActionResult> GetAll()
//{
//    try
//    {
//        var result = await _organizationsBusiness.GetOrganizations(true);
//        if (result.Succeeded == false)
//        {
//            return StatusCode(400, result);
//        }
//        return Ok(result);
//    }
//    catch (Exception e)
//    {
//        return StatusCode(500, new Response<string>(e.Message, false, message: "Server Error"));
//    }
//}
