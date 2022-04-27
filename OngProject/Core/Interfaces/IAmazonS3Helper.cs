using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IAmazonS3Helper
    {
        //métodos.
            Task<string> UploadFileAsync(IFormFile file);
            Task<string> UploadFileBucket(Stream stream, string fileName);
            Task<List<string>> GetAllFiles();
    }
}
