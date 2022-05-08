using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateSlideDTO
    {
        public IFormFile? Image { get; set; }
        [StringLength(255)]
        public string? Text { get; set; }
        public int? Order { get; set; }
        public int? OrganizationId { get; set; }
    }
}
