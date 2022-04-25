using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Slide : EntityBase
    {
        [Required]
        [StringLength(255)]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(255)]
        public string Text { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
