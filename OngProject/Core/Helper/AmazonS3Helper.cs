using Amazon;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using OngProject.Core.Interfaces;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System;
using Amazon.S3.Transfer;
using System.Collections.Generic;
using System.Linq;

namespace OngProject.Core.Helper
{
    public class AmazonS3Helper : IAmazonS3Helper
    {
        //private IAmazonS3 _s3Client { get; set; }
        //private IConfiguration _configuration;
        private readonly string _bucketName;
        private readonly IAmazonS3 _awsS3Client;

        public AmazonS3Helper(string awsAccessKeyId, string awsSecretAccessKey, string region, string bucketName)
        {
            _bucketName = bucketName;
            _awsS3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, RegionEndpoint.GetBySystemName(region));
        }

        public async Task<List<string>> GetAllFiles()
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_awsS3Client);
                ListVersionsResponse listItemsResponse = await fileTransferUtility.S3Client.ListVersionsAsync(_bucketName);
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
                        BucketName = _bucketName,
                        ContentType = file.ContentType,
                        CannedACL = new S3CannedACL("public-read")
                    };

                    var fileTransferUtility = new TransferUtility(_awsS3Client);

                    await fileTransferUtility.UploadAsync(uploadRequest);

                    
                    string url = string.Format("http://{0}.s3.amazonaws.com/{1}",_bucketName,file.FileName);
                    return url;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public AmazonS3Helper(IAmazonS3 s3Client, IConfiguration configuration)
        //{
        //    _s3Client = s3Client;
        //    _configuration = configuration;
        //   // var publicKey = ConfigurationManager.appAppSettings["AWSS3:PublicKey"];
        //    //var SecretKey = ConfigurationManager.AppSettings["AWSS3:SecretKey"];
        //    RegionEndpoint BucketRegion = RegionEndpoint.APSouth1;

        //  //  this._s3Client = new AmazonS3Client(publicKey, SecretKey, BucketRegion);
        //}
        public async Task<string> UploadFileBucket(Stream stream, string fileName)
        {
            //string _bucketName = _configuration.GetSection("AWSS3:BucketName").Value;
            //PutObjectRequest putObjectRequest = new()
            //{
            //    InputStream = stream,
            //    Key = fileName,
            //    BucketName = _bucketName
            //};
            //PutObjectResponse putObjectResponse = await _s3Client.PutObjectAsync(putObjectRequest);
            //if (putObjectResponse.HttpStatusCode == HttpStatusCode.OK)
            //{
            //    string url = $"http://{_bucketName}.s3.amazonaws.com/{fileName}";
            //    return url;
            //}
            return "Ocurrio un problema subiendo el archivo.";
        }
    }
}
