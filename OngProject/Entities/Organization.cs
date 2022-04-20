using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class Organization : EntityBase
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255)]
        public string Name { get; set; } 
        [Required]
        [DataType(DataType.ImageUrl)]
        [StringLength(255)]                                   
        public string Image { get; set; }
        public string? Address { get; set; }
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(320)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        public string WelcomeText { get; set; }
        [StringLength(2000)]
        [DataType(DataType.MultilineText)]
        public string? AboutUsText { get; set; }
        [StringLength(255)]
        [DataType(DataType.Url)]
        public string? FacebookUrl { get; set; }
        [StringLength(255)]
        [DataType(DataType.Url)]
        public string? InstagramUrl { get; set; }
        [StringLength(255)]
        [DataType(DataType.Url)]
        public string? LinkedinUrl { get; set; }
    }
}
