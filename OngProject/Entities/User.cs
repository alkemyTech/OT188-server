using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Entities
{
    public class User : EntityBase
    {
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(320)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20)]
        public string Password { get; set; }
        [DataType(DataType.ImageUrl)]
        [StringLength(255)]
        public string? Photo { get; set; }
        [Required]
        public int RolesId { get; set; }
        public Roles Roles { get; set; }
    }
}
