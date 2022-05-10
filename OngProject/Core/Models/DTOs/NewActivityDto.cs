using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs
{
    public class NewActivityDto
    {
        /// <summary>
        /// activity name.
        /// </summary>
        /// <example>webinar</example>
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(255)]
        public string Name { get; set; }
        /// <summary>
        /// content activity
        /// </summary>
        /// 
        [Required(ErrorMessage = "Content field is required")]
        [MaxLength(65535)]

        public string Content { get; set; }
        /// <summary>
        /// image file
        /// </summary>
        
        public IFormFile Image { get; set; }
    }
}
