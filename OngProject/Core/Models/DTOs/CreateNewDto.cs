using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CreateNewDto
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public int CategoryId { get; set; }

        //public List<CommentDTO> Comments { get; set; }
    }
}