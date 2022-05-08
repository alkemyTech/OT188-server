using OngProject.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class OrganizationDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }
        [StringLength(20)]
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public IEnumerable<PublicSlideDTO> Slides { get; set; }
    }
}
