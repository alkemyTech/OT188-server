using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class New : EntityBase
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MaxLength(65535)]
        public string Content { get; set; }
        [Required]
        [MaxLength(255)]
        public string Image { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
    }
}
