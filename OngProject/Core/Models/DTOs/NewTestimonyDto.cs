using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewTestimonyDto
    {
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
