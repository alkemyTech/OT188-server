using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Interfaces
{
    public interface IAmazonS3Helper
    {
        //métodos.
        Task<List<string>> GetAllFiles();
        Task<string> UploadFileAsync(IFormFile file);
    }
}
