using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Category : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string Image { get; set; }
    }
}
