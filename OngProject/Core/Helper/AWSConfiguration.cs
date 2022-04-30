using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class AWSConfiguration : IAWSConfiguration
    {
		private readonly IConfiguration Configuration;
		public AWSConfiguration(IConfiguration configuration)
		{
			Configuration = configuration;
			BucketName =  Configuration.GetSection("AWSS3:BucketName").Value;
			Region =  Configuration.GetSection("AWSS3:Region").Value;
			AwsAccessKey =  Configuration.GetSection("AWSS3:AwsAccessKey").Value;
			AwsSecretAccessKey =  Configuration.GetSection("AWSS3:AwsSecretAccessKey").Value;
			AwsSessionToken = "";
		}

		public string BucketName { get; set; }
		public string Region { get; set; }
		public string AwsAccessKey { get; set; }
		public string AwsSecretAccessKey { get; set; }
		public string AwsSessionToken { get; set; }
	}
}
