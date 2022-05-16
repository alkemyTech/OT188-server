using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
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
                                  .ReturnsAsync(new Response<IEnumerable<OrganizationDTO>>(listOrganizationDTOs, true, null, "Ok"));

            var result = await _organizationsController.GetAll();

            //var okResult = result as Response<IEnumerable<OrganizationDTO>>;
            var okResult = result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);


        }
        [TestMethod()]
        public async Task GetAll_Returns_
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
