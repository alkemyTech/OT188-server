using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Helper
{
    public class AWSConfiguration: IAWSConfiguration
    {
		public AWSConfiguration()
		{
			BucketName = "cohorte-abril-98a56bb4";
			Region = "sa-east-1";
			AwsAccessKey = "AKIAS2JWQJCDI4WQ6EIF";
			AwsSecretAccessKey = "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";
			AwsSessionToken = "";
		}

		public string BucketName { get; set; }
		public string Region { get; set; }
		public string AwsAccessKey { get; set; }
		public string AwsSecretAccessKey { get; set; }
		public string AwsSessionToken { get; set; }
	}
}
