using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class NewActivityDto
    {
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Content field is required")]
        [MaxLength(65535)]
        public string Content { get; set; }
        public IFormFile Image { get; set; }
    }
}
