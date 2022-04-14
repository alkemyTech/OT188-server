using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models
{
    public class Contact
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public int Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Messege { get; set; }
    }
}
