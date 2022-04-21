using OngProject.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Contact:EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        public int Phone { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }
    }
}
