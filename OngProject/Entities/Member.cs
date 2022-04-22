using OngProject.Repositories.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Member : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string FacebookUrl { get; set; }
        [MaxLength(255)]
        public string InstagramUrl { get; set; }
        [MaxLength(255)]
        public string LinkedinUrl { get; set; }
        [Required]
        [MaxLength(255)]
        public string Image { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
