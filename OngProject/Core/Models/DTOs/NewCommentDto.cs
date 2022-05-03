using OngProject.Entities;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class NewCommentDto
    {
        [Required]
        [MaxLength(65535)]
        public string Body { get; set; }

        
       
       
        [Required]
        public int NewId { get; set; }
        

    }
}
