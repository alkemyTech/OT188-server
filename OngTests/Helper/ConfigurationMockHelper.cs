using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests.Helper
{
    public class ConfigurationMockHelper
    {
        public Mock<IConfiguration> BuildConfiguration()
        {
            var configHelper = new Mock<IConfiguration>();
            configHelper.Setup(x => x.GetSection("emailContacto").Value).Returns("ONGTeam188@gmail.com");
            configHelper.Setup(x => x.GetSection("SENDGRID_API_KEY").Value).Returns("");
            return configHelper;
        }
    }
}
