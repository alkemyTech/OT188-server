using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OngProject.Controllers;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests
{
    [TestClass]
    internal class ActivitiesControllerTest
    {
        private readonly ActivitiesController _activitiesController;
        private readonly Mock<IActivitiesBusiness> _activitiesBusiness;
        private readonly Mock<IAmazonS3Helper> _amazonS3;

        public ActivitiesControllerTest(ActivitiesController activitiesController)
        {
            _activitiesController = activitiesController;
        }

        [TestMethod()]
        private async Task Insert_Should()
        {

        }
    }
}
