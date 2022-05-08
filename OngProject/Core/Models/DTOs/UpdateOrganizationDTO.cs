using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class UpdateOrganizationDTO
    {
        [Required(ErrorMessage = "Name field is required")]
        [StringLength(255)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Content field is required")]
        [DataType(DataType.ImageUrl)]
        [StringLength(255)]
        public string Image { get; set; }
        [Required(ErrorMessage = "Content field is required")]
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Content field is required")]
        [EmailAddress]
        [StringLength(320)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Content field is required")]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string WelcomeText { get; set; }
        [Required(ErrorMessage = "Content field is required")]
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string AboutUsText { get; set; }
        [StringLength(255)]
        [DataType(DataType.Url)]
        public string FacebookUrl { get; set; }
        [StringLength(255)]
        [DataType(DataType.Url)]
        public string InstagramUrl { get; set; }
        [StringLength(255)]
        [DataType(DataType.Url)]
        public string LinkedinUrl { get; set; }
    }
}
