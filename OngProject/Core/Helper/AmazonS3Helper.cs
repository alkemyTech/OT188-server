using Amazon.S3;
using OngProject.Core.Interfaces;

namespace OngProject.Core.Helper
{
    public class AmazonS3Helper : IAmazonS3Helper
    {
        private IAmazonS3 _s3Client { get; set; }

        public AmazonS3Helper(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }
    }
}
