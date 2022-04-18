using OngProject.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models
{
    public class Activity:EntityBase
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
