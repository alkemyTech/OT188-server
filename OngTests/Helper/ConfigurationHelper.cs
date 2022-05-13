using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests.Helper
{
    public class ConfigurationHelper
    {
        public readonly IConfiguration Config;
        public ConfigurationHelper()
        {
            var ConfigParams = new Dictionary<string, string>
            {
                {"AppSettings:DevelopmentJwtApiKey", "SuperAuthorizationKey"},
                {"ConnectionStrings:DefaultConnection", ""},                
                {"SENDGRID_API_KEY",""},
                {"emailContacto","ONGTeam188@gmail.com"},
                {"AWSS3:BucketName", ""},
                {"AWSS3:AwsAccessKey", ""},
                {"AWSS3:AwsSecretAccessKey", ""},
            };

            Config = new ConfigurationBuilder()
                .AddInMemoryCollection(ConfigParams)
                .Build();
        }
    }
}
