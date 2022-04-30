using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;
using Amazon.S3.Transfer;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Core.Helper
{
    public class AmazonS3Helper : IAmazonS3Helper
    {
        private IAWSConfiguration _configuration;
        private IAmazonS3 _awsS3Client;

        public AmazonS3Helper(IAWSConfiguration configuration)
        {
            _configuration = configuration;
            _awsS3Client = new AmazonS3Client(_configuration.AwsAccessKey,
                                                    _configuration.AwsSecretAccessKey,
                                                        RegionEndpoint.GetBySystemName(_configuration.Region));
        }

        public async Task<List<string>> GetAllFiles()
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_awsS3Client);
                ListVersionsResponse listItemsResponse = await fileTransferUtility.S3Client.ListVersionsAsync(_configuration.BucketName);
                return listItemsResponse.Versions.Select(x => x.Key).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            try
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = _configuration.BucketName,
                        ContentType = file.ContentType,
                        CannedACL = new S3CannedACL("public-read")
                    };

                    var fileTransferUtility = new TransferUtility(_awsS3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);


                    string url = string.Format("http://{0}.s3.amazonaws.com/{1}", _configuration.BucketName, file.FileName);
                    return url;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}