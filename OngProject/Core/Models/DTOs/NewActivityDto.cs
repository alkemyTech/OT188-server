using System.ComponentModel.DataAnnotations;

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
        [MaxLength(255)]
        public string? Image { get; set; }
    }
}
