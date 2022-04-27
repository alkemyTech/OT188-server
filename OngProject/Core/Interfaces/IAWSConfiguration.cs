using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IAWSConfiguration
    {
			string AwsAccessKey { get; set; }
			string AwsSecretAccessKey { get; set; }
			string AwsSessionToken { get; set; }
			string BucketName { get; set; }
			string Region { get; set; }
	}
}
