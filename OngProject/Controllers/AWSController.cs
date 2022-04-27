using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AWSController : ControllerBase
    {
        private readonly IAWSConfiguration _AWSConfiguration;
        private IAmazonS3Helper _amazonS3Helper;
        public AWSController(IAWSConfiguration AWSConfiguration)
        {
            _AWSConfiguration = AWSConfiguration;
        }
        //[HttpPost]
        //public async Task<IActionResult> Post(IFormFile formFile)
        //{
        //    try
        //    {
        //        using (var memory = new MemoryStream())
        //        {
        //            await formFile.CopyToAsync(memory);
        //            await _amazonS3Helper.
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost]
        public IActionResult UploadDocumentToS3(IFormFile file)
        {
            try
            {
                _amazonS3Helper = new AmazonS3Helper(_AWSConfiguration.AwsAccessKey, _AWSConfiguration.AwsSecretAccessKey, _AWSConfiguration.Region, _AWSConfiguration.BucketName);

                var result = _amazonS3Helper.UploadFileAsync(file);

                return Ok(file.FileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message+ (int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public async Task<List<String>> GetAllFiles()
        {
            try
            {
                _amazonS3Helper = new AmazonS3Helper(_AWSConfiguration.AwsAccessKey, _AWSConfiguration.AwsSecretAccessKey, _AWSConfiguration.Region, _AWSConfiguration.BucketName);
                return await _amazonS3Helper.GetAllFiles();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
