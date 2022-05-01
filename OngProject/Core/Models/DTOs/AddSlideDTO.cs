using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class AddSlideDTO
    {
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        public int? Order { get; set; }

        public int OrganizationId;
        public string ImageUrl;
    }
}
