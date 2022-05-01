using OngProject.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewDTO
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        public string Content { get; set; }
       
        public string Image { get; set; }
       
        public int CategoryId { get; set; }

        public List<CommentDTO> Comments { get; set; }
    }
}
