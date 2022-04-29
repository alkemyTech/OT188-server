using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class AddSlideDTO
    {
        [Required]
        [StringLength(255)]
        public IFormFile Image { get; set; }
        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        public int? Order { get; set; }
        [Required]
        public int OrganizationId { get; set; }
    }
}
