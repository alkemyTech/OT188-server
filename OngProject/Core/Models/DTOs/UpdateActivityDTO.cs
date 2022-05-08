using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateActivityDTO
    {
        [MaxLength(255)]
        public string? Name { get; set; }
        [MaxLength(65535)]
        public string? Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
