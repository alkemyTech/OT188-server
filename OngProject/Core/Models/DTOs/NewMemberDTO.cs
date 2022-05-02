using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewMemberDTO
    {
        [Required(ErrorMessage = "\"Name\" field is required")]
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string FacebookUrl { get; set; }
        [MaxLength(255)]
        public string InstagramUrl { get; set; }
        [MaxLength(255)]
        public string LinkedinUrl { get; set; }
        
        [MaxLength(255)]
        public string Image { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
