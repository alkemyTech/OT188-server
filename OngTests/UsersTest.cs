using Microsoft.VisualStudio.TestTools.UnitTesting;
using OngProject.Controllers;
using OngProject.Core.Business;
using OngTests.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests
{
    [TestClass]
    public class UsersTest
    {
        [ClassInitialize()]
        public static void Setup(TestContext testContext)
        {
            ContextHelper.CreateContext();
        }

        [TestMethod]
        public async Task TestMethod()
        {

            // Arrange

            var usersBusiness = new UsersBusiness(ContextHelper.UnitOfWork,
                                                 ContextHelper.EntityMapper,
                                                 ContextHelper.EmailService,
                                                 ContextHelper.Config,
                                                 ContextHelper.JwtTokenProvider,
                                                 ContextHelper.HttpContext);

            var usersController = new UsersController(usersBusiness);


            // Act

            var listUsers = await ContextHelper.UnitOfWork.UserRepository.GetAll(true);

            // Assert

            Assert.IsNotNull(listUsers);            
        }
    }
}
